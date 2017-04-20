function getModelsByMake(makeId) {
    console.log("getModelsByMake");
    $.ajax({
        url: 'http://localhost:59373/api/vehicles/models/makes/' + makeId,
        type: 'GET',
        contentType: "application/json"
    })
        .done(function (data) {
            console.log("done");
            let modelList = $('#Vehicle_ModelId');

            modelList.empty();
            $.each(data, function (index, item) {
                modelList.append($('<option>').text(item.Name).val(item.ModelId));
            });
        })
        .fail(function () {
            window.alert("Search failed.");
        });
}

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
                    <img class='detail-img' src='${imgPath}/${vehicle.Image}'/>
                </div>
                <div class='col-xs-12 col-md-3'>
                    <p class='padding-top-left-1em'><span class='strong'>Body Style: </span>${vehicle.BodyStyle}</p>
                    <p class='padding-top-left-1em'><span class='strong'>Trans: </span>${vehicle.IsAutomatic ? 'Automatic' : 'Manual'}</p>
                    <p class='padding-top-left-1em'><span class='strong'>Color: </span>${vehicle.ExteriorColor}</p>
                </div>
                <div class='col-xs-12 col-md-3'>
                    <p class='padding-top-left-1em'><span class='strong'>Interior: </span>${vehicle.InteriorColor}</p>
                    <p class='padding-top-left-1em'><span class='strong'>Mileage: </span>${vehicle.Mileage > 1000 ? vehicle.Mileage : 'New'}</p>
                    <p class='padding-top-left-1em'><span class='strong'>VIN #: </span>${vehicle.VIN}</p>
                </div>
                <div class='col-xs-12 col-md-3'>
                    <p class='padding-top-left-1em'><span class='strong'>Sale Price: </span>${vehicle.SalePrice}</p>
                    <p class='padding-top-left-1em'><span class='strong'>MSRP: </span>${vehicle.MSRP}</p>
                    <a href='${action}/${vehicle.VehicleId}'
                        class='btn btn-custom btn-large details-btn pull-down'>${action}
                    </a>
                </div>
            </div>
        </div>`
    )
}