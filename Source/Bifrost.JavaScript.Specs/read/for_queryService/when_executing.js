describe("when executing", function () {
    var task = {
        some:"task"
    };
    var tasks = {
        execute: sinon.mock().withArgs(task).returns({
            continueWith: function (callback) {
                callback();
            }
        })
    };
    
    var query = {
        name: "Its a query",
        generatedFrom: "Something",
        getParameterValues: function() {
            return {
                firstValue: 42,
                secondValue: "43"
            };
        },
        hasReadModel: function() { return false; },
        region: {
            tasks: tasks
        }
    };
    
    var mapper = {};
    var queryForTask = null;
    var payloadForTask = null;

    var taskFactory = {
        createQuery: function (query, paging) {
            taskFactory.createQuery.called = true;
            queryForTask = query;
            pagingForTask = paging;
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
    
    var promise = instance.execute(query, paging);
    
    it("should create a query task", function () {
        expect(taskFactory.createQuery.called).toBe(true);
    });

    it("should return a promise", function () {
        expect(promise instanceof Bifrost.execution.Promise).toBe(true);
    });

    it("should pass along the query to the task", function () {
        expect(queryForTask).toBe(query);
    });

    it("should pass along the paging to the task", function () {
        expect(pagingForTask).toBe(paging);
    });
});
