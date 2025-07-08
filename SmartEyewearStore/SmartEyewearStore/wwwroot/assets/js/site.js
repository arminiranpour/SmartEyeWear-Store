// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Ensure localStorage has a GuestId that matches the session value
document.addEventListener('DOMContentLoaded', async function () {
    if (!localStorage.getItem('GuestId')) {
        try {
            const resp = await fetch('/User/GetGuestId');
            if (resp.ok) {
                const data = await resp.json();
                if (data.guestId) {
                    localStorage.setItem('GuestId', data.guestId);
                }
            }
        } catch (err) {
            console.error('Failed to fetch guest id', err);
        }
    }
});