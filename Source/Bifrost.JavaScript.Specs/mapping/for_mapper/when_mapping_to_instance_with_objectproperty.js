describe("when mapping to instance with objectproperty", function(){
    var data = { innerData: { integer: 42 } };

    var parameters = {
        typeConverters: {
            convertFrom: sinon.stub().returns(42)
        },
        maps: { hasMapFor: sinon.stub().returns(false) }
    };

    var type = Bifrost.Type.extend(function () {
        this.innerData = { integer: 0 };
    });

    var mappedInstance = type.create();

    (function becauseOf(){
        var mapper = Bifrost.mapping.mapper.create(parameters);
        mapper.mapToInstance(type, data, mappedInstance);
    })();

    it("should set the converted value to the objectproperty", function () {
        expect(mappedInstance.innerData.integer).toBe(42);
    });
});