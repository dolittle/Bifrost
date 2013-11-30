describe("when setting parameters with two observables on query type", function () {

    var queryType = Bifrost.read.Query.extend(function () {
        this.someParameter = ko.observable();
        this.someOtherParameter = ko.observable();
    });

    var queryableFactory = {};
    var region = {};

    var instance = queryType.create({
        queryableFactory: queryableFactory,
        region: region
    });

    instance.setParameters({
        someParameter: 42,
        someOtherParameter: 43
    });

    it("should set some parameter", function () {
        expect(instance.someParameter()).toBe(42);
    });

    it("should set some other parameter", function () {
        expect(instance.someOtherParameter()).toBe(43);
    });
});