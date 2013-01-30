describe("when creating from path", function () {
    var viewBefore;
    var view = null;
    var promise = null;

    beforeEach(function () {
        viewBefore = Bifrost.views.View;

        Bifrost.views.View = {
            create: function () {
                var view = {}
                view.load = function (path) {
                    view.path = path;
                    return {
                        continueWith: function (callback) {
                            callback(view);
                        }
                    }
                };

                return view;
            }
        }

        var viewFactory = Bifrost.views.viewFactory.create();
        promise = viewFactory.createFrom("somePath");
        promise.continueWith(function (instance) {
            view = instance;
        });
    });

    afterEach(function () {
        Bifrost.views.View = viewBefore;
    });

    it("should return a promise", function () {
        expect(promise instanceof Bifrost.execution.Promise).toBe(true);
    });

    it("should create a new view", function () {
        expect(view).not.toBeNull();
    });

    it("should pass along the path", function () {
        expect(view.path).toBe("somePath");
    });
});