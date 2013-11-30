describe("when getting for an element without region and parent not having region", function () {
    var element = {
        DOM:"element"
    };

    var view = {
        element: element,
        path: ""
    };

    var elementSetRegionOn = null;
    var regionSet = null;
    var setRegionOnCalled = false;


    var documentService = {
        hasOwnRegion: sinon.mock().withArgs(element).returns(false),
        getParentRegionFor: sinon.mock().withArgs(element).returns(null),
        setRegionOn: function (e, r) {
            if (e == element) {
                setRegionOnCalled = true;
            }
            elementSetRegionOn = element;
            regionSet = r;
        }
    };

    var topLevelDescribed = null;

    var regionDescriptorManager = {
        describe: function () {
            return {
                continueWith: function (callback) {
                    callback();
                }
            }
        },
        describeTopLevel: function(topLevel) { topLevelDescribed = topLevel; }
    };

    var regionReturned = null;
    var regionType = null;
    var namespaceMappersType = null;

    var messengerFactory = { messenger: "factory" };
    var operationsFactory = { operations: "factory" };
    var tasksFactory = { tasks: "factory" };

    beforeEach(function () {
        regionType = Bifrost.views.Region;
        Bifrost.views.Region = function (messengerFactory, operationsFactory, tasksFactory) {
            this.messengerFactory = messengerFactory;
            this.operationsFactory = operationsFactory;
            this.tasksFactory = tasksFactory;
        };
        namespaceMappersType = Bifrost.namespaceMappers;
        Bifrost.namespaceMappers = {
            mapPathToNamespace: function () { return null; }
        };

        var instance = Bifrost.views.regionManager.createWithoutScope({
            documentService: documentService,
            regionDescriptorManager: regionDescriptorManager,
            messengerFactory: messengerFactory,
            operationsFactory: operationsFactory,
            tasksFactory: tasksFactory
        });
        
        instance.getFor(view).continueWith(function (instance) {
            regionReturned = instance;
        });
    });
    
    afterEach(function () {
        Bifrost.views.Region = regionType;
    });

    it("should set region on the element", function () {
        expect(regionSet).toBe(regionReturned);
    });

    it("should create a new region", function () {
        expect(regionReturned).not.toBeNull();
    });

    it("should set the parent region as prototype", function () {
        expect(regionReturned.__proto__).not.toBeNull();
    });

    it("should describe the top level", function () {
        expect(topLevelDescribed).toBe(regionReturned.__proto__);
    });

    it("should pass along the messenger factory", function () {
        expect(regionReturned.messengerFactory).toBe(messengerFactory);
    });

    it("should pass along the operations factory", function () {
        expect(regionReturned.operationsFactory).toBe(operationsFactory);
    });

    it("should pass along the tasks factory", function () {
        expect(regionReturned.tasksFactory).toBe(tasksFactory);
    });
});