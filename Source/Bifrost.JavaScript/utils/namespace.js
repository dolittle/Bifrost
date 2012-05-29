var Bifrost = Bifrost || {};
(function(global, undefined) {
    Bifrost.namespace = function (ns, content) {
        var parent = global;
        var parts = ns.split('.');
        $.each(parts, function (index, part) {
            if (!Object.prototype.hasOwnProperty.call(parent, part)) {
                parent[part] = {};
            }
            parent = parent[part];
        });

		if( typeof content === "object" ) {
			Bifrost.extend(parent, content);
		}
    };
})(window);