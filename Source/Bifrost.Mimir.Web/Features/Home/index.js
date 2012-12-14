Bifrost.features.featureManager.get("Home/index").defineViewModel(function () {
    this.someValue = ko.observableQueryParameter("someValue");

    this.doSomething = function () {
        History.pushState({}, "ASdasd", "?someValue=something");
    }
});