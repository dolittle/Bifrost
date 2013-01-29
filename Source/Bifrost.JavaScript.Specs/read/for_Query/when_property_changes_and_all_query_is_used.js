describe("when property changes", function () {
    var queryExecuted = null;
    var executeCalled = false;
    var data = [{ something: "Hello" }, { something: "World"}];
    var queryServiceMock = {
        execute: function (query) {
            return {
                continueWith: function () {
                }
            };
        }
    };
    var query = Bifrost.read.Query.extend(function () {
        this.someInteger = ko.observable(0);
        this.someString = ko.observable("something");
    });



    var instance = query.create({
        queryService: queryServiceMock
    });

    var all = instance.all();

    queryServiceMock.execute = function (query) {
        executeCalled = true;
        queryExecuted = query;
        return {
            continueWith: function (callback) {
                callback(data);
            }
        };
    };

    instance.someInteger(42);
    instance.someString("something else");

    it("should call execute on the query service", function () {
        expect(executeCalled).toBe(true);
    });

    it("should forward the query instance to the query service", function () {
        expect(queryExecuted).toBe(instance);
    });

    it("should populate the all observable with the data from the service", function () {
        expect(all()).toBe(data);
    });
});