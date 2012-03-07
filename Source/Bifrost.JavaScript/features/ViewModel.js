Bifrost.namespace("Bifrost.features");
Bifrost.features.ViewModel = (function () {
    function ViewModel(definition, isSingleton, state) {
        var self = this;
        this.definition = definition;
        this.isSingleton = isSingleton;
		this.state = {};
		
		Bifrost.extend(this.state, state);

        this.getInstance = function () {

            if (self.isSingleton) {
                if (!self.instance) {
                    self.instance = new self.definition();
                }

                return self.instance;
            }

            return new self.definition();
        };
    }

    return {
        create: function (definition, isSingleton, state) {
            var viewModel = new ViewModel(definition, isSingleton, state);
            return viewModel;
        }
    }
})();