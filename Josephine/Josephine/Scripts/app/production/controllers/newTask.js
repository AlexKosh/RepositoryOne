angular
    .module('Jos.production')
    .controller('ModalNewTaskController', NewTaskController);

NewTaskController.$inject = ['$scope', '$modalInstance', 'prodDataService'];
function NewTaskController($scope, $modalInstance, prodDataService) {

    //selected recipe
    $scope.recipe = [];
    $scope.isRecipeSelected = false;
    
    $scope.multiplier = 1;

    $scope.getItemInfoById = function (id, catId) {
        var text = $scope.$parent.getItemById(id, catId);
        return text = 'Id: ' + text.Id + ', ' + text.Name + ' ' + text.Color;
        //+ ' ' + text.Quantity + ' (' + text.UnitOfMeasurement + ')';
    };

    $scope.getRecipeInfo = function () {
        var r = $scope.recipe;
        var text = [
            '#' + r.ResultItemId,
            r.Name,
            r.ResultQuantity * $scope.multiplier,
            '('+ r.UnitsOfMeasurement +')'
        ];

        return text.join(' ');
    }
    $scope.setRecipe = function (r) {
        $scope.recipe = r;
        console.log($scope.recipe);
        $scope.isRecipeSelected = true;
    }
    $scope.search = [];

    //these methods get the array of recipes from the server 
    function getAllRecipes() {
        prodDataService.getRecipes().then(function (d) {
            $scope.recipes = d;
        });
    };
    $scope.recipes = [];
    getAllRecipes();

    //this method posts productionTask and TaskItems on server
    $scope.postProductionTaskData = function () {

        function ProductionTask(id, category, resultItemId, resultQuant, isCompl, sTime, fTime, priority) {
            this.TaskId = id;
            this.TaskCategory = category;
            this.ResultItemId = resultItemId;
            this.ResultQuantity = resultQuant;
            this.isCompleted = isCompl;
            this.StartTime = sTime;
            this.FinishTime = fTime;
            this.Priority = priority;

            this.TaskItems = [];
        }
        function TaskItem(recipeItem) {
            this.TaskItemId = 0;
            this.Quantity = recipeItem.Quantity;
            this.TaskId = 0;
            this.ItemId = recipeItem.ItemId;
        }
        function recipeItemsToTaskItems() {
            var resultArray = [];

            for (var i = 0; i < $scope.recipe.RecipeItems.length; i++) {
                var newTaskItem = new TaskItem($scope.recipe.RecipeItems[i]);
                resultArray.push(newTaskItem);
            }

            return resultArray;
        }

        $scope.ProductionTask = new ProductionTask(
            0,
            1,
            $scope.recipe.ResultItemId,
            $scope.recipe.ResultQuantity * $scope.multiplier,
            0,
            new Date(),
            new Date(1),
            11);
        $scope.ProductionTask.TaskItems = recipeItemsToTaskItems();

        //console.log($scope.ProductionTask.TaskItems);
        prodDataService.postProductionTask($scope.ProductionTask);
    }

    $scope.ok = function () {
        $modalInstance.close();
    };
    $scope.cancel = function () {
        $modalInstance.dismiss();
    };
}
