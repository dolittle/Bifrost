Bifrost.namespace("Bifrost.features");
Bifrost.features.ViewModelDefinition = (function () {
    function ViewModelDefinition(target, options) {
        var self = this;
        this.target = target;
        this.options = {
            isSingleton: false
        }
        Bifrost.extend(this.options, options);

        this.getInstance = function () {
			var instance = null;
            if (self.options.isSingleton) {
                if (!self.instance) {
                    self.instance = new self.target();
                }

                instance = self.instance;
            } else {
				instance = new self.target();
			}
            return instance;
        };
    }

    return {
        define: function (target, options) {
			Bifrost.features.ViewModel.baseFor(target);
            var viewModel = new ViewModelDefinition(target, options);
            return viewModel;
        }
    }
})();