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

    var regionDescriptorManager = {
        describe: function () {
            return {
                continueWith: function (callback) {
                    callback();
                }
            }
        }
    };

    var regionReturned = null;
    var regionType = null;
    var namespaceMappersType = null;

    beforeEach(function () {
        regionType = Bifrost.views.Region;
        Bifrost.views.Region = function () { };
        namespaceMappersType = Bifrost.namespaceMappers;
        Bifrost.namespaceMappers = {
            mapPathToNamespace: function () { return null; }
        };

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
        expect(regionReturned.__proto__).not.toBeNull();
    });
});