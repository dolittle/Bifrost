describe("when checking element without format if element has format", function () {
    var formatter = Bifrost.values.stringFormatter.createWithoutScope();

    var element = {
        attributes: {
            getNamedItem: sinon.stub().returns(null)
        }
    }

    var result = formatter.hasFormat(element);
    it("should not considered to have format", function () {
        expect(result).toBe(false);
    });
});