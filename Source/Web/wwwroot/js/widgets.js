$(document).ready(function () {

    $('#low-quantity-products-list .widget-heading-col input[type=checkbox]').on('change', lowQuantityProductsStoreChange);
    function lowQuantityProductsStoreChange(e) {
        var allCheckedStores = $(this).parent().parent().parent().find('input:checked');

        var url = '?lowquantitystores=';
        $.each(allCheckedStores, function (index, value) {
            url += $(value).val() + ',';
        });

        if (url.endsWith(',')) {
            url = url.substring(0, url.length - 1); 
        }

        window.location.href = '/administration' + url;
    }

});