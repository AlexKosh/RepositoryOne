angular
    .module('Jos.production')
    .controller('ModalEditItemController', EditItemController);

EditItemController.$inject = ['$scope', '$modalInstance', 'prodDataService'];
function EditItemController($scope, $modalInstance, prodDataService) {

    $scope.tempItem = { CategoryId: null };    
    $scope.recipeCategories = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];

    $scope.$watch("tempItem.CategoryId", watchCategoryId);

    function watchCategoryId(val) {
        
        if ($scope.tempItem.CategoryId < 6) {
            $scope.tempItem.UnitOfMeasurement = "м";
        }
        else {
            $scope.tempItem.UnitOfMeasurement = "шт.";
        }        
    }
    
    $scope.ok = function () {

        if ($scope.tempItem.CategoryId == 11) {
            prodDataService.postCut($scope.tempItem);
        } else {
            prodDataService.postMainWhItem($scope.tempItem);
        }
        
        $modalInstance.close();
    };
    $scope.cancel = function () {
        $modalInstance.dismiss();
    }
}