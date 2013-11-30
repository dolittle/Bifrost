describe("when getting parameter values", function () {
    var queryType = Bifrost.read.Query.extend(function () {
        this.firstObservable = ko.observable(42);
        this.secondObservable = ko.observable(43);
        this.nonObservable = "";
    });

    var queryableFactory = {};
    var region = {};

    var instance = queryType.create({
        queryableFactory: queryableFactory,
        region: region
    });
    var parameters = instance.getParameterValues();

    it("should include the first observable", function () {
        expect(parameters.firstObservable).toBe(42);
    });

    it("should include the second observable", function () {
        expect(parameters.secondObservable).toBe(43);
    });

    it("should not include the non observable", function () {
        expect(parameters.nonObservable).not.toBeDefined();
    });

});