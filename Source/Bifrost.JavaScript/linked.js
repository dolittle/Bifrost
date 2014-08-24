ko.extenders.linked = function (target, options) {
    function setupValueSubscription(value) {
        if (ko.isObservable(value)) {
            var subscription = value.subscribe(function () {
                target.valueHasMutated();
            });
            target._previousLinkedSubscription = subscription;
        }
    }

    target.subscribe(function (newValue) {
        if (target._previousLinkedSubscription) {
            target._previousLinkedSubscription.dispose();
        }
        setupValueSubscription(newValue);

    });

    var currentValue = target();
    setupValueSubscription(currentValue);
};