describe("MYAPP Angular Tests", function() {
    
    //Arrange 
    var mockScope, controller, backend;

    beforeEach(angular.mock.module("MYAPP.Angular.App"));

    beforeEach(angular.mock.inject(function ($controller, $rootScope) {
        mockScope = $rootScope.$new();
        controller = $controller("E2rmEventsController", {
            $scope: mockScope
        });
    }));

    beforeEach(angular.mock.inject(function ($httpBackend)
    {
        backend = $httpBackend;
        backend.expect('GET', 'test.json').respond(
        [{ "event": "Event1" },
        { "event": "Event2" }]);
    }));

    describe('$http.get with httpBackend', function() {
        beforeEach(inject(function($controller, $rootScope, $http) {

            mockScope = $rootScope.$new();
            $controller("E2rmEventsController", {
                $scope: mockScope,
                $http: $http
            });
            backend.flush();
        }));

        it("Makes an Ajax request", function () {
            backend.verifyNoOutstandingExpectation();
        });

        it("Processes the data", function () {
            expect(mockScope.products).toBeDefined();
            expect(mockScope.products.length).toEqual(2);
        });

        it("Preserves the data order", function () {
            expect(mockScope.products[0].name).toEqual("Event1");
            expect(mockScope.products[1].name).toEqual("Event2");
        });
    });
});
