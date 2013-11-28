describe("when executing and we get undefined back", function () {
    var query = {
        areAllParametersSet: function () {
            return true;
        }
    };
    var observable = ko.observableArray();
    var queryService = null;

    var pagingInfoType = null;
    var region = {};

    beforeEach(function () {
        pagingInfoType = Bifrost.read.PagingInfo;
        Bifrost.read.PagingInfo = {
            create: function () {
                return {};
            }
        };

        queryService = {
            execute: function () {
                return {
                    continueWith: function (callback) {
                        callback(undefined);
                    }
                }
            }
        };

        var queryable = Bifrost.read.Queryable.create({
            query: query,
            region: region,
            queryService: queryService,
            targetObservable: observable
        });

        queryable.execute();

    });

    afterEach(function () {
        Bifrost.read.PagingInfo = pagingInfoType;
    });


    it("should populate the target observable", function () {
        expect(observable()).toEqual([]);
    });

});