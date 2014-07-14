describe("when handling with one element", function() {
	var root = document.createElement("div");
	var element = document.createElement("button");
	
	var documentService = {
		traverseObjects: function(callback) {
			callback(element);
		}
	};

	var visitStub = sinon.stub();

	var visitorType = Bifrost.markup.ElementVisitor.extend(function() {
		this.visit = visitStub;
	});
	var actions = { some: "actions" };

	beforeEach(function() {
	    sinon.stub(Bifrost.markup.ElementVisitor, "getExtenders").returns([visitorType]);
	    sinon.stub(Bifrost.markup.ElementVisitorResultActions, "create").returns(actions);

	    var instance = Bifrost.views.UIManager.createWithoutScope({
			documentService: documentService
		})

		instance.handle(root);
	});

	afterEach(function() {
	    Bifrost.markup.ElementVisitor.getExtenders.restore();
	    Bifrost.markup.ElementVisitorResultActions.create.restore();
	});

	it("should call the visit function of the visitor", function() {
		expect(visitStub.calledWith(element, actions)).toBe(true);
	});
});