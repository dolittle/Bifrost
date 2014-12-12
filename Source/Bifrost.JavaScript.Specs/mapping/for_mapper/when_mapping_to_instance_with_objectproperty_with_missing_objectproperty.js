describe("when mapping to instance with objectproperty with missing objectproperty", function(){
    var data = { innerData: { integer: 42 } };

    var parameters = {
        typeConverters: {
            convertFrom: sinon.stub().returns(42)
        },
        maps: { hasMapFor: sinon.stub().returns(false) }
    };

    var type = Bifrost.Type.extend(function () {
    });

    var mappedInstance = type.create();

    (function becauseOf(){
        var mapper = Bifrost.mapping.mapper.create(parameters);
        mapper.mapToInstance(type, data, mappedInstance);
    })();

    it("should not create the objectproperty", function () {
        expect(mappedInstance.innerData).toBeUndefined();
    });
});