(function () {
    'use strict';

    angular
        .module('Jos.production')
        .controller('QuiltingController', QuiltingController);

    QuiltingController.$inject = ['$scope', 'prodDataService'];
    function QuiltingController($scope, prodDataService) {        

        var vm = this;        
        var mainWh = [];
        var recipes = [];

        vm.isGotData = {
            qTasks: false,
            mainWh: false,
            recipes: false
        };

        vm.getTextInfo = getTextInfo;
        vm.getItemInfo = getItemInfo;

        vm.getStatus = getStatus;
        vm.getStartTime = getStartTime;
        vm.getFinishTime = getFinishTime;        

        vm.quiltingTasks = [];

        vm.resultItem = vm.resultItemNext = {
            Id: 0,
            Name: '',
            Quantity: 0,
            UnitOfMeasurement: '',
            items: []
        };
        
        vm.resultSumCurrent = [];
        vm.resultSumNext = [];
        vm.showCurrTaskSum = showCurrTaskSum;
        vm.showNextTaskSum = showNextTaskSum;
        vm.startTask = startTask;

        vm.p = 80;        

        init();

        function init() {                        
            function checkData() {                
                if ($scope.$parent.isGotData.mainWh == true) {                    
                    mainWh = $scope.$parent.mainWhData;
                    recipes = $scope.$parent.recipesData;
                    vm.isGotData.mainWh = $scope.$parent.isGotData.mainWh;
                    vm.isGotData.recipes = $scope.$parent.isGotData.recipes;

                    clearInterval(timer);
                    getQuiltingTasks();                                        
                } 
            }
            var timer = setInterval(checkData, 200);                       
        }
        function getFinishTime() {
            if (vm.isGotData.qTasks == true) {
                if (vm.quiltingTasks[0].isCompleted == 0) {
                    return '---';
                } else {
                    var time = vm.quiltingTasks[0].FinishTime;

                    return time;
                }
                return '-----';
            }
        }
        function getFormattedDate(date) {
            return [
                date.getHours(),
                ':',
                date.getMinutes(),
                ' ',
                date.getDate(),
                '/',
                date.getMonth()].join('');
        }        
        function getItemById(id) {
            function findItem(id) {
                var arr = mainWh;

                for (var i = 0; i < arr.length; i++) {
                    for (var j = 0; j < arr[i].length; j++) {
                        if (id == arr[i][j].Id) {
                            return arr[i][j];
                        }
                    }
                }
            };
            var item = null;
            
            if (vm.isGotData.qTasks == false || vm.isGotData.mainWh == false) {
                return prodDataService.getMainWh().then(function (d) {
                    mainWh = d;
                    vm.isGotData.mainWh = true;
                    return findItem(id);
                });                
            } else {                
                return findItem(id);
            }            
        }
        function getItemInfo(tasklItem, resultQuant) {

            if (vm.isGotData.qTasks == false || vm.isGotData.mainWh == false) {                
                //alert('vm.isGotData == false in getItemInfo()');
                return
            }
            
            var text = getItemById(tasklItem.ItemId || tasklItem);
            var arr = [
                '#' + text.Id,
                ' \"' + text.Name + '\" ',
                text.Color,
                (tasklItem.Quantity || 1) * resultQuant,
                text.UnitOfMeasurement];

            return arr.join(' ');
        }
        function getTextInfo(item) {
            var tempItem = getItemById(item.Id);
            return [
                '#' + item.Id,
                ' \"' + tempItem.Name + '\" ',
                tempItem.Color,
                item.Quantity,
                item.UnitOfMeasurement
            ].join(' ');
        }
        function getQuiltingTasks() {
            vm.isGotData.qTasks = false;
            prodDataService.getTasksForQuilting().then(function (d) {
                vm.quiltingTasks = d;
                vm.isGotData.qTasks = true;

                for (var l = 0; l < d.length; l++) {
                    processTheTask(d[l], l);

                    if (l > 1) {
                        alert('error in production/getTasksForQuilting(), number of tasks is more than 2');
                        return;
                    }
                }
            });
        };        
        function getShortFormattedDate(date) {
            date = new Date(date);
            return [
                date.getHours(),
                ':',
                date.getMinutes()].join('');
        }
        function getStatus() {
            if (vm.isGotData.qTasks == true) {
                if (vm.quiltingTasks[0].isCompleted == 0) {
                    return ' - ожидает';
                }
                return ' - выполняется';
            }
        }        
        function getStartTime() {
            if (vm.isGotData.qTasks == true) {
                if (vm.quiltingTasks[0].isCompleted == 0) {
                    return '---';
                } else {
                    var time = vm.quiltingTasks[0].StartTime;
                    return time;
                }
                return '-----';
            }
        }                
        function processTheTask(task, index) {
            function fillItemById(id, quantity) {

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

                //we are going to search item in mainWhData with this param-id
                //console.log('we are going to search item in mainWhData with this param-id');                                       
                var item = null;
                var r = {};

                item = getItemById(id);

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
                function add(obj) {
                    for (var n = 0; n < rS.length; n++) {
                        if (rS[n].Id == obj.Id) {
                            rS[n].Quantity += obj.Quantity;
                            return;
                        }
                    }
                    rS.push(obj);
                }

                var rS = [];
                var item, categoryId;

                for (var i = 0; i < rI.items.length; i++) {

                    categoryId = getItemById(rI.items[i].Id).CategoryId;
                    if (!(categoryId == QUILTING_CAT || categoryId == KIT_CAT)) {
                        add(rI.items[i]);
                    }

                    for (var j = 0; j < rI.items[i].items.length; j++) {

                        categoryId = getItemById(rI.items[i].items[j].Id).CategoryId;
                        if (!(categoryId == QUILTING_CAT || categoryId == KIT_CAT)) {
                            add(rI.items[i].items[j]);
                        }
                    }
                }
                return rS;
            }

            var QUILTING_CAT = 1, KIT_CAT = 10;

            if (index == 0) {
                var rI = vm.resultItem;
            } else {
                var rI = vm.resultItemNext;
            }

            rI = fillItemById(task.ResultItemId);            

            if (index == 0) {
                vm.resultItem = rI;
                vm.resultSumCurrent = countTheSum();
            } else {
                vm.resultItemNext = rI;
                vm.resultSumNext = countTheSum();
            }
        }
        function showCurrTaskSum() {
            if (vm.isGotData.qTasks == true && vm.isGotData.mainWh == true) {
                var item;
                item = getItemById(vm.resultItem.Id);
                if (item.CategoryId == 10) {
                    return true;
                } else {
                    return false;
                }
            } else {
                return false
            }
        }
        function showNextTaskSum() {
            if (vm.isGotData.qTasks == true && vm.isGotData.mainWh == true) {
                var item = getItemById(vm.resultItemNext.Id);

                if (item.CategoryId == 10) {
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
        }
        function startTask() {
            vm.quiltingTasks[0].StartTime = getFormattedDate(new Date());
            vm.quiltingTasks[0].FinishTime = 'вычисляется';

            vm.quiltingTasks[0].isCompleted = 1;
        };
    }
})();