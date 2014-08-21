Bifrost.namespace("Bifrost", {
    StringMapper: Bifrost.Type.extend(function (stringMappingFactory) {
        var self = this;

        this.stringMappingFactory = stringMappingFactory;

        this.mappings = [];

        this.hasMappingFor = function (input) {
            var found = false;
            self.mappings.some(function (m) {
                if (m.matches(input)) {
                    found = true;
                }
                return found;
            });
            return found;
        };

        this.getMappingFor = function (input) {
            var found;
            self.mappings.some(function (m) {
                if (m.matches(input)) {
                    found = m;
                    return true;
                }
            });

            if (typeof found !== "undefined") {
                return found;
            }

            throw {
                name: "ArgumentError",
                message: "String mapping for (" + input + ") could not be found"
            };
        };

        this.resolve = function (input) {
            try {
                if (input === null || typeof input === "undefined") return "";
                
                var mapping = self.getMappingFor(input);
                if (Bifrost.isNullOrUndefined(mapping)) return "";

                return mapping.resolve(input);
            } catch (e) {
                return "";
            }
        };

        this.addMapping = function (format, mappedFormat) {
            var mapping = self.stringMappingFactory.create(format, mappedFormat);
            self.mappings.push(mapping);
        };
    })
});