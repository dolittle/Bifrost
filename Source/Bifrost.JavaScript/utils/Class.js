Bifrost.namespace("Bifrost", {
	Class : function(typeDefinition) {
		
		if( typeDefinition == null || typeof typeDefinition == "undefined" ) {
			throw new Bifrost.MissingClassDefinition();
		}
		if( typeof typeDefinition === "object") { 
			throw new Bifrost.ObjectLiteralNotAllowed();
		}
		
		var result = function() {
			typeDefinition.prototype = Bifrost.ClassPrototype;
			this.typeDefinition = typeDefinition;
		}
		result.prototype = Bifrost.ClassInfo;
		
		return new result();
	}
});

