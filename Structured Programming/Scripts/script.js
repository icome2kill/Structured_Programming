$(document).ready(function () {
    $("#filter_option").change(function () {
        window.location = "/Item/Index/" + $("select option:selected").val();
    });
});