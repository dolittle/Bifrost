Bifrost.namespace("Bifrost.markup", {
    AttachedProperty: Bifrost.values.ValueConsumer.extend(function () {
        this.canNotifyChanges = function () {
            return false;
        };

        this.notifyChanges = function (callback) {
        };

        this.consume = function (value) {
        };
    })
});