'use strict';

var MYAPP = MYAPP || {};

MYAPP.namespace = function (nsString) {
    var parts = nsString.split('.'),
    parent = MYAPP,
    i;
    
    if (parts[0] === "MYAPP") {
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

MYAPP.namespace('MYAPP.Angular');

MYAPP.Angular = (function (global) {
    var app = angular.module('App', ['ngSanitize']);

    app.controller('E2rmEventsController', function ($http, $scope, insertEvent) { 

        $scope.e2rmEvents = {};

        global.onload = function() {
            $http.get('/AppC/Api').success(function(data) {
                $scope.e2rmEvents = data;
            }).error(function() {
                alert("error");
            });
        };

        $scope.setScreen = function(index) {
            $scope.current = index;
        };

        $scope.getScreen = function() {
            var result;
            switch ($scope.current) {
                case undefined:
                {
                    result = "/AppC/RouteEvents";
                    break;
                }
                default:
                {
                    result = "/AppC/RouteEvent";
                    break;
                }
            }
            return result;
        };

        $scope.InsertEventScope = function (name) {
            if ($scope.newEventForm.$valid) {
                insertEvent(name);
                global.setTimeout(function() {
                    $http.get('/AppC/Api').success(function(data) {
                        $scope.e2rmEvents = data;
                    }).error(function() {
                        alert("error");
                    });
                }, 100);
                $scope.newEventForm.$setValidity("newEventForm", false);
            } 
        };

        $scope.getError = function(error) {
            if (angular.isDefined(error)) {
                if (error.required) {
                    return "Please enter a value";
                }
            }
            return "";
        };
    })
    .factory("insertEvent", function ($http) {
        return function(name) {
            $http.post('/AppC/InsertEvent', { name: name });
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

