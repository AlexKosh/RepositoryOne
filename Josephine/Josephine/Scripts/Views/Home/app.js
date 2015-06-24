var app = angular.module('Josephine', ['ngAnimate']);

app.controller('HomeController', function (dataService, $scope) {
    
    $scope.orderData = { Data: [], DataNotations: [] };
    $scope.warehouseData = {};
    $scope.storeData = {};
    $scope.leftTableView = {};
    $scope.rightTableView = {};
    $scope.isGotData = {
        store: false,
        warehouse: false,
        order: false,
        orders: false,
        customers: false,
        employees: false
    }

    $scope.swi = function () {
        $scope.isGotData.store = false;
    }
    $scope.unswi = function () {
        $scope.isGotData.store = true;
    }
    

    $scope.cl = function (text) {        
        console.log(text);
    }    

    $scope.orderProduct = function (p, modelArrByColors) {
          
        function indexOfModel(elem) {            
            var index = elem ? elem.length : 0;
            //console.log('Index of ModelArr: ' + index);
            var hasOtherModelNumb = false;

            if (index > 0) {
                
                for (var i = 0; i < elem.length; i++) {
                    if (elem[i][0][0].ModelNumber == p.ModelNumber) {
                        index = i;
                        hasOtherModelNumb = true;
                        break;
                    }
                }
            }
            // this block will work if the array of models has some models but has no required model
            // or this array has no model
            if (!hasOtherModelNumb)
            {
                //creates array of colors of this model
                elem[index] = [];
                //creates array of products of this color
                elem[index][0] = [];
                //populates array of products
                populateColorArray(elem[index][0]);
            }            
            
            //console.log('Index of Model: ' + index);
            return index;
        }

        function indexOfColor(elem) {
            index = elem ? elem.length : 0;
            console.log('Index of ColorArr: ' + index);
            var hasOtherColor = false;
            if (index > 0) {
                for (var i = 0; i < elem.length; i++) {
                    if (elem[i][0].Color == p.Color) {
                        index = i;
                        hasOtherColor = true;
                        break;
                    }
                }
            }
            if (!hasOtherColor) {
                elem[index] = [];
                populateColorArray(elem[index]);
            }           
               
            console.log('Index of Color: ' + index);
            return index;
        }

        function indexOfProduct(elem) {
            index = elem ? elem.length : 0;
            console.log('Index of ProductArr: ' + index);
            if (index > 0) {
                for (var i = 0; i < elem.length; i++) {
                    if (elem[i].Id == p.Id) {
                        index = i;                        
                        elem[i].Quantity++;
                        break;
                    }
                }
            } 
            
            console.log('Index of Product: ' + index);
            console.log('----------------------------');
            return index;
        }

        function populateColorArray(arr) {
            var notations = $scope.storeData.DataNotations[p.ModelNumber].Sizes;
            var tempProd;
            for (var i = 0; i < modelArrByColors.length; i++) {
                tempProd = new Product(modelArrByColors[i]);
                tempProd.Quantity = 0;
                arr.push(tempProd);
            }
        }
                
        var iM = indexOfModel($scope.orderData.Data);
        var iC = indexOfColor($scope.orderData.Data[iM]);
        indexOfProduct($scope.orderData.Data[iM][iC]);
        
        p.Quantity--;
        $scope.isGotData.order = true;
    }

    function getStoreData() {
        dataService.getStore().then(function (d) {
            $scope.storeData = d;
            $scope.isGotData.store = true;
        });           
    }
    function getWarehouseData() {
        $scope.warehouseData = dataService.getWarehouse().then(function (d) {
            $scope.warehouseData = d;
            $scope.isGotData.warehouse = true;
        });
    }
    function getOrdersData() {
        $scope.ordersData = dataService.getOrders().then(function (d) {
            $scope.ordersData = d;
            $scope.isGotData.orders = true;
        });
    }
    function getCustomersData() {
        $scope.customersData = dataService.getCustomers().then(function (d) {
            $scope.customersData = d;
            $scope.isGotData.customers = true;
        });
    }
    function getEmployeesData() {
        $scope.employeesData = dataService.getEmployees().then(function (d) {
            $scope.employeesData = d;
            $scope.isGotData.employees = true;
        });
    }

    function Product(p) {
        this.Id = p.Id;
        this.ModelNumber = p.ModelNumber;
        this.Name = p.Name;
        this.Color = p.Color;
        this.Size = p.Size;
        this.Quantity = p.Quantity;
        this.Price = p.Price;
    }

    getStoreData();
    getWarehouseData();    
    $scope.rightTableView = $scope.orderData;
    getOrdersData();
    getCustomersData();
    getEmployeesData();

});

app.factory('dataService', function ($http) {
    return {
        getStore: function () {
            var promise = $http.get('/home/store').then(function (response) {
                console.log('In factory: ' + response.data);
                return response.data;
            });
            return promise;
        },
        getWarehouse: function () {
            var promise = $http.get('/home/warehouse').then(function (response) {
                console.log('In factory: ' + response.data);
                return response.data;
            });
            return promise;
        },
        getOrders: function () {
            var promise = $http.get('/home/orders').then(function (response) {
                console.log('In factory: ' + response.data);
                return response.data;
            });
            return promise;
        },
        getCustomers: function () {
            var promise = $http.get('/home/customers').then(function (response) {
                console.log('In factory: ' + response.data);
                return response.data;
            });
            return promise;
        },
        getEmployees: function () {
            var promise = $http.get('/home/employees').then(function (response) {
                console.log('In factory: ' + response.data);
                return response.data;
            });
            return promise;
        }
    };
});

app.directive('leftTable', function () {
    return {
        restrict: 'E',
        templateUrl: '/home/leftTable'
    };
});
app.directive('rightTable', function () {
    return {
        restrict: 'E',
        templateUrl: '/home/rightTable'
    };
});