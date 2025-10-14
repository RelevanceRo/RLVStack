// JavaScript module for RlvModal component
// Provides open/close functionality and close event handling for HTML dialog modals

const modalInstances = new Map();

/**
 * Initializes a modal and sets up close event listener
 * @param {string} id - The modal ID
 * @param {any} dotNetRef - .NET object reference for callbacks
 */
export function initializeModal(id, dotNetRef) {
    const dialog = document.getElementById(id);
    if (!dialog) {
        console.error(`Modal with id '${id}' not found`);
        return;
    }

    // Create close event listener
    const closeHandler = () => {
        // Notify Blazor that the modal was closed
        dotNetRef.invokeMethodAsync('HandleModalClose');
    };

    // Add close event listener
    dialog.addEventListener('close', closeHandler);

    // Store instance data for cleanup
    modalInstances.set(id, {
        dialog,
        closeHandler,
        dotNetRef
    });
}

/**
 * Opens a modal using showModal() method
 * @param {string} id - The modal ID
 */
export function openModal(id) {
    const dialog = document.getElementById(id);
    if (dialog && typeof dialog.showModal === 'function') {
        try {
            dialog.showModal();
        } catch (error) {
            console.error(`Error opening modal '${id}':`, error);
        }
    }
}

/**
 * Closes a modal using close() method
 * @param {string} id - The modal ID
 */
export function closeModal(id) {
    const dialog = document.getElementById(id);
    if (dialog && typeof dialog.close === 'function') {
        try {
            dialog.close();
        } catch (error) {
            console.error(`Error closing modal '${id}':`, error);
        }
    }
}

/**
 * Cleans up modal instance
 * @param {string} id - The modal ID
 */
export function disposeModal(id) {
    const instance = modalInstances.get(id);
    if (instance) {
        // Remove event listener
        instance.dialog.removeEventListener('close', instance.closeHandler);

        // Clean up
        modalInstances.delete(id);
    }
}
