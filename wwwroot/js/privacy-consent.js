// File: wwwroot/js/privacy-consent.js

// Function to get a cookie value by name
function getCookie(name) {
    let cookieValue = null;
    
    // Make synchronous Ajax call to get cookie value
    $.ajax({
        url: '/Consent/GetCookie',
        type: 'GET',
        data: { name: name },
        async: false, // Making it synchronous to maintain similar usage pattern
        success: function(response) {
            if (response.success) {
                cookieValue = response.value;
            }
        },
        error: function(error) {
            console.error('Error fetching cookie:', error);
        }
    });
    
    return cookieValue;
}

// Main Privacy Consent Logic
$(document).ready(function () {
    var hasConsented = getCookie("PrivacyConsent");
    console.log('Privacy Consent:', hasConsented);
    
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
                hasConsented = getCookie("PrivacyConsent");
        
                // Hide the banner and refresh the page
                $('#privacyConsentModal').removeClass('d-block').addClass('d-none');
                $('body').css('overflow', 'auto');
                window.location.reload();
            },
            error: function (error) {
                console.error('Error accepting consent:', error);
            }
        });
    });

    // Handle Decline button click
    $("#declineConsentButton").on("click", function () {

        // Send decline status to the database
        $.ajax({
            url: '/consent/DeclineConsent',
            method: 'POST',
            contentType: 'application/json',
            success: function (response) {
            // Hide the banner and refresh the page
                $("#privacyConsentModal").removeClass('d-block').addClass('d-none');
                $('body').css('overflow', 'auto');
                window.location.reload();
            },
            error: function (error) {
                console.error('Error declining consent:', error);
            }
        });
    });
});
