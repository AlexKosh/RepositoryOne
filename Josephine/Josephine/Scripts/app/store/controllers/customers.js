(function () {
    //'use strict';

    angular
        .module('Jos.store')
        .controller('CustomersController', CustomersController);

    CustomersController.$inject = ['$scope'];
    function CustomersController($scope) {
        {
            $scope.getCustomerById = function (id) {
                for (var i = 0; i < $scope.customersData.length; i++) {
                    if ($scope.customersData[i].CustomerId == id) {
                        return $scope.customersData[i];
                    }
                }
                return null;
            }
            $scope.getCustomerNameAndSurname = function (id) {
                var c = $scope.getCustomerById(id);
                var text = c.Name + ' ' + c.Surname;
                return text;
            }
            $scope.getDate = function (s) {
                return s.substring(6, 19);
            }
        }
    }
})();