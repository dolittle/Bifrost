describe("when creating instance with dependencies specified", function () {
    var ex = undefined;
    var instance = null;

    beforeEach(function () {
        Bifrost.dependencyResolver = {
            getDependenciesFor: function () {
                return ["something"];
            }
        };

        var myType = Bifrost.Type.extend(function (something) {
            this.something = something;
        });

        try {
            instance = myType.create({
                something: "Hello"
            });
        } catch (e) {
            ex = e;
        }
    });

    it("should not throw an exception", function () {
        expect(ex === undefined).toBe(true);
    });

    it("should pass instances in when resolving dependencies", function () {
        expect(instance.something).toBe("Hello");
    });
});