describe("when visiting a tag with two namespaces", function() {
	var objectModelManager = {
		getObjectFromTagName: sinon.stub()

	};

	var visitor = Bifrost.views.ObjectModelElementVisitor.create({
		objectModelManager: objectModelManager,
		markupExtensions: {},
		typeConverters: {}
	});

	var exception = null;
	try {
		var element = { localName: "ns:ns2:something", attributes: [] };
		visitor.visit(element);
	} catch( e ) {
		exception = e;
	}

	it("should throw an exception", function() {
		expect(exception).not.toBe(null);
	});
});