$(function () {

    $('.privilages-container').on('click', '.fa-check-square-o', { type: 'remove' }, privilegeIconClick);
    $('.privilages-container').on('click', '.fa-square-o', { type: 'add' }, privilegeIconClick);
    function privilegeIconClick(e) {
        e.preventDefault();
        var type = e.data.type;

        if (type === 'remove') {
            $(this).parent().find('input').prop('checked', false);
            $(this).removeClass('fa-check-square-o').addClass('fa-square-o');
        } else {
            $(this).parent().find('input').prop('checked', true);
            $(this).removeClass('fa-square-o').addClass('fa-check-square-o');
        }
    }

});
