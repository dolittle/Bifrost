Bifrost.namespace("Bifrost", {
	namespaces: {
		root: "/",
		initialize: function() {		
			var scripts = Bifrost.assetsManager.getScripts();
            $.each(scripts, function (index, fullPath) {
                var path = Bifrost.path.getPathWithoutFilename(fullPath);
                if( path.startsWith("/") ) {
                	path = path.substr(1);
                }
			
                var namespacePath = path.split("/").join(".");
                var namespace = Bifrost.namespace(namespacePath);
                var actualRoot = Bifrost.namespaces.root;
                if( !actualRoot.endsWith("/") ) {
                	actualRoot += "/";
                }

                namespace._path = actualRoot + path;

                if( typeof namespace._scripts === "undefined" ) {
                	namespace._scripts = [];
                }

                var fileIndex = fullPath.lastIndexOf("/");
                var file = fullPath.substr(fileIndex+1);
                var extensionIndex = file.lastIndexOf(".");
                var system = file.substr(0,extensionIndex);

                namespace._scripts.push(system);
            });
		}
	}
});