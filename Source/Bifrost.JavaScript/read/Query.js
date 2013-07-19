Bifrost.namespace("Bifrost.read", {
    Query: Bifrost.Type.extend(function () {
        var self = this;
        this.name = "";
        this.target = this;
        this.generatedFrom = "";
        this.readModel = null;

        this.areAllParametersSet = null;

        this.hasReadModel = function () {
            return typeof self.target.readModel != "undefined" && self.target.readModel != null;
        };

        this.setParameters = function (parameters) {
            try {
                for (var property in parameters) {
                    if (self.target.hasOwnProperty(property) && ko.isObservable(self.target[property]) == true) {
                        self.target[property](parameters[property]);
                    }
                }
            } catch(ex) {}
        };

        this.getParameters = function () {
            var parameters = {};

            for (var property in self.target) {
                if (ko.isObservable(self.target[property]) &&
                    property != "areAllParametersSet") {
                    parameters[property] = self.target[property];
                }
            }

            return parameters;
        };

        this.getParameterValues = function () {
            var parameterValues = {};

            var parameters = self.getParameters();
            for (var property in parameters) {
                parameterValues[property] = ko.utils.unwrapObservable(parameters[property]);
            }

            return parameterValues;
        };

        this.all = function () {
            var queryable = Bifrost.read.Queryable.new({
                query: self.target
            });
            return queryable;
        };

        this.paged = function (pageSize, pageNumber) {
            var queryable = Bifrost.read.Queryable.new({
                query: self.target
            });
            queryable.setPageInfo(pageSize, pageNumber);
            return queryable;
        };

        

        this.onCreated = function (query) {
            self.target = query;

            self.areAllParametersSet = ko.computed(function () {
                var isSet = true;
                var hasParameters = false;
                for (var property in self.target) {
                    if (ko.isObservable(self.target[property]) == true) {
                        hasParameters = true;
                        var value = self.target[property]();
                        if (typeof value == "undefined" || value === null) {
                            isSet = false;
                            break;
                        }
                    }
                }
                if (hasParameters == false) return true;
                return isSet;
            });
        };
    })
});