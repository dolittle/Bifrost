describe("when loading and view has viewmodel but not in memory", function () {
    var task = {
        some:"task"
    };
    var viewPath = "some/view.html";
    var viewModelPath = "some/viewModel.js"

    var view = {
        some:"view"
    };

    var viewFile = {
        path: viewPath
    };

    var viewModelFile = {
        path: viewModelPath
    };

    var region = {
        tasks: {
            execute: sinon.mock().withArgs(task).returns({
                continueWith: function (callback) {
                    callback(view);
                }
            })
        }
    };

    var viewModelManager = {
        hasForView: sinon.mock().withArgs(viewPath).returns(true),
        isLoaded: sinon.stub().returns(false),
        getViewModelPathForView: sinon.stub().returns(viewModelPath)
    };

    var taskFactory = {
        createViewLoad: sinon.mock().withArgs([viewFile, viewModelFile]).returns(task).once()
    };


    var fileFactory = {
        create: function (path, type) {
            if (path == viewPath && type == Bifrost.io.fileType.html) return viewFile;
            if (path == viewModelPath && type == Bifrost.io.fileType.javaScript) return viewModelFile;
            return null;
        }
    };

    var regionManager = {
        getCurrent: sinon.stub().returns(region)
    };
    

    var viewLoader = Bifrost.views.viewLoader.createWithoutScope({
        viewModelManager: viewModelManager,
        taskFactory: taskFactory,
        fileFactory: fileFactory,
        regionManager: regionManager
    });

    var viewReceivedMock = sinon.mock().withArgs(view);
    viewLoader.load(viewPath).continueWith(viewReceivedMock);    

    it("should load both the view and the viewModel", function () {
        expect(taskFactory.createViewLoad.called).toBe(true);
    });

    it("should execute the task in the region", function () {
        expect(region.tasks.execute.called).toBe(true);
    });

    it("should pass the view along through the promise returned", function () {
        expect(viewReceivedMock.called).toBe(true);
    })
});
