(function () {
    'use strict';

    angular.module('app-trips')
    .controller('tripEditorController', tripEditorController);

    function tripEditorController($routeParams, $http) {
        var vm = this, url;

        vm.tripName = $routeParams.tripName;
        vm.stops = [];
        vm.errorMessage = '';
        vm.isBusy = true;
        vm.newStop = {};

        url = '/api/trips/' + vm.tripName + '/stops';
        $http.get(url)
            .then(function (response) { //Sucess                
                angular.copy(response.data, vm.stops);
                _showMap(vm.stops);
            }, function (error) { //Error
                vm.errorMessage = 'Failed to load stops: ' + error;
            })
            .finally(function () {
                vm.isBusy = false;
            });

        vm.addStop = function () {
            vm.isBusy = true;
            $http.post(url, vm.newStop)
                .then(function (response) { //Success
                    vm.stops.push(response.data);
                    _showMap(vm.stops);
                    vm.newStop = {};
                }, function (error) { //Error
                    vm.errorMessage = 'Failed to save new stop :' + error;
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        };
    }
    

    function _showMap(stops) { //_ is for private functions
        if (stops && stops.length > 0) {

            //_ es la libreía underscore y map es la funcion usada par el mapeo
            var mapStops = _.map(stops, function (item) {
                return {
                    lat: item.latitude,
                    long: item.longitude,
                    info: item.name
                };
            });

            //show map
            travelMap.createMap({
                stops: mapStops,  //the stops to load into the map
                selector: '#map', //where to place the map
                currentStop: 1, //The current stop
                initialZoom: 4
            });

        }
    }

})();