function GetPlaces(placename, placecity) {
    var procemessage = "<option value='0'> Please wait...</option>";
    $("#ddlplaces").html(procemessage).show();
    var url = "/Event/GetPlaces/";

    $.ajax({
        url: url,
        data: { placeName: placename, placeCity: placecity },
        cache: false,
        type: "POST",
        success: function (data) {
            console.log(data);
            var markup = "<option value='0'>Select Place</option>";
            for (var x = 0; x < data.length; x++) {
                console.log(data[x].text);
                markup += "<option value=" + data[x].value + ">" + data[x].text + "</option>";
            }
            $("#ddlplaces").html(markup).show();
        },
        error: function (reponse) {
            alert("error : " + reponse);
        }
    });

}

$(document).ready(function () {
    $('#PlaceName').change(function () {
        if ($('#PlaceCity').val() && $('#PlaceName').val()) {
            GetPlaces($('#PlaceName').val(), $('#PlaceCity').val());
        }
    });
    $('#PlaceCity').change(function () {
        if ($('#PlaceCity').val() && $('#PlaceName').val()) {
            GetPlaces($('#PlaceName').val(), $('#PlaceCity').val());
        }
    });
});