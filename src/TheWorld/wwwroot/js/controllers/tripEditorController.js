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
        
        url = '/api/trips/'+ vm.tripName + '/stops';
        $http.get(url)
            .then(function (response) { //Sucess                
                angular.copy(response.data, vm.stops);
            }, function (error) { //Error
                vm.errorMessage = 'Failed to load stops: ' + error;
            })
            .finally(function () {
                vm.isBusy = false;
            });
    }
})();