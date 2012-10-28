describe("when creating instance with dependencies and super having dependencies", function() {
	var super = null; 
	var type = null;
	var somethingDependency = {
		something: "hello"
	};
	var somethingElseDependency = {
		somethingElse: "world"
	};

	var superFunction = function(something) {
		this.something = something;
	};

	var typeFunction = function(somethingElse) {
		this.somethingElse = somethingElse;
	};

	var instance = null;

	beforeEach(function() {
		Bifrost.dependencyResolver = {
			getDependenciesFor: function(func) {
				if( func == superFunction ) {
					return ["something"];
				}
				if( func == typeFunction ) {
					return ["somethingElse"];
				}
			},
			resolve: function(name) {
				if( name === "something" ) {
					return somethingDependency;
				}
				if( name === "somethingElse" ) {
					return somethingElseDependency;
				}
			}
		}

		super = Bifrost.Type.define(superFunction);
		type = super.define(typeFunction);

		instance = type.create();
	});


	afterEach(function() {

	});

	it("should create an instance", function() {
		expect(instance).not.toBeNull();
	});

	it("should have the super dependency resolved", function() {
		expect(instance.something).toBe(somethingDependency);
	});

	it("should have the type dependency resolved", function() {
		expect(instance.somethingElse).toBe(somethingElseDependency);
	});	
});