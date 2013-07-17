describe("when executing", function() {
    var query = {
        name: "Its a query",
        generatedFrom: "Something",
        getParameterValues: function() {
            return {
                firstValue: 42,
                secondValue: "43"
            };
        }
    };

    var url = "/Bifrost/Query/Execute?_q=" + query.generatedFrom;
    var postedUrl = null;
    var postedParameters = null;

    var server = {
        post: function (url, parameters) {
            postedUrl = url;
            postedParameters = parameters;

            return {
                continueWith: function () { }
            }
        }
    };

    var readModelMapper = {};


    var instance = Bifrost.read.queryService.createWithoutScope({
        server: server,
        readModelMapper: readModelMapper
    });

    var paging = {
        size: 2,
        number: 5
    };
    
    var promise = instance.execute(query, paging);


    it("should return a promise", function () {
        expect(promise instanceof Bifrost.execution.Promise).toBe(true);
    });

    it("should post to the correct URL", function () {
        expect(postedUrl).toBe(url);
    });

    it("should put the name of query as part of the parameters", function () {
        expect(postedParameters.descriptor.nameOfQuery).toBe(query.name);
    });

    it("should put the generated from as part of the parameters", function () {
        expect(postedParameters.descriptor.generatedFrom).toBe(query.generatedFrom);
    });

    it("should put the first value into the parameters", function () {
        expect(postedParameters.descriptor.parameters.firstValue).toBe(42);
    });

    it("should put the second value into the parameters", function () {
        expect(postedParameters.descriptor.parameters.secondValue).toBe("43");
    });

    it("should include the size from paging", function () {
        expect(postedParameters.paging.size).toBe(2)
    });
    it("should include the number from paging", function () {
        expect(postedParameters.paging.number).toBe(5)
    });
});
