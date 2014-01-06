Bifrost.namespace("Bifrost.views", {
	ObjectModelElementVisitor: Bifrost.views.ElementVisitor.extend(function(objectModelManager, markupExtensions, typeConverters) {
		this.visit = function(element, actions) {
			// Tags : 
			//  - tag names automatically match type names
			//  - due to tag names in HTML elements being without case - they become lower case in the
			//    localname property, we will have to search for type by lowercase
			//  - multiple types found with different casing in same namespace should throw an exception
			// Namespaces :
			//  - split by ':'
			//  - if more than one ':' - throw an exception
			//  - if no namespace is defined, try to resolve in the global namespace
			//  - namespaces in the object model can point to multiple JavaScript namespaces
			//  - multiple types with same name in namespace groupings should throw an exception
			// Properties : 
			//  - Attributes on an element is a property
			//  - Values in property should always go through type conversion sub system
			//  - Values with encapsulated in {} should be considered markup extensions, go through 
			//    markup extension system for resolving them and then pass on the resulting value 
			//    to type conversion sub system
			//  - Properties can be set with tag suffixed with .<name of property> - more than one
			//    '.' in a tag name should throw an exception
			// Child tags :
			//  - Children which are not a property reference are only allowed if a content or
			//    items property exist. There can only be one of the other, two of either or both
			//    at the same time should yield an exception
			//
			// Example : 
			// Simple control:
			// <somecontrol property="42"/>
			// 
			// Control in different namespace:
			// <ns:somecontrol property="42"/>
			//
			// Assigning property with tags:
			// <ns:somecontrol>
			//    <ns:somecontrol.property>42</ns:somcontrol.property>
			// </ns:somecontrol>
			// 

			var namespace;
			var name = element.localName.toLowerCase();

			var namespaceSplit = name.split(":");
			if( namespaceSplit.length > 2 ) {
				throw "Syntax error: tagname '"+name+"' has multiple namespaces";
			}
 			if( namespaceSplit.length == 2 ) {
				name = namespaceSplit[1];
				namespace = namespaceSplit[0];
			}

			var instance = objectModelManager.getObjectFromTagName(name,namespace);
			for( var attributeIndex=0; attributeIndex<element.attributes.length; attributeIndex++ ) {
				var name = element.attributes[attributeIndex].localName;
				var value = element.attributes[attributeIndex].value;

				if( name in instance ) {
					var convertedValue = typeConverters.convert(typeof instance[name], value);
					instance[name] = convertedValue;
				}
			}

		};
	})
});