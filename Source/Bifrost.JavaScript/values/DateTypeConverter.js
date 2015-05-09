Bifrost.namespace("Bifrost.values", {
    DateTypeConverter: Bifrost.values.TypeConverter.extend(function () {
        this.supportedType = Date;

        function isNull(time) {
            // Treat serialization of default(DateTime) from server as null.
            return Bifrost.isNullOrUndefined(time) ||
                // ISO 8601 formats for default(DateTime):
                time === "0001-01-01T00:00:00" ||
                time === "0001-01-01T00:00:00Z" ||
                // new Date("0001-01-01T00:00:00") in Chrome and Firefox:
                (time instanceof Date && time.getTime() === -62135596800000) ||
                // new Date("0001-01-01T00:00:00") or any other invalid date in Internet Explorer:
                (time instanceof Date && isNaN(time.getTime()));
        }

        this.convertFrom = function (value) {
            if (isNull(value)) {
                return null;
            }
            var date = new Date(value);
            return date;
        };

        this.convertTo = function (value) {
            return value.format("yyyy-MM-dd");
        };
    })
});