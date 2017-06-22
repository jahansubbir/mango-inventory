$(document).ready(function () {
    var suggestionUrl = $("#suggestionUrl").val();
    $("#ProductName").keyup(function () {
        $("#productSuggestion").val('');
        var productName = $("#ProductName").val();
        var json = { pName: productName };
        $.ajax({
            type: "POST",
            url: suggestionUrl ,
            //url: '/Product/GetProductNameByCharacter',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(json),
            success: function (data) {
                $.each(data, function (key, value) {
                    //alert(value.Name);
                    $("#productSuggestion").val(value.Name);
                });
                //alert(data);
            }
        });
        //$("#productSuggestion").val();

    });

});