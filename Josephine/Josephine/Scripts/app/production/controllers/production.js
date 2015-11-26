angular
    .module('Jos.production')
    .controller('ProductionController', ProductionController);

ProductionController.$inject = ['$scope', '$modal', 'prodDataService'];
function ProductionController($scope, $modal, prodDataService) {
    
    $scope.mainWhData = {};
    $scope.endProdData = { Data: [], DataNotations: [] };
    $scope.searchText = [];
    $scope.isGotData = {
        mainWh: false,
        endProd: false,
        recipes: false,
        qTasks: false
    };

    init();

    function getMainWhData() {
        prodDataService.getMainWh().then(function (d) {
            $scope.mainWhData = d;
            $scope.isGotData.mainWh = true;

            getRecipesData();
        });
    };
    function getEndProdData() {
        prodDataService.getEndProd().then(function (d) { $scope.endProdData = d; $scope.isGotData.endProd = true; });
    };
    function getRecipesData() {
        prodDataService.getRecipes().then(function (d) {
            $scope.recipesData = d;
            $scope.isGotData.recipes = true;

            getQuiltingTasks();
        });
    }

    function init() {
        getMainWhData();
        getEndProdData();
        //getRecipesData();

        //getQuiltingTasks();
    }
    
    //mainWh region
    $scope.categoryView = [];
    $scope.mainWhGot = false;

    $scope.getCategoryName = function (i) {
        var result;
        switch (i) {
            case 1: result = 'Стеганый материал';
                break;
            case 2: result = 'Основной материал';
                break;
            case 3: result = 'Подкладочный материал';
                break;
            case 4: result = 'Утепляющий прокладочный материал';
                break;
            case 5: result = 'Прокладочный материал';
                break;
            case 6: result = 'Отделочный материал';
                break;
            case 7: result = 'Фурнитура';
                break;
            case 8: result = 'Опушка';
                break;
            case 9: result = 'Упаковка';
                break;
            case 10: result = 'Комплект материалов';
                break;
            case 11: result = 'Комплект кроя';
                break;
            case 12: result = 'Готовое изделие';
                break;
            case 77: result = 'Новое';
                break;
            default:
                result = 'Другое';
        }
        return result;
    }

    $scope.openNewItem = function () {
        var modalInstance = $modal.open({
            templateUrl: 'newItem.modal',
            controller: 'ModalEditItemController',            
            scope: $scope
        });
    };
        
    //end of mainWh region

    //quiling region
    $scope.populateRecipe = function () {
        prodDataService.pplRecipe();
    };
    function Recipe(name, recipeCat, recipeId) {
        this.Name = name;
        this.RecipeCategory = recipeCat;
        this.RecipeId = recipeId;
    };
    function RecipeItem(itemId, itemCategory, quant, uoF) {
        this.Id = null;
        this.RecipeId = null;
        this.ItemId = itemId;
        this.ItemCategory = itemCategory;
        this.Quantity = quant;
        this.UnitsOfMeasurement = uoF;
    };
    $scope.createTestRecipe = function () {
        var rcp = new Recipe("Стежка Вика светлый беж 100", 1, null);
        var rcpItems = [new RecipeItem(54, 2, 1, "м"), new RecipeItem(92, 4, 1, "м")];
        rcp.RecipeItems = rcpItems;

        prodDataService.postRecipe(rcp);
    };

    $scope.qTGot = false;


    $scope.p = 80;

    $scope.openNewTask = function () {
        var modalInstance = $modal.open({
            templateUrl: 'getNewQuiltingTask.modal',
            controller: 'ModalNewTaskController',
            size: 'lg',
            scope: $scope
        });
    };
    $scope.openNewRecipe = function () {
        var modalInstance = $modal.open({
            templateUrl: 'newRecipe.modal',
            controller: 'ModalNewRecipeController',
            scope: $scope
        });
    };
        
    //returns item from mainWhDb
    $scope.getItemById = function (id, itemCategory) {
        var result;
        for (var i = 0; i < $scope.mainWhData[itemCategory].length; i++) {
            if ($scope.mainWhData[itemCategory][i].Id == id) {
                result = $scope.mainWhData[itemCategory][i];
            }
        }
        return result;
    };
    $scope.quiltingTasks = [];

    $scope.getItemInfo = function (tasklItem, resultQuant) {
        function getItemById(id) {
            var arr = $scope.mainWhData;

            for (var i = 0; i < arr.length; i++) {
                for (var j = 0; j < arr[i].length; j++) {
                    if (id == arr[i][j].Id) {
                        return arr[i][j];
                    }
                }
            }
            return null;
        }

        if ($scope.isGotData.qTasks == false || $scope.isGotData.mainWh == false) {
            return
        }
        text = getItemById(tasklItem.ItemId || tasklItem);
        var arr = [
            '#' + text.Id,
            ' \"' + text.Name + '\" ',
            text.Color,
            (tasklItem.Quantity || 1) * resultQuant,
            text.UnitOfMeasurement];
        
        return arr.join(' ');
    }

    $scope.getTextInfo = getTextInfo;
    function getTextInfo(item) {
        function getItemByID(id) {
            var arr = $scope.mainWhData;
            for (var i = 0; i < arr.length; i++) {
                for (var j = 0; j < arr[i].length; j++) {
                    if (arr[i][j].Id == id) {
                        //
                        //looks like we found this item
                        //console.log('looks like we found this item:');
                        //console.log(arr[i][j]);
                        //
                        return arr[i][j];
                        break;
                    }
                }
            }
        }

        var tempItem = getItemByID(item.Id);
        return [
            '#' + item.Id,
            ' \"' + tempItem.Name + '\" ',
            tempItem.Color,
            item.Quantity,
            item.UnitOfMeasurement
        ].join(' ');
    }

    function getQuiltingTasks() {
        $scope.isGotData.qTasks = false;
        prodDataService.getTasksForQuilting().then(function (d) {
            $scope.quiltingTasks = d;
            $scope.isGotData.qTasks = true;

            $scope.resultItem = {
                Id: 0,
                Name: '',
                Quantity: 0,
                UnitOfMeasurement: '',
                items: []
            };
            $scope.resultSum = [];
                        
            processTheTask(d[0]);

            function processTheTask(task) {
                var rI = $scope.resultItem;
                
                var QUILTING_CAT = 1, KIT_CAT = 10;

                function getItemByID(id) {
                    var arr = $scope.mainWhData;
                    for (var i = 0; i < arr.length; i++) {
                        for (var j = 0; j < arr[i].length; j++) {
                            if (arr[i][j].Id == id) {
                                //
                                //looks like we found this item
                                //console.log('looks like we found this item:');
                                //console.log(arr[i][j]);
                                //
                                return arr[i][j];
                                break;
                            }
                        }                        
                    }
                }
                function fillItemById(id, quantity) {

                    //we are going to search item in mainWhData with this param-id
                    //console.log('we are going to search item in mainWhData with this param-id');                                       
                    var item = null;
                    var r = {};

                    item = getItemByID(id);

                    function searchForTheQuant(id) {

                        if (task.ResultItemId == id) {
                            //
                            //console.log('Quantity is:');
                            //console.log(task.ResultQuantity);
                            //
                            return task.ResultQuantity;
                        } else {
                            for (var i = 0; i < task.TaskItems.length; i++) {                                
                                if (task.TaskItems[i].ItemId == id) {
                                    //
                                    //console.log('Quantity is:');
                                    //console.log(task.TaskItems[i].Quantity);
                                    //
                                    return task.TaskItems[i].Quantity * task.ResultQuantity;
                                }
                            }
                        }
                    }

                    r.Id = item.Id;
                    r.UnitOfMeasurement = item.UnitOfMeasurement;
                    r.Quantity = quantity || searchForTheQuant(item.Id);
                    r.Name = [
                        '#' + item.Id,
                        ' \"' + item.Name + '\" ',
                        item.Color,
                        r.Quantity,
                        item.UnitOfMeasurement
                    ].join(' ');
                                        
                    //console.log('lets try to understand, is our item a complex or it is simple');
                    if (item.CategoryId == QUILTING_CAT || item.CategoryId == KIT_CAT) {
                        //our item is complex item, that consist of some other items
                        //so now, we are going to find this other items by id in list of recipes
                        //console.log('our item is complex item, that consist of some other items');
                        //console.log('we are going to find recipe in recipe-list');
                        var recipes = $scope.recipesData;
                        var recipeItemsId = [];
                        r.items = [];

                        for (var i = 0; i < recipes.length; i++) {
                            if (recipes[i].ResultItemId == item.Id) {
                                //looks like we have found our recipe
                                //console.log('look like we have found our recipe');
                                //
                                for (var j = 0; j < recipes[i].RecipeItems.length; j++) {
                                    //
                                    //lets try to write id of the items that this recipe contains
                                    //console.log('lets try to write id of items that this recipe contains');
                                    recipeItemsId.push(recipes[i].RecipeItems[j].ItemId)
                                    r.items.push({});
                                }
                                //console.log(recipeItemsId);


                                for (var k = 0; k < recipeItemsId.length; k++) {
                                    r.items[k] = fillItemById(recipeItemsId[k], recipes[i].RecipeItems[k].Quantity * r.Quantity);
                                }
                                break;
                            }
                        }
                    } else {
                        //console.log('our item is simple');
                        r.items = [];
                    }
                    //console.log('returns the r');                    
                    return r;
                }
                function countTheSum() {
                    var rS = $scope.resultSum;
                    var item, categoryId;

                    function add(obj) {
                        for (var n = 0; n < rS.length; n++) {
                            if (rS[n].Id == obj.Id) {
                                rS[n].Quantity += obj.Quantity;
                                return;
                            }
                        }
                        rS.push(obj);
                    }

                    for (var i = 0; i < rI.items.length; i++) {

                        categoryId = getItemByID(rI.items[i].Id).CategoryId;
                        if (!(categoryId == QUILTING_CAT || categoryId == KIT_CAT)) {
                            add(rI.items[i]);
                        }

                        for (var j = 0; j < rI.items[i].items.length; j++) {

                            categoryId = getItemByID(rI.items[i].items[j].Id).CategoryId;
                            if (!(categoryId == QUILTING_CAT || categoryId == KIT_CAT)) {
                                add(rI.items[i].items[j]);
                            }
                        }
                    }
                    return rS;
                }

                //console.log('Ok, lets get started');
                //console.log('ResultItemId: ');
                //console.log(task.ResultItemId);
                rI = fillItemById(task.ResultItemId);                

                $scope.resultItem = rI;
                $scope.resultSum = countTheSum();                                               
            }             
        });
    }
    //end of quiling region


    //managment region   
    $scope.getQTasks = function () {        
        prodDataService.getTasksForQuilting().then(function (d) {            
            console.log(d);
        });
    }    
    //end of managment region
    
}
