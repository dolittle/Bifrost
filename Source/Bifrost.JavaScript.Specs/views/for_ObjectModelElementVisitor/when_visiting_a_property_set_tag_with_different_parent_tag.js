describe("when visiting a property set tag with different parent tag", function() {
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
	var parentElement = document.createElement("something.property");
	var element = document.createElement("somethingelse");
	parentElement.appendChild(element);

	try {
		visitor.visit(element);
	} catch( e ) {
		exception = e;
	} 

	it("should throw an exception", function() {
		expect(exception).not.toBeNull();
	});
});