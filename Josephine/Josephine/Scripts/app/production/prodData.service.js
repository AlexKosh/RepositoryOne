(function () {
    'use strict';

    angular
        .module('Jos.production')
        .factory('prodDataService', prodDataService);

    prodDataService.$inject = ['$http'];
    function prodDataService($http) {
        return {
            getMainWh: function () {
                var promise = $http.get('/production/getMainWh').then(function (response) {
                    return response.data;
                });
                return promise;
            },
            getEndProd: function () {
                var promise = $http.get('/home/warehouse').then(function (response) {
                    return response.data;
                });
                return promise;
            },
            getRecipes: function () {
                return $http.get('/production/getRecipes').then(function (response) {
                    return response.data;
                });
            },
            getRecipesCategories: function () {
                return $http.get('/production/getRecipesCategories').then(function (response) {
                    return response.data;
                });
            },
            pplRecipe: function () {
                $http.get('/production/pplRecipe');
            },
            getFirstRcpByCategory: function (category) {
                return $http.post('/production/getFirstRcpByCategory', { cat: category }).then(function (response) {
                    return response.data;
                });
            },
            getTasksForQuilting: function () {
                var promise = $http.get('/production/getTasksForQuilting').then(function (response) {
                    return response.data;
                });
                return promise;
            },
            postMainWhItem: function (data) {
                $http.post('/production/postItem', { d: data })
                .success(function (resp) {
                    console.log('in postMainWhData success');
                })
                .error(function (resp) {
                    console.log('in postMainWhData error');
                    console.log(resp);
                });
            },
            postCut: function (data) {
                $http.post('/production/postCut', { d: data })
                .success(function (resp) {
                    console.log('in postCut success');
                })
                .error(function (resp) {
                    console.log('in postCut error');
                    console.log(resp);
                });
            },
            postProductionTask: function (data) {
                $http.post('/production/postTaskData', { d: data })
                    .success(function (data) {
                        console.log("-=----=Tasks=----=-");
                        console.log(data);
                        console.log('success!');
                        console.log("-=----=----=-");
                    })
                    .error(function () { alert('err in post new task'); });
            },
            postRecipe: function (data) {
                $http.post('/production/postRecipeData', { d: data })
                    .success(function (data) {
                        console.log("-=----=Recipe=----=-");
                        console.log(data);
                        console.log('success!');
                        console.log("-=----=----=-");
                    })
                    .error(function () { alert('err in post new recipe'); });
            }
        }
    }
})();