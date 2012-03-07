Bifrost.namespace("Bifrost.features");
Bifrost.features.FeatureState = (function () {
    function FeatureState(state, options) {
        var self = this;
        this.options = {
            changed: function () { }
        };
        Bifrost.extend(this.options, options);

        function onChanged(property, newValue) {
            self.options.changed(property, newValue);
        }

        function initialize(state) {
            for (var property in state) {
                self[property] = ko.observable(state[property]);
                self[property].subscribe(function (newValue) {
                    onChanged(property, newValue);
                });
            }
        }

        initialize(state);
    }

    return {
        create: function (state, options) {
            var featureState = new FeatureState(state, options);
            return featureState;
        }
    }
})();