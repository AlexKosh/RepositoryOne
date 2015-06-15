var app = angular.module('Josephine', []);

app.controller('HomeController', function (dataService, $scope) {
    
    $scope.orderData = { Data: [], DataNotations: []};

    $scope.cl = function (text) {        
        console.log(text);
    }    

    $scope.orderProduct = function (p, modelArrByColors) {
        
        function haveColorInArr(elem, index, array) {
            return elem[0].Color == p.Color;
        };
        function hasModel(elem){
            return elem[0][0].ModelNumber == p.ModelNumber;
        }
        function indexOfModel(elem) {            
            var index = elem ? elem.length : 0;
            if (index > 0) {
                for (var i = 0; i < elem.length; i++) {
                    if (elem[i][0][0].ModelNumber == p.ModelNumber) {
                        index = i;
                        break;
                    }
                }
            } else {
                //creates array of colors of this model
                elem[index] = [];
                //creates array of products of this color
                elem[index][0] = [];
                //populates array of products
                populateColorArray(elem[0][0]);
            }            
            console.log(1);            
            return index;
        }

        function indexOfColor(elem) {
            index = elem ? elem.length : 0;
            console.log('Index of ColorArr: ' + index);
            if (index > 0) {
                for (var i = 0; i < elem.length; i++) {
                    if (elem[i][0].Color == p.Color) {
                        index = i;
                        break;
                    }
                }
            } else {
                //elem.push(new Array());
            }
            console.log(2);            
            return index;
        }

        function indexOfProduct(elem) {
            index = elem ? elem.length : 0;
            console.log('Index of ProductArr: ' + index);
            if (index > 0) {
                for (var i = 0; i < elem.length; i++) {
                    if (elem[i].Id == p.Id) {
                        index = i;
                        break;
                    }
                }
            } else {
                //populateColorArray(elem);
            }
            console.log(3);
            console.log('Index of Product: ' + index);
            return index;
        }

        function populateColorArray(arr) {
            var notations = $scope.response.DataNotations[p.ModelNumber].Sizes;
            var tempProd;
            for (var i = 0; i < modelArrByColors.length; i++) {
                tempProd = new Product(modelArrByColors[i]);
                tempProd.Quantity = 0;
                arr.push(tempProd);
            }
        }
                
        var iM = indexOfModel($scope.orderData.Data);
        var iC = indexOfColor($scope.orderData.Data[iM]);
        var iP = indexOfProduct($scope.orderData.Data[iM][iC]);
        orderedProduct = $scope.orderData.Data[iM][iC][iP].Quantity++;
        p.Quantity--;
        //console.log(orderedProduct);        

        //var haveModel = false;
        //var haveColor = false;

        //mainLoop:
        //    for (var m = 0; m < $scope.orderData.Data.length; m++)
        //    {
        //        if ($scope.orderData.Data[m][0][0].ModelNumber == p.ModelNumber) {
        //            //we have model in order
        //            haveModel = true;
        //            for (var c = 0; c < $scope.orderData.Data[m].length; c++) {
        //                //console.log($scope.orderData.Data[m].some(haveColorInArr));
        //                if ($scope.orderData.Data[m][c][0].Color == p.Color) {
        //                    //we have model and color
        //                    haveColor = true;
        //                    if ($scope.orderData.Data[m][c].indexOf(p) == -1) {
        //                        console.log($scope.orderData.Data[m][c].indexOf(p.Size));
        //                        //we have model and color, but dont have this size
        //                        var tempP = new Product(p);
        //                        tempP.Quantity++;
        //                        $scope.orderData.Data[m][c].push(tempP);                                
        //                    } else {
        //                        //we have model and color and size
        //                        $scope.orderData.Data[m][c][$scope.orderData.Data[m][c].indexOf(p)].Quantity++;
        //                    }                            
        //                    //console.log($scope.orderData.Data[m][c]);
        //                    p.Quantity--;
        //                    break mainLoop;
        //                }
        //                if (!$scope.orderData.Data[m].some(haveColorInArr)) {
        //                    //we have model, but dont have a color
        //                    haveColor = true;
        //                    var tempColArr = [];
        //                    var tempP = new Product(p);
        //                    tempP.Quantity++;
        //                    tempColArr.push(tempP);
        //                    $scope.orderData.Data[m].push(tempColArr);
        //                    p.Quantity--;
        //                    break mainLoop;
        //                }
        //            }
        //        }                
        //        haveColor = false;
        //    }
        //if (!haveModel) {
        //    //we dont have model
        //    var nestedTempArr = [];
        //    var tempArr = [];
        //    var tempP = new Product(p);
        //    tempP.Quantity++;
        //    nestedTempArr.push(tempP);
        //    tempArr.push(nestedTempArr);
        //    $scope.orderData.Data.push(tempArr);
        //    p.Quantity--;            
        //}

        //if ($scope.orderData.Data.length == 0) {
        //    //we dont have any arrays
        //    var nestedTempArr = [];
        //    var tempArr = [];
        //    var tempP = new Product(p);
        //    tempP.Quantity++;
        //    nestedTempArr.push(tempP);
        //    tempArr.push(nestedTempArr);
        //    $scope.orderData.Data.push(tempArr);
            
        //    p.Quantity--;
        //}
        //console.log($scope.orderData.Data);

    }    

    $scope.getData = function () {
        $scope.response = dataService.get().then(function (d) { $scope.response = d });              
    }

    function Product(p) {
        this.Id = p.Id;
        this.ModelNumber = p.ModelNumber;
        this.Name = p.Name;
        this.Color = p.Color;
        this.Size = p.Size;
        this.Quantity = p.Quantity;
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