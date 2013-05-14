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

    var readModelType = Bifrost.Type.extend(function () {
        var self = this;
        this.something = "";
    });

    var mappedData = data.map(function( value, index){
                var readModel = readModelType.create();
                readModel.something = value.something;
                return readModel;
            });
    var readModelMapperStub = {
        mapDataToReadModel : function(){
            return mappedData;
        }
    }
    var query = Bifrost.read.Query.extend(function () {
        this.someInteger = ko.observable(0);
        this.someString = ko.observable("something");
    });



    var instance = query.create({
        queryService: queryServiceMock,
        readModelMapper : readModelMapperStub

    });

    instance.target.readModel = readModelType;


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
        expect(all()).toBe(mappedData);
    });
});