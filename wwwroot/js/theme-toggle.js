document.addEventListener('DOMContentLoaded', function() {
    const themeToggle = document.getElementById('themeToggle');
    const themeIcon = document.getElementById('themeIcon');
    
    // Get initial theme preference from database using Ajax
    $.ajax({
        url: '/Consent/GetThemePreference',
        type: 'GET',
        success: function(data) {
            if (data.success && data.isDarkMode) {
                document.body.classList.add('dark-mode');
                themeIcon.classList.remove('bi-moon-fill');
                themeIcon.classList.add('bi-sun-fill');
            }
        }
    });
    
    function toggleTheme() {
        const isDarkMode = document.body.classList.toggle('dark-mode');
        console.log('Dark Mode:', isDarkMode);
        
        // Update UI
        if (isDarkMode) {
            themeIcon.classList.remove('bi-moon-fill');
            themeIcon.classList.add('bi-sun-fill');
        } else {
            themeIcon.classList.remove('bi-sun-fill');
            themeIcon.classList.add('bi-moon-fill');
        }
        
        // Save preference to database using Ajax
        $.ajax({
            url: '/consent/UpdateThemePreference',
            method: 'POST',
            contentType: 'application/json', // Ensure content-type is JSON
            data: JSON.stringify({ isDarkMode: isDarkMode }), // Properly serialize to JSON
            success: function (response) {
                if (response.success) {
                    $("#privacyConsentModal").removeClass('d-block').addClass('d-none');
                    $('body').css('overflow', 'auto');
                    window.location.reload();
                } else {
                    console.error('Error updating theme preference:', response.message);
                }
            },
            error: function (error) {
                console.error('Error updating theme preference:', error);
            }
        });
        
    }
    
    themeToggle.addEventListener('click', toggleTheme);
}); 