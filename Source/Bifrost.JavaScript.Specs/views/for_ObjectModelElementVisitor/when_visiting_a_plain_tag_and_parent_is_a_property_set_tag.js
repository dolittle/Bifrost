describe("when visiting a plain tag and parent is a property set tag", function() {
	var instance = { some: "instance"};
	var objectModelManager = {
		getObjectFromTagName: sinon.stub().returns(instance)
	};

	var visitor = Bifrost.views.ObjectModelElementVisitor.create({
		objectModelManager: objectModelManager,
		markupExtensions: {},
		typeConverters: {}	
	});

	var parentObjectModelNode = {
	};

	var parentElement = document.createElement("something.property");
	parentElement.__objectModelNode = parentObjectModelNode;
	var element = document.createElement("somethingelse");
	parentElement.appendChild(element);
	visitor.visit(element);

	it("should ask for an object by tag name", function() {
		expect(objectModelManager.getObjectFromTagName.calledWith("somethingelse")).toBe(true);
	});

	it("should set the object instance to the parent elements object model node", function() {
		expect(parentObjectModelNode.property).toBe(instance);
	});
});