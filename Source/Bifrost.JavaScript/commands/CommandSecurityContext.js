Bifrost.namespace("Bifrost.commands", {
    CommandSecurityContext: Bifrost.Type.extend(function () {
        this.isAuthorized = ko.observable(false);
    })
});