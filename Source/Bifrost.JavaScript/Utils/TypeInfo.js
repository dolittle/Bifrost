Bifrost.namespace("Bifrost")
Bifrost.TypeInfo = (function() {
	function TypeInfo(obj) {
		var target = obj;

		this.initializeName = function() {
	   		var funcNameRegex = /function (.{1,})\(/;
	   		var results = (funcNameRegex).exec((target).constructor.toString());
	   		this.name = (results && results.length > 1) ? results[1] : "";
		}
		
		this.initializeName();
	}

	return {
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
