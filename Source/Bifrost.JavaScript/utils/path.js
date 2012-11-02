Bifrost.namespace("Bifrost", {
    path: {
        getPathWithoutFilename: function (fullPath) {
            var lastIndex = fullPath.lastIndexOf("/");
            return fullPath.substr(0, lastIndex);
        }
    }
});