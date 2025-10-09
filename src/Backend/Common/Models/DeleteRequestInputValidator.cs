﻿using FluentValidation;

namespace Backend.Common.Models
{
    public class DeleteRequestInputValidator: AbstractValidator<DeleteRequestInput>
    {
        public DeleteRequestInputValidator()
        {
            RuleFor(x => x.DeleteReason)
                .NotEmpty().WithMessage("Delete Reason is required")
                .Length(5, 500).WithMessage("Delete Reason must be between 5 and 500 characters");
        }
    }
}
 