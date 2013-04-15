describe("when copying to", function () {

    var readModelType = Bifrost.read.ReadModel.extend(function () {
        var self = this;
        this.nonObservablePropertyOnlyFoundInSource = 42;
        this.observablePropertyOnlyFoundInSource = ko.observable(43);
        this.valueInBothPlaces = ko.observable(45);
        this.observableInBothPlaces = ko.observable(47);
    });

    var instance = readModelType.create();

    var target = {
        valueInBothPlaces: 44,
        observableInBothPlaces: ko.observable(46)
    };

    instance.copyTo(target);


    it("should add non observable property only found in source as observable", function () {
        expect(ko.isObservable(target.nonObservablePropertyOnlyFoundInSource)).toBe(true);
    });

    it("should set the value for non observable property only found in source as observable", function () {
        expect(target.nonObservablePropertyOnlyFoundInSource()).toBe(42);
    });

    it("should add observable property only found in source as observable", function() {
        expect(ko.isObservable(target.observablePropertyOnlyFoundInSource)).toBe(true);
    });

    it("should set the value for observable property only found in source as observable", function () {
        expect(target.observablePropertyOnlyFoundInSource()).toBe(43);
    });

    it("should copy non observable value into observable", function () {
        expect(target.valueInBothPlaces).toBe(45);
    });

    it("should copy observable value into observable", function () {
        expect(target.observableInBothPlaces()).toBe(47);
    });
});