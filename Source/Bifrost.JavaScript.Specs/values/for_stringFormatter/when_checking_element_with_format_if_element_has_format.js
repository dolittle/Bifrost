describe("when checking element with format if element has format", function () {
    var formatter = Bifrost.values.stringFormatter.createWithoutScope();

    var format = "something";
    var element = {
        attributes: {
            getNamedItem: sinon.stub().returns(format)
        }
    }

    var result = formatter.hasFormat(element);
    it("should be considered to have format", function () {
        expect(result).toBe(true);
    });
});