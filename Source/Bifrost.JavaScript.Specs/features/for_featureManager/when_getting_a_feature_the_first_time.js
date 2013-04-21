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
        Bifrost.features.Feature.createCalled = Bifrost.features.Feature.createCalled+1;
        return this;
    }
};


describe("when getting a feature the first time", function () {
    var feature = Bifrost.features.featureManager.get("someFeature");

    it("should create the feature", function () {
        expect(Bifrost.features.Feature.createCalled).toBe(1);
    });
});