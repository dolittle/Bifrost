Bifrost.namespace("Bifrost", {
    path: {
        makeRelative: function (fullPath) {
            if (fullPath.indexOf("/") == 0) return fullPath.substr(1);

            return fullPath;
        },
        getPathWithoutFilename: function (fullPath) {
            var lastIndex = fullPath.lastIndexOf("/");
            return fullPath.substr(0, lastIndex);
        },
        getFilename: function (fullPath) {
            var lastIndex = fullPath.lastIndexOf("/");
            return fullPath.substr(lastIndex+1);
        },
        changeExtension: function (fullPath, newExtension) {
            var lastIndex = fullPath.lastIndexOf(".");
            return fullPath.substr(0, lastIndex) + "." + newExtension;
        }
    }
});