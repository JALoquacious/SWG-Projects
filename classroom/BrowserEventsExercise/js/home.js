$(document).ready(function () {
    let views = ['#main', '#akron', '#minneapolis', '#louisville'];
    
    function showPage(pageName) {
        $.each(views, function(idx, view) {
            if (view === pageName) {
                $(view + 'InfoDiv').show()
            } else {
                $(view + 'InfoDiv').hide();
                $(view + 'Weather').hide();
            }
        });
    }
    
    showPage('#main'); // initialize page

    $.each(views, function(idx, view) {
        $(view + 'Button')
            .on('click', () => showPage(view));
    });
    
    $.each(views.slice(1), function(idx, city) {
        $(city + 'WeatherButton')
            .on('click', () => $(city + 'Weather').toggle());
    });
    
    $('table tr').not(':first').hover(
        function() { $(this).css('background-color', 'whitesmoke') },
        function() { $(this).css('background-color', 'white') }
    )
});
    
//    function showMain(event) {
//        $('#akronInfoDiv').hide();
//        $('#minneapolisInfoDiv').hide();
//        $('#louisvilleInfoDiv').hide();
//
//        $('#mainInfoDiv').show();
//    }
//
//    function showAkron() {
//        $('#mainInfoDiv').hide();
//        $('#minneapolisInfoDiv').hide();
//        $('#louisvilleInfoDiv').hide();
//
//        $('#akronInfoDiv').show();
//        $('#akronWeather').hide();
//    }
//
//    function showMinneapolis() {
//        $('#mainInfoDiv').hide();
//        $('#akronInfoDiv').hide();
//        $('#louisvilleInfoDiv').hide();
//
//        $('#minneapolisInfoDiv').show();
//        $('#minneapolisWeather').hide();
//    }
//
//    function showLouisville() {
//        $('#mainInfoDiv').hide();
//        $('#akronInfoDiv').hide();
//        $('#minneapolisInfoDiv').hide();
//
//        $('#louisvilleInfoDiv').show();
//        $('#louisvilleWeather').hide();
//    }
//    $('#akronInfoDiv').hide();
//    $('#minneapolisInfoDiv').hide();
//    $('#louisvilleInfoDiv').hide();
//
//    $('#mainButton').on('click', function() {
//        showPage('#main')
//    });
//    $('#akronButton').on('click', function() {
//        showPage('#akron')
//    });
//    $('#minneapolisButton').on('click', function() {
//        showPage('#minneapolis')
//    });
//    $('#louisvilleButton').on('click', function() {
//        showPage('#louisville')
//    });
//
//    $('#akronWeatherButton').on('click', function() {
//        $('#akronWeather').toggle();
//    });
//    
//    $('#minneapolisWeatherButton').on('click', function() {
//        $('#minneapolisWeather').toggle();
//    });
//    
//    $('#louisvilleWeatherButton').on('click', function() {
//        $('#louisvilleWeather').toggle();
//    });
