describe("when executing", function () {
    var query = {
        name: "Its a query",
        generatedFrom: "Something",
        getParameterValues: function () {
            return {
                firstValue: 42,
                secondValue: "43"
            };
        },
        hasReadModel: function () { return false; },
    };

    var paging = {
        size: 2,
        number: 5
    };

    var urlForTask = null;
    var payloadForTask = null;

    var promise = { some:"promise" };
    var innerTaskExecute = sinon.mock().returns(promise);

    var taskFactory = {
        createHttpPost: function (url, payload) {
            urlForTask = url;
            payloadForTask = payload;
            return {
                execute: innerTaskExecute
            }
        }
    };

    var task = Bifrost.read.QueryTask.create({
        taskFactory: taskFactory,
        query: query,
        paging: paging
    });

    var returnedPromise = task.execute();

    it("should forward execution to inner task", function () {
        expect(innerTaskExecute.called).toBe(true);
    });

    it("should return the promise from the inner task", function () {
        expect(returnedPromise).toBe(promise);
    });

});