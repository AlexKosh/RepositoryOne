(function () {
    'use strict';

    angular
        .module('Jos.production')
        .directive('navBtnsProd', navBtnsProd);

    function navBtnsProd() {
        var directive = {
            restrict: 'E',
            templateUrl: 'navBtnsProd'
        }
        return directive;
    }


    angular
        .module('Jos.production')
        .directive('mainWhTable', mainWhTable);

    function mainWhTable() {
        var directive = {
            restrict: 'E',
            templateUrl: 'mainWhTable'
        }
        return directive;
    }


    angular
        .module('Jos.production')
        .directive('quiltingView', quiltingView);

    function quiltingView() {
        var directive = {
            restrict: 'E',
            templateUrl: 'quiltingView',
            controller: 'QuiltingController',
            controllerAs: 'vm'
        }
        return directive;
    }


    angular
        .module('Jos.production')
        .directive('whForQuilting', whForQuilting);

    function whForQuilting() {
        var directive = {
            restrict: 'E',
            templateUrl: 'whForQuilting'
        }
        return directive;
    }


    angular
        .module('Jos.production')
        .directive('validateNewRecipe', validateNewRecipe);

    function validateNewRecipe() {
        var directive = {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, ele, attrs, ctrl) {
                scope.$watch(attrs.ngModel, function (value) {
                    var isValid = ctrl.$modelValue.hasOwnProperty('Id') || ctrl.$modelValue.hasOwnProperty('ProductId');
                    ctrl.$setValidity('invalidTypeOfValue', isValid);
                });
            }
        }
        return directive;
    }
})();