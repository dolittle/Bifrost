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

		this.mapInstance = function(readModel, data) {
			var instance = readModel.create();
			copyProperties(data, instance);
			return instance;
		};

	})
});