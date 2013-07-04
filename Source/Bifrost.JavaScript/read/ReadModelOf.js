Bifrost.namespace("Bifrost.read", {
	ReadModelOf: Bifrost.Type.extend(function(readModelMapper) {
	    var self = this;
	    this.name = "";
	    this.generatedFrom = "";
	    this.target = null;
	    this.readModelType = Bifrost.Type.extend(function () { });
	    this.instance = ko.observable();
	    this.commandToPopulate = null;

		this.instanceMatching = function (propertyFilters) {
		    var methodParameters = {
		        descriptor: JSON.stringify({
		            readModel: self.target.name,
                    generatedFrom: self.target.generatedFrom,
		            propertyFilters: propertyFilters
		        })
		    };

		    $.ajax({
		        url: "/Bifrost/ReadModel/InstanceMatching?_rm=" + self.target.generatedFrom,
		        type: 'POST',
		        dataType: 'json',
		        data: JSON.stringify(methodParameters),
		        contentType: 'application/json; charset=utf-8',
		        complete: function (result) {
		            var item = $.parseJSON(result.responseText);
					var mappedReadModel = readModelMapper.mapDataToReadModel(self.target.readModelType, item);
		            self.instance(mappedReadModel);
		        }
		    });
		};

		this.populateCommandOnChanges = function (command) {
		    command.populatedExternally();

		    if (typeof self.instance() != "undefined" && self.instance() != null) {
		        command.populateFromExternalSource(self.instance());
		    }

		    self.instance.subscribe(function (newValue) {
		        command.populateFromExternalSource(newValue);
		    });
		};

		this.onCreated = function (lastDescendant) {
		    self.target = lastDescendant;
		};
	})
});