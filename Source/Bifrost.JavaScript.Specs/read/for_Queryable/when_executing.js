describe("when executing", function () {
        var query = {
            areAllParametersSet: function () {
                return true;
            }
        };
        var observable = ko.observableArray();
        var queryService = null;

        var pagingInfoType = null;
        
        var returnValue = null;
        var queryable = null;
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
                        }
                    }
                }
            };

            queryable = Bifrost.read.Queryable.new({
                query: query,
                region: region,
                queryService: queryService,
                targetObservable: observable
            });

            returnValue = queryable.execute();

        });

        afterEach(function () {
            Bifrost.read.PagingInfo = pagingInfoType;
        });


        it("should return itself", function () {
            expect(returnValue).toBe(queryable);
        });

    });