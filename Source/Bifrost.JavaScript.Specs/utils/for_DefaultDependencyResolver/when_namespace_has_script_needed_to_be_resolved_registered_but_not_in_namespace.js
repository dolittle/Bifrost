describe("when requested dependency is a valid script in namespace, but not registered in Bifrost namespace", function () {
    var resolver = new Bifrost.DefaultDependencyResolver();
    var ns;
    var resolved = null;
    var actualResolved = null;
    var requireStub;
    var requireArg;
    var canResolve;
    var systemResolved = {
        someValue: "value"
    };
    var fileFactory = null;
    var fileManager = null;

    var file = {
        some: "file"
    };
    
    var fileFactoryMock = {
        create: sinon.mock().withArgs("/Someplace/On/Server/something.js", Bifrost.io.fileType.javaScript).returns(file)
    };
    var fileManagerMock = {
        load: sinon.mock().withArgs([file]).returns({
            continueWith: function (callback) {
                callback([systemResolved]);
            }
        })
    };

    beforeEach(function () {
        ns = {
            _path: "/Someplace/On/Server",
            _scripts: ["something"]
        };

        fileFactory = Bifrost.io.fileFactory;
        fileManager = Bifrost.io.fileManager;

        Bifrost.io.fileFactory = {
            create: sinon.stub().returns(fileFactoryMock)
        };

        Bifrost.io.fileManager = {
            create: sinon.stub().returns(fileManagerMock)
        }
        
        canResolve = resolver.canResolve(ns, "something");
        resolved = resolver.resolve(ns, "something");
        resolved.continueWith(function (arg, nextPromise) {
            actualResolved = arg;
        });
    });

    afterEach(function () {
        Bifrost.io.fileFactory = fileFactory;
        Bifrost.io.fileManager = fileManager;
    });

    it("should be able to resolve", function () {
        expect(canResolve).toBe(true);
    });

    it("should create a file", function () {
        expect(fileFactoryMock.create.called).toBe(true);
    });

    it("should return a promise", function () {
        expect(resolved instanceof Bifrost.execution.Promise).toBe(true);
    });

    it("should resolve system loaded into namespace", function () {
        expect(actualResolved).toBe(systemResolved);
    });
});