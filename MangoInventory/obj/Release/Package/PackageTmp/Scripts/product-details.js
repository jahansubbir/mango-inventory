$(document).ready(function productdetail() {
    var detailsUrl = $("#productDetailsUrl").val();
    $("#ProductId").change(function () {
        $("#suggestionBody").empty();
        $("#suggestionBody").hide();
        //$(this).click();
        var productId = $("#ProductId").val();
        var json = {
            pId: productId
        }
        $.ajax({
            type: "POST",
            url: detailsUrl,
            contentType: "application/json; charset=utf-8",

            data: JSON.stringify(json),

            success: function (data) {
                $("#suggestionDiv").show();
                $("#suggestionBody").show();
               // alert(data.Name);
                $("#suggestionBody").append('<tr><td>' + data.Name + '</td></tr><tr><td>' + data.Model + '</td></tr>' +
                    '<tr><td>' + data.Brand + '</td></tr>');
            }
        });

    });
});