Bifrost.namespace("Bifrost.QuickStart.Features.Employees", {
    RegionDescriptor: Bifrost.views.RegionDescriptor.extend(function () {
        var self = this;

        this.describe = function (region) {
            region.koolShit = "hello";
        };
    })
});