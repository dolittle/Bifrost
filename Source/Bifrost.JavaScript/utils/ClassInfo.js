Bifrost.namespace("Bifrost", {
	ClassInfo: {
		create : function() {
			if( typeof this.typeDefinition === "undefined" ) {
				throw new Bifrost.MissingTypeDefinition();
			}
			var dependencies = Bifrost.functionParser.parse(this.typeDefinition);
			if( dependencies.length == 0 ) {
				return new this.typeDefinition();
			} else {
				
			}
			
		}
	}
});