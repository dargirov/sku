$(document).ready(function () {

    $('#header-search-input').on('keyup', function (e) {
        var text = $(this).val();
        var url = $(this).parent().data('url');
        var contentServerUrl = $(this).parent().data('content-server-url');
        var productUrl = $(this).parent().data('product-url');
        var $search = $('#search-results');

        if (text.length < 3) {
            $search.hide().html('');
            return;
        }

        $.ajax({ method: 'GET', url: url, data: { text: text }})
            .done(function (response) {
                var count = parseInt(response.count);
                var products = response.products;

                if (count === 0) {
                    $search.hide().html('');
                    return;
                }

                var content = '<ul>';

                for (var i = 0; i < products.length; i++) {
                    var variants = '<ul class="search-results-variants-container">';
                    for (var j = 0; j < products[i].variants.length; j++) {
                        variants += '<li>' + products[i].variants[j].code  + '</li>';
                    }

                    variants += '</ul>';

                    var picture = products[i].pictures[0] !== undefined ? '<div class="search-results-image-container"><img src="' + contentServerUrl + '/index/' + products[i].pictures[0].thumb.guid + '"></div>' : '';
                    content += '<li>' + picture + '<div><a href="' + productUrl + '/' + products[i].id + '">' + products[i].name + '</a>' + variants + '</div></li>';
                }

                content += '</ul>';

                $search.html(content).show();
            });
    });

});