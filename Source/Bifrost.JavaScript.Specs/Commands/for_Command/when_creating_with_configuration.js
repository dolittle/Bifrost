﻿describe("when creating with configuration", function () {
    var options,
        server,
        command,
        expectedMessage = "it should be here";

    beforeEach(function () {
        options = {
            error: function () {
                print("Error");
            },
            success: function () {
            },
            parameters: {
                plainValue: "test",
                computed: ko.computed(function () { return "test"; }),
                plainObject: {
                    plainValue: "test",
                    observable: ko.observable("test")
                }
            }
        };
        server = sinon.fakeServer.create();

        server.respondWith("POST", "/Validation/GetForCommand",
            [200, { "Content-Type": "application/json" }, ko.toJSON({
                properties: {
                    "computed": {
                        required: {
                            message: expectedMessage
                        }
                    },
                    "plainObject.observable": {
                        required: {
                            message: expectedMessage
                        }
                    }
                }
            })]);


        command = Bifrost.commands.Command.create(options);
        server.respond();
    });

    afterEach(function () {
        server.restore();
    })

    it("should create an instance", function () {
        expect(command).toBeDefined();
    });

    it("should include options", function () {
        for (var property in options) {
            expect(command.options[property]).toEqual(options[property]);
        }
    });

    it("should include properties", function () {
        for (var property in options.properties) {
            expect(command.options.parameters[property]).toEqual(options.parameters[property]);
        }
    });

    it("should include validatorsList", function () {
        expect(command.validatorsList.length).toBe(2);
        expect(command.validatorsList[0]).toBe(options.parameters.computed);
        expect(command.validatorsList[1]).toBe(options.parameters.plainObject.observable);
    });

    it("should have valid parameters", function () {
        expect(command.parametersAreValid()).toBe(true);
    });
});