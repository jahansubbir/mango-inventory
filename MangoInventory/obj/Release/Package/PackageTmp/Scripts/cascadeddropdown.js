$(document).ready(function () {
    var catUrl = $("#catUrl").val();
    var subcatUrl = $("#subCatUrl").val();
    $("#SubCategoryId").empty();
    $("#CategoryId").change(function () {
        $("#SubCategoryId").empty();
        var CategoryId = $("#CategoryId").val();
        var json = { catId: CategoryId };
        $.ajax({
            type: "POST",
            //url: '@Url.Action("GetSubCategoryIdById", "Product")',
            url: catUrl,
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
            //url: '@Url.Action("GetTypeIdById", "Product")',
            url: subcatUrl,
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