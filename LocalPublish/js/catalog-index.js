// catalog-index.js
var module = angular.module("catalogIndex", []);

module.config(["$routeProvider", function ($routeProvider) {
    $routeProvider.when("/", {
        controller: "catalogController",
        templateUrl: "/templates/catalogView.html"
    });
    
    $routeProvider.when("/group/:gid", {
        controller: "catalogController",
        templateUrl: "/templates/catalogView.html"
    });
    
    $routeProvider.when("/group/:gid/item/:iid", {
        controller: "itemController",
        templateUrl: "/templates/itemView.html"
    });

    $routeProvider.otherwise({ redirectTo: "/" });
}]);


module.factory("dataService", ["$http", "$q", function ($http, $q) {
    var _items = [];
    var _rows = [];
    var _isInit = false;
    var _currGroup = {};
    
    var _isReady = function () {
        return _isInit;
    };

    
    var _getItems = function (group) {

        var deferred = $q.defer();

        //var url = "/api/v1/items/" + group;
        var url = "/api/v1/catalogdata/" + group;

        console.log('getting items: ' + url);

        $http.get(url)
          .then(function (result) {
              // Successful

              angular.copy(result.data.items, _items);
              angular.copy(result.data.group, _currGroup);
              var matrix = [];
              var rowCount = Math.ceil(result.data.items.length / 3);
              var start = 0;
              for (var i = 0; i < rowCount; i++) {
                  var r = result.data.items.slice(start, start+3);
                  matrix.push(r);
                  start = start + 3;
              }
              angular.copy(matrix, _rows);

              _isInit = false;
              deferred.resolve();
          },
          function () {
              // Error
              deferred.reject();
          });

        return deferred.promise;
    };
    
    var _getItem = function (id, group) {
        var deferred = $q.defer();

        if (_items.length > 0) {
            var item = _findItem(id);
            if (item) {
                deferred.resolve(item);
            } else {
                deferred.reject();
            }
        } else {
            _getItems(group)
              .then(function () {
                  // success
                  var i = _findItem(id);
                  if (i) {
                      deferred.resolve(i);
                  } else {
                      deferred.reject();
                  }
              },
              function () {
                  // error
                  deferred.reject();
              });
        }

        return deferred.promise;
    };
    
    var _getDetails = function (item) {

        var deferred = $q.defer();

        var url = "/api/v1/details/" + item;

        $http.get(url)
          .then(function (result) {
              // Successful

              //angular.copy(result.data, _items);

              //var matrix = [];
              //var rowCount = Math.ceil(result.data.length / 3);
              //var start = 0;
              //for (var i = 0; i < rowCount; i++) {
              //    var r = result.data.slice(start, start + 3);
              //    matrix.push(r);
              //    start = start + 3;
              //}
              //console.log(_rows);
              //angular.copy(matrix, _rows);

              deferred.resolve(result.data);
          },
          function () {
              // Error
              deferred.reject();
          });

        return deferred.promise;
    };

    function _findItem(id) {
        var found = null;

        $.each(_items, function (i, item) {
            if (item.id == id) {
                found = item;
                return false;
            }
        });

        return found;
    }

    return {
        items: _items,
        //item: _item,
        getItems: _getItems,
        getItem: _getItem,
        getDetails: _getDetails,
        isReady: _isReady,
        rows: _rows,
        currGroup: _currGroup
    };
    
}]);

var catalogController = ["$scope", "dataService", "$window", "$routeParams",
  function ($scope, dataService, $window, $routeParams) {
      $scope.data = dataService;
      $scope.isBusy = false;

      var group = 0;

      if ($routeParams != undefined && $routeParams.gid != undefined) {
          group = $routeParams.gid;
      }
      $scope.cg = group;

      if (dataService.isReady() == false) {
          $scope.isBusy = true;

          console.log("group = " + group);
          
          dataService.getItems(group)
            .then(function () {
                // success
                console.log($scope.data);

                console.log($scope.data.currGroup);
            },
            function () {
                // error
                alert("could not load items");
            })
            .then(function () {
                $scope.isBusy = false;
            });
      }
  }];

var itemController = ["$scope", "dataService", "$window", "$routeParams",
  function ($scope, dataService, $window, $routeParams) {
      $scope.item = null;
      $scope.isBusy = false;

      var group = 0;
      var item = 0;

      if ($routeParams != undefined && $routeParams.gid != undefined) {
          group = $routeParams.gid;
      }
      
      if ($routeParams != undefined && $routeParams.iid != undefined) {
          item = $routeParams.iid;
      }

      
          dataService.getItem(item, group)
            .then(function (result) {
                // success
                $scope.item = result;
                $scope.cg = group;
                dataService.getDetails(result.id)
                    .then(function(d) {
                        $scope.details = d;
                    },
                        function () {
                            alert("unable to load details");
                        });
            },
            function () {
                // error
                alert("could not load item");
            })
            .then(function () {
                $scope.isBusy = false;
            });
      
  }];
