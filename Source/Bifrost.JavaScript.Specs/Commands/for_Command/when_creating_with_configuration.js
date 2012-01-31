describe("when creating with configuration", function () {
    var options = {
        error: function () {
            print("Error");
        },
        success: function () {
        }
    };
    var command = Bifrost.commands.Command.create(options);

    it("should create an instance", function () {
        expect(command).toBeDefined();
    });

    it("should include options", function () {
        for (var property in options) {
            expect(command.options[property]).toEqual(options[property]);
        }
    });
});