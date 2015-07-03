var app = angular.module('Josephine', ['ngAnimate', 'ui.bootstrap']);

app.controller('HomeController', function (dataService, $scope, $modal) {
    
    $scope.selected = { Data: [], DataNotations: [] };
    $scope.warehouseData = {};
    $scope.storeData = {};
    $scope.customersData = [];
    $scope.leftTableView = {};
    $scope.rightTableView = {};
    $scope.isGotData = {
        store: false,
        warehouse: false,
        select: false,
        orders: false,
        customers: false,
        employees: false,
        prices: false
    }   
    $scope.tempOrderData = { OrderInfo: [], OrderProduct: [] };
    $scope.tempOrderInfo = {
        ShipmentDateMin: new Date(),
        ShipmentDateMax: new Date(),
        OrderDiscount: 0,
        OrderCost: 0
    };
    $scope.isCreatingNewOrder = false;

    $scope.getLocation = function (v) {
        return dataService.getLocations(v);
    }

    $scope.cl = function (text) {        
        console.log(text);
    }    

    $scope.selectProduct = function (p, modelArrByColors) {
          
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
            //console.log('Index of ColorArr: ' + index);
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
               
            //console.log('Index of Color: ' + index);
            return index;
        }

        function indexOfProduct(elem) {
            index = elem ? elem.length : 0;
            //console.log('Index of ProductArr: ' + index);
            if (index > 0) {
                for (var i = 0; i < elem.length; i++) {
                    if (elem[i].ProductId == p.ProductId) {
                        index = i;                        
                        elem[i].Quantity++;
                        break;
                    }
                }
            } 
            
            //console.log('Index of Product: ' + index);
            //console.log('----------------------------');
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

        function findPrice(modelNumber) {            
            for (var i = ($scope.pricesData.length - 1); i >= 0 ; i--) {
                if ($scope.pricesData[i].ModelNumber == modelNumber) {                    
                    var pr = $scope.pricesData[i].Price;
                    break;
                }
            }
            return pr;
        }
                
        var iM = indexOfModel($scope.selected.Data);
        var iC = indexOfColor($scope.selected.Data[iM]);
        indexOfProduct($scope.selected.Data[iM][iC]);
        
        p.Quantity--;
        $scope.isGotData.select = true;
        recountOrderCost();

        function recountOrderCost() {
            $scope.tempOrderInfo.OrderCost = 0;
            for (var i = 0; i < $scope.selected.Data.length; i++) {
                for (var j = 0; j < $scope.selected.Data[i].length; j++) {
                    for (var k = 0; k < $scope.selected.Data[i][j].length; k++) {
                        if ($scope.selected.Data[i][j][k].Quantity > 0) {
                            var p = findPrice($scope.selected.Data[i][j][k].ModelNumber);
                            $scope.selected.Data[i][j][k].Price = p;
                            $scope.tempOrderInfo.OrderCost += (p * $scope.selected.Data[i][j][k].Quantity);                            
                        }
                    }
                }
            }
        }
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
    function getPrices() {
        $scope.pricesData = dataService.getPrices().then(function (d) {
            $scope.pricesData = d;
            $scope.isGotData.prices = true;
        })
    }

    function minificationSelectedArr(arr) {
        var retArr = [];

        for (var i = 0; i < arr.Data.length; i++) {
            for (var j = 0; j < arr.Data[i].length; j++) {
                for (var k = 0; k < arr.Data[i][j].length; k++) {
                    if (arr.Data[i][j][k].Quantity > 0) {
                        var toP = new OrdProduct(arr.Data[i][j][k]);
                        retArr.push(toP);
                        toP = null;
                    }
                }
            }
        }
        return retArr;
    }
    //post
    $scope.sell = function () {
        
        $scope.tempOrderData.OrderProduct = minificationSelectedArr($scope.selected);

        var tO = new OrderInfo($scope.tempOrderInfo);
        $scope.tempOrderData.OrderInfo.push(tO);

        console.log('BeforeSend');
        console.log($scope.tempOrderData);

        dataService.postOrder($scope.tempOrderData);
        
        //$scope.isGotData.select = false;
        //$scope.selected = { Data: [], DataNotations: [] };
    }

    function Product(p) {
        this.ProductId = p.ProductId;
        this.ModelNumber = p.ModelNumber;
        this.Name = p.Name;
        this.Color = p.Color;
        this.Size = p.Size;
        this.Quantity = p.Quantity;
        this.Price = p.Price;
    }
    function OrderInfo(oi) {
        this.OrderId = null;
        this.EmployeeId = null;
        this.CustomerId = null;

        this.ShippingMethod = oi.ShippingMethod;
        this.ShipFrom = oi.ShipFrom;
        this.ShippingToCity = oi.ShippingToCity;
        this.ShipAddress = oi.ShipAddress;

        this.OrderDate = new Date();
        this.ShipmentDateMin = oi.ShipmentDateMin;
        this.ShipmentDateMax = oi.ShipmentDateMax;
                       
        this.OrderNotation = oi.OrderNotation;      
        this.Priority = oi.Priority;

        this.PaymentMethod = oi.PaymentMethod;
        this.OrderRecievingCode = oi.OrderRecievingCode;
        this.Paid = oi.Paid;
        this.OrderDiscount = oi.OrderDiscount;
        this.OrderCost = oi.OrderCost;
    }
    function OrdProduct(op) {
        this.OrderId = null;
        this.ProductId = op.ProductId;
        this.Quantity = op.Quantity;
        this.ProductPrice = op.Price;
        this.ProductDiscount = null;
    }

    $scope.openCreateClient = function () {
        var modalInstance = $modal.open({
            templateUrl: 'createCustomerModal'
        });
    };
    $scope.openCreateWorker= function () {
        var modalInstance = $modal.open({
            templateUrl: 'createWorkerModal'
        });
    };

    getStoreData();
    getWarehouseData();    
    $scope.rightTableView = $scope.selected;
    getOrdersData();
    getCustomersData();
    getEmployeesData();
    getPrices();

});

app.factory('dataService', function ($http) {
    return {
        getStore: function () {
            var promise = $http.get('/home/store').then(function (response) {
                //console.log('In factory: ' + response.data);
                return response.data;
            });
            return promise;
        },
        getWarehouse: function () {
            var promise = $http.get('/home/warehouse').then(function (response) {
                //console.log('In factory: ' + response.data);
                return response.data;
            });
            return promise;
        },
        getOrders: function () {
            var promise = $http.get('/home/orders').then(function (response) {
                //console.log('In factory: ' + response.data);
                return response.data;
            });
            return promise;
        },
        getCustomers: function () {
            var promise = $http.get('/home/customers').then(function (response) {
                //console.log('In factory: ' + response.data);
                return response.data;
            });
            return promise;
        },
        getEmployees: function () {
            var promise = $http.get('/home/employees').then(function (response) {
                //console.log('In factory: ' + response.data);
                return response.data;
            });
            return promise;
        },
        getPrices: function(){
            var promise = $http.get('home/prices').then(function (response) {
                return response.data;
            });
            return promise;
        },
        getLocations: function(val){
            return $http.get('http://maps.googleapis.com/maps/api/geocode/json', {
                params: {
                    address: val,
                    sensor: false
                }
            }).then(function (response) {
                return response.data.results.map(function (item) {
                    return item.formatted_address;
                });
            });
        },
        postOrder: function (data) {
            $http.post('/home/processOrder', { d: data })
                .success(function (data) {
                    console.log("-=----=----=-");
                    console.log(data);
                    console.log("-=----=----=-");
                })
                .error(function () { alert('err'); });
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
app.directive('createOrder', function () {
    return {
        restrict: 'E',
        templateUrl: '/home/createOrder'
    };
});

