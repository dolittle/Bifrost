describe("when executing with two items coming back", function() {
    var task = {
        some: "task"
    };
    var items = [
        { something: 42 },
        { somethingElse: 43 }
    ];
    var tasks = {
        execute: sinon.mock().withArgs(task).returns({
            continueWith: function (callback) {
                callback(items);
            }
        })
    };
    var query = {
        _name: "Its a query",
        _generatedFrom: "Something",
        getParameterValues: function () { return {}; },
        hasReadModel: function () { return false; },
        region: {
            tasks: tasks
        }
    };

    var taskFactory = {
        createQuery: function (url, payload) {
            return task;
        }
    };

    var mapper = {};

    var instance = Bifrost.read.queryService.createWithoutScope({
        mapper: mapper,
        taskFactory: taskFactory
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
