describe("when executing with two items coming back for a read model", function() {
    var query = {
        name: "Its a query",
        generatedFrom: "Something",
        getParameterValues: function () { return {}; },
        readModel: "SureIsAReadModel",
        hasReadModel: function () { return true; }
    };

    var items = [
        { something: 42 },
        { somethingElse: 43 }
    ];

    var mappedItems = [
        { somethingElse: 44 },
        { something: 45 }
    ];

    var server = {
        post: function () {
            return {
                continueWith: function (callback) {
                    callback(items);
                }
            }
        }
    };

    var readModelMapper = {
        mapDataToReadModel: function (readModel, data) {
            return mappedItems;
        }
    };

    var instance = Bifrost.read.queryService.createWithoutScope({
        server: server,
        readModelMapper: readModelMapper
    });

    var paging = {
        size: 2,
        number: 5
    };

    var itemsReceived = null;

    instance.execute(query, paging).continueWith(function (data) {
        itemsReceived = data;
    });

    it("should map the items and pass the resultto the promise", function () {
        expect(itemsReceived).toBe(mappedItems);
    });
});
