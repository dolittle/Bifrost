Bifrost.namespace("Bifrost", {
    Path: Bifrost.Type.extend(function (fullPath) {
        var self = this;

        // Based on node.js implementation : http://stackoverflow.com/questions/9451100/filename-extension-in-javascript
        var splitDeviceRe =
            /^([a-zA-Z]:|[\\\/]{2}[^\\\/]+[\\\/][^\\\/]+)?([\\\/])?([\s\S]*?)$/;

        // Regex to split the tail part of the above into [*, dir, basename, ext]
        var splitTailRe =
            /^([\s\S]+[\\\/](?!$)|[\\\/])?((?:\.{1,2}$|[\s\S]+?)?(\.[^.\/\\]*)?)$/;

        function removeUnsupportedParts(filename) {
            var queryStringStart = filename.indexOf("?");
            if (queryStringStart > 0) {
                filename = filename.substr(0, queryStringStart);
            }
            return filename;
        }

        function splitPath(filename) {
            // Separate device+slash from tail
            var result = splitDeviceRe.exec(filename),
                device = (result[1] || '') + (result[2] || ''),
                tail = result[3] || '';
            // Split the tail into dir, basename and extension
            var result2 = splitTailRe.exec(tail),
                dir = result2[1] || '',
                basename = result2[2] || '',
                ext = result2[3] || '';
            return [device, dir, basename, ext];
        }

        fullPath = removeUnsupportedParts(fullPath);
        var result = splitPath(fullPath);
        this.device = result[0] || "";
        this.directory = result[1] || "";
        this.filename = result[2] || "";
        this.extension = result[3] || "";
        this.filenameWithoutExtension = this.filename.replaceAll(this.extension, "");
        this.fullPath = fullPath;

        this.hasExtension = function () {
            if (Bifrost.isNullOrUndefined(self.extension)) {
                return false;
            }
            if (self.extension === "") {
                return false;
            }
            return true;
        };
    })
});
Bifrost.Path.makeRelative = function (fullPath) {
    if (fullPath.indexOf("/") === 0) {
        return fullPath.substr(1);
    }

    return fullPath;
};
Bifrost.Path.getPathWithoutFilename = function (fullPath) {
    var lastIndex = fullPath.lastIndexOf("/");
    return fullPath.substr(0, lastIndex);
};
Bifrost.Path.getFilename = function (fullPath) {
    var lastIndex = fullPath.lastIndexOf("/");
    return fullPath.substr(lastIndex+1);
};
Bifrost.Path.getFilenameWithoutExtension = function (fullPath) {
    var filename = this.getFilename(fullPath);
    var lastIndex = filename.lastIndexOf(".");
    return filename.substr(0,lastIndex);
};
Bifrost.Path.hasExtension = function (path) {
    if (path.indexOf("?") > 0) {
        path = path.substr(0, path.indexOf("?"));
    }
    var lastIndex = path.lastIndexOf(".");
    return lastIndex > 0;
};
Bifrost.Path.changeExtension = function (fullPath, newExtension) {
    var path = Bifrost.Path.create({ fullPath: fullPath });
    var newPath = path.directory + path.filenameWithoutExtension + "." + newExtension;
    return newPath;
};
