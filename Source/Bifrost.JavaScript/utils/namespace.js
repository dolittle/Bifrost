var Bifrost = Bifrost || {};
Bifrost.namespace = function (ns, content) {
    var parent = window;
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