$(document).ready(function () {
    $("#ModelId").empty();
    var modUrl = $("#modelUrl").val();
    $("#ProductId").change(function () {
        $("#ModelId").empty();
        var product = $("#ProductId").val();
        var json = { pName: product };
        $.ajax({
            type: "POST",
            url: modUrl,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(json),
            
            success: function (data) {
                $("#ModelId").append('<option>Select</option');
                alert("WoW");
               // alert(data);
                $.each(data, function (key, value) {
                    alert(value.Name);

                    $("#ModelId").append('<option value=' + value.Id + '>' + value.Model + '</option>');

                });

            },
            error:function(ts) {
                alert(ts.responseText);
            }
        });
    });
});