'use strict';

var YangarooApp = YangarooApp || {};

YangarooApp.namespace = function (nsString) {
    var parts = nsString.split('.'),
    parent = YangarooApp,
    i;
    
    if (parts[0] === "YangarooApp") {
        parts = parts.slice(1);
    }
    for (i = 0; i < parts.length; i += 1) {
        if (typeof parent[parts[i]] === "undefined") {
            parent[parts[i]] = {};
        }
        parent = parent[parts[i]];
    }
    return parent;
};

YangarooApp.namespace('YangarooApp.Angular');

YangarooApp.namespace('YangarooApp.Post');

YangarooApp.Angular = (function (global) {
    var app = angular.module('App', ['ngSanitize']);

    app.controller('YangarooCupCakesController', function ($http, $scope, deleteCupCake) {

        $scope.cupCakes = {};
        $scope.current = {};

        global.onload = function() {
            $http.get('/Api/CupCakeApi').success(function(data) {
                $scope.cupCakes = data;
            }).error(function() {
                alert("error");
            });
        };

        $scope.setScreen = function (name, price, datecreated, datelastupdate, id, ingredients) {
            $scope.current.name = name;
            $scope.current.price = price;
            $scope.current.dateTimeCreated = datecreated;
            $scope.current.dateTimeLastModified = datelastupdate;
            $scope.current.id = id;
            $scope.current.ingredients = ingredients;
        };

        $scope.getScreen = function() {
            var result;
            switch ($scope.current.name) {
                case undefined:
                {
                    result = "/Cupcake/RouteCupCakes/" + $scope.cupCakes;
                    break;
                }
                default:
                {
                    result = "/Cupcake/RouteCupCake/" + $scope.cupCakes;
                    break;
                }
            }
            return result;
        };

        $scope.getError = function(error) {
            if (angular.isDefined(error)) {
                if (error.required) {
                    return "Please enter a value";
                }
            }
            return "";
        };

        $scope.DeleteCupCakeScope = function(id){
            deleteCupCake(id);
            global.setTimeout(function () {
                $http.get('/Api/CupCakeApi').success(function (data) {
                    $scope.cupCakes = data;
                }).error(function () {
                    alert("error");
                });
            }, 100);
        };
    })
    .factory("deleteCupCake", function ($http) {
        return function(id) {
            $http.post('/Api/CupCakeApi/Delete', { id: id })
            .success(function() {
                document.location.reload();
            });
        };
    });
}(this));

$(document).ready(function() {
    /* off-canvas sidebar toggle */
    $('[data-toggle=offcanvas]').click(function() {
        $(this).toggleClass('visible-xs text-center');
        $(this).find('i').toggleClass('glyphicon-chevron-right glyphicon-chevron-left');
        $('.row-offcanvas').toggleClass('active');
        $('#lg-menu').toggleClass('hidden-xs').toggleClass('visible-xs');
        $('#xs-menu').toggleClass('visible-xs').toggleClass('hidden-xs');
        $('#btnShow').toggle();
    });
}());

YangarooApp.post = (function () {

    var props = {};
    props.globalIngredients = '';
    props.globalName = '';
    props.globalPrice = 0;
    props.position = [];

    return {
            AddIngredient: function() {
                $('#newCupCakeData').append('<div class="form-group"><label class="col-sm-2 control-label">Name</label><input ng-model="ingredients" class="form-control" name="inputIngName" type="text" onblur="YangarooApp.post.GetIngredient(this);"/></div>');
            },

            GetName: function(elem) {
                props.globalName = elem.value !== undefined ? elem.value : '';
            },

            GetPrice: function(elem) {
                props.globalPrice = elem.value !== undefined ? elem.value : 0;
            },

            GetIngredient: function(elem) {
                var i = elem.value;
                props.globalIngredients += (i !== '' && props.globalIngredients.indexOf(i) === -1) ? i + '.' : '';

                for (var j = 5; j < elem.form.length; j++) {
                    if (elem.form[j].id == elem.id > -1) {
                        props.position.push(j);
                        break;
                    }
                }
            },

            GetProps: function() {
                return props;
            }
    };
}());



