Bifrost.namespace("Bifrost", {
    StringMapper: Bifrost.Type.extend(function (stringMappingFactory) {
        var self = this;

        this.stringMappingFactory = stringMappingFactory;

        this.mappings = [];

        this.reverseMappings = [];

        function hasMappingFor(mappings, input) {
            var found = false;
            mappings.some(function (m) {
                if (m.matches(input)) {
                    found = true;
                }
                return found;
            });
            return found;
        }

        function getMappingFor(mappings, input) {
            var found;
            mappings.some(function (m) {
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
        }

        function resolve(mappings, input) {
            try {
                if (input === null || typeof input === "undefined") {
                    return "";
                }
                
                var mapping = self.getMappingFor(input);
                if (Bifrost.isNullOrUndefined(mapping)) {
                    return "";
                }

                return mapping.resolve(input);
            } catch (e) {
                return "";
            }
        }

        this.hasMappingFor = function (input) {
            return hasMappingFor(self.mappings, input);
        };
        this.getMappingFor = function (input) {
            return getMappingFor(self.mappings, input);
        };
        this.resolve = function (input) {
            return resolve(self.mappings, input);
        };

        this.reverse = {
            hasMappingFor: function (input) {
                return self.hasMappingFor(self.reverseMappings, input);
            },

            getMappingFor: function (input) {
                return self.getMappingFor(self.reverseMappings, input);
            },

            resolve: function (input) {
                return self.resolve(self.reverseMappings, input);
            }
        };

        this.addMapping = function (format, mappedFormat) {
            var mapping = self.stringMappingFactory.create(format, mappedFormat);
            self.mappings.push(mapping);

            var reverseMapping = self.stringMappingFactory.create(mappedFormat, format);
            self.reverseMappings.push(reverseMapping);
        };
    })
});