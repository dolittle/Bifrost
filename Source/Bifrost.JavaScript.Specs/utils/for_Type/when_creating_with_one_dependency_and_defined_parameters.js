describe("when creating with one dependency and defined parameters", function () {

    Bifrost.dependencyResolver = {
        resolve: function (namespace, name) {
            if (name == "first") {
                return "first";
            }
            return null;
        },
        getDependenciesFor: function () {
            return ["first", "options"];
        }
    }

    var type = Bifrost.Type.extend(function (first, options) {
        this.something = "Hello";
        this.first = first;
        this.options = options;
    });


    var result = type.create({
        options: {
            value: "Hello"
        }
    });

    it("should resolve the first dependency", function () {
        expect(result.first).toBe("first");
    });
    
    it("should pass along the options", function () {
        expect(result.options.value).toBe("Hello");
    });
});