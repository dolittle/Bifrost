Bifrost.namespace("Bifrost.views", {
    viewManager: Bifrost.Singleton(function (viewLocationMapper, viewFactory) {
        var self = this;

        this.viewLocationMapper = viewLocationMapper;
        this.viewFactory = viewFactory;

        this.expandFor = function (element) {
            $("[data-view]", element).each(function () {
                var target = $(this)[0];
                var viewName = $(this).attr("data-view");
                var path = self.viewLocationMapper.resolve(viewName);
                self.viewFactory.createFrom(path).continueWith(function (view) {
                    target.view = view;
                    $(target).append($(view.content));
                });
            });
        };
    })
});
