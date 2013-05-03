Bifrost.namespace("Bifrost.commands", {
    CommandSecurityContext: Bifrost.Type.extend(function() {
        var self = this;

        this.isAuthorized = ko.observable(false);

    })
});