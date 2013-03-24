Bifrost.namespace("Bifrost.views", {
    UriMapperViewPathResolver: Bifrost.views.ViewPathResolver.extend(function () {
        this.canResolve = function (element, path) {
            var closest = $(element).closest("[data-urimapper]");
            if (closest.length == 1) {
                var mapperName = $(closest[0]).data("urimapper");
                if (Bifrost.uriMappers[mapperName].hasMappingFor(path) == true) return true;
            }
            return Bifrost.uriMappers.default.hasMappingFor(path);
        };

        this.resolve = function (element, path) {
            var closest = $(element).closest("[data-urimapper]");
            if (closest.length == 1) {
                var mapperName = $(closest[0]).data("urimapper");
                if (Bifrost.uriMappers[mapperName].hasMappingFor(path) == true) {
                    return Bifrost.uriMappers[mapperName].resolve(path);
                }
            }
            return Bifrost.uriMappers.default.resolve(path);
        };
    })
});
if (typeof Bifrost.views.viewPathResolvers != "undefined") {
    Bifrost.views.viewPathResolvers.UriMapperViewPathResolver = Bifrost.views.UriMapperViewPathResolver;
}