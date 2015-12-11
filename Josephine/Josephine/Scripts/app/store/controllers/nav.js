(function () {
    //'use strict';

    angular
        .module('Jos.store')
        .controller('StoreNavController', StoreNavController);

    StoreNavController.$inject = ['$scope'];
    function StoreNavController($scope) {
        {
            $scope.numberForActiveClass = 0;

            $scope.storeOnClick = function () {
                $scope.$parent.leftTableView = $scope.$parent.storeData;
                $scope.$parent.isSelected = 'st';
                $scope.numberForActiveClass = 1;
            };
            $scope.whOnClick = function () {
                $scope.$parent.leftTableView = $scope.$parent.warehouseData;
                $scope.$parent.isSelected = 'wh';
                $scope.numberForActiveClass = 2;
            };
            $scope.ordOnClick = function () {
                $scope.$parent.leftTableView = {};
                $scope.$parent.isSelected = 'ord';
                $scope.numberForActiveClass = 3;
            };
            $scope.custOnClick = function () {
                $scope.$parent.leftTableView = {};
                $scope.$parent.isSelected = 'cust';
                $scope.numberForActiveClass = 4;
            };
            $scope.salesOnClick = function () {
                $scope.$parent.leftTableView = {};
                $scope.$parent.isSelected = 'sales';
                $scope.numberForActiveClass = 5;
            }

        }
    }
})();