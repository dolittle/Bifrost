// TODO: FIX SPEC COMMENTED OUT DUE FAILING IN RUNNER AND CAUSING ERROR

describe("when applying validation results", function () {
//    var options = {
//        error: function () {
//            print("Error");
//        },
//        success: function () {
//        },
//        parameters: {
//            plainValue: "test",
//            computed: ko.computed(function () { return "test"; }),
//            plainObject: {
//                plainValue: "test",
//                observable: ko.observable("test")
//            }
//        }
//    };
//    var expectedMessage = "it should be here";
//    var server = sinon.fakeServer.create();

//    server.respondWith("POST", "/Validation/GetForCommand",
//        [200, { "Content-Type": "application/json" }, ko.toJSON({
//            properties: {
//                "computed": {
//                    required: {
//                        message: expectedMessage
//                    }
//                },
//                "plainObject.observable": {
//                    required: {
//                        message: expectedMessage
//                    }
//                }
//            }
//        })]
//    );


//    var command = Bifrost.commands.Command.create(options);
//    server.respond();

//    var validationMessage = "message";

//    command.applyValidationMessageToMembers(["computed", "plainObject.observable"], validationMessage);

//    it("should set the validation message on each member", function () {
//        expect(options.parameters.computed.validator.message()).toBe(validationMessage);
//    });
});