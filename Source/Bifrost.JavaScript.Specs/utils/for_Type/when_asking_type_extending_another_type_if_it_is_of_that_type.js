describe("when asking type extending another type if it is of that type", function () {

    var a = Bifrost.Type.extend(function () { });
    var b = a.extend(function () { });

    var result = b.typeOf(a);

    it("should return true", function () {
        expect(result).toBe(true);
    });
});