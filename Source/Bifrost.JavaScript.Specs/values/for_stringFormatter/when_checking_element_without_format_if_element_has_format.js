describe("when checking element without format if element has format", function () {
    var formatter = null;
    var formatterBefore = null;
    var result;

    beforeEach(function () {
        var element = {
            attributes: {
                getNamedItem: sinon.stub().returns(null)
            }
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

    
    it("should not considered to have format", function () {
        expect(result).toBe(false);
    });
});