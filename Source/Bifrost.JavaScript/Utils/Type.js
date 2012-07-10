Bifrost.namespace("Bifrost", {
	Type : function(typeDefinition) {
		
		if( typeDefinition == null || typeof typeDefinition == "undefined" ) {
			throw new Bifrost.MissingClassDefinition();
		}
		if( typeof typeDefinition === "object") { 
			throw new Bifrost.ObjectLiteralNotAllowed();
		}
		
		var result = function() {
			typeDefinition.prototype = Bifrost.TypePrototype;
			this.typeDefinition = typeDefinition;
		}
		result.prototype = Bifrost.TypeInfo;
		
		return new result();
	}
});

