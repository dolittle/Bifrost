describe("when creating instance with dependencies and super having dependencies", function() {
    print("FUCK");
	var super = null; 
	var type = null;
    var ns = null;

    

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
			resolve: function(namespace, name) {
                ns = namespace;

				if( name === "something" ) {
					return somethingDependency;
				}
				if( name === "somethingElse" ) {
					return somethingElseDependency;
				}
			}
		}

        var namespace = { name : "Somewhere" };
		super = Bifrost.Type.define(superFunction);
        super._namespace = namespace;
		type = super.define(typeFunction);
        type._namespace = namespace;

		instance = type.create();
	});


	afterEach(function() {

	});

    it("should pass along the namespace to resolver", function() {
        expect(ns.name).toBe("Somewhere");
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