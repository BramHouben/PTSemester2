$(document).ready(function () {

    $(".TaakId").change(function () {

        var GeselecteerdeItem = $("#TaakId option:selected").attr("taak_info");
        console.log(GeselecteerdeItem);
      
        $("#Info").text(GeselecteerdeItem);

    });

});