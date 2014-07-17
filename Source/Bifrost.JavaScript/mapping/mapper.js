Bifrost.namespace("Bifrost.mapping", {
	mapper: Bifrost.Type.extend(function () {
		"use strict";
		var self = this;

		function copyProperties (from, to) {
			for (var property in from){
			    if (typeof to[property] !== "undefined") {
			        if (Bifrost.isObject(to[property])) {
			            copyProperties(from[property], to[property]);
			        } else {
			            var value = from[property];
			            if (!Bifrost.isNullOrUndefined(to[property]) &&
                            to[property].constructor === Date) {
			                value = new Date(value);
			            }
			            to[property] = value;
					}
				}
			}
		}

		function mapSingleInstance(type, data) {
		    if (data) {
		        if (!Bifrost.isNullOrUndefined(data._sourceType)) {
		            type = eval(data._sourceType);
		        }
		    }

		    var instance = type.create();

		    if (data) {
		        copyProperties(data, instance);
		    }
		    return instance;
		};

		function mapMultipleInstances(type, data) {
		    var mappedInstances = [];
		    for (var i = 0; i < data.length; i++) {
		        var singleData = data[i];
		        mappedInstances.push(mapSingleInstance(type, singleData));
		    }
		    return mappedInstances;
		};

		this.map = function(type, data) {
			if(Bifrost.isArray(data)){
				return mapMultipleInstances(type, data);
			} else {
				return mapSingleInstance(type, data);
			}
		};

		this.mapToInstance = function (targetType, data, target) {

		};
	})
});
Bifrost.WellKnownTypesDependencyResolver.types.mapper = Bifrost.mapping.mapper;