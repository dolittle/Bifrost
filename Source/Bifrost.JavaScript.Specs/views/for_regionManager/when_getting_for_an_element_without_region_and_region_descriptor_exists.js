describe("when getting for an element without region and region descriptor exists", function () {
    var region = {
        existing: "region",
        children: []
    };

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
        getParentRegionFor: sinon.mock().withArgs(element).returns(region),
        setRegionOn: function (e, r) {
            if (e == element) {
                setRegionOnCalled = true;
            }
            elementSetRegionOn = element;
            regionSet = r;
        }
    };

    var regionDescriptorManager = {
        describe: function () {
            return {
                continueWith: function (callback) {
                    callback();
                }
            }
        }
    };

    var descriptor = {
        describe: sinon.stub()
    };

    var regionReturned = null;
    var regionType = null;
    var namespaceMappersType = null;
    var dependencyResolverType = null;

    var messengerFactory = { messenger: "factory" };
    var operationsFactory = { operations: "factory" };
    var tasksFactory = { tasks: "factory" };

    beforeEach(function () {
        regionType = Bifrost.views.Region;
        Bifrost.views.Region = function (messengerFactory, operationsFactory, tasksFactory) {
            this.messengerFactory = messengerFactory;
            this.operationsFactory = operationsFactory;
            this.tasksFactory = tasksFactory;
            this.view = ko.observable();
        };

        var instance = Bifrost.views.regionManager.createWithoutScope({
            documentService: documentService,
            regionDescriptorManager: regionDescriptorManager,
            messengerFactory: messengerFactory,
            operationsFactory: operationsFactory,
            tasksFactory: tasksFactory
        });
        
        regionReturned = instance.getFor(view);
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
        expect(regionReturned.__proto__).toBe(region);
    });

    it("should add the new region as a child to the parent region", function () {
        expect(region.children[0]).toBe(regionReturned);
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

    it("should set the view on the region", function () {
        expect(regionReturned.view()).toBe(view);
    });
});