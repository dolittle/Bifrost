describe("when creating asynchronously with defined parameters", function () {

    Bifrost.dependencyResolver = {
        beginResolve: function (namespace, name) {
            var promise = Bifrost.execution.Promise.create();
            promise.signal(name);
            return promise;
        },
        getDependenciesFor: function () {
            return ["options"];
        }
    }

    var type = Bifrost.Type.extend(function (options) {
        this.options = options;
    });


    var result = null;
    type.beginCreate({
        options: {
            value: "Hello"
        }
    }).continueWith(function (parameter, nextPromise) {
        result = parameter;
    });

    it("should pass along the options", function () {
        expect(result.options.value).toBe("Hello");
    });
});