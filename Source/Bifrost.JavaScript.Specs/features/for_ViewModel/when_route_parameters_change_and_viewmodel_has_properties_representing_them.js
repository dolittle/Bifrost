describe("when route parameters change and viewmodel has properties representing them", sinon.test(function() {
	var instance = null;
	var expectedString = "Horse";
	var expectedValue = "42";
	var location = "http://www.vg.no:8081/some/route#some/anchor?someString="+expectedString+"&someValue="+expectedValue+"&observedString="+expectedString+"&observedValue="+expectedValue;
	var uriOnCallback = null;
	
 	function MyViewModel() {
		this.queryParameters.define({
			someString: "",
			someValue: 0,
			observedString: ko.observable(),
			observedValue: ko.observable()
		});
		
		this.uriChanged(function(uri) {
			uriOnCallback = uri;
		});
	}
	
	beforeEach(function() {
		sinon.stub(Bifrost.Uri,"create", function() { 
			return { 
				parameters: {},
				setLocation: function(location) {
					this.parameters.someString = expectedString;
					this.parameters.someValue = expectedValue;
					this.parameters.observedString = expectedString;
					this.parameters.observedValue = expectedValue;
					this.fullPath = location;
				}
			}
		});
		Bifrost.messaging = Bifrost.messaging || {};
		Bifrost.messaging = Bifrost.messaging || {}
		Bifrost.messaging.Messenger = {
		    global: {}
		};

		var stateChangeCallback;
		sinon.stub(History.Adapter,"bind", function(window, event, callback) {
			stateChangeCallback = callback;
		});
		sinon.stub(History,"getState", function() {
			return {
				url: location
			}
		});
		
		Bifrost.features.ViewModel.baseFor(MyViewModel);

		callbackCount = 0;

		instance = new MyViewModel();
		stateChangeCallback();
	});

	afterEach(function() {
		Bifrost.Uri.create.restore();	
		History.getState.restore();
		History.Adapter.bind.restore();
	});
	
	it("should set the properties with the values", function() {
		expect(instance.queryParameters.someString).toBe(expectedString);
		expect(instance.queryParameters.someValue).toBe(expectedValue);
		expect(instance.queryParameters.observedString()).toBe(expectedString);
		expect(instance.queryParameters.observedValue()).toBe(expectedValue);
	});
	
	it("should call any uri changed subscribers with the uri as parameter", function() {
		expect(uriOnCallback.fullPath).toBe(location);
	});
}));