Bifrost.namespace("Bifrost.markup", {
    propertyExpander: Bifrost.Singleton(function (typeConverters) {

        this.expand = function (element, target) {

            /*
            var propertySplit = element.localName.split(".");
            if (propertySplit.length > 2) {
                throw Bifrost.markup.MultiplePropertyReferencesNotAllowed.create({ tagName: name });
            }

            if (propertySplit.length === 2) {
                if (!Bifrost.isNullOrUndefined(element.parentElement)) {
                    var parentName = element.parentElement.localName.toLowerCase();

                    if (parentName !== propertySplit[0]) {
                        throw Bifrost.markup.ParentTagNameMismatched.create({ tagName: name, parentTagName: parentName });
                    }
                }
            }

            if (!Bifrost.isNullOrUndefined(element.parentElement)) {
                propertySplit = element.parentElement.localName.split(".");
                if (propertySplit.length === 2) {
                    var propertyName = propertySplit[1];
                    if (!Bifrost.isNullOrUndefined(element.parentElement.__objectModelNode)) {
                        if (ko.isObservable(element.parentElement.__objectModelNode[propertyName])) {
                            element.parentElement.__objectModelNode[propertyName](instance);
                        } else {
                            element.parentElement.__objectModelNode[propertyName] = instance;
                        }
                    }
                }
            }*/

            for (var attributeIndex = 0; attributeIndex < element.attributes.length; attributeIndex++) {
                var name = element.attributes[attributeIndex].localName;
                var value = element.attributes[attributeIndex].value;

                if (name in target) {
                    var targetValue = target[name];
                    if (ko.isObservable(targetValue)) {
                        targetValue(value);
                    } else {
                        target[name] = value;
                    }

                    /*
                    var targetType = typeof targetValue;
                    if (ko.isObservable(targetValue)) {
                        targetType = typeof targetValue();
                    }

                    var convertedValue = typeConverters.convert(targetType, value);
                    if (ko.isObservable(targetValue)) {
                        targetValue(convertedValue);
                    } else {
                        target[name] = convertedValue;
                    }
                    */
                }
            }
        };
    })
});