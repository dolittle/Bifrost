Bifrost.namespace("Bifrost.features");
Bifrost.features.ViewModel = (function () {
    function ViewModel(definition, options) {
        var self = this;
        this.definition = definition;
        this.options = {
            isSingleton: false
        }
        Bifrost.extend(this.options, options);

        if (options && options.state) {
            this.state = Bifrost.features.FeatureState.create(options.state);
        } else {
            this.state = {};
        }

        this.getInstance = function () {
            if (self.options.isSingleton) {
                if (!self.instance) {
                    self.instance = new self.definition();
                    self.instance.state = self.state;
                }

                return self.instance;
            }

            var instance = new self.definition();
            instance.state = self.state;
            return instance;
        };
    }

    return {
        create: function (definition, options) {
            var viewModel = new ViewModel(definition, options);
            return viewModel;
        }
    }
})();