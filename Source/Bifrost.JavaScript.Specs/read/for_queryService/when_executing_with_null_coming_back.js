describe("when executing with null coming back", function() {
    var query = {
        name: "Its a query",
        generatedFrom: "Something",
        getParameterValues: function () { return {}; },
        hasReadModel: function () { return false; }
    };

    var server = {
        post: function () {
            return {
                continueWith: function (callback) {
                    callback(null);
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

    it("should pass an empty array to the promise", function () {
        expect(itemsReceived).toEqual([]);
    });
});
