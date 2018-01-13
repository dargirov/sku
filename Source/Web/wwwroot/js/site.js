
$(document).ready(function () {

    $.notify.addStyle('error', {
        html: '<div><i class="fa fa-exclamation-triangle" aria-hidden="true"></i> <span data-notify-html/></div>',
        classes: {
            base: {}
        }
    });

    $.notify.addStyle('success', {
        html: '<div><i class="fa fa-info-circle" aria-hidden="true"></i> <span data-notify-html/></div>',
        classes: {
            base: {}
        }
    });

    $.notify.defaults({ style: 'error' });

    vex.defaultOptions.className = 'vex-theme-plain';
    vex.dialog.buttons.YES.text = $('body').data('yes');
    vex.dialog.buttons.NO.text = $('body').data('no');

    $('.tooltip').tooltipster({
        theme: 'tooltipster-borderless',
        animation: 'fade',
        animationDuration: 100
    });

    $('.form-control-tooltip').tooltipster({
        theme: 'tooltipster-borderless'
    });

    if (!$.fn.DataTable.isDataTable('#search-result-table')) {

        var $table = $('#search-result-table');
        var orderColumn = $table.data('order-column') || 1;
        var orderDir = $table.data('order-dir') || 'asc';

        var table = $table.DataTable({
            searching: false,
            language: {
                paginate: {
                    first: '<i class="fa fa-step-backward" aria-hidden="true"></i>',
                    previous: '<i class="fa fa-play fa-rotate-180" aria-hidden="true"></i>',
                    next: '<i class="fa fa-play" aria-hidden="true"></i>',
                    last: '<i class="fa fa-step-forward" aria-hidden="true"></i>'
                },
                lengthMenu: 'Покажи _MENU_ разултата',
                info: 'Показани _START_ до _END_ от _TOTAL_ разултата',
            },
            pagingType: 'full_numbers',
            dom: 'lt<"table-footer-wrapper"ip>',
            order: [[orderColumn, orderDir]],
            columnDefs: [{ 'orderable': false, 'targets': 0 }]
        });

        $('#search-result-table tbody').on('dblclick', 'tr', function () {
            var data = table.row(this).data();
            var $a = $(data[1]);
            window.location.href = $a.attr('href');
        });

        $('#search-criteria-actions').find('.action-submit').on('click', searchCriteriaFormActionSubmitClick);
        function searchCriteriaFormActionSubmitClick(e) {
            e.preventDefault();
            $('#search-criteria').find('form').submit();
        }
    }

    $('form.validate').on('submit', formSubmit);
    function formSubmit(e) {
        var notifications = [];

        $('input[data-val-required], select[data-val-required]').each(function (index, element) {
            var name = $(element).attr('name');
            var skip = $(element).data('notify-skip');

            if (name === 'Id' || skip) {
                return;
            }

            var value = $(element).val();

            if (value === '' || value === '0') {
                e.preventDefault();
                var message = $(element).attr('data-val-required');
                notifications.push(message);
                $.notify(message);
            }
        });

        addNotificationsToDropdownMenu('error', notifications);
    }

    function addNotificationsToDropdownMenu(type, notifications) {
        $('#notifications-container > a').html('(' + notifications.length + ')');

        if (notifications.length === 0) {
            $('#notifications-menu').html('');
            return;
        }

        var iconClass = type === 'error' ? 'fa-exclamation-triangle' : '';

        var html = '<ul>';

        for (var i = 0; i < notifications.length; i++) {
            html += '<li class="' + type + '"><i class="fa ' + iconClass + '" aria-hidden="true"></i>' + notifications[i] + '</li>';
        }

        html += '</ul>';

        $('#notifications-menu').html(html);
    }

    var contentFormDirty = false;
    $('.content-box form').change(function () {
        contentFormDirty = true;
    });

    var clearFormConfirmMessage = $('body').data('clear-form-confirm-message');
    $('.btn-reset').on('click', resetBtnClick);
    function resetBtnClick(e) {
        e.preventDefault();
        var $this = $(this);
        if (contentFormDirty) {
            vex.dialog.confirm({
                message: clearFormConfirmMessage,
                callback: function (value) {
                    if (value) {
                        $('.main-form').find('input[type="reset"]').click();
                    }
                }
            });
        }
    }

    var deleteConfirmMessage = $('body').data('delete-confirm-message');
    $('.btn-delete').on('click', deleteBtnClick);
    function deleteBtnClick(e) {
        e.preventDefault();
        var $this = $(this);
        vex.dialog.confirm({
            message: deleteConfirmMessage,
            callback: function (value) {
                if (value) {
                    window.location.href = $this.attr('href');
                }
            }
        });
    }

    $('.btn-submit').on('click', submitBtnClick);
    function submitBtnClick(e) {
        e.preventDefault();
        $('.main-form').find('input[type="submit"]').click();
    }

    $('.datetime-picker').flatpickr({
        dateFormat: 'd.m.Y',
    });

    var mainMenuOpen = false;
    $('#main-menu-toggle-container').find('a').on('click', mainMenuBarsClick);
    function mainMenuBarsClick(e) {
        e.preventDefault();
        if (mainMenuOpen) {
            $('#main-menu-container').slideUp();
        } else {
            $('#main-menu-container').slideDown();
        }

        mainMenuOpen = !mainMenuOpen;
    }

    $(window).resize(function () {
        var width = $(this).width();
        if (width > 516) {
            $('#main-menu-container').removeAttr('style');
            mainMenuOpen = false;
        }
    });

    if ($('#notify-messages-container').length > 0) {
        var lis = $('#notify-messages-container').find('li');
        var style = $('#notify-messages-container').data('style');
        $.each(lis, function (key, value) {
            $.notify($(value).html(), { style: style });
        });
    }

    $('#notifications-container > a').on('click', notificationsClick);
    function notificationsClick(e) {
        e.preventDefault();
        var open = $(this).data('open');

        if (open) {
            $('#notifications-menu').hide();
            $(this).removeClass('active');
        } else {
            $('#notifications-menu').show();
            $(this).addClass('active');
        }

        $(this).data('open', !open);
    }

});