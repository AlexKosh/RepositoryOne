var app = angular.module('Josephine', []);

app.controller('HomeController', function (dataService, $scope) {
    
    $scope.orderData = { Data: [], DataNotations: []};

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
        indexOfProduct($scope.orderData.Data[iM][iC]);
        
        p.Quantity--;        
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