Bifrost.namespace("Bifrost", {
    StringMapper: Bifrost.Type.extend(function () {
        var self = this;

        this.mappings = [];

        this.hasMappingFor = function (input) {
            var found = false;
            self.mappings.forEach(function (m) {
                if (m.matches(input)) {
                    found = true;
                    return false;
                }
            });
            return found;
        };

        this.getMappingFor = function (input) {
            var found;
            self.mappings.forEach(function (m) {
                if (m.matches(input)) {
                    found = m;
                    return false;
                }
            });

            if (typeof found !== "undefined") {
                return found;
            }

            throw {
                name: "ArgumentError",
                message: "String mapping for (" + input + ") could not be found"
            }
        };

        this.resolve = function (input) {
            try {
                if( input === null || typeof input === "undefined" || input == "" ) return "";
                
                var mapping = self.getMappingFor(input);
                return mapping.resolve(input);
            } catch (e) {
                return "";
            }
        };

        this.addMapping = function (format, mappedFormat) {
            var mapping = Bifrost.StringMapping.create({
                format: format,
                mappedFormat: mappedFormat
            });
            self.mappings.push(mapping);
        };
    })
});