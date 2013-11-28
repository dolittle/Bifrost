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
    
    var url = "/Bifrost/Query/Execute?_q=" + query.generatedFrom;

    var readModelMapper = {};
    var payloadWhenCreatingTask = null;

    var taskFactory = {
        createHttpPost: function (url, payload) {
            taskFactory.createHttpPost.called = true;
            payloadWhenCreatingTask = payload;
            return task;
        }
    };
    
    var instance = Bifrost.read.queryService.createWithoutScope({
        readModelMapper: readModelMapper,
        taskFactory: taskFactory
    });
   
    var paging = {
        size: 2,
        number: 5
    };
    
    var promise = instance.execute(query, paging);
    
    it("should create a http post task", function () {
        expect(taskFactory.createHttpPost.called).toBe(true);
    });

    it("should return a promise", function () {
        expect(promise instanceof Bifrost.execution.Promise).toBe(true);
    });

    
    it("should put the name of query as part of the parameters", function () {
        expect(payloadWhenCreatingTask.descriptor.nameOfQuery).toBe(query.name);
    });

    it("should put the generated from as part of the parameters", function () {
        expect(payloadWhenCreatingTask.descriptor.generatedFrom).toBe(query.generatedFrom);
    });

    it("should put the first value into the parameters", function () {
        expect(payloadWhenCreatingTask.descriptor.parameters.firstValue).toBe(42);
    });

    it("should put the second value into the parameters", function () {
        expect(payloadWhenCreatingTask.descriptor.parameters.secondValue).toBe("43");
    });

    it("should include the size from paging", function () {
        expect(payloadWhenCreatingTask.paging.size).toBe(2)
    });
    it("should include the number from paging", function () {
        expect(payloadWhenCreatingTask.paging.number).toBe(5)
    });
});
