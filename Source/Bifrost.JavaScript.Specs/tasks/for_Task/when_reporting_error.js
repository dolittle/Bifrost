describe("when reporting error", function () {

    var task = Bifrost.tasks.Task.create();
    task.reportError("something");

    it("should have one error", function () {
        expect(task.errors().length).toBe(1);
    });

    it("should hold the actual error reported", function () {
        expect(task.errors()[0]).toBe("something");
    });
    
});