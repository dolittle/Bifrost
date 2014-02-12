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

            function getQueryStringParametersWithValueForParameter(url, parameterValue) {
                var parameters = Bifrost.hashString.decode(url);
                parameters[parameterName] = parameterValue;

                var queryString = "";
                var parameterIndex = 0;
                for (var parameter in parameters) {
                    var value = parameters[parameter];
                    if (!Bifrost.isNullOrUndefined(value)) {
                        if (parameterIndex > 0) {
                            queryString += "&";
                        }
                        queryString += parameter + "=" + value;
                    }
                    parameterIndex++;
                }

                return queryString;
            }

            function cleanQueryString(queryString) {
                if (queryString.indexOf("#") == 0 || queryString.indexOf("?") == 0) queryString = queryString.substr(1);
                return queryString;
            }

            observable.subscribe(function (newValue) {
                if (historyEnabled) {
                    var state = History.getState();
                    state[parameterName] = newValue;
                    var queryString = "?" + getQueryStringParametersWithValueForParameter(cleanQueryString(state.url), newValue);
                    History.pushState(state, state.title, queryString);
                } else {
                    var queryString = "#" + getQueryStringParametersWithValueForParameter(cleanQueryString(document.location.hash), newValue);
                    document.location.hash = queryString;
                }
            });

            return observable;
        };
    })
});

ko.observableQueryParameter = function (parameterName, defaultValue) {
    var navigationManager = Bifrost.navigation.navigationManager;
    var observable = Bifrost.navigation.observableQueryParameterFactory.create().create(parameterName, defaultValue, navigationManager);
    return observable;
};
