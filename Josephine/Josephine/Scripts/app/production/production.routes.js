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
            templateUrl: 'quiltingView'
        })
        .when('/manage', {
            templateUrl: 'managmentView'
        });
}