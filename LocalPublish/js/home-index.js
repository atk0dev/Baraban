// home-index.js
var module = angular.module("homeIndex", []);

module.config(["$routeProvider", function ($routeProvider) {
    $routeProvider.when("/", {
        controller: "homeController",
        templateUrl: "/templates/homeView.html"
    });

    $routeProvider.otherwise({ redirectTo: "/" });
}]);

module.factory("dataService", ["$http", "$q", function ($http, $q) {

    var _isInit = true;

    var _isReady = function () {
        return _isInit;
    };

   
    
    return {
        isReady: _isReady
    };
}]);

var homeController = ["$scope", "$http", "dataService",
  function ($scope, $http, dataService) {
      $scope.data = dataService;
      $scope.isBusy = false;

      
  }];



