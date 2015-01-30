describe("when visiting custom element", given("an object model element visitor", function () {
    var context = this;
    var element;

    var localName = "element";
    var namespaceDefinition = { prefix: "custom", targets: [] };
    var instance = {};

    var successStub = sinon.stub();


    beforeEach(function () {
        element = document.createElement("custom:element");

        // TODO: This is bleeding in support for running in a browser and in a test runner
        if (element.constructor.toString().indexOf("HTMLUnknownElement") < 0) {
            element.constructor = new HTMLUnknownElement();
        }

        context.element_naming.getLocalNameFor.returns(localName);
        context.namespaces.resolveFor.returns(namespaceDefinition);

        context.object_model_factory.createFrom = function () { };

        sinon.stub(context.object_model_factory, "createFrom", function (element, localName, namespaceDefinition, success, error) {
            success(instance)
        });

        (function becauseOf() {
            context.object_model_element_visitor.visit(element);
        })();
    });


    it("should expand namespace definitions", function () {
        expect(context.namespaces.expandNamespaceDefinitions.calledWith(element)).toBe(true);
    });

    it("should get local name for element", function () {
        expect(context.element_naming.getLocalNameFor.calledWith(element)).toBe(true);
    });

    it("should resolve namespace for element", function () {
        expect(context.namespaces.resolveFor.calledWith(element)).toBe(true);
    });

    it("should create instance of object model", function () {
        expect(context.object_model_factory.createFrom.calledWith(element, localName, namespaceDefinition)).toBe(true);
    });

    it("should expand properties", function () {
        expect(context.property_expander.expand.called).toBe(true);
    });

    it("should prepare the element", function () {
        expect(context.ui_element_preparer.prepare.called).toBe(true);
    });

    it("should ensure binding context", function () {
        expect(context.binding_context_manager.ensure.calledWith(element)).toBe(true);
    });
}));