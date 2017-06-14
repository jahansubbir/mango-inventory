$(function () {
    $('#Add').click(function () {
        // var li = $('ul li:first').clone().appendTo($('ul'));
        var li = $('#mrLi').clone().appendTo($('#mrUl'));
        // empty the value if something is already filled in the cloned copy
        li.children('input').val('');
        li.append($('<button />').click(function () {
            li.remove();
            // don't need to check how many there are, since it will be less than 5.
            $('#Add').attr('disabled', false);
        }).text('Remove'));

        // disable button if its the 5th that was added
        if ($('ul').children().length == 10) {
            $(this).attr('disabled', true);
        }
    });
});