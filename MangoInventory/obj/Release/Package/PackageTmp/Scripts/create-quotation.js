$(document).ready(function () {
    $("#ProductId").val('');
    $("#Quantity").val('');
    $("#UnitPrice").val('');
    //$("#mrTr").hide();
    
    var createUrl = $("#createUrl").val();
    $('#inputHead').hide();
    //$("#suggestionDiv").hide();
    //$("#AddButton").click(function () {
    //    //$("#suggestionDiv").hide();
    //    $("#ContactPerson").prop('readonly', true);
    //    $("#CompanyName").prop('readonly', true);
    //    $("#Address").prop('readonly', true);
    //    $("#Date").prop('readonly', true);
    //    // $("#CompanyName").prop('readonly', true);

        $("#AddButton").click(function() {

            if (!$.trim($('#ContactPerson').val())) {
                alert("Contact Person cannot be empty");

            }
            if (!$.trim($('#CompanyName').val())) {
                alert("Contact Person cannot be empty");

            }
            if (!$.trim($('#Address').val())) {
                alert("Address cannot be empty");

            }
            if (!$.trim($('#Date').val())) {
                alert("Pick Date");
            }
            if (!$.trim($('#CategoryId').val())) {
                alert("Category cannot be empty");

            }
            if (!$.trim($('#SubCategoryId').val())) {
                alert("Sub category cannot be empty");

            }
            if (!$.trim($('#TypeId').val())) {
                alert("Type cannot be empty");

            }
            if (!$.trim($('#ProductId').val())) {
                alert("Select Product");
            }
            if (!$.trim($('#Quantity').val())) {
                alert("Write Quantity");
            }
            if (!$.trim($('#UnitPrice').val())) {
                alert("Unit price cannot be null");
            } else {


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
                    success: function(data) {
                        $("#inputHead").show();

                        $("#tableBody").show();
                        //alert(data.Product);
                        $.each(data, function(key, value) {
                            i = key;
                            //alert(value.)
                            $("#tableBody").append('<tr><td><input type="hidden" id="Id" name="Id" value="' + value.Id + '"/>' + value.Product + '</td><td>' + value.Model + '</td><td>' + value.Quantity + '</td><td>' + value.UnitPrice + '</td><td>' + value.Total + '</td></tr>');
                            // $("#tableBody").append('<tr><td>' + value.Product + '</td><td>' + value.Model + '</td><td>' + value.Quantity + '</td><td>' + value.UnitPrice + '</td><td>' + value.Total + '</td></tr>');


                        });


                    }
                });
            }
    });
    //$("#Edit[" + i + "]").click(function () {
    //    alert('wow!');
    //});
});