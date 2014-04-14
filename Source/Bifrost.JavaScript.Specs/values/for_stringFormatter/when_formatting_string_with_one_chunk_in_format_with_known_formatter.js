describe("when formatting string with one chunk in format with known formatter", function () {

    var format = "something {cool}";
    var formatter = null;
    var formatterBefore = null;
    var result;
    var value = new Date();

    var dateFormatter = {
        supportedType: Date,
        format: sinon.stub().returns("FORMATTED")
    };
    var dateFormatterType = {
        create: sinon.stub().returns(dateFormatter)
    };

    beforeEach(function () {
        var element = {
            nodeType: 1,
            attributes: {
                getNamedItem: sinon.stub().returns({ value: format })
            }
        }

        formatterBefore = Bifrost.values.Formatter;
        Bifrost.values.Formatter = {
            getExtenders: sinon.stub().returns([dateFormatterType])
        };

        formatter = Bifrost.values.stringFormatter.createWithoutScope();
        result = formatter.format(element, value);
    });

    afterEach(function () {
        Bifrost.values.Formatter = formatterBefore;
    });

    it("should format the chunk", function () {
        expect(dateFormatter.format.calledWith(value, "cool")).toBe(true);
    });

    it("should replace chunk with formatted value", function () {
        expect(result).toBe("something FORMATTED");
    });
});