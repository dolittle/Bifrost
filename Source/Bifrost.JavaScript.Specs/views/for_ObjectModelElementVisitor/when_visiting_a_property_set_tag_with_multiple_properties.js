describe("when visiting a property set tag with multiple properties", function() {
	var instance = { some: "instance" };
	var objectModelManager = {
		getObjectFromTagName: sinon.stub().returns(instance)
	};

	var visitor = Bifrost.views.ObjectModelElementVisitor.create({
		objectModelManager: objectModelManager,
		markupExtensions: {},
		typeConverters: {}	
	});

	var exception = null;
	var element = { localName: "something.property.otherProperty", attributes: [], isKnownType: sinon.stub().returns(false) };
	try {
		visitor.visit(element);
	} catch( e ) {
		exception = e;
	} 

	it("should throw an exception", function() {
		expect(exception).not.toBeNull();
	});
});