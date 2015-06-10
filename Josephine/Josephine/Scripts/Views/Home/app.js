var app = angular.module('Josephine', []);

app.controller('HomeController', function (dataService, $scope) {
    
    $scope.data = [1,2,3]

    $scope.getData = function () {
        $scope.data = dataService.get().then(function (data) { $scope.data = data});
        console.log($scope.data);
    }   
});

app.factory('dataService', function ($http) {
    return {
        get: function () {
            var promise = $http.get('/home/products').then(function (response) {
                console.log('In factory: ' + response.data);
                return response.data;
            });
            return promise;
        }
    };
});