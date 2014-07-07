Bifrost.namespace("Bifrost.types", {
    TypeInfo: Bifrost.Type.extend(function () {

        this.number = ko.observable(42);
        this.string = "";
        this.customType = customType.create();

        this.blah = {
            get: function () {

            },
            set: function (value) {

            },
            type: number
        };
    })
});
Bifrost.types.TypeInfo.createFrom = function (type) {

};
