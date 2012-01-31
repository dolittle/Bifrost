var Bifrost = Bifrost || {};
(function(global, undefined) {
    Bifrost.namespace = function (ns) {
        var parent = global;
        var parts = ns.split('.');
        $.each(parts, function (index, part) {
            if (!parent.hasOwnProperty(part)) {
                parent[part] = {};
            }
            parent = parent[part];
        });
    };
})(window);