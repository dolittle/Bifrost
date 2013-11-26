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

    beforeEach(function () {
        regionType = Bifrost.views.Region;
        Bifrost.views.Region = function () { };

        var instance = Bifrost.views.regionManager.createWithoutScope({
            documentService: documentService,
            regionDescriptorManager: regionDescriptorManager
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
        expect(regionReturned.__proto__).toBe(region);
    });

    it("should add the new region as a child to the parent region", function () {
        expect(region.children[0]).toBe(regionReturned);
    });
});