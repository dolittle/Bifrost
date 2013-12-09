Bifrost.namespace("Bifrost.io", {
    File: Bifrost.Type.extend(function (path) {
        this.type = Bifrost.io.fileType.unknown;
        this.path = Bifrost.Path.create({ fullPath: path });
    })
});