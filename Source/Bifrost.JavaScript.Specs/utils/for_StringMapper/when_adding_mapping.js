describe("when adding mapping", function () {
    var mappings = [{
        something: 42
    }, {
        _else: "fourty two"
    }];
    var mappingIndex=0;

    var format = "Something";
    var mappedFormat = "else";
    
    var stringMappingFactory = {
        create: function () { }
    };

    sinon.stub(stringMappingFactory, "create", function () {
        return mappings[mappingIndex++];
    });

    var mapper = Bifrost.StringMapper.create({
        stringMappingFactory: stringMappingFactory
    });
    mapper.addMapping("Something", "else");

    it("should create a new string mapping passing along the format and mapped format", function () {
        expect(stringMappingFactory.create.calledWith(format, mappedFormat)).toBe(true);
    });

    it("should create a new reverse string mapping passing along the format and mapped format", function () {
        expect(stringMappingFactory.create.calledWith(mappedFormat, format)).toBe(true);
    });

    it("should add the mapping to itself", function () {
        expect(mapper.mappings[0]).toBe(mappings[0]);
    });

    it("should add the mapping to itself", function () {
        expect(mapper.reverseMappings[0]).toBe(mappings[1]);
    });
});