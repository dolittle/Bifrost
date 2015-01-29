given("an object model element visitor", function () {
    this.element_naming = {
        getLocalNameFor: sinon.stub()
    };
    this.namespaces = {
        expandNamespaceDefinitions: sinon.stub(),
        resolveFor: sinon.stub()
    };
    this.object_model_factory = {
        createFrom: sinon.stub()
    };
    this.property_expander = {
        expand: sinon.stub()
    };
    this.ui_element_preparer = {
        prepare: sinon.stub()
    };

    this.attribute_values = {
        expandFor: sinon.stub()
    };

    this.object_model_element_visitor = Bifrost.markup.ObjectModelElementVisitor.create({
        elementNaming: this.element_naming,
        namespaces: this.namespaces,
        objectModelFactory: this.object_model_factory,
        propertyExpander: this.property_expander,
        UIElementPreparer: this.ui_element_preparer,
        attributeValues: this.attribute_values
    });
});