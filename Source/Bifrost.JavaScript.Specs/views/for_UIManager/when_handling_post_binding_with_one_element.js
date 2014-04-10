describe("when handling post binding with one element", function() {
	var root = document.createElement("div");
	var element = document.createElement("button");
	
	var documentService = {
		traverseObjects: function(callback) {
			callback(element);
		}
	};

	var visitStub = sinon.stub();

	var visitorType = Bifrost.views.PostBindingVisitor.extend(function () {
		this.visit = visitStub;
	});

	beforeEach(function() {
	    sinon.stub(Bifrost.views.PostBindingVisitor, "getExtenders").returns([visitorType]);

		var instance = Bifrost.views.UIManager.createWithoutScope({
			documentService: documentService
		})

		instance.handlePostBinding(root);
	});

	afterEach(function() {
		Bifrost.views.PostBindingVisitor.getExtenders.restore();
	});

	it("should call the visit function of the visitor", function() {
		expect(visitStub.calledWith(element)).toBe(true);
	});
});