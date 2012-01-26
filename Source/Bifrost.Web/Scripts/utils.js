var Bifrost = Bifrost || {};
(function(global, undefined) {
    function S4() {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    }

    Bifrost.guid = function() {
        return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
    }

    Bifrost.extend = function extend(destination, source) {
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

    Bifrost.namespace = function (ns) {
        var parent = global;
        var parts = ns.split('.');
        $.each(parts, function(index , part) {
            if( !parent.hasOwnProperty(part) ) {
                parent[part] = {};
            }
            parent = parent[part];
        });
    };
})(window);



