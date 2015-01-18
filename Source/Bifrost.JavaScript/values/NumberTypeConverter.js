Bifrost.namespace("Bifrost.values", {
    NumberTypeConverter: Bifrost.values.TypeConverter.extend(function () {
        var allowedCharacters = "0123456789.,";

        this.supportedType = Number;

        function stripLetters(value) {
            value = value.toString();
            var returnValue = "";

            for (var charIndex = 0; charIndex < value.length; charIndex++) {
                var found = false;
                for (var allowedCharIndex = 0; allowedCharIndex < allowedCharacters.length; allowedCharIndex++) {
                    if (value[charIndex] === allowedCharacters[allowedCharIndex]) {
                        found = true;
                        returnValue = returnValue + value[charIndex];
                        break;
                    }
                }
            }

            return returnValue;
        }

        this.convertFrom = function (value) {
            if (value.constructor === Number) {
                return value;
            }
            if (value === "") {
                return 0;
            }
            value = stripLetters(value);
            var result = 0;
            if (value.indexOf(".") >= 0) {
                result = parseFloat(value);
            } else {
                result = parseInt(value);
            }
            return result;
        };
    })
});