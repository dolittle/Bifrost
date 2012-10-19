describe("when defining", function () {
    Bifrost.ClassInfo = {};
    var typeDefinition = function (something) { };
    var result = Bifrost.Type.define(typeDefinition);
    
    it("should return the type definition", function () {
        expect(result).toBe(typeDefinition);
    });
    
    it("should a create function", function () {
        expect(typeof result.create).toBe("function");
    });
});