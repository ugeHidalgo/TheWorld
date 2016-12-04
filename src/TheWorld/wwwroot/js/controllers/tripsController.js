(function () {
    'use strict';

    //getting the existing module.
    angular
        .module('app-trips')
        .controller('tripsController', tripsController);    
    
    function tripsController($location) {
        var vm = this;
        vm.trips = [{
            name: "US Trip",
            created: new Date()
        }, {
            name: "World Trip",
            created: new Date()
        }];

        vm.newTrip = {};

        vm.addTrip = function () {
            vm.newTrip.created = new Date();
            vm.trips.push(vm.newTrip);
        };
    }
})();
