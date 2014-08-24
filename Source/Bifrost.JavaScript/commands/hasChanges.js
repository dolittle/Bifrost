ko.extenders.hasChanges = function (target) {
    target._initialValueSet = false;
    target.hasChanges = ko.observable(false);
    function updateHasChanges() {
        if (target._initialValueSet === false) {
            target.hasChanges(false);
        } else {
            if (Bifrost.isArray(target._initialValue)) {
                target.hasChanges(!target._initialValue.shallowEquals(target()));
                return;
            }
            target.hasChanges(target._initialValue !== target());
        }
    }

    target.subscribe(function () {
        updateHasChanges();
    });

    target.setInitialValue = function (value) {
        var initialValue;
        if (Bifrost.isArray(value)) {
            initialValue = value.clone();
        } else {
            initialValue = value;
        }

        target._initialValue = initialValue;
        target._initialValueSet = true;
        updateHasChanges();
    };
};