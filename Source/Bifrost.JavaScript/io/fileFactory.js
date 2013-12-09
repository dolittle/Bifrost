Bifrost.namespace("Bifrost.io", {
    fileFactory: Bifrost.Singleton(function () {
        this.create = function (path, fileType) {
            var file = Bifrost.io.File.create({ path: path });
            if (!Bifrost.isNullOrUndefined(fileType)) {
                file.fileType = fileType;
            }
            return file;
        };
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.fileFactory = Bifrost.io.fileFactory;