describe("when loading and view has no viewmodel", function () {
    var task = {
        some:"task"
    };
    var viewPath = "some/view.html";

    var view = {
        some:"view"
    };

    var file = {
        path: viewPath
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
        hasForView: sinon.stub().returns(false),
        isLoaded: sinon.stub().returns(false),
    };

    var taskFactory = {
        createViewLoad: sinon.mock().withArgs([file]).returns(task).once()
    };


    var fileFactory = {
        create: sinon.mock().withArgs(viewPath, Bifrost.io.fileType.html).once().returns(file)
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

    it("should create a file for the view", function () {
        expect(fileFactory.create.called).toBe(true);
    });

    it("should create a view load task for the view file", function () {
        expect(taskFactory.createViewLoad.called).toBe(true);
    });

    it("should execute the task in the region", function () {
        expect(region.tasks.execute.called).toBe(true);
    });

    it("should pass the view along through the promise returned", function () {
        expect(viewReceivedMock.called).toBe(true);
    })
});
