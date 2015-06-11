var app = angular.module('Josephine', []);

app.controller('HomeController', function (dataService, $scope) {
    
    $scope.response = [1, 2, 3];

    $scope.cl = function (text) {        
        console.log(text);
    }

    $scope.getData = function () {
        $scope.response = dataService.get().then(function (d) { $scope.response = d });
        //console.log($scope.data);
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