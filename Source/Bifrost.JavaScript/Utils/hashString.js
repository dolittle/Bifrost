Bifrost.namespace("Bifrost");
Bifrost.hashString = (function() {
	return {
		decode: function(a) {
		    if (a == "") return { };
			a = a.replace("/?","").split('&');

		    var b = { };
		    for (var i = 0; i < a.length; ++i) {
		        var p = a[i].split('=', 2);
		        if (p.length != 2) continue;
		
				var value = decodeURIComponent(p[1].replace( /\+/g , " "));
				var valueAsFloat = parseFloat(value);
				if( !isNaN(valueAsFloat) ) {
					value = valueAsFloat;
				}
		        b[p[0]] = value;
		    }
		    return b;
		}
	}
})();
