var applicationDir = "/Mimir";

require.config({
    appDir: applicationDir,
    baseUrl: applicationDir+"/Scripts",
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
    ["jquery", "knockout"], function () {
        require(["jquery.history"], function () {
            require(["knockout.mapping", "bifrost", "knockout.plugins"], function () {
                Bifrost.features.featureMapper.add("{feature}/{subFeature}", applicationDir + "/Features/{feature}/{subFeature}", false);
                Bifrost.features.featureMapper.add("{feature}", applicationDir + "/Features/{feature}", true);

                require([
                    applicationDir + "/bootstrap/js/bootstrap.min.js",
                    applicationDir + "/Scripts/libs/google-code-prettify/prettify.js",
                    applicationDir + "/Scripts/libs/jquery.tablesorter/jquery.tablesorter.min.js",
                    applicationDir + "/Scripts/libs/jquery.pageslide/jquery.pageslide.min.js",

                ], function () {

                });
            });
        });
    }
);
