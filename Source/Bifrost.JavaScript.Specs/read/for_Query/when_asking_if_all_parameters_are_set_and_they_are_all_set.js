describe("when asking if all parameters are set and they are all set", function () {

    var queryType = Bifrost.read.Query.extend(function () {
        var self = this;

        this.firstParameter = ko.observable(1);
        this.secondParameter = ko.observable(2);
    });

    var queryableFactory = {};
    var region = {};

    var instance = queryType.create({
        queryableFactory: queryableFactory,
        region: region
    });
    var result = instance.areAllParametersSet();

    it("should return true", function () {
        expect(result).toBe(true);
    });

});