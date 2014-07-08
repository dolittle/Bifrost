
describe("when executing task that fails", function () {
    var error = "Some error";
    var task = {
        isExecuting: ko.observable(false),
        execute: sinon.stub().returns({
            continueWith: function (callback) {
                return this;
            },
            onFail: function (callback) {
                callback(error);
                return this;
            }

        })
    };

    var taskHistoryId = "someId";
    var taskHistory = {
        begin: sinon.mock().withArgs(task).returns(taskHistoryId),
        failed: sinon.mock().withArgs(taskHistoryId, error)
    };

    var tasks = Bifrost.tasks.Tasks.create({ taskHistory: taskHistory });

    var isBusyTimeline = [];

    tasks.isBusy.subscribe(function (newValue) {
        isBusyTimeline.push(newValue);
    });

    var onFailMock = sinon.mock().withArgs(error);
    tasks.execute(task).onFail(onFailMock);

    it("should not hold the task unfiltered", function () {
        expect(tasks.unfiltered().length).toBe(0);
    });

    it("should add the task to errors", function () {
        expect(tasks.errors()[0]).toBe(task);
    });

    it("should remove the task when failed", function () {
        expect(tasks.all().length).toBe(0);
    });

    it("should execute the task", function () {
        expect(task.execute.called).toBe(true);
    });

    it("should begin the task history", function () {
        expect(taskHistory.begin.called).toBe(true);
    });

    it("should fail the task in history", function () {
        expect(taskHistory.failed.called).toBe(true);
    });

    it("should set the busy flag to true first", function () {
        expect(isBusyTimeline[0]).toBe(true);
    });

    it("should set the busy flag to false last", function () {
        expect(isBusyTimeline[1]).toBe(false);
    });

    it("should fail the promise with the error", function () {
        expect(onFailMock.called).toBe(true);
    });
});