describe("when applying rules to command properties as observable", function () {
    var command = {
        name: "something",
        something: ko.observable(),
        deep: {
            property: ko.observable()
        }
    };
    var rules = {
        "something": {
            name: "something"
        },
        "deep.property": {
            name: "deep.property"
        }
    };
    var rulesSet = {};

    var parameters = {
        validationService: {
            getForCommand: function () {
                return {
                    continueWith: function (callback) {
                        callback(rules);
                    }
                }
            }
        }
    };

    beforeEach(function () {

        ko.extenders.validation = function (target, options) {
            function validator() {
                var self = this;

                this.setOptions = function (rule) {

                    rulesSet[rule.name] = true;
                };

                this.validate = function () { };

                this.validateSilently = function () { };

                this.isValid = ko.observable(true);

            }
            target.validator = new validator();
            return target;
        };
        var commandValidationService = Bifrost.commands.commandValidationService.create(parameters);
        commandValidationService.applyRulesTo(command);
    });

    it("should extend the top level property with validation", function () {
        expect(command.something.validator).toBeDefined();
    });

    it("should extend the deep property with validation", function () {
        expect(command.deep.property.validator).toBeDefined();
    });

    it("should set the rule for the top level property", function () {
        expect(rulesSet["something"]).toBe(true);
    });

    it("should set the rule for the deep property", function () {
        expect(rulesSet["deep.property"]).toBe(true);
    });
});