describe("when visiting a plain tag with uppercase name", function() {
	var objectModelManager = {
		getObjectFromTagName: sinon.stub()

	};

	var visitor = Bifrost.views.ObjectModelElementVisitor.create({
		objectModelManager: objectModelManager,
		markupExtensions: {},
		typeConverters: {}
	});

	var element = { localName: "SOMETHING", attributes: [], isKnownType: sinon.stub().returns(false) };
	visitor.visit(element);

	it("should ask for an object by tag name", function() {
		expect(objectModelManager.getObjectFromTagName.calledWith("something")).toBe(true);
	});
});