﻿using Backend.Application.BackgroundJobs;
using Backend.Application.Notifications;
using Backend.Common.Interfaces;
using Backend.Features.Tenants;
using TickerQ.Utilities;
using TickerQ.Utilities.Base;
using TickerQ.Utilities.Interfaces.Managers;
using TickerQ.Utilities.Models;
using TickerQ.Utilities.Models.Ticker;

namespace Backend.Infrastructure.BackgroundJobs;

public class Jobs(IEmailService emailService)
{
    [TickerFunction(functionName: nameof(SendEmailJob))]
    public async Task SendEmailJob(TickerFunctionContext<SendEmailRequestInput> tickerContext, CancellationToken cancellationToken)
    {
        await emailService.SendEmailAsync(
               tickerContext.Request.Email,
               tickerContext.Request.Subject,
               tickerContext.Request.HtmlMessage
           );
    }
}

public class JobService(ITenantGetterService tenantGetterService, ITimeTickerManager<TimeTicker> timeTickerManager) : IJobService
{
    public async Task EnqueueAsync<T>(T requestInput, string methodName, CancellationToken cancellationToken)
    {
        var res = await timeTickerManager.AddAsync(new TimeTicker
        {
            Request = TickerHelper.CreateTickerRequest<T>(requestInput),
            ExecutionTime = DateTime.Now.AddSeconds(1),
            Function = methodName,
            Description = $"Short Description",
            Retries = 3,
            RetryIntervals = [20, 60, 100] // set in seconds
        }, cancellationToken);
    }
}