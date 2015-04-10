describe("when visiting known element", given("an object model element visitor", function () {
    var context = this;
    var element;

    beforeEach(function () {
        element = document.createElement("div");

        (function becauseOf() {
            context.object_model_element_visitor.visit(element);
        })();
    });

    it("should expand namespace definitions", function () {
        expect(context.namespaces.expandNamespaceDefinitions.calledWith(element)).toBe(true);
    });

    it("should not get local name for element", function () {
        expect(context.element_naming.getLocalNameFor.called).toBe(false);
    });

    it("should not resolve namespace for element", function () {
        expect(context.namespaces.resolveFor.called).toBe(false);
    });

    it("should not create instance of object model", function () {
        expect(context.object_model_factory.createFrom.called).toBe(false);
    });

    it("should not expand properties", function () {
        expect(context.property_expander.expand.called).toBe(false);
    });

    it("should not prepare the element", function () {
        expect(context.ui_element_preparer.prepare.called).toBe(false);
    });

    it("should ensure binding context", function () {
        expect(context.binding_context_manager.ensure.calledWith(element)).toBe(true);
    });

    it("should expand attribute values", function () {
        expect(context.attribute_values.expandFor.calledWith(element)).toBe(true);
    });
}));