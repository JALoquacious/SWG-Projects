$(document).ready(function () {
    
    const PORT = 14181;

    function displayErrorMessages(errorList) {
        $('.error-messages').empty();
        
        if (errorList.length > 0) {
            $.each(errorList, function(idx, error) {
                $('.error-messages')
                    .append($('<li>')
                        .attr({
                            class: 'list-group-item list-group-item-danger'
                        })
                        .text(error)
                    );
                }
            );
        }
    }
    
    function getModelStatus(xhr, status, error) {
        let xhrResponse = (JSON.parse(xhr.responseText)),
            errorList = [];
        
        for (let key in xhrResponse.modelState) {
            if (xhrResponse.modelState.hasOwnProperty(key)) {
                errorList.push(xhrResponse.modelState[key][0]);
            }
        }
        return errorList;
    }

    function highlightField(page) {
        let formFields = ['title']; // 'year', 'director', 'rating'

        $.each(formFields, function (index, element) {
            let field = $('#' + page + '-' + element);

            if (!field.val() || field.val() === '') {
                field.addClass('error-outline');
            } else {
                field.removeClass('error-outline');
            }
        });
    }

    function displayScreen(pageId) {
        let pages = ['#view', '#make', '#edit', '#list'];
        for (let i = 0; i < pages.length; i++) {
            if (pageId === pages[i]) {
                $(pageId).show();
            } else {
                $(pages[i]).hide();
            }
        }
    }

    function loadList() {
        $('.reqd').removeClass('error-outline');
        $('.form-control').val('');
        $('.error-messages').empty();
        $("#result-table").children().remove();
        displayScreen('#list');
        getListItems('');
    }

    function addScreenButtonHandler() {
        displayScreen('#make');
    }

    function editScreenButtonHandler() {
        $.ajax({
            type: 'GET',
            url: `http://localhost:${PORT}/dvd/${this.id}`,
            success: function (data, status) {
                $('.error-messages').empty();
                $('#update-dvd').text('Edit DVD: ' + data.title);
                $('#update-title').val(data.title);
                $('#update-year').val(data.releaseYear);
                $('#update-director').val(data.director);
                $('#update-rating').val(data.rating);
                $('#update-notes').val(data.notes);
                $('#update-id').val(data.id);

                displayScreen('#edit');
            },
            error: function (xhr, status, error) {
                displayErrorMessages(getModelStatus(xhr, status, error));
            }
        });
    }

    function viewScreenButtonHandler() {
        $.ajax({
            type: 'GET',
            url: `http://localhost:${PORT}/dvd/${this.id}`,
            success: function (data, status) {
                $('.error-messages').empty();
                $('#view-title').text(data.title);
                $('#view-year').text(data.releaseYear);
                $('#view-director').text(data.director);
                $('#view-rating').text(data.rating);
                $('#view-notes').text(data.notes);

                displayScreen('#view');
            },
            error: function (xhr, status, error) {
                displayErrorMessages(getModelStatus(xhr, status, error));
            }
        });
    }

    function deleteScreenButtonHandler() {
        let confirmed = confirm('Are you sure you want to delete this DVD?');
        if (confirmed) {
            $.ajax({
                type: 'DELETE',
                url: `http://localhost:${PORT}/dvd/${this.id}`,
                success: function (data, status) {
                    loadList();
                },
                error: function (xhr, status, error) {
                    displayErrorMessages(getModelStatus(xhr, status, error));
                }
            });
        }
    }

    function cancelButtonHandler() {
        loadList();
    }

    function createButtonHandler() {
        $.ajax({
            type: 'POST',
            url: `http://localhost:${PORT}/dvd`,
            data: JSON.stringify({
                title: $('#add-title').val(),
                releaseYear: $('#add-year').val(),
                director: $('#add-director').val(),
                rating: $('#add-rating').val(),
                notes: $('#add-notes').val()
            }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            'dataType': 'json',
            success: function (data, status) {
                cancelButtonHandler();
            },
            error: function (xhr, status, error) {
                displayErrorMessages(getModelStatus(xhr, status, error));
            }
        });
        highlightField('add');
    }

    function searchButtonHandler() {
        let category = $('#search-category').val(),
            searchTerm = $('#search-term').val();

        if (!searchTerm) loadList();
        else if (!category || !searchTerm) return;
        else {
            let pathSuffix = category + '/' + searchTerm.trim();
            $("#result-table").children().remove();

            getListItems(pathSuffix);
        }
    }

    function getListItems(pathSuffix) {
        $.ajax({
            type: 'GET',
            url: `http://localhost:${PORT}/dvds/${pathSuffix}`,
            success: function (data, status) {
                $.each(data, function (index, dvd) {
                    let row = $('<tr></tr>');
                    row.append($('<td></td>')
                        .append($('<a></a>')
                            .text(dvd.title)
                            .addClass('view-screen-button')
                            .attr('id', dvd.id)
                        )
                    );
                    row.append($('<td></td>').text(dvd.releaseYear));
                    row.append($('<td></td>').text(dvd.director));
                    row.append($('<td></td>').text(dvd.rating));
                    row.append($('<td></td>')
                        .append($('<a></a>')
                            .text('Edit')
                            .addClass('edit-screen-button')
                            .attr('id', dvd.id))
                        .append('&nbsp;&nbsp;|&nbsp;&nbsp;')
                        .append($('<a></a>')
                            .text('Delete')
                            .addClass('delete-screen-button')
                            .attr('id', dvd.id)));

                    $('#result-table').append(row);
                });
            },
            error: function (xhr, status, error) {
                displayErrorMessages(getModelStatus(xhr, status, error));
            }
        });
    }

    function updateButtonHandler() {
        $.ajax({
            type: 'PUT',
            url: `http://localhost:${PORT}/dvd/${$('#update-id').val()}`,
            data: JSON.stringify({
                id: $('#update-id').val(),
                title: $('#update-title').val(),
                releaseYear: $('#update-year').val(),
                director: $('#update-director').val(),
                rating: $('#update-rating').val(),
                notes: $('#update-notes').val()
            }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            dataType: 'json',
            success: function (data, status) {
                loadList();
            },
            error: function (xhr, status, error) {
                displayErrorMessages(getModelStatus(xhr, status, error));
            }
        });
        highlightField('update');
    }

    loadList();
    
    $('.reqd').on('blur', highlightField.bind(null, 'add'));
    $('.reqd').on('blur', highlightField.bind(null, 'edit'));
    $('#create-button').on('click', createButtonHandler);
    $('#update-button').on('click', updateButtonHandler);
    $('.cancel-button').on('click', cancelButtonHandler);
    $('#search-button').on('click', searchButtonHandler);
    $(document).on('click', '#add-screen-button', addScreenButtonHandler);
    $(document).on('click', '.view-screen-button', viewScreenButtonHandler);
    $(document).on('click', '.edit-screen-button', editScreenButtonHandler);
    $(document).on('click', '.delete-screen-button', deleteScreenButtonHandler);
    
});
