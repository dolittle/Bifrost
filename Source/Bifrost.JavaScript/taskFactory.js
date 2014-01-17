Bifrost.namespace("Bifrost", {
    taskFactory: Bifrost.Singleton(function () {
        var self = this;

        this.createHttpPost = function (url, payload) {
            var task = Bifrost.tasks.HttpPostTask.create({
                url: url,
                payload: payload
            });
            return task;
        };

        this.createHttpGet = function (url, payload) {
            var task = Bifrost.tasks.HttpGetTask.create({
                url: url,
                payload: payload
            });
            return task;
        };

        this.createQuery = function (query, paging) {
            var task = Bifrost.read.QueryTask.create({
                query: query,
                paging: paging
            });
            return task;
        };

        this.createReadModel = function (readModelOf, propertyFilters) {
            var task = Bifrost.read.ReadModelTask.create({
                readModelOf: readModelOf,
                propertyFilters: propertyFilters
            });
            return task;
        };

        this.createHandleCommand = function (command) {
            var task = Bifrost.commands.HandleCommandTask.create({
                command: command
            });
            return task;
        };

        this.createHandleCommands = function (commands) {
            var task = Bifrost.commands.HandleCommandsTask.create({
                commands: commands
            });
            return task;
        };

        this.createViewLoad = function (files) {
            var task = Bifrost.views.ViewLoadTask.create({
                files: files
            });
            return task;
        };

        this.createViewModelLoad = function (files) {
            var task = Bifrost.views.ViewModelLoadTask.create({
                files: files
            });
            return task;
        };

        this.createViewModelApplier = function (view, masterViewModel) {
            var task = Bifrost.views.ViewModelApplierTask.create({
                view: view,
                masterViewModel: masterViewModel
            });
            return task;
        };


        this.createViewModelsApplier = function (root, masterViewModel) {
            var task = Bifrost.views.ViewModelsApplierTask.create({
                root: root,
                masterViewModel: masterViewModel
            });
            return task;
        };

        this.createViewRender = function (element) {
            var task = Bifrost.views.ViewRenderTask.create({
                element: element
            });
            return task;
        };

        this.createFileLoad = function (files) {
            var task = Bifrost.tasks.FileLoadTask.create({
                files: files
            });
            return task;
        };
    })
});