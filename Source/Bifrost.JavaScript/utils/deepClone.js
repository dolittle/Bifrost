Bifrost.namespace("Bifrost", {
    deepClone: function (source, target) {
        function isReservedMemberName(member) {
            return member.indexOf("_") >= 0 || member == "model" || member == "commons" || member == "targetViewModel" || member == "region";
        }

        if (ko.isObservable(source)) source = source();

        if (target == null) {
            if (Bifrost.isArray(source)) {
                target = []
            } else {
                target = {};
            }
        }

        if (Bifrost.isArray(source)) {
            for (var index = 0; index < source.length; index++) {
                var sourceValue = source[index];
                var clonedValue = Bifrost.deepClone(sourceValue);
                target.push(clonedValue);
            }
        } else {
            for (var member in source) {
                if (isReservedMemberName(member)) continue;

                var sourceValue = source[member];

                if (ko.isObservable(sourceValue)) sourceValue = sourceValue();

                if (Bifrost.isFunction(sourceValue)) continue;

                var targetValue = null;
                if (Bifrost.isObject(sourceValue)) {
                    targetValue = {};
                } else if (Bifrost.isArray(sourceValue)) {
                    targetValue = [];
                } else {
                    target[member] = sourceValue;
                }

                if (targetValue != null) {
                    target[member] = targetValue;
                    Bifrost.deepClone(sourceValue, targetValue);
                }
            }
        }

        return target;
    }
})
