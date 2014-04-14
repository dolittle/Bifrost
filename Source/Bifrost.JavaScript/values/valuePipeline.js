Bifrost.namespace("Bifrost.values", {
    valuePipeline: Bifrost.Singleton(function (typeConverters, stringFormatter) {
        this.getValueForView = function (element, value) {
            if (Bifrost.isNullOrUndefined(value)) return value;
            var actualValue = ko.utils.unwrapObservable(value);
            if (Bifrost.isNullOrUndefined(actualValue)) return value;

            var returnValue = actualValue;

            if (stringFormatter.hasFormat(element)) {
                returnValue = stringFormatter.format(element, actualValue)
            } else {
                if (!Bifrost.isNullOrUndefined(value._typeAsString)) {
                    returnValue = typeConverters.convertTo(actualValue);
                }
            }
            return returnValue;
        };

        this.getValueForProperty = function (property, value) {
            if (!Bifrost.isNullOrUndefined(property._typeAsString)) {
                value = typeConverters.convertFrom(value, property._typeAsString);
            }

            return value;
        };
    })
});

(function () {
    var valuePipeline = Bifrost.values.valuePipeline.create();

    var oldReadValue = ko.selectExtensions.readValue;
    ko.selectExtensions.readValue = function (element) {
        var value = oldReadValue(element);

        var bindings = ko.bindingProvider.instance.getBindings(element, ko.contextFor(element));
        var result = valuePipeline.getValueForProperty(bindings.value, value);
        return result;
    };

    var oldWriteValue = ko.selectExtensions.writeValue;
    ko.selectExtensions.writeValue = function (element, value, allowUnset) {
        var bindings = ko.bindingProvider.instance.getBindings(element, ko.contextFor(element));
        var result = ko.utils.unwrapObservable(valuePipeline.getValueForView(element, bindings.value));

        oldWriteValue(element, result, allowUnset);
    };

    var oldSetTextContent = ko.utils.setTextContent;
    ko.utils.setTextContent = function (element, value) {
        result = valuePipeline.getValueForView(element, value);
        oldSetTextContent(element, result);
    };

    var oldSetHtml = ko.utils.setHtml;
    ko.utils.setHtml = function (element, value) {
        result = valuePipeline.getValueForView(element, value);
        oldSetHtml(element, result);
    };

    /*
    Bifrost.values.valuePipeline.getValueForView = function (element, bindingHandlerName, value) {
        var result = valuePipeline.getValueForView(element, bindingHandlerName, value);
        return result;
    }

    var complexExpressionCharacters = [";", "+", "-", "(", ")", "'"];
    function isComplexExpression(expression) {
        for (var charIndex = 0; charIndex < expression.length; charIndex++) {
            for (var complexCharIndex = 0; complexCharIndex < complexExpressionCharacters.length; complexCharIndex++) {
                if (expression[charIndex] == complexExpressionCharacters[complexCharIndex]) return true;
            }
        }
        
        return false;
    }

    var oldWriteValueToProperty = ko.expressionRewriting.writeValueToProperty;
    ko.expressionRewriting.writeValueToProperty = function (property, allBindings, key, value, checkIfDifferent) {
        value = valuePipeline.getValueForProperty(property, value);
        return oldWriteValueToProperty(property, allBindings, key, value, checkIfDifferent);
    };

    var oldPreProcessBindings = ko.expressionRewriting.preProcessBindings;
    ko.expressionRewriting.preProcessBindings = function (bindingsStringOrKeyValueArray, bindingOptions) {
        var bindings = ko.expressionRewriting.parseObjectLiteral(bindingsStringOrKeyValueArray);
        var bindingString = "";
        bindings.forEach(function (binding) {
            var bindingHandler = binding.key;
            var expression = binding.value;
            var rewrittenExpression = expression;
            if (!isComplexExpression(expression) && expression[0] != '{') {
                rewrittenExpression = "Bifrost.values.valuePipeline.getValueForView($element, '"+bindingHandler+"', " + expression + ")";
            }
            if( bindingString !== "" ) bindingString = bindingString +", ";
            bindingString = bindingString + bindingHandler + ":" + rewrittenExpression;
        });

        var result = oldPreProcessBindings(bindingString, bindingOptions);
        return result;
    };*/
})();