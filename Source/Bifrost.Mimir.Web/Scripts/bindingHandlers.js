ko.bindingHandlers.slide = {
    init: function (element, valueAccessor, allBindingAccessor, viewModel) {
        var value = valueAccessor()();
        if (value === true) {
            $(element).show();
        } else {
            $(element).hide();
        }
    },
    update: function (element, valueAccessor, allBindingAccessor, viewModel) {
        var value = valueAccessor()();
        if (value === true) {
            $(element).slideDown({ duration: 500, queue: false }).css('display', 'none');
        } else {
            $(element).slideUp({ duration: 500, queue: false });
        }
    }
};

ko.bindingHandlers.slideAndFade = {
    init: function (element, valueAccessor, allBindingAccessor, viewModel) {
        var value = valueAccessor()();
        if (value === true) {
            $(element).show();
        } else {
            $(element).hide();
        }
    },
    update: function (element, valueAccessor, allBindingAccessor, viewModel) {
        var value = valueAccessor()();
        var configuration = { duration: 500, queue: false };
        if (value === true) {
            $(element).stop(true, true).fadeIn(configuration).css('display', 'none').slideDown(configuration);
        } else {
            $(element).stop(true, true).fadeOut(configuration).slideUp(configuration);
        }
    }
};

ko.bindingHandlers.fade = {
    init: function (element, valueAccessor, allBindingAccessor, viewModel) {
        var value = valueAccessor()();
        if (value === true) {
            $(element).show();
        } else {
            $(element).hide();
        }
    },
    update: function (element, valueAccessor, allBindingAccessor, viewModel) {
        var value = valueAccessor()();
        if (value === true) {
            $(element).fadeIn({ duration: 500, queue: false }).css('display', 'none');

        } else {
            $(element).fadeOut({ duration: 500, queue: false });

        }
    }
};

ko.bindingHandlers.fadeVisible = {
    init: function (element, valueAccessor) {
        var value = valueAccessor();
        $(element).toggle(ko.utils.unwrapObservable(value)); // Use "unwrapObservable" so we can handle values that may or may not be observable
    },
    update: function (element, valueAccessor) {
        var value = valueAccessor();
        ko.utils.unwrapObservable(value) ? $(element).fadeIn() : $(element).fadeOut();
    }
};