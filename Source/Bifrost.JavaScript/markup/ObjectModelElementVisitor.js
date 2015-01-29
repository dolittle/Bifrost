Bifrost.namespace("Bifrost.markup", {
    ObjectModelElementVisitor: Bifrost.markup.ElementVisitor.extend(function (elementNaming, namespaces, objectModelFactory, propertyExpander, UIElementPreparer, attributeValues) {
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
            //  - registering a namespace can be done on any tag by adding xmlns:name="point to JS namespace"
            //  - If one registers a namespace with a prefix a parent already has and no naming root sits in between, 
            //    it should add the namespace target on the same definition
            //  - Naming roots are important - if there occurs a naming root, everything is relative to that and 
            //    breaking any "inheritance"
            // Properties : 
            //  - Attributes on an element is a property
            //  - Values in property should always go through type conversion sub system
            //  - Values with encapsulated in {} should be considered markup extensions, go through 
            //    markup extension system for resolving them and then pass on the resulting value 
            //    to type conversion sub system
            //  - Properties can be set with tag suffixed with .<name of property> - more than one
            //    '.' in a tag name should throw an exception
            // Value Provider :
            //  - Any value escaped with {{ }} should be considered a value provider
            // Value Consumer :
            //  - In the opposite end of a value sits a consumer. If the target property is a consumer, pass this
            //    in to the value provider. If the property is just a regular property, use the default property 
            //    value consumer
            // Dependency Properties
            //  - A property type that has the ability of notifying something when it changes
            //    Typically a property gets registered with the ability to offer a callback
            //    Dependency properties needs to be explicitly setup
            //  - Attached dependency properties - one should be able to attach dependency properties 
            //    Adding new functionality to an existing element through exposing new properties on
            //    existing elements. It does not matter what elements, it could be existing ones.
            //    The attached dependency property defines what it is for by specifying a type. Once
            //    we're matching a particular dependency property in the markup with the type it supports
            //    its all good
            // Services
            //  - Nodes should have the ability to specify a service that is relevant for the node.
            //    The service will be called with the element itself. It is defined as an attribute on
            //    a node, any values in the attribute will be handed in, obviously resolved through
            //    the value provider system.
            // Child tags :
            //  - Children which are not a property reference are only allowed if a content or
            //    items property exist. There can only be one of the other, two of either or both
            //    at the same time should yield an exception
            // Templating :
            //  - If a UIElement is found, it will need to be instantiated
            //  - If the instance is of a Control type - we will look at the 
            //    ControlTemplate property for its template and use that to replace content
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
            // Using a markup extension:
            // <ns:somecontrol somevalue="{{binding property}}">
            // <ns:somecontrol
            //
            // <span>{{binding property}}</span>
            //
            // <ns:somecontrol>
            //    <ns:somecontrol.property>{{binding property}}</ns:somcontrol.property>
            // </ns:somecontrol>

            namespaces.expandNamespaceDefinitions(element);

            if (element.isKnownType()) {
                attributeValues.expandFor(element);
                return;
            }

            var localName = elementNaming.getLocalNameFor(element);
            var namespaceDefinition = namespaces.resolveFor(element);
            objectModelFactory.createFrom(element, localName, namespaceDefinition,
                function (instance) {
                    propertyExpander.expand(element, instance);
                    UIElementPreparer.prepare(element, instance);
                },
                function () {
                }
            );

        };
    })
});