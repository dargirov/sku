$(function () {

    $('.privilages-container').on('click', '.fa-check-square', { type: 'remove' }, privilegeIconClick);
    $('.privilages-container').on('click', '.fa-square', { type: 'add' }, privilegeIconClick);
    function privilegeIconClick(e) {
        e.preventDefault();
        var type = e.data.type;

        if (type === 'remove') {
            $(this).parent().find('input').prop('checked', false);
            $(this).removeClass('fa-check-square').addClass('fa-square');
        } else {
            $(this).parent().find('input').prop('checked', true);
            $(this).removeClass('fa-square').addClass('fa-check-square');
        }
    }

});
