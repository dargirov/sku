$(function () {

    $.notify.addStyle('error', {
        html: '<div><span data-notify-html/></div>',
        classes: {
            base: {}
        }
    });

    $.notify.defaults({ style: 'error' });

    $('.go').on('click', goButtonClick);
    function goButtonClick(e) {
        e.preventDefault();
        var submit = true;

        $('input[data-val-required], select[data-val-required]').each(function (index, element) {
            $element = $(element);
            var iconClass = $element.parent().find('i').attr('class');
            var value = $element.val();
            var message = '<i class="' + iconClass + '"></i> ' + $element.attr('data-val-required');

            if (value === '' || value === '0') {
                submit = false;
                $.notify(message);
            }
        });

        if (submit) {
            $('#container').find('form').submit();
        }
    }

});