var applicationDir = "/";
var path = window.location.pathname.toLowerCase();
if (path.indexOf("/mimir") == 0 || path.indexOf("mimir") == 0) {
    applicationDir = "/Mimir";
}

function combinePaths(base, path) {
    if (base.lastIndexOf("/") == base.length - 1 && path.indexOf("/") == 0) {
        path = path.substr(1);
    }
    return base + path;
}

require.config({
    appDir: applicationDir,
    baseUrl: combinePaths(applicationDir,"/Scripts"),
    optimize: "none",

    paths: {
        "jquery": "http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min",
        "signalr": "jquery.signalR-1.0.0-rc2.min",
        "signalrHubs": "/signalr/hubs",
        "knockout": "knockout-2.0.0", //"http://cdn.dolittle.com/knockout/knockout-2.0.0",
        "knockout.mapping": "knockout.mapping-2.0.0",
        "bifrost": "Bifrost.debug",
        "order": "order", //"http://cdn.dolittle.com/require/order",
        "domReady": "domReady", // "http://cdn.dolittle.com/require/domReady",
        "text": "text", //"http://cdn.dolittle.com/require/text"
    }
});

function loadHubs() {
    var script = document.createElement("script");
    script.type = "text/javascript";
    script.src = "/signalr/hubs";
    document.body.appendChild(script);
}

require(["jquery", "knockout"], function () {
    require(["signalr", "order!noext!signalr/hubs", "jquery.history"], function () {
        require(["knockout.mapping", "bifrost", "knockout.plugins"], function () {
            Bifrost.features.featureMapper.add("{feature}/{subFeature}", combinePaths(applicationDir, "/Features/{feature}/{subFeature}"), false);
            Bifrost.features.featureMapper.add("{feature}", combinePaths(applicationDir, "/Features/{feature}"), true);

            require([
                "noext!/Bifrost/Proxies",
                combinePaths(applicationDir, "/bootstrap/js/bootstrap.min.js"),
                combinePaths(applicationDir, "/Scripts/libs/google-code-prettify/prettify.js"),
                combinePaths(applicationDir, "/Scripts/libs/jquery.tablesorter/jquery.tablesorter.min.js"),
                combinePaths(applicationDir, "/Scripts/libs/jquery.pageslide/jquery.pageslide.min.js"),
                combinePaths(applicationDir, "/Scripts/applicationBar.js")
            ], function () { });

            $(function () {
                $.connection.hub.start()
                    .done(function () {
                    })
                    .fail(function () {
                    });

                $(window).load();
            });
        });
    });
});
