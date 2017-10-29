$(document).ready(function () {

    $('#header-search-input').on('keyup', function (e) {
        var text = $(this).val();
        var url = $(this).parent().data('url');
        var contentServerUrl = $(this).parent().data('content-server-url');
        var productUrl = $(this).parent().data('product-url');
        var manufacturerUrl = $(this).parent().data('manufacturer-url');
        var legalClientUrl = $(this).parent().data('legal-client-url');
        var naturalClientUrl = $(this).parent().data('legal-natural-url');

        var getEntityUrl = function (type) {
            switch (type) {
                case 'NaturalClient':
                    return naturalClientUrl;
                case 'LegalClient':
                    return legalClientUrl;
                case 'Product':
                    return productUrl;
                case 'Manufacturer':
                    return manufacturerUrl;
            }
        }

        var $search = $('#search-results');

        if (text.length < 3) {
            $search.hide().html('');
            return;
        }

        $.ajax({ method: 'GET', url: url, data: { searchFor: text }})
            .done(function (response) {
                var count = parseInt(response.count);
                var results = response.results;

                if (count === 0) {
                    $search.hide().html('');
                    return;
                }

                var content = '<ul>';

                for (var i = 0; i < results.length; i++) {
                    var entityUrl = getEntityUrl(results[i].type);

                    var picture = results[i].picture !== null ? '<div class="search-results-image-container"><img src="' + contentServerUrl + '/index/' + results[i].picture + '"></div>' : '';
                    content += '<li>' + picture + '<div><a href="' + entityUrl + '/' + results[i].id + '">' + results[i].title + '</a><span>' + results[i].details1 + '</span></div></li>';
                }

                content += '</ul>';

                $search.html(content).show();
            });
    });

});