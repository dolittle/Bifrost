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
        getFilenameWithoutExtension: function (fullPath) {
            var filename = this.getFilename(fullPath);
            var lastIndex = filename.lastIndexOf(".");
            return filename.substr(0,lastIndex);
        },
        hasExtension: function (path) {
            if (path.indexOf("?") > 0) path = path.substr(0, path.indexOf("?"));
            var lastIndex = path.lastIndexOf(".");
            return lastIndex > 0;
        },
        changeExtension: function (fullPath, newExtension) {
            if (fullPath.indexOf("?") > 0) fullPath = fullPath.substr(0, fullPath.indexOf("?"));
            var lastIndex = fullPath.lastIndexOf(".");
            if (lastIndex > 0) {
                return fullPath.substr(0, lastIndex) + "." + newExtension;
            }
            return fullPath + "." + newExtension;
        }
    }
});