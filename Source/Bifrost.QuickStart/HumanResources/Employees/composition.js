Bifrost.namespace("Web.HumanResources.Employees", {
    composition: Bifrost.views.ViewModel.extend(function (globalMessenger) {
        var self = this;

        this.showPopup = function () {
            globalMessenger.publish("openPopup");
        };

        this.showSecondPopup = function () {
            globalMessenger.publish("openSecondPopup");
        };

        this.showThirdPopup = function () {
            globalMessenger.publish("openThirdPopup");
        };

    })
});
