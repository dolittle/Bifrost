describe("when getting parameters", function () {
    var queryType = Bifrost.read.Query.extend(function () {
        this.firstObservable = ko.observable();
        this.secondObservable = ko.observable();
        this.nonObservable = "";
    });

    var instance = queryType.create();
    var parameters = instance.getParameters();

    it("should include the first observable", function () {
        expect(parameters.firstObservable).toBeDefined();
    });

    it("should include the second observable", function () {
        expect(parameters.secondObservable).toBeDefined();
    });

    it("should not include the non observable", function () {
        expect(parameters.nonObservable).not.toBeDefined();
    });

    it("should not include areAllParametersSet property", function () {
        expect(parameters.areAllParametersSet).not.toBeDefined();
    });
});