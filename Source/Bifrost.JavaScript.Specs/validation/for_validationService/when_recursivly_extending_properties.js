describe("when recursivly extending properties", function () {
    function something() {
        var self = this;

        this.extend = function (extension) {
            self.extendedWith = extension;
            self.validator = {
                setOptions: function (options) {

                }
            };
        };
    }

    var rules = {
        prop1: {
            require: "should be here"
        },
        prop2: {
            require: "should be here as well"
        },
        "prop3.prop1": {
            require: "should also be here"
        }
    };

    it("should add validation knockout extension", function () {
        var containingObject = {
            prop1: new something(),
            prop2: new something(),
            prop3: {
                prop1: new something()
            }
        }
        Bifrost.validation.validationService.applyRulesToProperties(containingObject, rules);

        var prop1Extended = typeof containingObject.prop1.extendedWith.validation !== "undefined";
        var prop2Extended = typeof containingObject.prop2.extendedWith.validation !== "undefined";
        var prop3Extended = typeof containingObject.prop3.prop1.extendedWith.validation !== "undefined";

        expect(prop1Extended && prop2Extended && prop3Extended).toBe(true);
    });

    it("should return a list of validators", function () {
        var containingObject = {
            prop1: new something(),
            prop2: new something(),
            prop3: {
                prop1: new something()
            }
        }
        var validatorsList = Bifrost.validation.validationService.applyRulesToProperties(containingObject, rules);


        expect(validatorsList.length).toBe(3);
    });
});