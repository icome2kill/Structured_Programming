$(document).ready(function () {
    $("#filter_option").change(function () {
        window.location = "/Item?typeId=" + $("select option:selected").val();
    });
    alert("Blah");
});