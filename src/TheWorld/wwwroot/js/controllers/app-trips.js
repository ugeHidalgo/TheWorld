(function () {
    
    //Creating the module
    angular.module('app-trips', ['simpleControls','ngRoute'])
        .config(function ($routeProvider) { //Used to configure client-side route
            $routeProvider.when('/', {  //Vista de todos los trips
                controller: 'tripsController',
                controllerAs: 'vm',
                templateUrl: '/views/tripsView.html'
            });

            $routeProvider.when('/editor/:tripName', { //Vista para editar el trip selecionado (tripName)
                controller: 'tripEditorController',
                controllerAs: 'vm',
                templateUrl: '/views/tripEditorView.html'
            });

            $routeProvider.otherwise({
                redirectTo: '/'
            })
        });

})();
