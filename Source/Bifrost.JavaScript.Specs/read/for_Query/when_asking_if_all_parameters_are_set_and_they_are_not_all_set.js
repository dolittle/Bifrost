describe("when asking if all parameters are set and they are not all set", function () {

    var queryType = Bifrost.read.Query.extend(function () {
        var self = this;

        this.firstParameter = ko.observable(1);
        this.secondParameter = ko.observable();
    });

    var instance = queryType.create();
    var result = instance.areAllParametersSet();

    it("should return false", function () {
        expect(result).toBe(false);
    });

});