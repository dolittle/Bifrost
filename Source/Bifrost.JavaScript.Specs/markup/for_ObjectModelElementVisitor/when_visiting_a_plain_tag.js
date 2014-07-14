describe("when visiting a plain tag", function() {
	var instance = { some: "instance" };
	var objectModelManager = {
		getObjectFromTagName: sinon.stub().returns(instance)
	};

	var visitor = Bifrost.markup.ObjectModelElementVisitor.create({
		objectModelManager: objectModelManager,
		markupExtensions: {},
		typeConverters: {}	
	});

	var element = { localName: "something", attributes: [], isKnownType: sinon.stub().returns(false) };
	visitor.visit(element);

	it("should ask for an object by tag name", function() {
		expect(objectModelManager.getObjectFromTagName.calledWith("something")).toBe(true);
	});

	it("should set the object instance on the element", function() {
		expect(element.__objectModelNode).toBe(instance);
	});
});