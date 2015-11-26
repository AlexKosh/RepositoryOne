angular
    .module('Jos.production')
    .controller('ModalNewRecipeController', NewRecipeController);

NewRecipeController.$inject = ['$scope', '$modalInstance', 'prodDataService'];
function NewRecipeController($scope, $modalInstance, prodDataService) {
    $scope.selectedCategory = 77;
    //this variable contains Recipe object or text of recipe name
    $scope.selectedRecipe = '';
    $scope.mainWhDataCopy = []
    $scope.mainWhDataCopy = getCopyOfArray($scope.$parent.mainWhData);
    //recipeItems[] keeps the components of recipe and rcpFakeArr[] needs to ng-repeat in <li>
    $scope.rcpFakeArr = [{ Id: ' ' }];
    $scope.recipeItems = [];

    function mainWhItem(item) {
        this.Id = item.Id;
        this.CategoryId = item.CategoryId;
        this.Name = item.Name;
        this.Color = item.Color;
        this.Quantity = item.Quantity;
        this.UnitOfMeasurement = item.UnitOfMeasurement;
    };
    function getCopyOfArray(arr) {
        console.log('have got copy of array');
        var resultArr = [];
        var tempArr = arr;

        for (var i = 0; i < tempArr.length; i++) {
            for (var j = 0; j < tempArr[i].length; j++) {
                var tempVar = new mainWhItem(tempArr[i][j]);
                resultArr.push(tempVar);
            }
        }

        return resultArr;
    };
    function Recipe(rCategory, name, resItemId, resName, resQuant, uoF) {
        this.RecipeCategory = rCategory;
        this.Name = name;
        this.ResultItemId = resItemId;
        this.ResultName = resName;
        this.ResultQuantity = resQuant;
        this.UnitsOfMeasurement = uoF;
    };
    function RecipeItem(itemId, iCat, name, quant, uoF) {
        this.ItemId = itemId;
        this.ItemCategory = iCat;
        this.Name = name;
        this.Quantity = quant;
        this.UnitsOfMeasurement = uoF;
    };

    $scope.recipeCategories = [1, 10, 11, 12];

    //get all Recipes from server and save it in $scope.recipes[]
    //$scope.recipes[] needs for typeahead in uName <input>
    function getAllRecipes() {
        prodDataService.getRecipes().then(function (d) {
            $scope.recipes = d;
        });
    };
    $scope.recipes = [];
    getAllRecipes();

    //watcher checks is there a Recipe object in $scope.selectedRecipe variable or just a text
    //if it is a Recipe object selected, function fills an arrays of RecipeItems
    $scope.$watch("selectedRecipe", function (val) {
        if (val.hasOwnProperty("RecipeItems")) {
            $scope.resultItem = new mainWhItem($scope.$parent.getItemById(val.ResultItemId, val.RecipeCategory - 1));
            $scope.resultItem.Quantity = val.ResultQuantity;

            var l = val.RecipeItems.length;
            for (var i = 0; i < l; i++) {
                $scope.rcpFakeArr[i] = { Id: ' ' };
                $scope.recipeItems[i] = new mainWhItem($scope.$parent.getItemById(val.RecipeItems[i].ItemId, val.RecipeItems[i].ItemCategory - 1));
                $scope.recipeItems[i].Quantity = val.RecipeItems[i].Quantity;
            }
        }
    });

    //resultItem is a mainWarehouse item
    // 
    $scope.resultItem = '';
    $scope.clResIt = function () {
        console.log($scope.resultItem);
    }

    $scope.processTheOrder = function () {
        var recipe, items, tempRcp, tempResItem;

        /* true: The selectedRecipe variable is a Recipe object, so this method do not creates new Recipe, it just edits this one      
         * false: The selectedRecipe variable is not a Recipe object, maybe it is a text, so this method creates new Recipe with
         * name which comprises selectedRecipe's text */
        console.log($scope.selectedRecipe.hasOwnProperty("Name"));
        if ($scope.selectedRecipe.hasOwnProperty("Name")) {
            tempRcp = $scope.selectedRecipe;

            recipe = new Recipe(tempRcp.RecipeCategory,
                tempRcp.Name,
                tempRcp.ResultItemId,
                tempRcp.ResultName,
                tempRcp.ResultQuantity,
                tempRcp.UnitsOfMeasurement);
        } else {
            tempResItem = $scope.resultItem;

            recipe = new Recipe(tempResItem.CategoryId,
                $scope.selectedRecipe,
                tempResItem.Id,
                tempResItem.Name,
                tempResItem.Quantity,
                tempResItem.UnitOfMeasurement);
        }

        items = function () {
            var o = $scope.recipeItems;
            var r = [];

            for (var i = 0; i < o.length; i++) {
                r[i] = new RecipeItem(o[i].Id,
                    o[i].CategoryId,
                    o[i].Name,
                    o[i].Quantity,
                    o[i].UnitOfMeasurement);
            }
            return r;
        };

        recipe.RecipeItems = items();

        console.log(recipe);
        prodDataService.postRecipe(recipe);
    }

    $scope.ok = function () {
        $modalInstance.close();
    };
    $scope.cancel = function () {
        $modalInstance.dismiss();
    };
}