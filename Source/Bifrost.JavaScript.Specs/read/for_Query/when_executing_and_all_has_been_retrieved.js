describe("when executing and all has been retrieved", function () {


    var instance = Bifrost.read.Query.create({
        queryService: {
            execute: function () {
                return {
                    continueWith: function () {
                    }
                }
            }
        },
        readModelMapper : {
            mapDataToReadModel: function () {}
        }
    });

    var all = instance.all();

    all.execute = sinon.stub();
    instance.execute();

    it("should call execute on the all query", function () {
        expect(all.execute.called).toBe(true);
    });
});