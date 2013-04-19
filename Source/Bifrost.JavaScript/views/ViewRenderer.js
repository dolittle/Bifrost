Bifrost.namespace("Bifrost.views", {
    ViewRenderer: Bifrost.Type.extend(function () {
        this.canRender = function (element) {
            return false;
        };

        this.render = function (element) {
        };
	})
});