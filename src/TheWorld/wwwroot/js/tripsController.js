(function () {
    'use strict';

    //getting the existing module.
    angular
        .module('app-trips')
        .controller('tripsController', tripsController);    
    
    function tripsController($location) {
        var vm = this;
        vm.name = 'tripsController by uge Hidalgo';

    }
})();
