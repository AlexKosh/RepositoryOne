angular
    .module('Jos.production')
    .controller('ProdNavController', ProdNavController);

ProdNavController.$inject = ['$scope'];
function ProdNavController($scope) {
    $scope.numberForActiveClass = 0;
}