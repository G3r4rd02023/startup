$(document).ready(function () {
    $("#CountryId").change(function () {
        $("#CityId").empty();
        $("#CityId").append('<option value="0">[Seleccione una ciudad...]</option>');
        $.ajax({
            type: 'POST',
            url: Url,
            dataType: 'json',
            data: { countryId: $("#CountryId").val() },
            success: function (data) {
                $.each(data, function (i, data) {
                    $("#CityId").append('<option value="'
                        + data.CityId + '">'
                        + data.Name + '</option>');
                });
            },
            error: function (ex) {
                alert('Error al recuperar ciudades.' + ex);
            }
        });
        return false;
    })
});