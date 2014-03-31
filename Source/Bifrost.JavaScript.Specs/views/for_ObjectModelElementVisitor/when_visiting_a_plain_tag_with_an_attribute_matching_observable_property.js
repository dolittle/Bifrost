describe("when visiting a plain tag with an attribute matching observable property", function() {
	var obj = {
		someProperty: ko.observable(0)
	};

	var objectModelManager = {
		getObjectFromTagName: sinon.stub().returns(obj)
	};
	var typeConverters = {
		convert: sinon.stub().returns(42)
	}

	var visitor = Bifrost.views.ObjectModelElementVisitor.create({
		objectModelManager: objectModelManager,
		markupExtensions: {},
		typeConverters: typeConverters
	});

	var element = { 
		localName: "something",
		attributes: [
			{localName:"someProperty", value:"42"}
		],
		isKnownType: sinon.stub().returns(false)
	};
	visitor.visit(element);

	it("should ask the type converters for a conversion", function() {
		expect(typeConverters.convert.calledWith("number","42")).toBe(true);
	});

	it("should set the converted value on the object", function() {
		expect(obj.someProperty()).toBe(42);
	});
});