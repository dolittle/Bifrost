Bifrost.namespace("Bifrost.navigation", {
    observableQueryParameterFactory: Bifrost.Singleton(function () {
        var self = this;

        var historyEnabled = typeof History !== "undefined" && typeof History.Adapter !== "undefined";

        this.create = function (parameterName, defaultValue, navigationManager) {
            
            function getState() {
                var uri = navigationManager.getCurrentLocation();
                if (uri.parameters.hasOwnProperty(parameterName)) {
                    return uri.parameters[parameterName];
                }

                return null;
            }

            var observable = null;

            if (historyEnabled) {
                History.Adapter.bind(window, "statechange", function () {
                    if (observable != null) {
                        observable(getState());
                    }
                });
            } else {
                window.addEventListener("hashchange", function () {
                    if (observable != null) {
                        var state = getState();
                        if (observable() != state) {
                            observable(state);
                        }
                    }
                }, false);
            }

            var state = getState();
            observable = ko.observable(state || defaultValue);

            if (historyEnabled) {
                observable.subscribe(function (newValue) {
                    var state = History.getState();
                    state[parameterName] = newValue;

                    var parameters = Bifrost.hashString.decode(state.url);
                    parameters[parameterName] = newValue;

                    var url = "?";
                    var parameterIndex = 0;
                    for (var parameter in parameters) {
                        if (parameterIndex > 0) {
                            url += "&";
                        }
                        url += parameter + "=" + parameters[parameter];
                        parameterIndex++;
                    }

                    History.pushState(state, state.title, url);
                });
            }

            return observable;
            
        };


    })
});

ko.observableQueryParameter = function (parameterName, defaultValue) {
    var navigationManager = Bifrost.navigation.navigationManager;
    var observable = Bifrost.navigation.observableQueryParameterFactory.create().create(parameterName, defaultValue, navigationManager);
    return observable;
};
