// File: wwwroot/js/privacy-consent.js

// Function to get a cookie value by name
function getCookie(name) {
    console.log('Ini Path dia');
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
    return null; // Return null if the cookie is not found
}

// Main Privacy Consent Logic
$(document).ready(function () {
    console.log('Test');
    const targetPaths = ["/Pages/About", "/Pages/Privacy", "/Pages/Terms"];

    // Only get the cookie if not on the root URL
    if (window.location.pathname !== '/') {
        hasConsented = getCookie("privacyConsent");
        console.log('Has Consented: ' + hasConsented);

        // Check if the cookie has the value "declined" or not, and if the user is on a target page
        if ((hasConsented !== "declined" || hasConsented !== "accepted" ) && targetPaths.includes(window.location.pathname) && hasConsented === null) {
            $("#privacyConsentModal").modal({
                backdrop: 'static', // Prevent closing by clicking outside
                keyboard: false // Prevent closing with the Escape key
            }).modal("show"); // Show the modal
        }

        // Handle Accept button click
        $('#acceptConsentButton').on('click', function () {
            $.ajax({
                url: '/consent/AcceptConsent', 
                method: 'POST',
                contentType: 'application/json',
                success: function (response) {
                    var privacyConsentCookie = response.privacyConsentCookie;
                    console.log(privacyConsentCookie)
                    document.cookie = `privacyConsent=accepted; path=/; expires=${new Date(new Date().getTime() + 365 * 24 * 60 * 60 * 1000).toUTCString()}`;
                    $('#privacyConsentModal').modal('hide');
                },
            });
        });

        // Handle Decline button click
        $("#declineConsentButton").on("click", function () {
            // Create a cookie for decline that lasts 1 day
            const expirationDays = 1;
            const expiryDate = new Date();
            expiryDate.setDate(expiryDate.getDate() + expirationDays);

            document.cookie = `privacyConsent=declined; path=/; expires=${expiryDate.toUTCString()}`;

            // Close the modal
            $("#privacyConsentModal").modal("hide");
        });
    }
});
