(function () {
    'use strict';

    angular
        .module('Jos.production')
        .controller('CuttingController', CuttingController);

    CuttingController.$inject = ['$scope', 'prodDataService'];
    function CuttingController($scope, prodDataService) {
        var vm = this;
        vm.asd = 'asd';
    }
})();