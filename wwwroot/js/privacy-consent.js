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
    const hasConsented = getCookie("privacyConsent");
    
    // Simplified condition to check consent status
    if (hasConsented === null || hasConsented === undefined) {
        $("#privacyConsentModal").removeClass('d-none').addClass('d-block');
        $('body').css('overflow', 'hidden');
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
        
                // Hide the banner instead of modal
                $('#privacy-consent-banner').removeClass('d-block').addClass('d-none');
                $('body').css('overflow', 'auto');
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

        // Hide the banner instead of modal
        $("#privacy-consent-banner").removeClass('d-block').addClass('d-none');
        $('body').css('overflow', 'auto');
    });
});
