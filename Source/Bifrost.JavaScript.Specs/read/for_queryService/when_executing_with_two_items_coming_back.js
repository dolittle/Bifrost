describe("when executing with two items coming back", function() {
    var query = {
        name: "Its a query",
        generatedFrom: "Something",
        getParameterValues: function () { return {}; },
        hasReadModel: function () { return false; }
    };

    var items = [
        { something: 42 },
        { somethingElse: 43 }
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

    var readModelMapper = {};

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

    it("should pass the items to the promise", function () {
        expect(itemsReceived).toBe(items);
    });
});
