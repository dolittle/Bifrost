Bifrost.namespace("Bifrost.io", {
    fileFactory: Bifrost.Singleton(function () {
        /// <summary>Represents a factory for creating instances of Bifrost.io.File</summary>
        this.create = function (path, fileType) {
            /// <summary>Creates a new file</summary>
            /// <param name="path" type="String">Path of file</param>
            /// <param name="fileType" type="Bifrost.io.fileType" optional="true">Type of file to use</param>
            /// <returns type="Bifrost.io.File">An instance of a file</returns>

            var file = Bifrost.io.File.create({ path: path });
            if (!Bifrost.isNullOrUndefined(fileType)) {
                file.fileType = fileType;
            }
            return file;
        };
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.fileFactory = Bifrost.io.fileFactory;