Bifrost.namespace("Bifrost.read", {
    Query: Bifrost.Type.extend(function () {
        var self = this;
        this.name = "";
        this.target = this;

        this.areAllParametersSet = null;

        this.setParameters = function (parameters) {
            try {
                for (var property in parameters) {
                    if (self.target.hasOwnProperty(property) && ko.isObservable(self.target[property]) == true) {
                        self.target[property](parameters[property]);
                    }
                }
            } catch(ex) {}
        };

        this.all = function () {
            var queryable = Bifrost.read.Queryable.create({
                query: self.target
            });
            return queryable;
        };

        this.paged = function (pageSize, pageNumber) {
            var queryable = Bifrost.read.Queryable.create({
                query: self.target
            });
            queryable.pageSize(pageSize);
            queryable.pageNumber(pageNumber);
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