$(document).ready(function () {
    $("#check").click(function () {
        $(":checkbox").prop("checked", true);
    });

    $("#uncheck").click(function () {
        $(":checkbox").prop("checked", false);
    });

    $("#checkUncheck").click(function () {
        $(":checkbox").each(function () {
            this.checked = !this.checked;
        });
    })
});