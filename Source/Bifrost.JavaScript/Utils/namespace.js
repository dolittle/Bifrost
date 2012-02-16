var Bifrost = Bifrost || {};
(function(global, undefined) {
    Bifrost.namespace = function (ns) {
        var parent = global;
        var parts = ns.split('.');
        $.each(parts, function (index, part) {
            if (!Object.prototype.hasOwnProperty.call(parent, part)) {
                parent[part] = {};
            }
            parent = parent[part];
        });
    };
})(window);