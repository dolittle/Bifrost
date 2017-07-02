describe("when posting data to an absolute url with target set", function () {
    var url = "http://www.db.no/Somewhere/With?query=value";
    var data = { something: 42 };

    var target = "http://www.vg.no";
    var server = Bifrost.server.create({
        configuration: {
            origins: {
                files: target,
                APIs: target
            }
        }
    });
    
    beforeEach(function() {
        sinon.stub($, "ajax")
        server.post(url, data);
    });

    afterEach(function() {
        $.ajax.restore();
    });

    it("should get relative to target defined", function () {
        expect($.ajax.firstCall.args[0].url).toBe(url);
    });
});