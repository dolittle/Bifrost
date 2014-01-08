Bifrost.namespace("Bifrost.views", {
	objectModelManager: Bifrost.Singleton(function() {
		var self = this;
		this.globalNamespacePrefix = "__global";

		this.prefixNamespaceArrayDictionary = {};

		this.registerNamespace = function(prefix, namespace) {
			var ns = Bifrost.namespace(namespace);
			var array;

			if( self.prefixNamespaceArrayDictionary.hasOwnProperty(prefix) ) {
				array = self.prefixNamespaceArrayDictionary[prefix];
			} else {
				array = [];
				self.prefixNamespaceArrayDictionary[prefix] = array;
			}

			self.prefixNamespaceDictionary[prefix] = namespace;
		};


		this.getObjectFromTagName =  function(name, namespace) {
			var hasNamespace = true;
			if( Bifrost.isNullOrUndefined(namespace) ) {
				namespace = self.globalNamespacePrefix;
				hasNamespace = false;
			}
			namespace = namespace.toLowerCase();
			name = name.toLowerCase();

			var foundType = null;

			if( self.prefixNamespaceArrayDictionary.hasOwnProperty(namespace) ) {
				self.prefixNamespaceArrayDictionary[namespace].forEach(function(ns) {
					for( var type in ns ) {
						type = type.toLowerCase();
						if( type == name ) {
							foundType = type;
							return;
						}
					}
				})
			}

			if( foundType == null ) {
				var namespaceMessage = "";
				if( hasNamespace == true ) {
					namespaceMessage = " in namespace prefixed '"+namespace+"'";
				}
				throw "Could not resolve type '"+name+"'"+namespaceMessage;
			}

			return foundType;
		};
	})
});