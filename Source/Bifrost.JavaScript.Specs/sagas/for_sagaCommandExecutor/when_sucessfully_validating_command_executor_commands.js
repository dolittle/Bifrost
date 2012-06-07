describe("when successfully validating a saga command executor", function () {
    var sce,
        options,
        parametersAreValidSpy,
        validateSpy,
        onBeforeExecuteSpy;

    beforeEach(function () {
        options = {
            sagaId: "bleh",
            commands: [
                Bifrost.commands.Command.create({
                    beforeExecute: function () {
                    }
                }),
                Bifrost.commands.Command.create({
                    beforeExecute: function () {
                    }
                }),
                Bifrost.commands.Command.create({
                    beforeExecute: function () {
                    }
                })
            ],
            beforeExecute: sinon.stub()
        };

        validateSpy = sinon.stub();
        parametersAreValidSpy = sinon.stub();

        for (var i = 0; i < options.commands.length; i++) {
            var command = options.commands[i];
            command.validate = validateSpy;
            command.parametersAreValid = parametersAreValidSpy;
        }

        sce = Bifrost.sagas.SagaCommandExecutor.create(options);
        onBeforeExecuteSpy = sinon.spy(sce, "onBeforeExecute");
        sce.execute();

    });

    afterEach(function () {
    });

    it("should validate on all commands", function () {
        expect(validateSpy.callCount).toBe(3);
    });

    it("should call parameters are valid on all commands", function () {
        expect(parametersAreValidSpy.callCount).toBe(3);
    });

    it("should execute if all validations succeed", function () {
        expect(onBeforeExecuteSpy.returnValues[0]).toBeTruthy();
    });


});