Bifrost.namespace("Bifrost", {
    isObject: function (o) {
        if (o === null || typeof o === "undefined" ) {
            return false;
        }
        return Object.prototype.toString.call(o) === '[object Object]';
    }
});