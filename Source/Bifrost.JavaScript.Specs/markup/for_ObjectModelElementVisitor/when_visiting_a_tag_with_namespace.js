describe("when visiting a tag with namespace", function() {
	var objectModelManager = {
		getObjectFromTagName: sinon.stub()
	};

	var visitor = Bifrost.markup.ObjectModelElementVisitor.create({
		objectModelManager: objectModelManager,
		markupExtensions: {},
		typeConverters: {}
	});

	var element = { localName: "ns:something", attributes: [], isKnownType: sinon.stub().returns(false) };
	visitor.visit(element);

	it("should ask for an object by tag name and namespace", function() {
		expect(objectModelManager.getObjectFromTagName.calledWith("something","ns")).toBe(true);
	});
});