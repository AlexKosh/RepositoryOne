(function () {
    //'use strict';

    angular
        .module('Jos.store')
        .directive('leftTable', leftTable);

    function leftTable() {
        var directive = {
            restrict: 'E',
            templateUrl: '/home/leftTable'
        };
        return directive;
    }


    angular
        .module('Jos.store')
        .directive('rightTable', rightTable);

    function rightTable() {
        var directive = {
            restrict: 'E',
            templateUrl: '/home/rightTable'
        };
        return directive;
    }


    angular
        .module('Jos.store')
        .directive('createOrder', createOrder);

    function createOrder() {
        var directive = {
            restrict: 'E',
            templateUrl: '/home/createOrder'
        }
        return directive;
    }


    angular
        .module('Jos.store')
        .directive('navButtons', navButtons);

    function navButtons() {
        var directive = {
            restrict: 'E',
            templateUrl: '/home/navButtons'
        }
        return directive;
    }


    angular
        .module('Jos.store')
        .directive('salesReportView', salesReportView);

    function salesReportView() {
        var directive = {
            restrict: 'E',
            templateUrl: 'salesReportView'
        }
        return directive;
    }


    angular
        .module('Jos.store')
        .directive('filtPanel', filtPanel);

    function filtPanel() {
        var directive = {
            restrict: 'E',
            templateUrl: 'filtPanel'
        }
        return directive;
    }
})();