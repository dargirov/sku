$(document).ready(function () {

    $('.request-item-delete').on('click', requestItemDeleteClick);
    function requestItemDeleteClick(e) {
        e.preventDefault();
        var $that = $(this);
        var requestStockId = $(this).parent().parent().data('id');
        var url = $(this).parent().parent().parent().data('url-delete');

        $that.parent().prepend('<div class="grid-loading"></div>');

        $.ajax({ method: "POST", url: url, data: { requestStockId: requestStockId } })
            .done(function (msg) {
                $that.parent().parent().remove();
                $that.parent().find('.grid-loading').remove();
            })
            .fail(function () {
                $that.parent().find('.grid-loading').remove();
            });
    }

});