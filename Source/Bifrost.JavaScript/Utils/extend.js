Bifrost.namespace("Bifrost");
Bifrost.extend = function(destination, source) {
    var toString = Object.prototype.toString,
			            objTest = toString.call({});
    for (var property in source) {
        if (source[property] && objTest == toString.call(source[property])) {
            destination[property] = destination[property] || {};
            extend(destination[property], source[property]);
        } else {
            destination[property] = source[property];
        }
    }
    return destination;
};
