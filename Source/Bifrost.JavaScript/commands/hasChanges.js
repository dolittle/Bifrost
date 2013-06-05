if (typeof ko !== 'undefined') {
    ko.extenders.hasChanges = function (target, options) {
        target.hasChanges = ko.observable(false);
        function updateHasChanges() {
            if (target._initialValue === null || typeof target._initialValue == "undefined") {
                target.hasChanges(false);
            } else {
                target.hasChanges(target._initialValue != target());
            }
        }

        target.subscribe(function (newValue) {
            updateHasChanges();
        });

        target.setInitialValue = function (value) {
            target._initialValue = value;
            updateHasChanges();
        };
    };
}
