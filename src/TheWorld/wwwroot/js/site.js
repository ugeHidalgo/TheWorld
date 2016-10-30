(function() {
    //var ele = $('#username'); //jQuery style of var ele = document.getElementById('username');
    //ele.text('Uge Hidalgo'); //ele.innerHTML = 'Uge Hidalgo';

    //var main = $('#main');
    //main.mouseenter(function() {
    //    main.css('background-color', '#888');
    //});

    //main.mouseleave(function() {
    //    main.css('background-color', '#eee');butto
    //});

    //var menuitems = $('ul.menu li a');
    //menuitems.click(function() {
    //    var me = this,
    //        sender = $(this);
    //    alert('Hello from :' + sender.text());
    //});    
    var sideBarAndWrapper = $('#sidebar, #wrapper');
    var toggleSidebarButton = $('#toggleSidebar');

    toggleSidebarButton.click(function () {
        var button = $(this);

        sideBarAndWrapper.toggleClass('hide-sidebar');  //añade o quita la clase indicada
        if (sideBarAndWrapper.hasClass('hide-sidebar')) {
            toggleSidebarButton.text('Show sidebar');
        } else {
            toggleSidebarButton.text('Hide sidebar');
        }
    });
})();

