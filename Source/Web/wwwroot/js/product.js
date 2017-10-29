$(function () {

    /*$('.store-checkbox').on('change', storeCheckboxChange);
    function storeCheckboxChange(e) {
        var storeId = $(this).data('store-id');
        var storeName = $(this).data('store-name');
        if ($(this).prop('checked')) {
            if ($('.store-quantities.hidden').length === 1) {
                var $last = $('.store-quantities').last().removeClass('hidden');
                $last.attr('data-store-id', storeId);
                $last.find('.store-id').attr('value', storeId);
                return;
            }
            var $last = $('.store-quantities').last().clone();
            $last.attr('data-store-id', storeId);
            $last.find('.store-id').attr('value', storeId);
            $last.find('.stock-id').attr('value', 0);
            $last.find('strong').html(storeName);
            $('#price-container').append($last[0].outerHTML);
        } else {
            if ($('.store-quantities').length === 1) {
                $(this).prop('checked', true);
                return;
            }

            $('.store-quantities[data-store-id=' + storeId + ']').remove();
        }
    }*/

    $('.variants-container').on('change', '.store-toggle', storeToggleChange);
    function storeToggleChange(e) {
        var checked = $(this).prop('checked');
        if (checked) {
            $(this).parent().find('div').removeClass('hidden');
        } else {
            $(this).parent().find('div').addClass('hidden');
        }
    }

    $('#product-edit-form').on('submit', productEditFormSubmit);
    var allowFormSubmit = true;
    function productEditFormSubmit(e) {
        if (!allowFormSubmit) {
            e.preventDefault();
            return;
        }

        $('div.hidden').remove();

        var counter = 0;
        allowFormSubmit = false;
        var replacedHtml = '';
        /*$('.store-quantities').each(function (index, value) {
            $value = $(value);
            //$value.find('.store-quantity-price-net').attr('name', 'Stocks[' + index + '].PriceNet');
            //$value.find('.store-quantity-price-customer').attr('name', 'Stocks[' + index + '].PriceCustomer');
            $value.find('.store-quantity-quantity').attr('name', 'Stocks[' + index + '].Quantity');
            $value.find('.store-quantity-low-quantity').attr('name', 'Stocks[' + index + '].LowQuantity');
            $value.find('.store-id').attr('name', 'Stocks[' + index + '].StoreId');
            $value.find('.stock-id').attr('name', 'Stocks[' + index + '].Id');
            index++;
        });*/

        counter = 0;
        $('.variant-data').each(function (index, value) {
            $value = $(value);
            $value.find('.variant-id').attr('name', 'Variants[' + counter + '].Id');
            $value.find('.variant-code').attr('name', 'Variants[' + counter + '].Code');
            $value.find('.variant-price-net').attr('name', 'Variants[' + counter + '].PriceNet');
            $value.find('.variant-price-net-type').attr('name', 'Variants[' + counter + '].PriceNetType');
            $value.find('.variant-price-customer').attr('name', 'Variants[' + counter + '].PriceCustomer');
            $value.find('.variant-price-customer-type').attr('name', 'Variants[' + counter + '].PriceCustomerType');

            var counterStore = 0;
            $value.find('.stock-data').each(function (index, value) {
                $value = $(value);
                $value.find('.stock-quantity').attr('name', 'Variants[' + counter + '].Stocks[' + counterStore + '].Quantity');
                $value.find('.stock-quantity-measure-type').attr('name', 'Variants[' + counter + '].Stocks[' + counterStore + '].QuantityMeasureType');
                $value.find('.stock-low-quantity').attr('name', 'Variants[' + counter + '].Stocks[' + counterStore + '].LowQuantity');
                $value.find('.stock-low-quantity-measure-type').attr('name', 'Variants[' + counter + '].Stocks[' + counterStore + '].LowQuantityMeasureType');
                $value.find('.stock-store-id').attr('name', 'Variants[' + counter + '].Stocks[' + counterStore + '].StoreId');
                $value.find('.stock-stock-id').attr('name', 'Variants[' + counter + '].Stocks[' + counterStore + '].Id');
                counterStore++;
            });

            counter++;
        });

        //index = 0;
        //$('.stock-data').each(function (index, value) {
        //    $value = $(value);
        //    $value.find('.stock-quantity').attr('name', 'Variants[' + index + '].Stocks[' + index + '].Quantity');
        //    $value.find('.stock-low-quantity').attr('name', 'Variants[' + index + '].Stocks[' + index + '].LowQuantity');
        //    index++;
        //});

        //console.log(replacedHtml)
        //$('#price-container').html(replacedHtml);
        allowFormSubmit = true;
        //e.preventDefault();
    }

    var allChecked = false;
    $('#row-checkbox-all').on('change', rowCheckboxAllChange);
    function rowCheckboxAllChange(e) {
        if (!allChecked) {
            $('.row-checkbox').prop('checked', true);
        } else {
            $('.row-checkbox').prop('checked', false);
        }

        allChecked = !allChecked;
        rowCheckboxChange();
    }

    $('.row-checkbox').on('change', rowCheckboxChange);
    function rowCheckboxChange(e) {
        var checkedCount = $('.row-checkbox:checked').length;
        if (checkedCount > 0) {
            $('#delete-selected-btn').removeClass('hidden');
        } else {
            $('#delete-selected-btn').addClass('hidden');
        }
    }

    $('#delete-selected-btn').on('click', deleteSelectedRowsBtnClick);
    function deleteSelectedRowsBtnClick(e) {
        e.preventDefault();
        let url = $(this).data('url');
        let productIds = [];
        $('.row-checkbox:checked').each((index, element) => {
            productIds.push($(element).data('product-id'));
        });

        $.ajax({ method: 'POST', url: url, data: { action: 'ajax', productIds: JSON.stringify(productIds) } })
            .done(function (msg) {
                alert("Data Saved: " + msg);
            });
    }

    $('#add-variant-btn').on('click', addVariantBtnClick);
    function addVariantBtnClick(e) {
        var $last = $('.variants-container > div').last().clone();

        var currentIndex = -1;
        $last.find('label').each(function (index, value) {
            if (currentIndex === -1) {
                currentIndex = parseInt($(value).attr('for').replace('variant-code-', ''));
            }

            var newFor = $(value).attr('for');
            newFor = newFor.replace(currentIndex, currentIndex + 1);
            $(value).attr('for', newFor);
        });

        $last.find('input[type=text],input[type=number]').each(function (index, value) {
            var newId = $(value).attr('id');
            newId = newId.replace(currentIndex, currentIndex + 1);
            $(value).attr('id', newId);
        });

        $last.addClass('temp-variant-container');
        $('.variants-container').append($last[0].outerHTML);
        var $temp = $('.temp-variant-container');
        $temp.find('.variant-id').val(0);
        $temp.find('.stock-stock-id').val(0);
        $temp.removeClass('temp-variant-container');
    }

    $('.variants-container').on('click', '.variant-remove', variantRemoveClick);
    function variantRemoveClick(e) {
        e.preventDefault();
        $(this).parent().parent().remove();
    }

    function serializeForm(params) {
        $('#search-criteria form').find('input[type=text], select').each(function (index, element) {
            var value = $(element).val();
            params[$(element).attr('name')] = value;
        });
    }

    var productUrl = $('#search-result-table').data('product-url');
    var ajaxUrl = $('#search-result-table').data('url');

    var table = $('#search-result-table').DataTable({
        searching: false,
        processing: false,
        serverSide: true,
        iDisplayLength: 25,
        ajax: { 
            url: ajaxUrl,
            data: function (params) {
                serializeForm(params)
            }
        },
        order: [[5, 'desc']],
        columnDefs: [
            { width: '100px', targets: [5] },
            { className: 'left', targets: [2] },
            { orderable: false, targets: [0, 1] }
        ],
        columns: [
            //{
            //    data: 'id',
            //    render: function (id) {
            //        return '<input type="checkbox" data-product-id="' + id + '" class="row-checkbox">';
            //    }
            //},
            //{
            //    'className': 'details-control',
            //    'orderable': false,
            //    'data': null,
            //    'defaultContent': '<i class="fa fa-plus-square-o" aria-hidden="true"></i>'
            //},
            {
                data: null,
                render: function (data) {
                    return '<input type="checkbox" data-id="' + data.id + '">';
                }
            },
            {
                data: 'picture',
                render: function (path) {
                    if (path === null) {
                        return '';
                    }

                    return '<img src="' + path + '">';
                }
            },
            {
                data: null,
                render: function (data) {
                    return '<a href="' + productUrl + '/' + data.id + '">' + data.productName + '</a>';
                }
            },
            { data: 'categoryName' },
            { data: 'manufacturerName' },
            { data: 'createdOn' }
        ],
        language: {
            paginate: {
                first: '<i class="fa fa-step-backward" aria-hidden="true"></i>',
                previous: '<i class="fa fa-play fa-rotate-180" aria-hidden="true"></i>',
                next: '<i class="fa fa-play" aria-hidden="true"></i>',
                last: '<i class="fa fa-step-forward" aria-hidden="true"></i>'
            },
            lengthMenu: 'Покажи _MENU_ продукта',
            info: 'Показани _START_ до _END_ от _TOTAL_ продукта',
        },
        pagingType: 'full_numbers',
        dom: 'lt<"table-footer-wrapper"ip>'
    });

    //$('#search-result-table tbody').on('click', 'td.details-control', function () {
    //    var tr = $(this).closest('tr');
    //    var row = table.row(tr);

    //    if (row.child.isShown()) {
    //        row.child.hide();
    //        tr.removeClass('shown');
    //        tr.find('.fa').removeClass('fa-minus-square-o').addClass('fa-plus-square-o');
    //    } else {
    //        row.child(format(row.data())).show();
    //        tr.addClass('shown');
    //        tr.find('.fa').removeClass('fa-plus-square-o').addClass('fa-minus-square-o');
    //    }
    //});

    //function format(d) {
    //    // `d` is the original data object for the row
    //    return '<table class="search-result-sub-table">' +
    //        '<tr>' +
    //            '<td>Full name:</td>' +
    //            '<td>' + d.productName + '</td>' +
    //        '</tr>' +
    //        '<tr>' +
    //            '<td>Variants:</td>' +
    //            '<td>' + d.variants + '</td>' +
    //        '</tr>' +
    //    '</table>';
    //}

    $('#search-criteria-actions').find('.action-submit').on('click', searchCriteriaFormActionSubmitClick);
    function searchCriteriaFormActionSubmitClick(e) {
        e.preventDefault();
        serializeForm(table.ajax.params());
        table.ajax.reload();
    }

    $('#search-result-table tbody').on('dblclick', 'tr', function () {
        var data = table.row(this).data();
        window.location.href = productUrl + '/' + data.id;
    });

    //$('.product-stock-checkbox').on('change', onProductStockCheckboxChange);
    //function onProductStockCheckboxChange(e) {
    //    var checked = $(this).prop('checked');
    //    var $quantities = $(this).parent().parent().find('.product-stock-quantities');

    //    if (checked) {
    //        $quantities.removeClass('hidden');
    //    } else {
    //        $quantities.addClass('hidden');
    //    }
    //}

    $('input[type=checkbox].readonly').on('change', onReadonlyChange);
    function onReadonlyChange(e) {
        e.stopPropagation();
        $(this).prop('checked', true);
    }

    $('select.readonly').on('mousedown', onReadonlyChange);
    function onReadonlyChange(e) {
        e.preventDefault();
        this.blur();
        window.focus();
    }

});