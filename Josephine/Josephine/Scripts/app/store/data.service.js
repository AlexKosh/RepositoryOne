(function () {
    //'use strict';

    angular
        .module('Jos.store')
        .factory('dataService', dataService);

    dataService.$inject = ['$http'];
    function dataService($http) {
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
                var promise = $http.get('/home/prices').then(function (response) {
                    return response.data;
                });
                return promise;
            },
            getTodaySales: function () {
                var promise = $http.get('/home/sales').success(function (d) {
                    promise = d;
                }).error(function () { alert('err in getTodaySales'); });
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
                        console.log("-=----=orders=----=-");
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
            },
            postSales: function (data) {
                $http.post('/home/sale', { d: data })
                .success(function (data) {
                    console.log(6);
                    console.log("-=----=sales=----=-");
                    console.log(data);
                    console.log("-=----=----=-");
                })
                .error(function () { alert('err in post new sale'); });
            },
            getSalesData: function ($scope) {
                var promise = $http.post('/home/getSalesData', {
                    minDate: $scope.minDate,
                    maxDate: $scope.maxDate
                });
                return promise;
            },
            formattingByModelNumberAndQuantity: function (d, storeData) {

                function getModelsName(numb, arr) {
                    for (var i = 0; i < arr.length; i++) {
                        if (arr[i][0][0].ModelNumber == numb) {
                            return arr[i][0][0].Name;
                        }
                    }
                };

                var r = { Data: [], DataNotations: { qSum: 0, cSum: 0 } };

                r.Data.push([{
                    ModelNumber: d[0].ModelNumber,
                    Name: getModelsName(d[0].ModelNumber, storeData.Data),
                    Price: d[0].ProductPrice,
                    Quantity: d[0].Quantity
                }]);

                r.DataNotations.qSum += r.Data[0][0].Quantity;
                r.DataNotations.cSum += r.Data[0][0].Price * r.Data[0][0].Quantity;
                //console.log(r);
                //console.log(r.Data[0][0]);

                for (var i = 1; i < d.length; i++) {
                    d[i].SaleDate = d[i].SaleDate.substring(6, 19);
                    r.DataNotations.qSum += d[i].Quantity;
                    r.DataNotations.cSum += d[i].ProductPrice * d[i].Quantity;

                    if (i == 0) {
                        continue;
                    } else {
                        for (var j = 0; j < r.Data.length; j++) {
                            if (r.Data[j][0].ModelNumber == d[i].ModelNumber) {

                                for (var k = 0; k < r.Data[j].length; k++) {
                                    if (r.Data[j][k].Price == d[i].ProductPrice) {
                                        r.Data[j][k].Quantity += d[i].Quantity;
                                        break;
                                    }
                                    if (k == (r.Data[j].length - 1)) {
                                        r.Data[j].push({
                                            ModelNumber: d[i].ModelNumber,
                                            Name: getModelsName(d[i].ModelNumber, storeData.Data),
                                            Price: d[i].ProductPrice,
                                            Quantity: d[i].Quantity
                                        });
                                        break;
                                    }
                                }
                                break;
                            }

                            if (j == (r.Data.length - 1)) {
                                r.Data.push([{
                                    ModelNumber: d[i].ModelNumber,
                                    Name: getModelsName(d[i].ModelNumber, storeData.Data),
                                    Price: d[i].ProductPrice,
                                    Quantity: d[i].Quantity
                                }]);
                                break;
                            }
                        }
                    }
                }
                //console.log(r);
                return r;
            },
            getModelNamesAndNumbsDictionary: function () {
                var promise = $http.get('/home/getModelNames').then(function (response) {

                    return response.data;
                });
                return promise;
            }
        }
    }
})();
