$(document).ready(function () {
    $("#characteristicGroupsToAddButton").click(function () {
        var array = [];
        $("input:checked").each(function () {
            array.push($(this).val());
        });
        var selected;
        selected = array.join(',');
        $("#characteristicGroupsToAdd").val(selected);
    });
});