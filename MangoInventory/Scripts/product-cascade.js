$(document).ready(function () {
    var subUrl = $("#subUrl").val();
    var typeUrl = $("#typeUrl").val();
    $("#CategoryId").val("");
    $("#CategoryId").change(function () {
        $("#SubCategoryId").empty();
        $("#TypeId").empty();
        var CategoryId = $("#CategoryId").val();
        var json = { catId: CategoryId };
        $.ajax({
            type: "POST",
            url: subUrl,
            //url: RootUrl+ '/Product/GetSubCategoryIdById',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(json),
            success: function (data) {
                $("#SubCategoryId").append('<option>Select</option');
                $.each(data, function (key, value) {

                    $("#SubCategoryId").append('<option value=' + value.Id + '>' + value.Name + '</option>');

                });

            }
        });
    });
    $("#SubCategoryId").change(function () {
        $("#TypeId").empty();
        var SubCategoryId = $("#SubCategoryId").val();
        var json = { subId: SubCategoryId };
        $.ajax({
            type: "POST",
            url: typeUrl,
            // url: RootUrl + '/Product/GetTypeIdById',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(json),
            success: function (data) {
                $("#TypeId").append('<option>Select</option');
                $.each(data, function (key, value) {

                    $("#TypeId").append('<option value=' + value.Id + '>' + value.Name + '</option>');

                });

            }
        });
    });
});