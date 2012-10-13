require.config({
	appDir: "/",
	baseUrl: "/scripts",
	optimize: "none",

	paths: {
	    "jquery": "http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min",
	    "knockout": "http://cdn.dolittle.com/Knockout/knockout-2.0.0",
		"knockout.mapping": "knockout.mapping-2.0.0",
	    "bifrost": "Bifrost.debug",
	    "order": "http://cdn.dolittle.com/Require/order",
	    "domReady": "http://cdn.dolittle.com/Require/domReady",
	    "text": "http://cdn.dolittle.com/Require/text"
	}
});

require(
    ["jquery","knockout"],	function() {
		require(["jquery.history"], function () {
		        require(["knockout.mapping", "bifrost", "knockout.plugins"], function () {
		                Bifrost.features.featureMapper.add("{feature}/{subFeature}", "/Features/{feature}/{subFeature}", false);
		                Bifrost.features.featureMapper.add("{feature}", "/Features/{feature}", true);

		                require([
                            "/bootstrap/js/bootstrap.min.js",
                            "/js/libs/flot/excanvas.min.js",
                            "/js/libs/google-code-prettify/prettify.js",
                            "/js/libs/jquery.tablesorter/jquery.tablesorter.min.js",
                            "/js/libs/jquery.pageslide/jquery.pageslide.min.js",
                            
                            /*
                            "/js/madmin.js"
                            "/js/application.js",
                            "/js/demo-area-chart.js",
                            "/js/demo-dynamic-chart.js",
                            "/js/demo-pie-chart.js"
                            "/js/libs/flot/jquery.flot.min.js",
                            "/js/libs/flot/jquery.flot.resize.min.js",
                            "/js/libs/flot/jquery.flot.pie.min.js",
                            */

		                ], function () {
		                    
    	                });
		            }
		        );
		    }
		);
	}
);
