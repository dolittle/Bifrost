Bifrost.namespace("Bifrost", {
    areEqual: function (source, target) {
        function isReservedMemberName(member) {
            return member.indexOf("_") >= 0 || member == "model" || member == "commons" || member == "targetViewModel" || member == "region";
        }

        if (ko.isObservable(source)) source = source();
        if (ko.isObservable(target)) target = target();

        if (Bifrost.isNullOrUndefined(source) && Bifrost.isNullOrUndefined(target)) return true;

        if (Bifrost.isNullOrUndefined(source)) return false;
        if (Bifrost.isNullOrUndefined(target)) return false;

        if (Bifrost.isArray(source) && Bifrost.isArray(target)) {
            if (source.length != target.length) {
                return false;
            }

            for (var index = 0; index < source.length; index++) {
                if (Bifrost.areEqual(source[index], target[index]) == false) {
                    return false;
                }
            }
        } else {
            for (var member in source) {
                if (isReservedMemberName(member)) continue;
                if (target.hasOwnProperty(member)) {
                    var sourceValue = source[member];
                    var targetValue = target[member];

                    if (Bifrost.isObject(sourceValue) ||
                        Bifrost.isArray(sourceValue) ||
                        ko.isObservable(sourceValue)) {

                        if (!Bifrost.areEqual(sourceValue, targetValue)) {
                            return false;
                        }
                    } else {
                        if (sourceValue != targetValue) {
                            return false;
                        }
                    }
                } else {
                    return false;
                }
            }
        }

        return true;
    }
});