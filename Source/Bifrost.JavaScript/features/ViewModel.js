Bifrost.namespace("Bifrost.features");
Bifrost.features.ViewModel = (function () {
    function ViewModel(definition, isSingleton) {
        var self = this;
        this.definition = definition;
        this.isSingleton = isSingleton;

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
        create: function (definition, isSingleton) {
            var viewModel = new ViewModel(definition, isSingleton);
            return viewModel;
        }
    }
})();