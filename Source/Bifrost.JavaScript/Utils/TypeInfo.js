function TypeInfo(obj) {
	var target = obj;

	try {
		var funcNameRegex = /function (.{1,})\(/;
		var results = (funcNameRegex).exec((target).constructor.toString());
		this.name = (results && results.length > 1) ? results[1] : "";
	} catch( e ) {
		this.name = "unknown";
	}
}

Bifrost.namespace("Bifrost", {
	TypeInfo: {
		create : function() {
			if( typeof this.typeDefinition === "undefined" ) {
				throw new Bifrost.MissingTypeDefinition();
			}
			var dependencies = Bifrost.functionParser.parse(this.typeDefinition);
			if( dependencies.length == 0 ) {
				return new this.typeDefinition();
			} else {
				
				// A little note to self for how this should come together : 
				// - Add a options parameter to create so that we can hand it dependencies manually - nice for testing
				// - Add greater flexibility to solving - not only require
				// 		- require being one solver
				//		- namespace solving
				
				var resolvedDependencies = [];
				var a = this.typeDefinition;
				resolvedDependencies.push(a);
				$.each(dependencies, function(index, dependency) {
					var resolvedDependency = require(dependency);
					resolvedDependencies.push(resolvedDependency);
				});
				return new (a.bind.apply(a,resolvedDependencies))();
			}
		},
		
		getFor: function(obj) {
			var typeInfo = new TypeInfo(obj);
			return typeInfo;
		}
	}
});