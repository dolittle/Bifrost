describe("when extending all properties", function () {
    function something() {
        var self = this;

        this.extend = function(extension) {
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
        }
    };

    it("should add validation knockout extension", function () {
        var containingObject = {
            prop1: new something(),
            prop2: new something()
        }
        Bifrost.validation.validationService.recursivlyExtendProperties(containingObject, rules);

        var prop1Extended = typeof containingObject.prop1.extendedWith.validation !== "undefined";
        var prop2Extended = typeof containingObject.prop2.extendedWith.validation !== "undefined";

        expect(prop1Extended && prop2Extended).toBe(true);
    });
});