var app = angular.module('Josephine', []);

app.controller('HomeController', function (dataService, $scope) {
    
    $scope.orderData = { Data: [], DataNotations: []};

    $scope.cl = function (text) {        
        console.log(text);
    }    

    $scope.orderProduct = function (p) {
        
        function haveColorInArr(elem, index, array) {
            return elem[0].Color == p.Color;
            };

        var haveModel = false;
        var haveColor = false;

        mainLoop:
            for (var m = 0; m < $scope.orderData.Data.length; m++)
            {
                if ($scope.orderData.Data[m][0][0].ModelNumber == p.ModelNumber) {
                    //we have model in order
                    haveModel = true;
                    for (var c = 0; c < $scope.orderData.Data[m].length; c++) {
                        //console.log($scope.orderData.Data[m].some(haveColorInArr));
                        if ($scope.orderData.Data[m][c][0].Color == p.Color) {
                            //we have model and color
                            haveColor = true;
                            if ($scope.orderData.Data[m][c].indexOf(p) == -1) {
                                //we have model and color, but dont have this size
                                var tempP = p;
                                tempP.Quantity++;
                                $scope.orderData.Data[m][c].push(tempP);                                
                            } else {
                                //we have model and color and size
                                $scope.orderData.Data[m][c][$scope.orderData.Data[m][c].indexOf(p)].Quantity++;
                            }                            
                            //console.log($scope.orderData.Data[m][c]);
                            p.Quantity--;
                            break mainLoop;
                        }
                        if (!$scope.orderData.Data[m].some(haveColorInArr)) {
                            //we have model, but dont have a color
                            haveColor = true;
                            var tempColArr = [];
                            var tempP = p;
                            tempP.Quantity++;
                            tempColArr.push(tempP);
                            $scope.orderData.Data[m].push(tempColArr);
                            p.Quantity--;
                            break mainLoop;
                        }
                    }
                }                
                haveColor = false;
            }
        if (!haveModel) {
            //we dont have model
            var nestedTempArr = [];
            var tempArr = [];
            var tempP = p;
            tempP.Quantity++;
            nestedTempArr.push(tempP);
            tempArr.push(nestedTempArr);
            $scope.orderData.Data.push(tempArr);
            p.Quantity--;            
        }

        if ($scope.orderData.Data.length == 0) {
            //we dont have any arrays
            var nestedTempArr = [];
            var tempArr = [];
            var tempP = p;
            tempP.Quantity++;
            nestedTempArr.push(tempP);
            tempArr.push(nestedTempArr);
            $scope.orderData.Data.push(tempArr);
            
            p.Quantity--;
        }
        console.log($scope.orderData.Data);

    }    

    $scope.getData = function () {
        $scope.response = dataService.get().then(function (d) { $scope.response = d });
              
    }
    $scope.makeKeys = function () {
        console.log(Object.keys($scope.response.DataNotations));
    }
    
});

app.factory('dataService', function ($http) {
    return {
        get: function () {
            var promise = $http.get('/home/products').then(function (response) {
                console.log('In factory: ' + response.data);
                return response.data;
            });
            return promise;
        }
    };
});