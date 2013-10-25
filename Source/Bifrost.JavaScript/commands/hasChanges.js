if (typeof ko !== 'undefined') {
    ko.extenders.hasChanges = function (target, options) {
        target._initialValueSet = false;
        target.hasChanges = ko.observable(false);
        function updateHasChanges() {
            if (target._initialValueSet == false) {
                target.hasChanges(false);
            } else {
                if(Bifrost.isArray(target._initialValue)){
                    target.hasChanges(!target._initialValue.shallowEquals(target()));
                    return;
                }
                else
                    target.hasChanges(target._initialValue !== target());
            }
        }

        target.subscribe(function (newValue) {
            updateHasChanges();
        });

        target.setInitialValue = function (value) {
            var initialValue;
            if (Bifrost.isArray(value))
                initialValue = value.clone();
            else
                initialValue = value;
            
            target._initialValue = initialValue;
            target._initialValueSet = true;
            updateHasChanges();
        };
    };
}
