describe("when creating instance with some dependencies specified", function () {
    var ex = undefined;
    var instance = null;

    beforeEach(function () {
        Bifrost.dependencyResolver = {
            getDependenciesFor: function () {
                return ["something","second"];
            },
            resolve: function (namespace, name) {
                if (name == "second") {
                    return "second";
                }
            }
        };

        var myType = Bifrost.Type.extend(function (something,second) {
            this.something = something;
            this.second = second;
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

    it("should resolve the dependency not specified", function () {
        expect(instance.second).toBe("second");
    });
});