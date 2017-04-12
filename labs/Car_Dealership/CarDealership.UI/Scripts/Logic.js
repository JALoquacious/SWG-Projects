
function queryVehicles(imgPath, isUsed, isAspNetUser, action) {
    $.ajax({
        url: 'http://localhost:59373/api/vehicles/search?condition='
            + ((isUsed == 0 || isUsed == 1) ? isUsed : '')
            + '&isAspNetUser=' + ((isAspNetUser) ? 'true' : 'false')
            + '&searchTerm=' + $('#SearchTerm').val()
            + '&minPrice=' + $('#MinPrice').val()
            + '&maxPrice=' + $('#MaxPrice').val()
            + '&minYear=' + $('#MinYear').val()
            + '&maxYear=' + $('#MaxYear').val(),
        type: 'GET',
        contentType: "application/json"
    })
    .done(function (data) {
        $.each(data, function (index, vehicle) {
            getResultList(index, vehicle, action);
        });
    })
    .fail(function () {
        window.alert("Search failed.");
    });
}

function getResultList(index, vehicle, action) {
    $('.search-results').append(
        `<div class='panel panel-darkslategray'>
            <div class='panel-heading'> ${vehicle.Year} ${vehicle.Make} ${vehicle.Model}</div>
            <div class='row'>
                <div class='col-xs-12 col-md-3'>
                    <img class='featured-img' src='${imgPath}/${vehicle.Image}'/>
                </div>"
                <div class='col-xs-12 col-md-3'>
                    <table class='table table-hover'>
                        <tbody>
                            <tr>
                                <td class='strong'>Body Style:</td>
                                <td>${vehicle.BodyStyle}</td>
                            </tr>
                            <tr>
                                <td class='strong'>Trans:</td>
                                <td>${vehicle.IsAutomatic ? 'Automatic' : 'Manual'}</td>
                            </tr>
                            <tr>
                                <td class='strong'>Color:</td>
                                <td>${vehicle.ExteriorColor}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class='col-xs-12 col-md-3'>
                    <table class='table table-hover'>
                        <tbody>
                            <tr>
                                <td class='strong'>Interior:</td>
                                <td>${vehicle.InteriorColor}</td>
                            </tr>
                            <tr>
                                <td class='strong'>Mileage:</td>
                                <td>${vehicle.Mileage > 1000 ? vehicle.Mileage : 'New'}</td>
                            </tr>
                            <tr>
                                <td class='strong'>VIN:</td>
                                <td>${vehicle.VIN}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class='col-xs-12 col-md-3'>
                    <table class='table table-hover'>
                        <tbody>
                            <tr>
                                <td class='strong'>Sale Price:</td>
                                <td>${vehicle.SalePrice}</td>
                            </tr>
                            <tr>
                                <td class='strong'>MSRP:</td>
                                <td>${vehicle.MSRP}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div class='row'>
                <div class='col-xs-12'>
                    <p>
                        <a href='${action}/${vehicle.VehicleId}'
                            class='btn btn-custom btn-large pull-right details-btn'>${action}</a>
                    </p>
                </div>
            </div>
        </div>`
    )
}