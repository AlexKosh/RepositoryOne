(function () {
    'use strict';

    angular
        .module('Jos.store')
        .controller('SalesController', SalesController);

    SalesController.$inject = ['$scope', 'dataService'];
    function SalesController($scope, dataService) {
        {

            $scope.minDate = new Date();
            $scope.maxDate = new Date();
            $scope.selectedMinDate = new Date();
            $scope.selectedMaxDate = new Date();

            $scope.openMin = function () {
                $scope.openedMin = true;
            };
            $scope.openMax = function () {
                $scope.openedMax = true;
            };

            $scope.getSalesData = function () {
                $scope.showHelpAdviseAlert = false;
                $scope.minDate = $scope.selectedMinDate;
                $scope.maxDate = $scope.selectedMaxDate;

                dataService.getSalesData($scope)
                    .success(function (d) {

                        if (d == 0) {
                            $scope.$parent.alertText = "Продаж за этот период нет.";
                            $scope.$parent.alertClass = "alert-warning";
                            $scope.$parent.isGotData.alert = true;
                            $scope.salesData = {};
                            return;
                        }
                        $scope.salesData = dataService.formattingByModelNumberAndQuantity(d, $scope.$parent.storeData);
                    }).error(function () { alert('err in getSalesData'); });;
            };
            $scope.notSoFast = function () {
                document.getElementById('notSoFast').play();
                $scope.$parent.alertText = "Этот функционал в процессе разработки.";
                $scope.$parent.alertClass = "alert-danger";
                $scope.$parent.isGotData.alert = true;
            }

            $scope.getQuantitySum = function (arr) {

                var qSum = 0;
                for (var i = 0; i < arr.length; i++) {
                    qSum += arr[i].Quantity;
                }
                return qSum;
            };
            $scope.getCostSum = function (arr) {
                var cSum = 0;
                for (var i = 0; i < arr.length; i++) {
                    cSum += arr[i].Price * arr[i].Quantity;
                }
                return cSum + ' грн.';
            };
            $scope.getPrice = function (arr) {
                if (arr.length == 1) {
                    return arr[0].Price + ' грн.';
                }
                return '-';
            };
            $scope.getClass = function (l) {
                return l == 1 ? '' : 'btn-default';
            };

            $scope.setToday = function () {
                $scope.selectedMinDate = $scope.selectedMaxDate = new Date();
                $scope.getSalesData();
            }
        }
    }
})();