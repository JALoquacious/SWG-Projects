$(document).ready(function () {

function errorMessage(type, error) {
    let map = {
        'formDataError' : 'Error on form. Title, year, director, and rating are mandatory fields.',
        'webServiceError' : 'The web service returned an error: ' + error
    }
    
    $('.error-messages')
       .append($('<li>')
       .attr({class: 'list-group-item list-group-item-danger'})
       .text(map[type])
    );
}
    
function validate(page) {
    let formFields = ['title', 'year', 'director', 'rating', 'id'],
        valid = true;
    
    $.each(formFields, function(index, element) {
        let field = $('#' + page + '-' + element);
        
        if (!field.val() || field.val() === '') {
            field.addClass('error-outline');
            valid = false;
        }
    });
    return valid;
}

function loadList() {
    $('#view').hide();
    $('#make').hide();
    $('#edit').hide();
    $('#list').show();
    
    $('.error-messages').empty();
    $("#result-table").children().remove();
    getListItems('');
}
    
function addScreenButtonHandler() {
    $('#list').hide();
    $('#view').hide();
    $('#edit').hide();
    $('#make').show();
}
    
function editScreenButtonHandler() {
    $('#list').hide();
    $('#view').hide();
    $('#make').hide();
    
    $.ajax ({
        type: 'GET',
        url: 'http://localhost:8080/dvd/' + this.id,
        success: function (data, status) {
            $('.error-messages').empty();
            $('#update-title').val(data.title);
            $('#update-year').val(data.realeaseYear);
            $('#update-director').val(data.director);
            $('#update-rating').val(data.rating);
            $('#update-notes').val(data.notes);
            $('#update-id').val(data.dvdId);
        },
        error: function(xhr, status, error) {
            errorMessage('webServiceError', error);
        }
    });
    
    $('#edit').show();
}
    
function viewScreenButtonHandler() {
    $('#list').hide();
    $('#edit').hide();
    $('#make').hide();
    
    $.ajax ({
        type: 'GET',
        url: 'http://localhost:8080/dvd/' + this.id,
        success: function (data, status) {
            $('.error-messages').empty();
            $('#view-title').text(data.title);
            $('#view-year').text(data.realeaseYear);
            $('#view-director').text(data.director);
            $('#view-rating').text(data.rating);
            $('#view-notes').text(data.notes);
        },
        error: function(xhr, status, error) {
            errorMessage('webServiceError', error);
        }
    });
    
    $('#view').show();
}

function deleteScreenButtonHandler() {
    let confirmed = confirm('Are you sure you want to delete this DVD?');
    if (confirmed) {
        $.ajax({
            type: 'DELETE',
            url: 'http://localhost:8080/dvd/' + this.id,
            success: function (data, status) {
                loadList();
            },
            error: function(xhr, status, error) {
                errorMessage('webServiceError', error);
            }
        });
    }
}

function cancelButtonHandler() {
    loadList();
}
    
function createButtonHandler() {
    if (validate('add')) {
        $.ajax({
            type: 'POST',
            url: 'http://localhost:8080/dvd',
            data: JSON.stringify({
                title: $('#add-title').val(),
                realeaseYear: $('#add-year').val(),
                director: $('#add-director').val(),
                rating: $('#add-rating').val(),
                notes: $('#add-notes').val()
            }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            'dataType': 'json',
            success: function(data, status) {
                $('#add-title').val('');
                $('#add-year').val('');
                $('#add-director').val('');
                $('#add-rating').val('');
                $('#add-notes').val('');
                loadList();
            },
            error: function(xhr, status, error) {
                errorMessage('webServiceError', error);
            }
        });
    } else {
        errorMessage('formDataError');
    }
}
    
function searchButtonHandler() {
    let category = $('#search-category').val(),
        term     = $('#search-term').val();
    
    if (!term) loadList();
    else if(!category || !term) return;
    else {
        let pathSuffix = category + '/' + term.trim();
        $("#result-table").children().remove();
        
        getListItems(pathSuffix);
    }
}
    
function getListItems(pathSuffix) {
    $.ajax ({
        type: 'GET',
        url: 'http://localhost:8080/dvds/' + pathSuffix,
        success: function (data, status) {
            $.each(data, function (index, dvd) {
                let row = $('<tr></tr>');
                row.append($('<td></td>')
                    .append($('<a></a>')
                        .text(dvd.title)
                        .addClass('view-screen-button')
                        .attr('id', dvd.dvdId)
                    )
                );
                row.append($('<td></td>').text(dvd.realeaseYear));
                row.append($('<td></td>').text(dvd.director));
                row.append($('<td></td>').text(dvd.rating));
                row.append($('<td></td>')
                    .append($('<a></a>')
                        .text('Edit')
                        .addClass('edit-screen-button')
                        .attr('id', dvd.dvdId))
                    .append('&nbsp;&nbsp;|&nbsp;&nbsp;')
                    .append($('<a></a>')
                        .text('Delete')
                        .addClass('delete-screen-button')
                        .attr('id', dvd.dvdId)));

                $('#result-table').append(row);
            });
        },
        error: function(xhr, status, error) {
            errorMessage('webServiceError', error);
        }
    });
}
    
function updateButtonHandler() {
    if(validate('update')) {
        $.ajax({
            type: 'PUT',
            url: 'http://localhost:8080/dvd/' + $('#update-id').val(),
            data: JSON.stringify({
                title: $('#update-title').val(),
                realeaseYear: $('#update-year').val(),
                director: $('#update-director').val(),
                rating: $('#update-rating').val(),
                notes: $('#update-notes').val()
            }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            dataType: 'json',
            success: function(data, status) {
                loadList();
            },
            error: function(xhr, status, error) {
                errorMessage('webServiceError', error);
            }
        });
    } else {
        errorMessage('formDataError');
    }
}

loadList();

$('#create-button').on('click', createButtonHandler);
$('#update-button').on('click', updateButtonHandler);
$('.cancel-button').on('click', cancelButtonHandler);
$('#search-button').on('click', searchButtonHandler);
$(document).on('click', '#add-screen-button', addScreenButtonHandler);
$(document).on('click', '.view-screen-button', viewScreenButtonHandler);
$(document).on('click', '.edit-screen-button', editScreenButtonHandler);
$(document).on('click', '.delete-screen-button', deleteScreenButtonHandler);

});