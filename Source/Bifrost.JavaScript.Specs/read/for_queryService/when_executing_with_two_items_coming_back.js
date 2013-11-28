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
        name: "Its a query",
        generatedFrom: "Something",
        getParameterValues: function () { return {}; },
        hasReadModel: function () { return false; },
        region: {
            tasks: tasks
        }
    };


    var payloadWhenCreatingTask = null;
    var taskFactory = {
        createHttpPost: function (url, payload) {
            taskFactory.createHttpPost.called = true;
            payloadWhenCreatingTask = payload;
            return task;
        }
    };

    var readModelMapper = {};

    var instance = Bifrost.read.queryService.createWithoutScope({
        readModelMapper: readModelMapper,
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
