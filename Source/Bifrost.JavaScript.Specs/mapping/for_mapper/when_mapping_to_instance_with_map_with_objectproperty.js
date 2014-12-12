describe("when mapping to instance with map with objectproperty", function(){
    var data = { innerData: { integer: 42 } };

    var mapStub = {
        canMapProperty: function (property) {
            return property == "innerData";
        },
        mapProperty: function(alreadyMapped, from, to) {
            to.innerData = { mapped: 'yes' };
        }
    };

    var parameters = {
        typeConverters: {
            convertFrom: sinon.stub().returns(42)
        },
        maps: {
            hasMapFor: sinon.stub().returns(true),
            getMapFor: sinon.stub().returns(mapStub)
        }
    };

    var type = Bifrost.Type.extend(function () {
    });

    var mappedInstance = type.create();

    (function becauseOf(){
        var mapper = Bifrost.mapping.mapper.create(parameters);
        mapper.mapToInstance(type, data, mappedInstance);
    })();

    it("should map the objectproperty", function () {
        expect(mappedInstance.innerData.mapped).toBe("yes");
    });
});