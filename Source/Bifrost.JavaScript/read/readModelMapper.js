Bifrost.namespace("Bifrost.read", { 
	readModelMapper : Bifrost.Type.extend(function () {
		var self = this;

		this.mapInstance = function(readModel, data) {
			var instance = readModel.create();

			instance.copyTo(data);

			return data;
		}

	})
})