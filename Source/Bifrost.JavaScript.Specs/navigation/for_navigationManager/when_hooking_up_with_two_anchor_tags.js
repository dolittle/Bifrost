describe("when hooking up with three anchor tags were two have navigation attribute", function() {
	var first = "http://www.vg.no/";
	var second = "/Something/Cool";
	var third = "/Something/Else";
	
	var firstAnchor = $("<a id='first' href='"+first+"'/>");
	var secondAnchor = $("<a id='second' href='"+second+"'/>");
	var thirdAnchor = $("<a id='third' href='"+third+"'/>");
	
	var root = $("<div/>")
					.append(firstAnchor)
					.append(secondAnchor)
					.append(thirdAnchor);
	var secondResult;
	var thirdResult;
	
	var secondOnClick;
	var thirdOnClick;	

    beforeEach(function() {
		sinon.stub($.fn, "attr", function(name, value) {
			var element = $(this)[0];
			if( name == "href" ) {
				if( value ) {
					if( element.id == "second" ) {
						secondResult = value;
					}
					if( element.id == "third" ) {
						thirdResult = value;
					}
				}
			}
			return this[name];
		});
		
		sinon.stub($.fn,"bind", function(event, callback) {
			var element = $(this)[0];
			if( event == "click") {
				if( callback ) {
					if( element.id == "second" ) {
						secondOnClick = callback;
					}
					if( element.id == "third" ) {
						thirdOnClick = callback;					
					}
				}
			}
		});
		Bifrost.navigation.navigationManager.hookup(root[0]);
	});
	
	
	afterEach(function() {
		$.fn.attr.restore();
		$.fn.bind.restore();
	});

	it("should not affect anchor without navigation attribute", function() {
		expect(firstAnchor[0].href).toBe(first);
	});
});