Bifrost.namespace("Bifrost.read", {
	readModelMapper : Bifrost.Type.extend(function () {
		"use strict";
		var self = this;

		function copyProperties (from, to) {
			for (var property in from){
			    if (typeof to[property] !== "undefined") {
			        if (Bifrost.isObject(to[property])) {
			            copyProperties(from[property], to[property]);
					} else {
			            to[property] = from[property];
					}
				}
			}
		}

		function mapSingleInstance(readModel, data) {
		    var instance = readModel.create();
		    copyProperties(data, instance);
		    return instance;
		};

		function mapMultipleInstances(readModel, data) {
		    var mappedInstances = [];
		    for (var i = 0; i < data.length; i++) {
		        var singleData = data[i];
		        mappedInstances.push(mapSingleInstance(readModel, singleData));
		    }
		    return mappedInstances;
		};

		this.mapDataToReadModel = function(readModel, data) {
			if(Bifrost.isArray(data)){
				return mapMultipleInstances(readModel, data);
			} else {
				return mapSingleInstance(readModel, data);
			}
		};
	})
});