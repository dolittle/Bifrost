describe("when creating a new instance using new", function () {
    var options = {
        query: {
            areAllParametersSet: sinon.stub().returns(false)
        },
        queryService: {},
    };
    var instance = Bifrost.read.Queryable.new(options);
    var queryable = Bifrost.read.Queryable.create(options);

    it("should return an instance", function () {
        expect(instance).toBeDefined();
    });

    it("should return an instance that is knockout observable", function () {
        expect(ko.isObservable(instance)).toBe(true);
    });

    it("should extend with queryable properties", function () {
        for (var property in queryable) {
            expect(instance.hasOwnProperty(property)).toBe(true);
        }
    });

    it("should call execute on the queryable", function () {
        expect(options.query.areAllParametersSet.called).toBe(true);
    });
});