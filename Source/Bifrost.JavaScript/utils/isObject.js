Bifrost.namespace("Bifrost", {
    isObject: function (o) {
        if (o === null) {
            return false;
        }
        return Object.prototype.toString.call(o) === '[object Object]';
    }
});