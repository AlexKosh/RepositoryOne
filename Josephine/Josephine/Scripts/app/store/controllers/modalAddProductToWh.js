angular
    .module('Jos.store')
    .controller('ModalAddProductToWhController', AddProductToWhController);

AddProductToWhController.$inject = ['$scope', '$modalInstance', 'dataService'];
function AddProductToWhController($scope, $modalInstance, dataService) {
    {
        $scope.modelArr = $scope.$parent.warehouseData.DataNotations;
        $scope.mNumbers = Object.keys($scope.modelArr);
        $scope.mNamesAndNumbsDict = dataService.getModelNamesAndNumbsDictionary().then(function (d) { $scope.mNamesAndNumbsDict = d });

        $scope.getName = function () {
            for (var i = 0; i < $scope.mNamesAndNumbsDict.length; i++) {

                if ($scope.ModelNumber == $scope.mNamesAndNumbsDict[i].ModelNumber) {
                    $scope.Name = $scope.mNamesAndNumbsDict[i].Name;
                    break;
                }
            }
        }

        $scope.add = function () {
            dataService.postNewProd($scope);
            $scope.$parent.alertText = "Продукция добавлена.";
            $scope.$parent.alertClass = "alert-success";
            $scope.$parent.isGotData.alert = true;
            $modalInstance.close();
        }
        $scope.cancel = function () {
            $modalInstance.dismiss();
        }
    }
}