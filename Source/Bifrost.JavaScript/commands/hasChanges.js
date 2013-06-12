if (typeof ko !== 'undefined') {
    ko.extenders.hasChanges = function (target, options) {
        target._initialValueSet = false;
        target.hasChanges = ko.observable(false);
        function updateHasChanges() {
            if (target._initialValueSet == false) {
                target.hasChanges(false);
            } else {
                target.hasChanges(target._initialValue !== target());
            }
        }

        target.subscribe(function (newValue) {
            updateHasChanges();
        });

        target.setInitialValue = function (value) {
            target._initialValue = value;
            target._initialValueSet = true;
            updateHasChanges();
        };
    };
}
