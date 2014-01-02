describe("when handling with one element", function() {
	var root = document.createElement("div");
	var element = document.createElement("button");
	
	var documentService = {
		traverseObjects: function(callback) {
			callback(element);
		}
	};

	var visitStub = sinon.stub();

	var visitorType = Bifrost.views.ElementVisitor.extend(function() {
		this.visit = visitStub;
	});
	var actions = { some: "actions" };

	beforeEach(function() {
		sinon.stub(Bifrost.views.ElementVisitor,"getExtenders").returns([visitorType]);
		sinon.stub(Bifrost.views.ElementVisitorResultActions,"create").returns(actions);

		var instance = Bifrost.views.UIManager.createWithoutScope({
			documentService: documentService
		})

		instance.handle(root);
	});

	afterEach(function() {
		Bifrost.views.ElementVisitor.getExtenders.restore();
		Bifrost.views.ElementVisitorResultActions.create.restore();
	});

	it("should call the visit function of the visitor", function() {
		expect(visitStub.calledWith(element, actions)).toBe(true);
	});
});