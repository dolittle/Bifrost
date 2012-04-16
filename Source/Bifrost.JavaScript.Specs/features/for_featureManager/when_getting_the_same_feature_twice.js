Bifrost.features.featureMapper = (function () {
    return {
        getFeatureMappingFor: function (uri) {
            return {
                resolve: function (uri) {
                    return "";
                }
            }
        }
    }
})();

Bifrost.features.Feature = Bifrost.features.Feature || {
    createCalled: 0,
    create: function () {
        Bifrost.features.Feature.createCalled = Bifrost.features.Feature.createCalled + 1;
        return this;
    }
};

describe("when getting the same feature twice", function () {
    var firstInstance = Bifrost.features.featureManager.get("someFeature");
    var secondInstance = Bifrost.features.featureManager.get("someFeature");

    it("should only create once", function () {
        expect(Bifrost.features.Feature.createCalled).toBe(1);
    });
});