(function () {
    'use strict';

    angular
        .module('Jos.production')
        .config(routeProviderHelper);

    routeProviderHelper.$inject = ['$routeProvider'];
    function routeProviderHelper($routeProvider) {

        $routeProvider
            .when('/mainWh', {
                templateUrl: 'mainWhView'
            })
            .when('/quilting', {
                templateUrl: 'quiltingView',
                controller: 'QuiltingController',
                controllerAs: 'vm'
            })
            .when('/cutting', {
                templateUrl: 'cuttingView',
                controller: 'CuttingController',
                controllerAs: 'vm'
            })
            .when('/manage', {
                templateUrl: 'managmentView'
            });
    }
})();