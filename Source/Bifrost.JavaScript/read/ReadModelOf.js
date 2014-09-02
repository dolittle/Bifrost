Bifrost.namespace("Bifrost.read", {
    ReadModelOf: Bifrost.Type.extend(function (region, mapper, taskFactory, readModelSystemEvents) {
        var self = this;
        this.target = null;

        this._name = "";
        this._generatedFrom = "";
        this._readModelType = Bifrost.Type.extend(function () { });
        this.instance = ko.observable();
        this.commandToPopulate = null;
        this.region = region;

        function unwrapPropertyFilters(propertyFilters) {
            var unwrappedPropertyFilters = {};
            for (var property in propertyFilters) {
                unwrappedPropertyFilters[property] = ko.utils.unwrapObservable(propertyFilters[property]);
            }
            return unwrappedPropertyFilters;
        }

        function performLoad(target, propertyFilters) {
            var task = taskFactory.createReadModel(target, propertyFilters);
            target.region.tasks.execute(task).continueWith(function (data) {
                if (!Bifrost.isNullOrUndefined(data)) {
                    var mappedReadModel = mapper.map(target._readModelType, data);
                    self.instance(mappedReadModel);
                } else {
                    readModelSystemEvents.noInstance.trigger(target);
                }
            });
        }

        this.instanceMatching = function (propertyFilters) {
            function load() {
                var unwrappedPropertyFilters = unwrapPropertyFilters(propertyFilters);
                performLoad(self.target, unwrappedPropertyFilters);
            }

            load();

            for (var property in propertyFilters) {
                var value = propertyFilters[property];
                if (ko.isObservable(value)) {
                    value.subscribe(load);
                }
            }
        };

        this.populateCommandOnChanges = function (command) {
            command.populatedExternally();
            
            if (typeof self.instance() !== "undefined" && self.instance() != null) {
                command.populateFromExternalSource(self.instance());
            }

            self.instance.subscribe(function (newValue) {
                command.populateFromExternalSource(newValue);
            });
        };

        this.onCreated = function (lastDescendant) {
            self.target = lastDescendant;
            var readModelInstance = lastDescendant._readModelType.create();
            self.instance(readModelInstance);
        };
    })
});