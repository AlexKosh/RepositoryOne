angular
    .module('Jos.store')
    .controller('StoreController', StoreController);

StoreController.$inject = ['$scope', '$modal', 'dataService'];
function StoreController($scope, $modal, dataService) {
    {

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
        $scope.salesData = { Data: [], DataNotations: {} };

        $scope.isGotData = {
            store: false,
            warehouse: false,
            select: false,
            orders: false,
            customers: false,
            employees: false,
            prices: false,
            orderSelected: false,
            sales: false,
            alert: false
        };

        $scope.tempOrderData = { OrderInfo: [], OrderProduct: [] };
        $scope.tempOrderInfo = {
            ShipmentDateMin: new Date(),
            ShipmentDateMax: new Date(),
            OrderDiscount: 0,
            OrderCost: 0,
            needPack: true
        };
        $scope.isCreatingNewOrder = false;

        $scope.searchText = [];
        $scope.alertText = "Привет, все хорошо!";
        $scope.alertClass = "alert-success"

        $scope.getLocation = function (v) {
            return dataService.getLocations(v);
        }

        $scope.cl = function (text) {
            console.log(text);
        }

        function getStoreData(needToSelect) {
            $scope.isGotData.store = false;
            dataService.getStore().then(function (d) {
                if (needToSelect) {
                    $scope.leftTableView = $scope.storeData = d;
                } else {
                    $scope.storeData = d;
                }
                $scope.isGotData.store = true;
                getTodaySales();
            });
        }
        function getWarehouseData(needToSelect) {
            $scope.isGotData.warehouse = false;
            $scope.warehouseData = dataService.getWarehouse().then(function (d) {
                if (needToSelect) {
                    $scope.leftTableView = $scope.warehouseData = d;
                } else {
                    $scope.warehouseData = d;
                }
                $scope.isGotData.warehouse = true;
            });
        }
        function getOrdersData() {
            $scope.isGotData.orders = false;
            $scope.ordersData = dataService.getOrders().then(function (d) {

                for (var i = 0; i < d.OrderInfo.length; i++) {
                    d.OrderInfo[i].ShipmentDateMin = new Date(parseInt(d.OrderInfo[i].ShipmentDateMin.substring(6, 19)));
                    d.OrderInfo[i].ShipmentDateMax = new Date(parseInt(d.OrderInfo[i].ShipmentDateMax.substring(6, 19)));
                    d.OrderInfo[i].colorOfReadiness = $scope.setColorOfReadinessForOrder(d.OrderInfo[i]);
                }
                $scope.ordersData = d;

                populateOrdersInfoByDatesArr();
                $scope.isGotData.orders = true;
            });
        }
        function getCustomersData() {
            $scope.isGotData.customers = false;
            $scope.customersData = dataService.getCustomers().then(function (d) {
                for (var i = 0; i < d.length; i++) {
                    d[i].isCollapsed = true;
                }
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
        function getTodaySales() {
            dataService.getTodaySales().success(function (d) {
                $scope.salesData = dataService.formattingByModelNumberAndQuantity(d, $scope.storeData);
                $scope.isGotData.sales = true;
            });
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

        $scope.selectProduct = function (p, array) {

            function Product(p) {
                this.ProductId = p.ProductId;
                this.ModelNumber = p.ModelNumber;
                this.Name = p.Name;
                this.Color = p.Color;
                this.Size = p.Size;
                this.Quantity = p.Quantity;
                this.Price = p.Price;
                this.from = $scope.isSelected;
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

        $scope.setColorOfReadinessForOrder = function (ord) {
            if (ord.isPacked == 'не упакован') {
                return 'panel-danger';
            }
            if ((ord.isPacked == 'без упаковки' || ord.isPacked == 'упакован')
                && (ord.isDelivered == 'отнести' || ord.isDelivered == 'заберут')
                && ord.isPaid == 'не оплачен') {
                return 'panel-warning';
            }
            if ((ord.isPacked == 'без упаковки' || ord.isPacked == 'упакован')
                && (ord.isDelivered == 'отнести' || ord.isDelivered == 'заберут')
                && (ord.isPaid == 'оплачен' || ord.isPaid == 'оплата на месте')) {
                return 'panel-success';
            }
            if ((ord.isPacked == 'без упаковки' || ord.isPacked == 'упакован')
                && ord.isDelivered == 'доставлен'
                && ord.isPaid == 'оплата на месте') {
                return 'panel-success';
            }
            if ((ord.isPacked == 'без упаковки' || ord.isPacked == 'упакован')
                && ord.isDelivered == 'доставлен'
                && ord.isPaid == 'оплачен') {
                return 'panel-success resolved';
            }
        }

        $scope.refreshData = function () {
            switch ($scope.isSelected) {
                case "st":
                    getStoreData(true);
                    break;
                case "wh":
                    getWarehouseData(true);
                    break;
                default:
                    break;
            };
        }
        //add product to selected[]
        $scope.selectProductToOrder = function (p, modelArrByColors) {

            //cheks that the product which selects is from same DB which first selected product
            if ($scope.selected.Data.length > 0) {
                if ($scope.isSelected != $scope.selected.Data[0][0][0].from) {
                    alert('Сначала очистите заказ');
                    return;
                }
            }

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

            var productToOrder = $scope.selectProduct(p, $scope.selected.Data);

            productToOrder.Quantity++;
            p.Quantity--;
            $scope.isGotData.select = true;
            $scope.rightTableView = $scope.selected;
            $scope.isSelectedRight = 'sel';
            recountOrderCost();
        };
        $scope.moveProductFromSelected = function (p) {
            if (p.Quantity == 0) {
                return;
            }

            var from;
            if (p.from == 'st') {
                from = $scope.storeData.Data;
            } else {
                from = $scope.warehouseData.Data;
            }
            var c = 0;
            for (var i = 0; i < from.length; i++) {
                if (p.ModelNumber != from[i][0][0].ModelNumber) {
                    continue;
                }
                for (var j = 0; j < from[i].length; j++) {
                    if (p.Color != from[i][j][0].Color) {
                        continue;
                    }
                    for (var k = 0; k < from[i][j].length; k++) {
                        if (p.ProductId == from[i][j][k].ProductId) {
                            from[i][j][k].Quantity += 1;
                            p.Quantity -= 1;
                            return;
                        }
                    }
                }
            }
        };
        //[post] sell or make an order on production from selected[]
        $scope.sell = function (sold) {
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
            function SoldProduct(sp) {
                this.SaleId = null;
                this.OrderId = sp.OrderId;
                this.ProductId = sp.ProductId;
                this.CustomerId = tOi.CustomerId;

                this.SaleDate = new Date();

                this.ModelNumber = sp.ModelNumber;
                this.Color = sp.Color;
                this.Size = sp.Size;
                this.Quantity = sp.Quantity;
                this.ProductPrice = sp.ProductPrice;
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

            $scope.tempOrderData = { OrderInfo: [], OrderProduct: [] };
            $scope.tempOrderData.OrderProduct = minificationSelectedArr($scope.selected);

            var tOi = new OrderInfo($scope.tempOrderInfo);
            tOi.EmployeeId = $scope.tempEmployee.EmployeeId;
            tOi.CustomerId = $scope.tempCustomer.CustomerId;

            tOi.isDelivered = fillIsDelivered(tOi);
            tOi.isPaid = fillIsPaid(tOi);

            $scope.tempOrderData.OrderInfo.push(tOi);

            if (sold) {
                console.log(1);
                $scope.isGotData.alert = true;
                console.log(2);
                $scope.alertText = "Продано";
                $scope.alertClass = "alert-success";
                console.log(3);
                $scope.isCreatingNewOrder = !$scope.isCreatingNewOrder;
                console.log(4);
                dataService.postSales($scope.tempOrderData);
                console.log(5);
            } else {
                console.log(1);
                $scope.isGotData.alert = true;
                console.log(2);
                $scope.alertText = "Заказ принят";
                $scope.alertClass = "alert-success";
                console.log(3);
                $scope.isCreatingNewOrder = !$scope.isCreatingNewOrder;
                console.log(4);
                dataService.postOrder($scope.tempOrderData);
                console.log(5);
            }
        };

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
            });
        };

        //get all datas from server
        getStoreData(false);
        getWarehouseData(false);
        $scope.rightTableView = $scope.selected;
        getOrdersData();
        getCustomersData();
        getEmployeesData();
        getPrices();
    }
}