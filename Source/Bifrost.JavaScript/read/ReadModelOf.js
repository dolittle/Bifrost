Bifrost.namespace("Bifrost.read", {
	ReadModelOf: Bifrost.Type.extend(function() {
	    var self = this;
	    this.name = "";
	    this.target = null;
	    this.readModelType = Bifrost.Type.extend(function () { });

		this.instance = ko.observable();

		this.instanceMatching = function (propertyFilters) {
		    var methodParameters = {
		        descriptor: JSON.stringify({
		            readModel: self.target.name,
		            propertyFilters: propertyFilters
		        })
		    };

		    $.ajax({
		        url: "/Bifrost/ReadModel/InstanceMatching",
		        type: 'POST',
		        dataType: 'json',
		        data: JSON.stringify(methodParameters),
		        contentType: 'application/json; charset=utf-8',
		        complete: function (result) {
		            var item = $.parseJSON(result.responseText);
		            self.instance(item);
		        }
		    });
		};


		this.onCreated = function (lastDescendant) {
		    self.target = lastDescendant;
		};
	})
});