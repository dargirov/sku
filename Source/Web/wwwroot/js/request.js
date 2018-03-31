$(document).ready(function () {

    $('.request-variant-select').on('change', requestVariantSelectChange);
    function requestVariantSelectChange(e) {
        var variantId = $(this).val();
        var $storeSelect = $(this).parent().next().find('select');
        $storeSelect.find('option').addClass('hidden');
        var $firstStore = $storeSelect.find('option[data-variant=' + variantId + ']').removeClass('hidden').first();
        $firstStore.prop('selected', true);
        var quantity = $firstStore.data('quantity');
        $(this).parent().parent().find('.request-quantity-col').html(quantity);
    }

    $('.request-store-select').on('change', requestStoreSelectChange);
    function requestStoreSelectChange(e) {
        var quantity = $(this).find(':selected').data('quantity');
        console.log(quantity)
        $(this).parent().parent().find('.request-quantity-col').html(quantity);
    }

});