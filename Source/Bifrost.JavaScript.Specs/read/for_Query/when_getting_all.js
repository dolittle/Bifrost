describe("when getting all", function () {
    var queryExecuted = null;
    var executeCalled = false;
    var data = [{ something: "Hello" }, { something: "World"}];
    var queryServiceMock = {
        execute: function (query) {
            
            executeCalled = true;
            queryExecuted = query;

            return {
                continueWith: function (callback) {
                    callback(data);
                }
            };
        }
    };
    var readModelMapperStub = {
        mapInstance : function(){

        }
    }

    var query = Bifrost.read.Query.extend(function () { });


    var instance = query.create({
        queryService: queryServiceMock,
        readModelMapper : readModelMapperStub
    });

    var all = instance.all();

    it("should return an observable", function () {
        expect(ko.isObservable(all)).toBe(true);
    });

    it("should return an array", function () {
        expect(Bifrost.isArray(all())).toBe(true);
    });

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