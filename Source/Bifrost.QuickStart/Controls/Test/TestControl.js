Bifrost.namespace("Web.Controls.Test", {
    TestControl: Bifrost.markup.Control.extend(function () {
        var self = this;
        this.something = "";
        this.observable = ko.observable();
        this.timer = ko.observable(0);

        setInterval(function () {
            self.timer(self.timer() + 1);
        }, 1000);
    })
});