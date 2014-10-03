Bifrost.namespace("Web.HumanResources.Employees", {
    RegionDescriptor: Bifrost.views.RegionDescriptor.extend(function () {
        var self = this;

        this.describe = function (region) {
            region.koolShit = "hello";
        };
    })
});