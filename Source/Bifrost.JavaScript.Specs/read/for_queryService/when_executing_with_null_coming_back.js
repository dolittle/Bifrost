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

    var result = null;

    instance.execute(query, paging).continueWith(function (data) {
        result = data;
    });

    it("should create a default result with empty items to the promise", function () {
        expect(result).toEqual({
            items: [],
            totalItems: 0
        });
    });
});
