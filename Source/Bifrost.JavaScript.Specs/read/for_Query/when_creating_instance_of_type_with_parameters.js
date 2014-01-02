describe("when asking if all parameters are set and they are all set", function () {
    ko.extenders.linked = sinon.stub();

    var firstParameter = ko.observable(1);
    var secondParameter = ko.observable(1);

    var queryType = Bifrost.read.Query.extend(function () {
        var self = this;

        this.firstParameter = firstParameter;
        this.secondParameter = secondParameter;
    });


    var queryableFactory = {};
    var region = {};

    var instance = queryType.create({
        queryableFactory: queryableFactory,
        region: region
    });

    it("should extend the first parameter with linked", function () {
        expect(ko.extenders.linked.calledWith(firstParameter)).toBe(true);
    });

    it("should extend the second parameter with linked", function () {
        expect(ko.extenders.linked.calledWith(secondParameter)).toBe(true);
    });

});