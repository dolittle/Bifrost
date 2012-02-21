ko.bindingHandlers.fadeInElement = {
    init: function (element, valueAccessor) {
    },
    update: function (element, valueAccessor) {
        $(element).hide().fadeIn(500);
    }
};
ko.bindingHandlers.fadeOutElement = {
    init: function (element, valueAccessor) {
    },
    update: function (element, valueAccessor) {
        $(element).fadeOut(500);
    }
};