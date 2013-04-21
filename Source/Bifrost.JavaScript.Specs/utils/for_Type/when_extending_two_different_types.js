describe("when extending two different types", function () {
    var firstTypeDefinition = Bifrost.Type.extend(function () { });
    var secondTypeDefinition = Bifrost.Type.extend(function () { });

    it("should have different type identifiers", function () {
        expect(firstTypeDefinition._typeId).not.toBe(secondTypeDefinition._typeId);
    });
});