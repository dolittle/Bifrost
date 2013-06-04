Bifrost.namespace("Bifrost.read", {
	ReadModelOf: Bifrost.Type.extend(function(readModelMapper) {
	    var self = this;
	    this.name = "";
	    this.target = null;
	    this.readModelType = Bifrost.Type.extend(function () { });
	    this.instance = ko.observable();
	    this.commandToPopulate = null;

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
					var mappedReadModel = readModelMapper.mapDataToReadModel(self.target.readModel, data);
		            self.instance(mappedReadModel);
		        }
		    });
		};

		this.populateCommandOnChanges = function (command) {
		    command.populatedExternally();

		    if (typeof self.instance() != "undefined" && self.instance() != null) {
		        command.setPropertyValuesFrom(self.instance());
		    }

		    self.instance.subscribe(function (newValue) {
		        command.setPropertyValuesFrom(newValue);
		    });
		};

		this.onCreated = function (lastDescendant) {
		    self.target = lastDescendant;
		};
	})
});