describe("when getting parameter values with parameters holding observables", function () {
    var firstValue = ko.observable(42);
    var secondValue = ko.observable(43);

    var queryType = Bifrost.read.Query.extend(function () {
        this.firstObservable = ko.observable(firstValue);
        this.secondObservable = ko.observable(secondValue);
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