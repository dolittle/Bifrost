describe("when checking if element has formant with element of type other than one without attributes", function () {

    var format = "something";
    var formatter = null;
    var formatterBefore = null;
    var result;

    beforeEach(function () {
        var element = {
            nodeType: 8
        }

        formatterBefore = Bifrost.values.Formatter;
        Bifrost.values.Formatter = {
            getExtenders: sinon.stub().returns([])
        };

        formatter = Bifrost.values.stringFormatter.createWithoutScope();
        result = formatter.hasFormat(element);
    });

    afterEach(function () {
        Bifrost.values.Formatter = formatterBefore;
    });

    it("should not be considered to have format", function () {
        expect(result).toBe(false);
    });
});