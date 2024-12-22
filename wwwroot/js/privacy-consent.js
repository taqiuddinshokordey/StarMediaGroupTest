// File: wwwroot/js/privacy-consent.js

// Function to get a cookie value by name
function getCookie(name) {
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
        if ((hasConsented !== "declined") && targetPaths.includes(window.location.pathname) && hasConsented === null) {
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
                    // Extract the privacyConsentCookie from the response
                    var privacyConsentCookie = response.privacyConsentCookie;
                    
                    // Set the expiry date for the cookie (1 year from now)
                    var expiryDate = new Date(new Date().getTime() + 365 * 24 * 60 * 60 * 1000).toUTCString();
            
                    // Set the consent cookie in the document
                    document.cookie = `privacyConsent=${privacyConsentCookie}; path=/; expires=${expiryDate}`;

                    hasConsented = getCookie("privacyConsent");
            
                    // Optionally, you can close the modal after acceptance
                    $('#privacyConsentModal').modal('hide');
                },
                error: function (error) {
                    console.error('Error accepting consent:', error);
                }
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
