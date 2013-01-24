Bifrost.namespace("Bifrost.read", {
    Query: Bifrost.Type.extend(function () {
        var self = this;
        this.name = "";
        this.allObservable = ko.observableArray();

        this.all = function () {
            var methodParameters = {
                descriptor: JSON.stringify({
                    nameOfQuery: this.name,
                    parameters: {}
                })
            };

            $.ajax({
                url: "/Bifrost/Query/Execute",
                type: 'POST',
                dataType: 'json',
                data: JSON.stringify(methodParameters),
                contentType: 'application/json; charset=utf-8',
                complete: function (result) {
                    var items = $.parseJSON(result.responseText);
                    self.allObservable(items);
                }
            });

            return self.allObservable;
        }
    })
});