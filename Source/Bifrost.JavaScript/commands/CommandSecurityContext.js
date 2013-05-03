Bifrost.namesapce("Bifrost.commands", {
    CommandSecurityContext: Bifrost.Type.extend(function() {
        var self = this;

        this.canExecute = ko.observable(false);

    });
});