angular
    .module('Jos.store')
    .controller('OrdersController', OrdersController);

OrdersController.$inject = ['$scope'];
function OrdersController($scope) {
    {

        $scope.collapseAndSelect = function (order) {

            var productsInThisOrder = [];
            $scope.selectedOrder = { Data: [], OrderId: null };

            for (var i = 0; i < $scope.$parent.ordersData.OrderProduct.length; i++) {
                if (order.OrderId == $scope.$parent.ordersData.OrderProduct[i].OrderId) {
                    productsInThisOrder.push($scope.$parent.ordersData.OrderProduct[i]);
                }
            }

            for (var i = 0; i < productsInThisOrder.length; i++) {
                var prod = $scope.$parent.selectProduct(productsInThisOrder[i], $scope.selectedOrder.Data);
                prod.Quantity = productsInThisOrder[i].Quantity;
            }

            var isAllOrdersCollapsed = true;

            for (var i = 0; i < $scope.$parent.ordersData.OrderInfo.length; i++) {

                if ($scope.$parent.ordersData.OrderInfo[i].OrderId == order.OrderId) {
                    $scope.$parent.ordersData.OrderInfo[i].isCollapsed = !($scope.$parent.ordersData.OrderInfo[i].isCollapsed);
                } else {
                    $scope.$parent.ordersData.OrderInfo[i].isCollapsed = true;
                }
                if ($scope.$parent.ordersData.OrderInfo[i].isCollapsed == false) {
                    isAllOrdersCollapsed = false;
                }
            }

            if (isAllOrdersCollapsed == true) {
                $scope.selectedOrder = { Data: [], OrderId: null };
                $scope.$parent.isGotData.orderSelected = false;
            } else {
                $scope.$parent.isGotData.orderSelected = true;
                //sets header on right table view
                $scope.selectedOrder.OrderId = order.OrderId;
            }

            $scope.$parent.rightTableView = $scope.selectedOrder;
            $scope.$parent.isSelectedRight = 'ordSel';
        }

        $scope.getCustomerById = function (id) {
            for (var i = 0; i < $scope.customersData.length; i++) {
                if ($scope.customersData[i].CustomerId == id) {
                    return $scope.customersData[i];
                }
            }
            return null;
        }
        $scope.getCustomerNameAndSurname = function (id) {
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
                if (ord.isPacked == 'без упаковки') {
                    ord.isPacked = 'без упаковки';
                } else {
                    ord.isPacked = 'упакован';
                }
            }
            ord.colorOfReadiness = $scope.$parent.setColorOfReadinessForOrder(ord);
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
            ord.colorOfReadiness = $scope.$parent.setColorOfReadinessForOrder(ord);
            //var date = new Date(1999, 1);
            //var datenow = new Date().toDateString();
            //datenow = new Date(datenow);
            //console.log(date);
            //console.log(datenow);
            //console.log(date < datenow);
        }
        $scope.setOrderResolved = function (ord) {
            ord.isResolved = !ord.isResolved;
            ord.colorOfReadiness = $scope.$parent.setColorOfReadinessForOrder(ord);
        }
        $scope.setOrderIsPaid = function (ord) {
            if (ord.isPaid == 'оплачен' && ord.PaymentMethod == 'Водитель') {
                ord.isPaid = 'оплата на месте';
            } else {
                if (ord.PaymentMethod == 'Водитель') {
                    ord.isPaid = 'оплачен';
                } else {
                    if (ord.isPaid == 'не оплачен') {
                        ord.isPaid = 'оплачен';
                    } else {
                        ord.isPaid = 'не оплачен';
                    }
                }
            }
            console.log(ord.PaymentMethod);
            ord.colorOfReadiness = $scope.$parent.setColorOfReadinessForOrder(ord);

        }

    }
}