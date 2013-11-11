describe("when adding mapping", function () {
    var mapping = {
        something: 42
    };
    
    var stringMappingFactory = {
        create: sinon.mock().withArgs("Something", "else").once().returns(mapping)
    };

    var mapper = Bifrost.StringMapper.create({
        stringMappingFactory: stringMappingFactory
    });
    mapper.addMapping("Something", "else");

    it("should create a new string mapping passing along the format and mapped format", function () {
        expect(stringMappingFactory.create.called).toBe(true);
    });

    it("should add the mapping to itself", function () {
        expect(mapper.mappings[0]).toBe(mapping);
    });
});