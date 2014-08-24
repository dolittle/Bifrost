Bifrost.namespace("Bifrost.values", {
    stringFormatter: Bifrost.Singleton(function () {
        var formatterTypes = Bifrost.values.Formatter.getExtenders();
        var formattersByType = {};

        formatterTypes.forEach(function (type) {
            var formatter = type.create();
            formattersByType[formatter.supportedType] = formatter;
        });

        function getFormat(element) {
            if (element.nodeType !== 1 || Bifrost.isNullOrUndefined(element.attributes)) {
                return null;
            }
            var stringFormatAttribute = element.attributes.getNamedItem("data-stringformat");
            if (!Bifrost.isNullOrUndefined(stringFormatAttribute)) {
                return stringFormatAttribute.value;
            }

            return null;
        }

        this.hasFormat = function (element) {
            var format = getFormat(element);
            return format !== null;
        };

        this.format = function (element, value) {
            var format = getFormat(element);

            if (formattersByType.hasOwnProperty(value.constructor)) {
                var formatter = formattersByType[value.constructor];
                var regex = new RegExp("{(.[^{}])*}", "g");
                var result = format.replace(regex, function (formatExpression) {
                    var expression = formatExpression.substr(1, formatExpression.length - 2);
                    return formatter.format(value, expression);
                });
                return result;
            }

            return format;
        };
    })
});