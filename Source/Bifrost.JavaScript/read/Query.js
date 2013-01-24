Bifrost.namespace("Bifrost.read", {
    Query: Bifrost.Type.extend(function () {
        var self = this;
        this.name = "";
        var allObservable = ko.observableArray();

        this.all = function () {
            return allObservable;
        }
    })
});