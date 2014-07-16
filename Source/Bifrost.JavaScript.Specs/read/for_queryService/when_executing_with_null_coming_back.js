describe("when executing with null coming back", function() {
    var task = {
        some: "task"
    };
    var tasks = {
        execute: sinon.mock().withArgs(task).returns({
            continueWith: function (callback) {
                callback(null);
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

    var mapper = {};

    var taskFactory = {
        createQuery: function (query, paging) {
            return task;
        }
    };


    var instance = Bifrost.read.queryService.createWithoutScope({
        mapper: mapper,
        taskFactory: taskFactory
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
