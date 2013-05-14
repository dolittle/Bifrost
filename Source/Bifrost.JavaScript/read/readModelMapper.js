Bifrost.namespace("Bifrost.read", { 
	readModelMapper : Bifrost.Singleton(function () {
		"use strict";
		var self = this;

		function copyProperties (from, to) {
			for (var prop in from){
				if (typeof to[prop] !== "undefined" && typeof to[prop] === typeof from[prop]){
					if(Object.prototype.toString.call( to[prop] )  === "[object Object]"){
						copyProperties(from[prop], to[prop]);
					} else {
						to[prop] = from[prop];
					}

				}
			}
		}

		function mapSingleInstance(readModel, data){
			var instance = readModel.create();
			copyProperties(data, instance);
			return instance;
		}

		function mapMultipleInstances(readModel, data){
			var mappedInstances = [];
			for (var i = 0; i < data.length; i++) {
				var singleData = data[i];
				mappedInstances.push(mapSingleInstance(readModel, singleData));
			}
			return mappedInstances;
		}

		this.mapDataToReadModel = function(readModel, data) {
			if(Object.prototype.toString.call(data) === "[object Array]"){
				return mapMultipleInstances(readModel, data);
			} else {
				return mapSingleInstance(readModel, data);
			}
		};



	})
});