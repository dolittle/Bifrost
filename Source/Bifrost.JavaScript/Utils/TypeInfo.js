Bifrost.namespace("Bifrost");
Bifrost.TypeInfo = (function() {
	function TypeInfo(obj) {
		var target = obj;

		this.initializeName = function() {
			try {
	   			var funcNameRegex = /function (.{1,})\(/;
	   			var results = (funcNameRegex).exec((target).constructor.toString());
	   			this.name = (results && results.length > 1) ? results[1] : "";
			} catch( e ) {
				this.name = "unknown";
			}
		}
		
		this.initializeName();
	}

	return {
		create : function() {
			if( typeof this.typeDefinition === "undefined" ) {
				throw new Bifrost.MissingTypeDefinition();
			}
			var dependencies = Bifrost.functionParser.parse(this.typeDefinition);
			if( dependencies.length == 0 ) {
				return new this.typeDefinition();
			} else {
				
			}
		},
		
		getFor: function(obj) {
			var typeInfo = new TypeInfo(obj);
			return typeInfo;
		}
	};
})();


// Object extensions
Object.prototype.getTypeInfo = function() { 
	return Bifrost.TypeInfo.getFor(this);
};
