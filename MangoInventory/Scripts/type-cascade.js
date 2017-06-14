$(document).ready(function () {
    var subUrl = $("#SubUrl").val();
    $("#SubCategoryId").empty();
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
});