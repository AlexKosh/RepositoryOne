var app = angular.module('askDemoData', []);
app.controller('AskDemoController', function ($scope, $http) {

    $scope.isDbEmpty = "ou-la-la!";
    $scope.getDbsEmptyData = function () {
        var result = $http.get('/home/isDbsEmpty').then(function (response) {
            $scope.isDbEmpty = response.data;
        });
    };
    $scope.getDemoWhAndSt = function () {
        $http.get('/home/pplWhAndSt').then(function () {
            $scope.isDbEmpty[0] = false;
            $scope.isDbEmpty[1] = false;
            $scope.checkDbs();
        })
    }
    $scope.delWhAndSt = function () {
        $http.get('/home/delWhAndSt').then(function () {
            $scope.isDbEmpty[0] = true;
            $scope.isDbEmpty[1] = true;
        })
    }

    $scope.getDemoPrices = function () {
        $http.get('/home/pplPrices').then(function () {
            $scope.isDbEmpty[2] = false;            
            $scope.checkDbs();
        })
    }
    $scope.delPrices = function () {
        $http.get('/home/delPrices').then(function () {
            $scope.isDbEmpty[2] = true;
        })
    }

    $scope.getDemoCust = function () {
        $http.get('/home/pplCust').then(function () {
            $scope.isDbEmpty[3] = false;
            $scope.checkDbs();
        })
    }
    $scope.delCust = function () {
        $http.get('/home/delCust').then(function () {
            $scope.isDbEmpty[3] = true;
        })
    }

    $scope.getDemoEmp = function () {
        $http.get('/home/pplEmp').then(function () {
            $scope.isDbEmpty[4] = false;
            $scope.checkDbs();
        })
    }
    $scope.delEmp = function () {
        $http.get('/home/delEmp').then(function () {
            $scope.isDbEmpty[4] = true;
        })
    }
    $scope.getDemoOrders = function () {
        $http.get('/home/pplOrders').then(function () {
            $scope.isDbEmpty[5] = false;
            $scope.checkDbs();
        })
    }
    $scope.delOrders = function () {
        $http.get('/home/delOrders').then(function () {
            $scope.isDbEmpty[5] = true;
        })
    }

    $scope.checkDbs = function () {
        var cond = false;
        for (var i = 0; i < $scope.isDbEmpty.length; i++) {
            if ($scope.isDbEmpty[i] == true) {
                cond = true;
            }
        }
        if (!cond) {
            location.reload();
        }
    };
    $scope.getDemo = function (i) {
        switch (i) {
            case 0:
            case 1: $scope.getDemoWhAndSt();
                break;
            case 2: $scope.getDemoPrices();
                break;
            case 3: $scope.getDemoCust();
                break;
            case 4: $scope.getDemoEmp();
                break;
            case 5: $scope.getDemoOrders();
                break;
            default: alert('default in getDemo()');

        }
    };
    $scope.delDb = function (i) {
        switch (i) {
            case 0:
            case 1: $scope.delWhAndSt();
                break;
            case 2: $scope.delPrices();
                break;
            case 3: $scope.delCust();
                break;
            case 4: $scope.delEmp();
                break;
            case 5: $scope.delOrders();
                break;
            default: alert('default in delDb()');

        }
    }


    $scope.getDbName = function (index) {
        switch (index) {
            case 0: return "Магазин";
                break;
            case 1: return "Склад";
                break;
            case 2: return "Цены";
                break;
            case 3: return "Клиенты";
                break;
            case 4: return "Работники";
                break;
            case 5: return "Заказы";
                break;
            case 6: return "Главный склад";
                break;
            default: return "Что-то новенькое";
        };
    };
    $scope.getTextInfo = function (b) {
        return text = b == true ? "пуста" : "заполнена";
    };
    $scope.getDbsEmptyData();
    console.log('hello asdasd');
});