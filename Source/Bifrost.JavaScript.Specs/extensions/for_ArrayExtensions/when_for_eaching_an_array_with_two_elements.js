describe("when for eaching an array with two elements", function () {

    var originalForEach;
    var expected = ["hello", "world"];
    var result = [];
    var indices = [];
    var arrayPassed = null;
    var actualThis = null;

    beforeEach(function () {
        originalForEach = Array.prototype.forEach;
        Array.prototype.forEach = undefined;
        polyfillForEach();

        result = [];
        indices = [];

        expected.forEach(function (item, index, array) {
            actualThis = this;
            result.push(item);
            indices.push(index);
            arrayPassed = array;
        });
    });

    afterEach(function () {
        Array.prototype.forEach = originalForEach;
    });

    it("should call the callback twice", function () {
        expect(result.length).toBe(2);
    });

    it("should pass the items into the callback", function () {
        expect(result).toEqual(expected);
    });

    it("should pass the correct index in correct order", function () {
        expect(indices).toEqual([0, 1]);
    });

    it("should pass the actual array", function () {
        expect(arrayPassed).toBe(expected);
    });

    it("should have this set to the window", function () {
        expect(actualThis).toBe(window);
    });

});