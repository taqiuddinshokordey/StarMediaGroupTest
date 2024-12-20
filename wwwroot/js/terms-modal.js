$(document).ready(function() {
    $('#termsLink').on('click', function(e) {
        e.preventDefault();
        $('#termsContent').load('/Pages/_TermsContent'); // Load the partial view into the modal
        $('#termsModal').modal('show'); // Show the Terms modal
    });

    // Handle the back button to return to the privacy consent modal from Terms Modal
    $('#backToPrivacyConsentButton').on('click', function() {
        $('#termsModal').modal('hide'); // Hide the Terms modal
        $('#privacyConsentModal').modal('show'); // Show the Privacy Consent modal
    });

    // Handle the Privacy Statement link
    $('#privacyStatementLink').on('click', function(e) {
        e.preventDefault();
        $('#privacyContent').load('/Pages/_PrivacyContent'); // Load the partial view into the modal
        $('#privacyStatementModal').modal('show'); // Show the Privacy Statement modal
    });

    // Handle the back button to return to the privacy consent modal from Privacy Statement Modal
    $('#backToPrivacyConsentButtonPrivacy').on('click', function() {
        $('#privacyStatementModal').modal('hide'); // Hide the Privacy Statement modal
        $('#privacyConsentModal').modal('show'); // Show the Privacy Consent modal
    });
});
