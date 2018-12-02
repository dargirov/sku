$(document).ready(function () {

    //$('#new-requests-modal').iziModal({
    //    title: 'Създаване на заявка',
    //    width: 400,
    //    headerColor: '#525157'
    //});

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

    $('.request-from-store-select').on('change', requestStoreSelectChange);
    function requestStoreSelectChange(e) {
        var quantity = $(this).find(':selected').data('quantity');
        $(this).parent().parent().find('.request-quantity-col').html(quantity);
    }

    function requestAjax(url, data, onDone) {
        $.ajax({ method: "POST", contentType: 'application/json', url: url, data: JSON.stringify(data) })
            .done(function (result) {
                onDone(result);
            });
    }

    function getNewRequestData() {
        var data = [];

        $('#search-result-table').find('tbody > tr').each(function (index, element) {
            var quantity = parseInt($(element).find('.request-quantity-input').val());
            if (!isNaN(quantity) && quantity > 0) {
                var stockId = parseInt($(element).find('.request-from-store-select option:selected').data('stock'));
                var priority = parseInt($(element).find('.request-from-store-select option:selected').data('priority'));
                var fromStoreId = parseInt($(element).find('.request-from-store-select').val());
                var toStoreId = parseInt($(element).find('.request-to-store-select').val());

                data.push({ stockId: stockId, fromStoreId: fromStoreId, toStoreId: toStoreId, quantity: quantity, priority: priority });
            }
        });

        return data;
    }

    $('#request-create').on('click', requestCreateButtonClick);
    function requestCreateButtonClick(e) {
        e.preventDefault();
        var url = $(this).data('url');
        var token = $('body').find('input[name=__RequestVerificationToken]').val();
        var data = getNewRequestData();

        if (data.length === 0) {
            $.notify('Въведи заявено количество');
            return;
        }

        var requestId = sessionStorage.getItem('StockRequestId');

        $.ajax({ method: 'POST', headers: { 'RequestVerificationToken': token }, contentType: 'application/json', url: url, data: JSON.stringify({ StockRequests: data, RequestId: requestId }) })
            .done(function (result) {
                console.log(result)
                sessionStorage.setItem('StockRequestId', result);
            });

        //if ($('#new-requests-modal').length === 0) {
        //    requestAjax(url, { StockRequests: data, RequestId: 0 }, function (result) {
        //        window.location.reload(true);
        //    });
        //    return;
        //}

        //$('#new-requests-modal').iziModal('open');
    }

    //$('#new-requests-modal-add').on('click', newRequestsModalAddClick);
    //function newRequestsModalAddClick(e) {
    //    var url = $('#request-create').data('url');
    //    var requestId = $('#new-requests-modal-select-request').val();
    //    var storeId = $('#new-requests-modal-select-store').val();
    //    var data = getNewRequestData(storeId);

    //    requestAjax(url, { StockRequests: data, RequestId: requestId }, function (result) {
    //        window.location.reload(true);
    //    });
    //}

});