describe("when visiting a tag with namespace", function() {
	var objectModelManager = {
		getObjectFromTagName: sinon.stub()
	};

	var visitor = Bifrost.views.ObjectModelElementVisitor.create({
		objectModelManager: objectModelManager,
		markupExtensions: {},
		typeConverters: {}
	});

	var element = { localName: "ns:something", attributes: [] };
	visitor.visit(element);

	it("should ask for an object by tag name and namespace", function() {
		expect(objectModelManager.getObjectFromTagName.calledWith("something","ns")).toBe(true);
	});
});