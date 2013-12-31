describe("when traversing without specifying a root element", function() {
	var DOMRoot = document.createElement("body");
	var container = document.createElement("div");
	DOMRoot.appendChild(container);
	var customTag = document.createElement("custom");
	container.appendChild(customTag);
	var deepCustomTag = document.createElement("deepcustom");
	customTag.appendChild(deepCustomTag);

	var service = Bifrost.documentService.createWithoutScope({DOMRoot: DOMRoot});
	var callback = sinon.stub();
	service.traverseObjects(callback);

	it("should call the callback for the body", function() {
		expect(callback.calledWith(DOMRoot)).toBe(true);
	});

	it("should call the callback for the container", function() {
		expect(callback.calledWith(container)).toBe(true);
	});

	it("should call the callback for the custom tag", function() {
		expect(callback.calledWith(customTag)).toBe(true);
	});

	it("should call the callback for the deep custom tag", function() {
		expect(callback.calledWith(deepCustomTag)).toBe(true);
	});
});