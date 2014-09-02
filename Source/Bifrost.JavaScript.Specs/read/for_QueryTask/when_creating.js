describe("when creating", function () {
    var query = {
        _name: "Its a query",
        _generatedFrom: "Something",
        getParameterValues: function () {
            return {
                firstValue: 42,
                secondValue: "43"
            };
        },
        hasReadModel: function () { return false; },
    };

    var paging = {
        size: 2,
        number: 5
    };

    var urlForTask = null;
    var payloadForTask = null;

    var taskFactory = {
        createHttpPost: function (url, payload) {
            urlForTask = url;
            payloadForTask = payload;
        }
    };

    var task = Bifrost.read.QueryTask.create({
        taskFactory: taskFactory,
        query: query,
        paging: paging
    });

    var expectedUrl = "/Bifrost/Query/Execute?_q=" + query._generatedFrom;

    it("should create an inner task with correct url", function () {
        expect(urlForTask).toBe(expectedUrl);
    });

    it("should put the name of query as part of the parameters", function () {
        expect(payloadForTask.descriptor.nameOfQuery).toBe(query._name);
    });

    it("should put the generated from as part of the parameters", function () {
        expect(payloadForTask.descriptor.generatedFrom).toBe(query._generatedFrom);
    });

    it("should put the first value into the parameters", function () {
        expect(payloadForTask.descriptor.parameters.firstValue).toBe(42);
    });

    it("should put the second value into the parameters", function () {
        expect(payloadForTask.descriptor.parameters.secondValue).toBe("43");
    });

    it("should include the size from paging", function () {
        expect(payloadForTask.paging.size).toBe(2)
    });
    it("should include the number from paging", function () {
        expect(payloadForTask.paging.number).toBe(5)
    });
});