$(document).ready(function () {
    $("#Password").val('');
    $("#submit").click(function () {

        $("#loginForm").validate({
            rules: {
                UserId: {
                    required: true
                },
                Password: {
                    required: true
                }
            },
            messages: {
                UserId: "Enter Username",
                Password: "Enter Password"
            }
        });
    });

});