var app = angular.module('Josephine', ['ngAnimate', 'ui.bootstrap']);

app.controller('HomeController', function (dataService, $scope, $modal) {

    //=======================================================
    //=========== ------- initials start ------ =============
    //-------------------------------------------------------

    $scope.selected = { Data: [], DataNotations: [] };
    $scope.warehouseData = {};
    $scope.storeData = {};
    $scope.customersData = [];
    $scope.leftTableView = {};
    $scope.rightTableView = {};
    $scope.selectedOrder = { Data: [] };
    $scope.isGotData = {
        store: false,
        warehouse: false,
        select: false,
        orders: false,
        customers: false,
        employees: false,
        prices: false,
        orderSelected: false
    }   
    $scope.tempOrderData = { OrderInfo: [], OrderProduct: [] };
    $scope.tempOrderInfo = {
        ShipmentDateMin: new Date(),
        ShipmentDateMax: new Date(),
        OrderDiscount: 0,
        OrderCost: 0,
        needPack: true
    };
    $scope.isCreatingNewOrder = false;      

    $scope.getLocation = function (v) {
        return dataService.getLocations(v);
    }

    $scope.cl = function (text) {        
        console.log(text);
    }        

    function getStoreData() {
        dataService.getStore().then(function (d) {
            $scope.storeData = d;
            $scope.isGotData.store = true;
        });           
    }
    function getWarehouseData(needToSelect) {
        $scope.warehouseData = dataService.getWarehouse().then(function (d) {            
            if (needToSelect) {
                $scope.leftTableView = $scope.warehouseData = d;
                
            } else {
                $scope.warehouseData = d;
                $scope.isGotData.warehouse = true;
            }
        });
    }
    function getOrdersData() {
        $scope.ordersData = dataService.getOrders().then(function (d) {
            for (var i = 0; i < d.OrderInfo.length; i++) {
                d.OrderInfo[i].ShipmentDateMin = new Date(parseInt(d.OrderInfo[i].ShipmentDateMin.substring(6, 19)));
                d.OrderInfo[i].ShipmentDateMax = new Date(parseInt(d.OrderInfo[i].ShipmentDateMax.substring(6, 19)));
                d.OrderInfo[i].colorOfReadiness = setColorOfReadinessForOrder(d.OrderInfo[i]);
            }
            $scope.ordersData = d;
            
            populateOrdersInfoByDatesArr();
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
        
    $scope.ordersInfoByDates = [[]];

    function populateOrdersInfoByDatesArr() {
        var today = new Date();
        var tempDate;
        var orders = $scope.ordersData.OrderInfo;
        var sorted = $scope.ordersInfoByDates;
        var datesArr = [orders[0].ShipmentDateMax.getDate()];

        for (var i = 0; i < orders.length; i++) {
            orders[i].isCollapsed = true;
            
            for (var j = 0; j < datesArr.length; j++) {
                tempDate = orders[i].ShipmentDateMax.getDate();
                if (tempDate == datesArr[j]) {
                    sorted[j].push(orders[i]);
                    break;
                }
                if (datesArr.length - 1 == j) {
                    sorted.push([orders[i]]);
                    datesArr.push(orders[i].ShipmentDateMax.getDate());
                    break;
                }
            }
        }

        //console.log(sorted);
    }

    function selectProduct(p, array) {

        function Product(p) {
            this.ProductId = p.ProductId;
            this.ModelNumber = p.ModelNumber;
            this.Name = p.Name;
            this.Color = p.Color;
            this.Size = p.Size;
            this.Quantity = p.Quantity;
            this.Price = p.Price;
        }

        function getModelArrByColors(p) {
            for (var i = 0; i < $scope.warehouseData.Data.length; i++) {

                if ($scope.warehouseData.Data[i][0][0].ModelNumber == p.ModelNumber) {

                    for (var j = 0; j < $scope.warehouseData.Data[i][j].length; j++) {
                        if ($scope.warehouseData.Data[i][j][0].Color == p.Color) {
                            //console.log($scope.warehouseData.Data[i][j]);
                            return $scope.warehouseData.Data[i][j];
                        }
                    }
                }
            }
        }

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
            if (!hasOtherModelNumb) {
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
                        //elem[i].Quantity++;
                        break;
                    }
                }
            }

            //console.log('Index of Product: ' + index);
            //console.log('----------------------------');
            return index;
        }

        function populateColorArray(arr) {
            var tempProd;
            var modelArrByColors = getModelArrByColors(p);

            for (var i = 0; i < modelArrByColors.length; i++) {
                tempProd = new Product(modelArrByColors[i]);
                tempProd.Quantity = 0;
                arr.push(tempProd);
            }
        }

        var iM = indexOfModel(array);
        var iC = indexOfColor(array[iM]);
        var iP = indexOfProduct(array[iM][iC]);

        return array[iM][iC][iP];
    }
    //-------------------------------------------------------
    //=========== ------- initials end ------ ===============
    //=======================================================    
    $scope.collapseAndSelect = function (order) {        
        
        var productsInThisOrder = [];
        $scope.selectedOrder = { Data: [], OrderId: null };
                
        for (var i = 0; i < $scope.ordersData.OrderProduct.length; i++) {
            if (order.OrderId == $scope.ordersData.OrderProduct[i].OrderId) {
                productsInThisOrder.push($scope.ordersData.OrderProduct[i]);
            }
        }

        for (var i = 0; i < productsInThisOrder.length; i++) {
            var prod = selectProduct(productsInThisOrder[i], $scope.selectedOrder.Data);
            prod.Quantity = productsInThisOrder[i].Quantity;
        }

        var isAllOrdersCollapsed = true;

        for (var i = 0; i < $scope.ordersData.OrderInfo.length; i++) {
            
            if ($scope.ordersData.OrderInfo[i].OrderId == order.OrderId) {
                $scope.ordersData.OrderInfo[i].isCollapsed = !($scope.ordersData.OrderInfo[i].isCollapsed);
            } else {
                $scope.ordersData.OrderInfo[i].isCollapsed = true;
            }
            if ($scope.ordersData.OrderInfo[i].isCollapsed == false) {
                isAllOrdersCollapsed = false;
            }
        }        

        if (isAllOrdersCollapsed == true) {
            $scope.selectedOrder = { Data: [], OrderId: null };
            $scope.isGotData.orderSelected = false;
        } else {            
            $scope.isGotData.orderSelected = true;
            $scope.selectedOrder.OrderId = order.OrderId;
        }

        $scope.rightTableView = $scope.selectedOrder;
        $scope.isSelectedRight = 'ordSel';
    }
        
    //add product to selected[]
    $scope.selectProductToOrder = function (p, modelArrByColors) {
        
        function findPrice(modelNumber) {
            for (var i = ($scope.pricesData.length - 1) ; i >= 0 ; i--) {
                if ($scope.pricesData[i].ModelNumber == modelNumber) {
                    var pr = $scope.pricesData[i].Price;
                    break;
                }
            }
            return pr;
        }
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
               
        var productToOrder = selectProduct(p, $scope.selected.Data);

        productToOrder.Quantity++;
        p.Quantity--;
        $scope.isGotData.select = true;
        $scope.rightTableView = $scope.selected;
        $scope.isSelectedRight = 'sel';
        recountOrderCost();        
    }

    $scope.getCustomerById = function (id) {
        for (var i = 0; i < $scope.customersData.length; i++) {
            if ($scope.customersData[i].CustomerId == id) {
                return $scope.customersData[i];
            }
        }
        return null;
    }
        
    $scope.getCustomerNameAndSurname = function(id){
        var c = $scope.getCustomerById(id);
        var text = c.Name + ' ' + c.Surname;
        return text;
    }
    $scope.getCustomerBalance = function (id) {
        var c = $scope.getCustomerById(id);
        return c.Balance;
    }
    $scope.getPaymentAmount = function (ord) {
        return (ord.OrderCost - ord.Paid - ord.OrderDiscount - $scope.getCustomerBalance(ord.CustomerId));
    }
    $scope.shippingMethodToString = function (m) {
        switch (m) {
            case '1': return 'Самовывоз';
            case '2': return 'Автобус';
            case '3': return 'Новая Почта';
            case '4': return 'Автолюкс';
            case '5': return 'Другое';
            default: return 'Нет информации';
        }
    }
    $scope.setOrderPacked = function (ord) {
        if (ord.isPacked == 'упакован') {
            ord.isPacked = 'не упакован';
        } else {
            ord.isPacked = 'упакован';
        }
        ord.colorOfReadiness = setColorOfReadinessForOrder(ord);
    }
    $scope.setOrderDelivered = function (ord) {
        if (ord.isDelivered == 'доставлен') {
            switch (ord.ShippingMethod) {
                case '1': ord.isDelivered = 'заберут';
                    break;
                case '2':
                case '3':
                case '4': ord.isDelivered = 'отнести';
                    break;
                case '5': ord.isDelivered = 'нет информации';
                    break;
                default: ord.isDelivered = 'нет информации';
            }
        } else {
            ord.isDelivered = 'доставлен';
        }
        ord.colorOfReadiness = setColorOfReadinessForOrder(ord);
    }
    $scope.setOrderResolved = function (ord) {
        ord.isResolved = !ord.isResolved;
        ord.colorOfReadiness = setColorOfReadinessForOrder(ord);
    }
    $scope.setOrderIsPaid = function (ord) {
        if (ord.isPaid == 'оплачен' || ord.isPaid == 'оплата на месте') {
            ord.isPaid = 'не оплачен';
        } else {
            ord.isPaid = 'оплачен';
        }
        ord.colorOfReadiness = setColorOfReadinessForOrder(ord);
    }
    function setColorOfReadinessForOrder(ord) {
        if (ord.isPacked == 'не упакован') {
            return 'alert-danger';
        }
        if ((ord.isPacked == 'без упаковки' || ord.isPacked == 'упакован')
            && (ord.isDelivered == 'отнести' || ord.isDelivered == 'заберут')
            && ord.isPaid == 'не оплачен') {
            return 'alert-warning';
        }
        if ((ord.isPacked == 'без упаковки' || ord.isPacked == 'упакован')
            && ord.isDelivered == 'отнести'
            && (ord.isPaid == 'оплачен' || ord.isPaid == 'оплата на месте')) {
            return 'alert-success';
        }
        if ((ord.isPacked == 'без упаковки' || ord.isPacked == 'упакован')
            && (ord.isDelivered == 'доставлен' || ord.isDelivered == 'заберут')
            && ord.isPaid == 'оплачен') {
            return 'alert-success resolved';
        }
    }

    //post, sell this order
    $scope.sell = function () {
        function OrderInfo(oi) {
            this.OrderId = oi.OrderId || null;
            this.EmployeeId = oi.EmployeeId || null;
            this.CustomerId = oi.CustomerId || null;

            this.ShippingMethod = oi.ShippingMethod;
            this.ShipFrom = oi.ShipFrom;
            this.ShippingToCity = oi.ShippingToCity;
            this.ShipAddress = oi.ShipAddress;

            this.OrderDate = oi.OrderDate || new Date();
            this.ShipmentDateMin = oi.ShipmentDateMin;
            this.ShipmentDateMax = oi.ShipmentDateMax;

            this.OrderNotation = oi.OrderNotation;
            this.Priority = oi.Priority;

            this.PaymentMethod = oi.PaymentMethod;
            this.OrderRecievingCode = oi.OrderRecievingCode;
            this.Paid = oi.Paid;
            this.OrderDiscount = oi.OrderDiscount;
            this.OrderCost = oi.OrderCost;

            this.isResolved = false;
            this.isPaid = null;
            this.isDelivered = null;
            this.needPack = oi.needPack;
            this.isPacked = needPackOrNot();

            function needPackOrNot() {
                if (oi.needPack) {
                    return 'не упакован';
                } else {
                    return 'без упаковки';
                }
            }
        }
        function OrdProduct(op) {
            this.OrderId = null;
            this.ProductId = op.ProductId;
            this.ModelNumber = op.ModelNumber;
            this.Color = op.Color;
            this.Size = op.Size;
            this.Quantity = op.Quantity;
            this.ProductPrice = op.Price;
            this.ProductDiscount = null;
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
        
        function fillIsDelivered(ordInf) {
            switch (ordInf.ShippingMethod) {
                case '1': return 'заберут';
                case '2':
                case '3':
                case '4': return 'отнести';
                case '5': return 'нет информации';
                default: return 'нет информации';
            }
        }
        function fillIsPaid(ordInf) {
            if (ordInf.PaymentMethod == 'Водитель') {
                return 'оплата на месте';
            } else {
                return 'не оплачен';
            }
        }

        $scope.tempOrderData.OrderProduct = minificationSelectedArr($scope.selected);

        var tO = new OrderInfo($scope.tempOrderInfo);
        tO.EmployeeId = $scope.tempEmployee.EmployeeId;
        tO.CustomerId = $scope.tempCustomer.CustomerId;
        
        tO.isDelivered = fillIsDelivered(tO);        
        tO.isPaid = fillIsPaid(tO);
        //console.log(tO);
        
        $scope.tempOrderData.OrderInfo.push(tO);

        console.log('BeforeSend');
        console.log($scope.tempOrderData);

        dataService.postOrder($scope.tempOrderData);
        
        //$scope.isGotData.select = false;
        //$scope.selected = { Data: [], DataNotations: [] };
    }   

    //modals for creating new customer or employee
    $scope.openCreateClient = function () {
        var modalInstance = $modal.open({
            templateUrl: 'createCustomerModal',
            controller: 'ModalNewCustomerController',
            scope: $scope
        });
    };
    $scope.openCreateWorker = function () {
        var modalInstance = $modal.open({
            templateUrl: 'createWorkerModal',
            controller: 'ModalNewEmployeeController',
            scope: $scope
        });        
    };
    $scope.openAddProductToWh = function () {
        var modalInstance = $modal.open({
            templateUrl: 'addProductToWh',
            controller: 'ModalAddProductToWhController',
            scope: $scope
        });
        
        modalInstance.result.then(function () {
            getWarehouseData(true);            
            console.log('good');
        });
    };

    //get all datas
    getStoreData();
    getWarehouseData(false);    
    $scope.rightTableView = $scope.selected;
    getOrdersData();
    getCustomersData();
    getEmployeesData();
    getPrices();
    
});

app.controller('ModalNewCustomerController', function (dataService, $modalInstance, $scope, $rootScope) {
    
    function Customer(c) {
        this.CustomerId = null;
        this.Name = c.Name;
        this.Surname = c.Surname;
        this.Nickname = c.Nickname;
        this.Gender = c.Gender;
        this.Birthday = c.Birthday;
        this.Age = 0;

        this.FirstMet = new Date();

        this.Notations = c.Notations;
        this.locationOfTrade = c.locationOfTrade;
        this.PhoneNumber = c.PhoneNumber;
        this.AlternatePhone = c.AlternatePhone;
        this.Email = c.Email;
        this.Skype = c.Skype;
        this.OtherContact = c.OtherContact;

        this.Speciality = c.Speciality;
        this.lastPurchase = this.FirstMet;
        this.isInformed = true;
        this.Balance = 0;

        this.MoneySpent = 0;
        this.Level = 1;    
    };    

    $scope.createNewCust = function () {        
        var cust = new Customer($scope.tempCustomer);
        dataService.postNewCust(cust, $scope.$parent);        
        $modalInstance.close();
    };  
    
    $scope.cancel = function () {        
        $modalInstance.dismiss();
    };
});
app.controller('ModalNewEmployeeController', function (dataService, $modalInstance, $scope) {
    function Employee(e) {
        this.EmployeeId = null;
        this.Name = e.Name;
        this.Surname = e.Surname;
        this.HireDate = new Date();
        this.Birthday = e.Birthday;
        this.Address = e.Address;
        this.PhoneNumber = e.PhoneNumber;
        this.AlternatePhone = e.AlternatePhone;
        this.Skype = e.Skype;
        this.SocialNetwork = e.SocialNetwork;
        this.Email = e.Email;
        this.Speciality = e.Speciality;
        this.Description = e.Description;
        this.Gender = e.Gender;
    }

    //$scope.tempWorker = {};

    $scope.createNewEmpl = function () {
        var emp = new Employee($scope.tempWorker);
        dataService.postNewEmpl(emp, $scope.$parent);
        $modalInstance.close();
    };

    $scope.cancel = function () {
        $modalInstance.dismiss();
    };
});
app.controller('ModalAddProductToWhController', function (dataService, $modalInstance, $scope) {    
    $scope.modelArr = $scope.$parent.warehouseData.DataNotations;
    $scope.mNumbers = Object.keys($scope.modelArr);
    $scope.add = function () {
        dataService.postNewProd($scope);        
        $modalInstance.close();
    }
    $scope.cancel = function () {
        $modalInstance.dismiss();
    }
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
        getPrices: function () {
            var promise = $http.get('home/prices').then(function (response) {
                return response.data;
            });
            return promise;
        },
        getLocations: function (val) {
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
                .error(function () { alert('err in post new order'); });
        },
        postNewEmpl: function (data, $scope) {
            $http.post('/home/newEmployee', { d: data })
                .success(function (data) {
                    console.log("-=----=----=-");
                    console.log(data);
                    console.log("-=----=----=-");
                    $scope.tempEmployee = data;
                    $scope.employeesData.push(data);
                })
            .error(function () { alert('err in post new employee'); });
        },
        postNewCust: function (data, $scope) {
            var promise = $http.post('/home/newCustomer', { d: data })
                .success(function (data) {
                    console.log("-=----=----=-");
                    console.log(data);
                    console.log("-=----=----=-");                   
                    $scope.tempCustomer = data;
                    $scope.customersData.push(data);
                })
                .error(function () { alert('err in post new customer'); });
        },
        postNewProd: function ($scope) {
            $http.post('/home/newProduct', {
                d: {
                    ModelNumber: $scope.ModelNumber,
                    Name: $scope.Name,
                    Color: $scope.Color,
                    Size: $scope.Size,
                    Quantity: $scope.Quantity
                }
            });
        }
    }
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
app.directive('navButtons', function () {
    return {
        restrict: 'E',
        templateUrl: '/home/navButtons'
    };
});

