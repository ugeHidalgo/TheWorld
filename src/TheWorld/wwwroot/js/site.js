(function() {    
    var sideBarAndWrapper = $('#sidebar, #wrapper');
    var sidebarToggleButton = $('#sidebarToggle');
    var sidebarToggleIcon = $('#sidebarToggle i.fa');

    sidebarToggleButton.click(function () {
        var button = $(this);

        sideBarAndWrapper.toggleClass('hide-sidebar');  //añade o quita la clase indicada
        if (sideBarAndWrapper.hasClass('hide-sidebar')) { //si existe la clase hide-sidebar quita la left y añade right
            sidebarToggleIcon.removeClass('fa-chevron-left');
            sidebarToggleIcon.addClass('fa-chevron-right');
        } else {
            sidebarToggleIcon.removeClass('fa-chevron-right'); //viceversa
            sidebarToggleIcon.addClass('fa-chevron-left');
        }
    });
})();

