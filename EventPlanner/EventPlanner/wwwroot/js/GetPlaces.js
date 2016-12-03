function formatPlace(place) {
    if (place.loading) return place.text;

    var markup = "<div class='select2-result-place clearfix'>";
    if (place.photoUrl) {
        markup += "<div class='select2-result-place_image'><img src='" + place.photoUrl + "' /></div>";
    }
    markup += "<div class='select2-result-place_meta'>" +
        "<div class='select2-result-place_title'>" + place.name + "</div>";

    if (place.location.address) {
        markup += "<div class='select2-result-place_description'>" + place.location.address + "</div>";
    }

    markup += "</div></div>";

    return markup;
}

function formatPlaceSelection(place) {
    return place.name;
}

$(document).ready(function () {

    $("#ddlplaces")
       .select2({
           ajax: {
               url: "/Event/GetPlaces/",
               dataType: "json",
               type: "POST",
               delay: 250,
               data: function (params) {
                   return {
                       placeName: params.term, placeCity: $("#PlaceCity").val()
                   };
               },
               processResults: function (data) {
                   return {
                       results: data
                   };
               },
               cache: true
           },
           escapeMarkup: function (markup) { return markup; },
           minimumInputLength: 1,
           templateResult: formatPlace,
           templateSelection: formatPlaceSelection 
       });

});