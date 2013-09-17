$(document).ready(function () {
    $("#filter_option").change(function () {
        window.location = "/Item/Index/?typeId=" + $("select option:selected").val();
    });
});