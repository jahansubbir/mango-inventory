$(document).ready(function () {
    var i;
    var createUrl = $("#createUrl").val();
    $('#inputHead').hide();
    //$("#suggestionDiv").hide();
    $("#AddButton").click(function () {
        //$("#suggestionDiv").hide();
        $("#ContactPerson").prop('readonly', true);
        $("#CompanyName").prop('readonly', true);
        $("#Address").prop('readonly', true);
        $("#Date").prop('readonly', true);
        // $("#CompanyName").prop('readonly', true);

        $("#tableBody").empty();
        var contactPerson = $("#ContactPerson").val();
        var companyName = $("#CompanyName").val();
        var address = $("#Address").val();
        var date = $("#Date").val();
        var catId = $("#CategoryId").val();
        var subCatId = $("#SubCategoryId").val();
        var typeId = $("#TypeId").val();
        var productId = $("#ProductId").val();
        var quantity = $("#Quantity").val();
        var unitPrice = $("#UnitPrice").val();
        $("#ProductId").val('');
        $("#Quantity").val('');
        $("#UnitPrice").val('');
        var json = {
            ContactPerson: contactPerson,
            CompanyName: companyName,
            Address: address,
            Date: date,
            CategoryId: catId,
            SubCategoryId: subCatId,
            TypeId: typeId,
            ProductId: productId,
            Quantity: quantity,
            UnitPrice: unitPrice
        }
        $.ajax({
            type: "POST",
            url: createUrl,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(json),
            success: function (data) {
                $("#inputHead").show();

                $("#tableBody").show();
                //alert(data.Product);
                $.each(data, function (key, value) {
                    i = key;
                    $("#tableBody").append('<tr><td><input type="hidden" id="Id" name="Id" value="' + value.Id + '"/>' + value.Product + '</td><td>' + value.Model + '</td><td>' + value.Quantity + '</td><td>' + value.UnitPrice + '</td><td>' + value.TotalPrice + '</td></tr>');
                    // $("#tableBody").append('<tr><td>' + value.Product + '</td><td>' + value.Model + '</td><td>' + value.Quantity + '</td><td>' + value.UnitPrice + '</td><td>' + value.TotalPrice + '</td></tr>');


                });


            }
        });
    });
    $("#Edit[" + i + "]").click(function () {
        alert('wow!');
    });
});