Bifrost.namespace("Bifrost");
Bifrost.hashString = (function() {
	return {
		decode: function(a) {
		    if (a == "") return { };
			a = a.replace("/?","").split('&');

		    var b = { };
		    for (var i = 0; i < a.length; ++i) {
		        var p = a[i].split('=');
		        if (p.length != 2) continue;
		        b[p[0]] = decodeURIComponent(p[1].replace( /\+/g , " "));
		    }
		    return b;
		}
	}
})();
