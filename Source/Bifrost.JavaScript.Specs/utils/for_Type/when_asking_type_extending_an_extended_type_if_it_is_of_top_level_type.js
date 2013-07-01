describe("when asking type extending an extended type if it is of top level type", function () {

    var a = Bifrost.Type.extend(function () { });
    var b = a.extend(function () { });
    var c = b.extend(function () { });

    var result = c.typeOf(a);

    it("should return true", function () {
        expect(result).toBe(true);
    });
});