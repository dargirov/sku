$(function () {

    //$('#document-search-for-client').on('click', documentSearchForClientClick);
    //function documentSearchForClientClick(e) {
    //    e.preventDefault();
    //    var url = $(this).data('url');

    //    $.ajax({ method: 'GET', url: url, data: {} })
    //        .done(function (response) {
    //            var data = response.data;
    //            var table = '<table id="vex-table"><tr><th>МОЛ / Име на клиент</th><th>Тип</th><th>Име на фирма / ЕГН</th><th>Град</th><th>Адрес</th></tr>';
    //            for (var i = 0; i < data.length; i++) {
    //                var client = data[i];
    //                var name = client.name === undefined ? client.mol : client.name;
    //                var firmName = client.firmName === undefined ? client.personalNo : client.firmName;
    //                table += '<tr><td>' + name + '</td><td></td><td>' + firmName + '</td><td>' + client.city.name + '</td><td>' + client.address + '</td></tr>';
    //            }

    //            table += '</table>';

    //            vex.dialog.confirm({
    //                message: 'tt',
    //                callback: function (value) {
    //                    if (value) {
    //                        console.log('Successfully destroyed the planet.')
    //                    } else {
    //                        console.log('Chicken.')
    //                    }
    //                }
    //            })

    //        });


    //}

    var clients;
    var clientSelectAutoComplete = new autoComplete({
        selector: '#document-select-client',
        minChars: 1,
        source: function (term, response) {
            var url = $('#document-select-client').data('url');
            $.ajax({ method: 'GET', url: url, data: { 'SearchCriteria.FirmaNamePersonlNo': term, 'SearchCriteria.MolName': term } })
                .done(function (result) {
                    clients = result.data;
                    var autocompleteData = [];
                    for (var i = 0; i < result.data.length; i++) {
                        var name = result.data[i].name === undefined ? result.data[i].mol : result.data[i].name;
                        var firmName = result.data[i].firmName === undefined ? result.data[i].personalNo : result.data[i].firmName;
                        autocompleteData.push(result.data[i].id + '/' + name + ' - ' + firmName);
                    }

                    response(autocompleteData)
                });
        },
        renderItem: function (item, search) {
            var itemSplit = item.split('/');
            search = search.replace(/[-\/\\^$*+?.()|[\]{}]/g, '\\$&');
            var re = new RegExp("(" + search.split(' ').join('|') + ")", "gi");
            return '<div class="autocomplete-suggestion" data-val="' + itemSplit[1] + '" data-id="' + itemSplit[0] + '">' + itemSplit[1].replace(re, "<b>$1</b>") + '</div>';
        },
        onSelect: function (event, term, item) {
            var id = parseInt($(item).attr('data-id'));
            var $container = $('#document-header-client-info');
            $container.html('');
            for (var i = 0; i < clients.length; i++) {
                if (clients[i].id === id) {
                    var ul = generateClientUl(clients[i]);
                    $container.html(ul)
                    break;
                }
            }
        }
    });

    function generateClientUl(client) {
        var ul = '<ul>';
        var name = client.name === undefined ? { name: client.mol, title: 'МОЛ' } : { name: client.name, title: 'Име на клиент' };
        var firmName = client.firmName === undefined ? { name: client.personalNo, title: 'ЕГН' } : { name: client.firmName, title: 'Име на фирма' };
        ul += '<li><i>' + name.title + '</i> ' + name.name + '</li>';
        ul += '<li><i>' + firmName.title + '</i> ' + firmName.name + '</li>';
        if (client.eik !== undefined) {
            ul += '<li><i>ЕИК</i> ' + client.eik + '</li>';
        }

        if (client.hasDds !== undefined && client.hasDds) {
            ul += '<li><i>ДДС номер</i> BG' + client.eik + '</li>';
        }

        ul += '<li><i>Град</i> ' + client.city.name + '</li>';
        ul += '<li><i>Адрес</i> ' + client.address + '</li>';
        return ul;
    }

    var clientsUrl = $('#document-search-for-client-modal').data('url');
    var modal = $('#document-search-for-client-modal').iziModal({
        title: 'Search for client',
        width: 800,
        headerColor: '#525157',
        onOpening: function (modal) {
            modal.startLoading();

            $.get(clientsUrl, function (response) {
                clients = response.data;
                var search = '<div id="search-criteria"><div><form>';
                search += '<div><label for="SearchCriteria_MolName">МОЛ / Име на клиент</label><input type="text" id="SearchCriteria_MolName" class="modal-search-criteria" name="SearchCriteria.MolName"></div>';
                search += '<div><label for="SearchCriteria_FirmaNamePersonlNo">Име на фирма / ЕГН</label><input type="text" id="SearchCriteria_FirmaNamePersonlNo" class="modal-search-criteria" name="SearchCriteria.FirmaNamePersonlNo"></div>';
                search += '<div><label for="SearchCriteria_CityId">Град</label><select class="modal-search-criteria" id="SearchCriteria_CityId" name="SearchCriteria.CityId"><option selected="selected" value="0"></option><option value="1">София</option><option value="2">Пловдив</option><option value="3">Асеновград</option><option value="4">Варна</option></select></div>';
                search += '</form></div></div>';
                var table = '<div>' + generateTable(response.data) + '</div>';
                $("#document-search-for-client-modal .iziModal-content").html(search + table);
                modal.stopLoading();
            });
        }
    });

    function generateTable(data) {
        var table = '<table class="modal-table"><tr><th>МОЛ / Име на клиент</th><th>Type</th><th>Име на фирма / ЕГН</th><th>Град</th><th>Адрес</th><th>Телефон</th><th></th></tr>';
        var icons = ['', 'fa-gavel', 'fa-user'];
        for (var i = 0; i < data.length; i++) {
            var client = data[i];
            var name = client.name === undefined ? { name: client.mol, type: 1 } : { name: client.name, type: 2 };
            var firmName = client.firmName === undefined ? client.personalNo : client.firmName;
            table += '<tr><td>' + name.name + '</td><td><i class="fa ' + icons[name.type] + '" aria-hidden="true"></i></td><td>' + firmName + '</td><td>' + client.city.name + '</td><td>' + client.address + '</td><td>' + client.phone + '</td><td><a class="modal-select" data-client-id="' + client.id + '"><i class="fa fa-check" aria-hidden="true"></i></td></a></tr>';
        }

        table += '</table>';
        return table;
    }

    $('#document-search-for-client').on('click', function (e) {
        e.preventDefault();
        $('#document-search-for-client-modal').iziModal('open');
    });

    $('body').on('change keyup paste', '.modal-search-criteria', modalSearchCriteriaChange);
    function modalSearchCriteriaChange(e) {
        var form = $(this).parent().parent().serialize();
        $.ajax({ method: 'GET', url: clientsUrl, data: form })
            .done(function (response) {
                clients = response.data;
                var table = generateTable(response.data);
                $('body').find('.modal-table').parent().html(table);
            });
    }

    $('body').on('click', '.modal-select', modalSelectClick);
    function modalSelectClick(e) {
        e.preventDefault();
        var clientId = $(this).data('client-id');
        var $container = $('#document-header-client-info');
        $container.html('');
        for (var i = 0; i < clients.length; i++) {
            if (clients[i].id === clientId) {
                var ul = generateClientUl(clients[i]);
                $container.html(ul);
                modal.iziModal('close');
                break;
            }
        }
    }

});