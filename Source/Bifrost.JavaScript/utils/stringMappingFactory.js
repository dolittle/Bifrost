Bifrost.namespace("Bifrost", {
    stringMappingFactory: Bifrost.Singleton(function () {
        var self = this;

        this.create = function (format, mappedFormat) {
            var mapping = Bifrost.StringMapping.create({
                format: format,
                mappedFormat: mappedFormat
            });
            return mapping;
        };
    })
});