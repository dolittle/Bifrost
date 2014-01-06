Bifrost.namespace("Bifrost.views", {
	ObjectModelElementVisitor: Bifrost.views.ElementVisitor.extend(function() {
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

			// Example : 
			// <
		};
	})
});