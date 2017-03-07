/*
Requirements

* Your assignment is to make the following changes to the way the supplied
  home.html document is displayed in the browser.


Restrictions

* All your code changes must appear in the home.js file (this file is in
  the js folder). This is the only file you are allowed to edit.
* You cannot edit home.html.
* You cannot edit or add any CSS files.
* You cannot add any JavaScript files.


Required Changes

* Center all H1 elements
* Center all H2 elements
* Replace the class that is on the element containing the text “Team Up!”
  with the class page-header
* Change the text of “The Squad” to “Yellow Team”
* Change the background color for each team list to match the name of the team
* Add Joseph Banks and Simon Jones to the Yellow Team list
* Hide the element containing the text “Hide Me!!!”
* Remove the element containing the text “Bogus Contact Info” from the footer
* Add a paragraph element containing your name and email to the footer. The
  text must be in Courier font and be 24 pixels in height
*/


$(document).ready(function () {
    
    function Player(name) {
        this.name = name;
    }
    
    let yellowPlayerRoster = [
        new Player('Joseph Banks'),
        new Player('Simon Jones')
    ];
    
    let name = 'James A. Likoudis',
        email = 'jalikoudis@protonmail.com';
    
    $('h1').css('text-align', 'center');
    $('h2').css('text-align', 'center');
    
    $('#myBanner')
        .find('h1')
        .removeClass('myBannerHeading')
        .addClass('page-header');
    
    $('#yellowHeading').text('Yellow Team');
    
    $('#orangeHeading')
        .parent()
        .css('background-color', '#e0a46d');
    $('#blueHeading')
        .parent()
        .css('background-color', '#789dd6');
    $('#redHeading')
        .parent()
        .css('background-color', '#cb8583');
    $('#yellowHeading')
        .parent()
        .css('background-color', '#efe487');
    
    $.each(yellowPlayerRoster, function(idx, player) {
        $('#yellowTeamList').append('<li>' + player.name + '</li>');
    });
    
    $('#oops').hide();
    $('#footerPlaceholder').remove();
    
    $('#footer')
        .prepend('<p>Name:' + name + '   |   Email:' + email)
        .css('font-family', 'Courier')
        .css('font-size', '24')
        .css('text-align', 'center');
});