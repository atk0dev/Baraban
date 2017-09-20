// moderate-index.js
var module = angular.module("moderateIndex", []);

module.config(["$routeProvider", function ($routeProvider) {
    $routeProvider.when("/", {
        controller: "groupsController",
        templateUrl: "/templates/groupsView.html"
    });
    
    $routeProvider.when("/newgroup", {
        controller: "newGroupController",
        templateUrl: "/templates/newGroupView.html"
    });
    
    $routeProvider.when("/group/:id", {
        controller: "singleGroupController",
        templateUrl: "/templates/singleGroupView.html"
    });
    
    $routeProvider.when("/group/:id/delete", {
        controller: "deleteGroupController",
        templateUrl: "/templates/groupsView.html"
    });
    
    $routeProvider.when("/group/:id/newitem", {
        controller: "newItemController",
        templateUrl: "/templates/newItemView.html"
    });
    
    $routeProvider.when("/item/:id/delete", {
        controller: "deleteItemController",
        templateUrl: "/templates/singleGroupView.html"
    });

    $routeProvider.when("/group/:gid/edit/:iid", {
        controller: "singleItemController",
        templateUrl: "/templates/singleItemView.html"
    });
    
    $routeProvider.when("/item/:id/newdetail", {
        controller: "newDetailController",
        templateUrl: "/templates/newDetailView.html"
    });
    
    $routeProvider.when("/detail/:id/delete", {
        controller: "deleteDetailController",
        templateUrl: "/templates/singleItemView.html"
    });

    $routeProvider.otherwise({ redirectTo: "/" });
}]);

module.factory("dataService", ["$http", "$q", function ($http, $q) {
    
    var _groups = [];
    var _currItems = [];
    var _isInit = false;
    
    var _isReady = function () {
        return _isInit;
    };


    var _getGroups = function () {

        var deferred = $q.defer();

        $http.get("/api/v1/groups?includeItems=true")
          .then(function (result) {
              console.log(result);
              // Successful
              angular.copy(result.data, _groups);
              _isInit = true;
              //console.log(_groups);
              deferred.resolve();
          },
          function () {
              // Error
              deferred.reject();
          });

        return deferred.promise;
    };

    var _getItemsByGroup = function (id) {
        var deferred = $q.defer();

        $http.get("/api/v1/items/" + id)
          .then(function (result) {
              console.log(result);
              // Successful
              angular.copy(result.data, _currItems);
              _isInit = true;
              deferred.resolve();
          },
          function () {
              // Error
              deferred.reject();
          });

        return deferred.promise;
    };

    var _addGroup = function (newGroup) {
        var deferred = $q.defer();

        $http.post("/api/v1/groups", newGroup)
         .then(function (result) {
             // success
             var newlyCreatedGroup = result.data;
             _groups.splice(0, 0, newlyCreatedGroup);
             deferred.resolve(newlyCreatedGroup);
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };

    function _findGroup(id) {
        var found = null;

        $.each(_groups, function (i, item) {
            if (item.id == id) {
                found = item;
                return false;
            }
        });

        return found;
    }
    
    function _findItemInGroup(id) {
        var found = null;

        $.each(_currItems, function (i, item) {
            if (item.id == id) {
                found = item;
                return false;
            }
        });
        
        return found;
    }

    var _getGroupById = function (id) {
        var deferred = $q.defer();

        if (_isReady()) {
            var group = _findGroup(id);
            if (group) {
                deferred.resolve(group);
            } else {
                deferred.reject();
            }
        } else {
            _getGroups()
              .then(function () {
                  // success
                  var group = _findGroup(id);
                  if (group) {
                      deferred.resolve(group);
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
    
    var _getItemById = function (groupId, itemId) {
        var deferred = $q.defer();

           _getItemsByGroup(groupId)
              .then(function () {
                  // success
                  
                      var item = _findItemInGroup(itemId);
                      if (item) {
                          deferred.resolve(item);
                  
                  } else {
                      deferred.reject();
                  }
              },
              function () {
                  // error
                  deferred.reject();
              });
        

        return deferred.promise;
    };
    
    var _editGroup = function (updGroup) {
        var deferred = $q.defer();

        $http.put("/api/v1/groups", updGroup)
         .then(function (result) {
             // success
             var updatedGroup = result.data;
             //_groups.splice(0, 0, newlyCreatedGroup);
             deferred.resolve(updatedGroup);
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };
    
    var _editItem = function (updItem) {
        var deferred = $q.defer();

        $http.put("/api/v1/items", updItem)
         .then(function (result) {
             // success
             var updatedItem = result.data;
             deferred.resolve(updatedItem);
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };
    
    var _deleteGroupById = function (id) {
        var deferred = $q.defer();
        $http.delete("/api/v1/groups/" + id)
         .then(function () {
             // success
             var g = _findGroup(id);
             var i = _groups.indexOf(g);
             _groups.splice(i, 1);
             deferred.resolve();
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };

    var _addItem = function (newItem) {
        var deferred = $q.defer();

        $http.post("/api/v1/items", newItem)
         .then(function (result) {
             // success
             console.log(result);
             var newlyCreatedItem = result.data;
             var group = _findGroup(newlyCreatedItem.groupId);
             group.items.splice(0, 0, newlyCreatedItem);
             deferred.resolve(newlyCreatedItem);
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };
    
    var _addDetail = function (newDetail) {
        var deferred = $q.defer();

        $http.post("/api/v1/details", newDetail)
         .then(function (result) {
             // success
             console.log(result);
             var newlyCreatedDetail = result.data;
             //var group = _findGroup(newlyCreatedItem.groupId);
             //group.items.splice(0, 0, newlyCreatedItem);
             deferred.resolve(newlyCreatedDetail);
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };
    
    var _deleteItemById = function (id) {
        var deferred = $q.defer();
        $http.delete("/api/v1/items/" + id)
         .then(function () {
             // success
             // now delete this item in all groups at the client side.
             console.log('groups collection before:');
             console.log(_groups);
             for (var i = 0; i < _groups.length; i++) {
                 var singleGroup = _groups[i];
                 console.log('single group:');
                 console.log(singleGroup);
                 if (singleGroup != null) {
                     console.log('items in the current group:');
                     console.log(singleGroup.items);
                     var itemToDelete = -1;
                     for (var j = 0; j < singleGroup.items.length; j++) {
                         var currentItem = singleGroup.items[j];
                         if (currentItem != null && currentItem.id == id) {
                             console.log('need to delete this item:');
                             console.log(currentItem);
                             itemToDelete = j;
                             break;
                         }
                     }
                     if (itemToDelete != -1) {
                         console.log('need to delete item with id = ' + itemToDelete);
                         singleGroup.items.splice(itemToDelete, 1);
                         console.log('updated items collection:');
                         console.log(singleGroup.items);
                     }
                 }
             }
             //var g = _findGroup(id);
             //var i = _groups.indexOf(g);
             //_groups.splice(i, 1);
             deferred.resolve();
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };
    
    var _deleteDetailById = function (id) {
        var deferred = $q.defer();
        $http.delete("/api/v1/details/" + id)
         .then(function () {
             // success
             deferred.resolve();
         },
         function () {
             // error
             deferred.reject();
         });

        return deferred.promise;
    };


    return {
        groups: _groups,
        isReady: _isReady,
        getGroups: _getGroups,
        addGroup: _addGroup,
        getGroupById: _getGroupById,
        editGroup: _editGroup,
        deleteGroupById: _deleteGroupById,
        addItem: _addItem,
        deleteItemById: _deleteItemById,
        getItemById: _getItemById,
        editItem: _editItem,
        addDetail: _addDetail,
        deleteDetailById: _deleteDetailById
    };
}]);

var groupsController = ["$scope", "$http", "dataService",
  function ($scope, $http, dataService) {
      $scope.data = dataService;
      $scope.isBusy = false;

      if (dataService.isReady() == false) {
          $scope.isBusy = true;

          dataService.getGroups()
            .then(function () {
                // success
            },
            function () {
                // error
                alert("could not load groups");
            })
            .then(function () {
                $scope.isBusy = false;
            });
      }
  }];

var newGroupController = ["$scope", "$http", "$window", "dataService",
  function ($scope, $http, $window, dataService) {
      $scope.newGroup = {};

      $scope.save = function () {

          dataService.addGroup($scope.newGroup)
            .then(function () {
                // success
                $window.location = "#/";
            },
            function () {
                // error
                alert("could not save the new group");
            });

      };
  }];

var singleGroupController = ["$scope", "dataService", "$window", "$routeParams",
  function ($scope, dataService, $window, $routeParams) {
      $scope.group = null;
      //$scope.newReply = {};

      dataService.getGroupById($routeParams.id)
        .then(function (group) {
            // success
            $scope.group = group;
            console.log(group);
        },
        function () {
            // error
            $window.location = "#/";
        });

      $scope.editGroup = function () {
          console.log($scope.group);
              dataService.editGroup($scope.group)
                .then(function () {
                    // success
                },
                function () {
                    // error
                    alert("Could not update group");
                });
      };
  }];


var deleteGroupController = ["$scope", "dataService", "$window", "$routeParams",
  function ($scope, dataService, $window, $routeParams) {
      dataService.deleteGroupById($routeParams.id)
        .then(function () {
            // success
            $window.location = "#/";
        },
        function () {
            // error
            $window.location = "#/";
        });
  }];

var newItemController = ["$scope", "$http", "$window", "dataService", "$routeParams",
  function ($scope, $http, $window, dataService, $routeParams) {
      $scope.newItem = {};

      $scope.save = function () {

          $scope.newItem.groupId = $routeParams.id;
          dataService.addItem($scope.newItem)
            .then(function () {
                // success
                $window.location = "#/group/" + $routeParams.id;
            },
            function () {
                // error
                alert("could not save the new item");
            });

      };
  }];

var deleteItemController = ["$scope", "dataService", "$window", "$routeParams",
  function ($scope, dataService, $window, $routeParams) {
      dataService.deleteItemById($routeParams.id)
        .then(function () {
            // success
            $window.location = "#/";
        },
        function () {
            // error
            $window.location = "#/";
        });
  }];

var singleItemController = ["$scope", "dataService", "$window", "$routeParams",
  function ($scope, dataService, $window, $routeParams) {
      $scope.item = null;

      var groupId = $routeParams.gid;
      var itemId = $routeParams.iid;

      dataService.getItemById(groupId, itemId)
        .then(function (item) {
            // success
            $scope.item = item;
            console.log(item);
        },
        function () {
            // error
            $window.location = "#/";
        });

      $scope.editItem = function () {
          console.log("Updated item:");
          console.log($scope.item);
          dataService.editItem($scope.item)
            .then(function () {
                // success
            },
            function () {
                // error
                alert("Could not update item");
            });
      };
  }];


var newDetailController = ["$scope", "$http", "$window", "dataService", "$routeParams",
  function ($scope, $http, $window, dataService, $routeParams) {
      $scope.newDetail = {};

      $scope.save = function () {

          $scope.newDetail.itemId = $routeParams.id;
          dataService.addDetail($scope.newDetail)
            .then(function () {
                // success
                $window.location = "#/";
            },
            function () {
                // error
                alert("could not save the new detail");
            });

      };
  }];

var deleteDetailController = ["$scope", "dataService", "$window", "$routeParams",
  function ($scope, dataService, $window, $routeParams) {
      dataService.deleteDetailById($routeParams.id)
        .then(function () {
            // success
            $window.location = "#/";
        },
        function () {
            // error
            $window.location = "#/";
        });
  }];
