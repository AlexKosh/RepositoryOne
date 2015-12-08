(function () {
    'use strict';

    angular
        .module('Jos.production')
        .controller('NewCuttingTaskController', NewCuttingTaskController);
})();

NewCuttingTaskController.$inject = ['$scope', '$modalInstance', 'prodDataService']
function NewCuttingTaskController($scope, $modalInstance, prodDataService) {
    var vm = this;
    var mainWh = [];
    var sum = 0;
    var CUT_CAT = 11;

    vm.addQuan = addQuan;
    vm.checkSum = checkSum;
    vm.cuttingItems = {
        color: setColor,
        sizes: { min: 44, max: 60 },
        items: [],
        sum: countSum
    };
    vm.isGotData = {
        mainWh: false
    };
    vm.minusQuan = minusQuan;
    vm.multiplier = 1;    
    vm.selectRecipe = selectRecipe;
    vm.selectedRecipe = ' ';
    vm.selectedRecipeToString = selectedRecipeToString;
    vm.selectedRecipeResultToString = selectedRecipeResultToString;

    vm.ok = submitTask;
    vm.cancel = function () { $modalInstance.dismiss(); }

    init();

    $scope.$watch('vm.cuttingItems.sizes.min', checkSizes);
    $scope.$watch('vm.cuttingItems.sizes.max', checkSizes);

    function init() {
        function checkData() {
            if ($scope.$parent.isGotData.mainWh == true) {
                mainWh = $scope.$parent.mainWhData;                
                vm.isGotData.mainWh = $scope.$parent.isGotData.mainWh;                

                clearInterval(timer);
                prodDataService.getCuttingRecipes().then(function (d) { vm.recipes = d; })
            }
        }
        var timer = setInterval(checkData, 200);        
    }

    function addQuan(i) {        
        if (sum < vm.selectedRecipe.ResultQuantity * vm.multiplier) {
            i.quantity = parseInt(i.quantity) + 1;
        }        
    }
    function minusQuan(i) {
        if (i.quantity > 0) {
            i.quantity -= 1;
        }
    }    
    function checkSizes() {
        function fillCuttingItems() {
            vm.cuttingItems.items = [];
            for (var i = vm.cuttingItems.sizes.min; i <= vm.cuttingItems.sizes.max; i += 2) {
                vm.cuttingItems.items.push({ size: i, quantity: 0 });
            }
        }
        var val = vm.cuttingItems.sizes;        
        if (val.min <= val.max) {
            fillCuttingItems();            
        } else {
            vm.cuttingItems.items = [];
        }
    }
    function checkValid() {
        if (sum == vm.selectedRecipe.ResultQuantity * vm.multiplier) {
            return true;
        } else {
            return false;
        }
    }
    function checkSum() {
        if (sum > vm.selectedRecipe.ResultQuantity * vm.multiplier) {
            return "label-danger";
        } else {
            if (sum == vm.selectedRecipe.ResultQuantity * vm.multiplier) {
                return "label-success";
            }
            return "label-warning";
        }
    }
    function countSum() {
        var s = 0;        
        for (var i = 0; i < vm.cuttingItems.items.length; i++) {
            s = s + parseInt(vm.cuttingItems.items[i].quantity);            
        }
        sum = s;
        return s;
    }
    function getItemByCat(id, cat) {
        var result;
        var array = mainWh;

        for (var i = 0; i < array[cat].length; i++) {
            if (array[cat][i].Id == id) {
                result = array[cat][i];
            }
        }
        return result || ' ';
    }
    function setColor() {
        return getItemByCat(vm.selectedRecipe.ResultItemId, CUT_CAT-1).Color;
    }
    function submitTask() {
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
            this.TaskCuts = [];
        }
        function Cut(mNumb, name, size, color, quant, taskId, isCompl) {
            this.ModelNumber = mNumb;
            this.Name = name,
            this.Size = size,
            this.Color = color,
            this.Quantity = quant,
            this. TaskId = taskId,
            this.isComplete = isCompl
        }
        
        var items = [];
        var task = {};

        if (checkValid()) {
            for (var i = 0; i < vm.cuttingItems.items.length; i++) {
                items.push(
                    new Cut(vm.selectedRecipe.ResultName.slice(0, 3),
                    vm.selectedRecipe.ResultName,
                    vm.cuttingItems.items[i].size,
                    vm.cuttingItems.color(),
                    vm.cuttingItems.items[i].quantity * vm.multiplier,
                    null,
                    0
                    ));
            }

            task = new ProductionTask(
                null,
                2,
                vm.selectedRecipe.ResultItemId,
                vm.selectedRecipe.ResultQuantity * vm.multiplier,
                0,
                new Date(),
                new Date(1),
                11
                );

            task.TaskCuts = items;
            prodDataService.postCuttingTask(task);
            $modalInstance.close();
        }        
    }    
    function selectRecipe(r) {
        vm.selectedRecipe = r;        
        console.log(setColor());
    }
    function selectedRecipeToString() {
        var string;

        if (vm.selectedRecipe == ' ') {
            return ' ';
        }

        return string = [
            vm.selectedRecipe.Name,
            vm.selectedRecipe.ResultQuantity * vm.multiplier,
            vm.selectedRecipe.UnitsOfMeasurement
        ].join(' ');
    }
    function selectedRecipeResultToString() {
        var string;

        if (vm.selectedRecipe == ' ') {
            return ' ';
        }
        var ri = vm.selectedRecipe.RecipeItems[0];

        var item = getItemByCat(
            ri.ItemId,
            ri.ItemCategory-1);

        return string = [
            ri.Name,
            ri.Quantity * vm.multiplier,
            ri.UnitsOfMeasurement,
            "/",
            item.Quantity,            
            item.UnitOfMeasurement
        ].join(' ');
    }
}
