Bifrost.namespace("Bifrost.values", {
    valuePipeline: Bifrost.Singleton(function (typeConverters) {

        this.getValueForView = function (element, value) {
            if (ko.isObservable(value)) {

                if (value() !== value._previousValue) {
                    value._previousValue = value();

                    if (!Bifrost.isNullOrUndefined(value._typeAsString)) {
                        value = typeConverters.convertTo(value());
                    }
                }
            }

            return value;
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

    Bifrost.values.valuePipeline.getValueForView = function (element, value) {
        var result = valuePipeline.getValueForView(element, value);
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

        var expressionIndex = bindingsStringOrKeyValueArray.indexOf(":");
        var expression = bindingsStringOrKeyValueArray.substr(expressionIndex + 1).trim();
        var bindingString = bindingsStringOrKeyValueArray;
        if (!isComplexExpression(expression)) {
            var rewrittenExpression = bindingsStringOrKeyValueArray;
            var bindingHandler = bindingsStringOrKeyValueArray.substr(0, expressionIndex + 1);
            rewrittenExpression = "Bifrost.values.valuePipeline.getValueForView($element, " + expression + ")";
            bindingString = bindingHandler + rewrittenExpression;
        }

        var result = oldPreProcessBindings(bindingString, bindingOptions);
        return result;
    };
})();