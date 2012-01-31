Bifrost.namespace("Bifrost.features");
Bifrost.features.Container = (function () {
    function Container(name) {
        this.name = name;
        var self = this;

        this.isRoot = function () {
            return self.name === "root";
        }

        this.getBasePath = function (isAdministration) {
            var path = isAdministration ? "/administration" : "/features";
            if (self.isRoot()) {
                return path;
            }
            return path + "/" + self.name;
        }

        this.addFeature = function (isAdministration, name, loaded) {
            var view = "view"
            var viewModel = "viewModel";

            var path = self.getBasePath(isAdministration);

            if (!self.isRoot()) {
                view = name;
                viewModel = name;
            } else {
                path = path + "/" + name;
            }

            var feature = Bifrost.features.Feature.create(name, view, viewModel, path);
            self[name] = feature;
            feature.load(loaded);
        }
    }

    Bifrost.extend(Bifrost.features, new Container("root"));
    Bifrost.features.addOrGetContainer = function (name, isAdministration) {
        if (Bifrost.features[name] != undefined) {
            return Bifrost.features[name];
        }
        var container = new Container(name, isAdministration);
        Bifrost.features[name] = container;
        return container;
    }

    return {
        create: function (name) {
            var container = new Container(name);
            return container;
        }
    }

})();