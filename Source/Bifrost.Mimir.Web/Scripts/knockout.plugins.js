ko.bindingHandlers.fadeInOrOut = {
    init: function (element, valueAccessor) {
    },
    update: function (element, valueAccessor) {
		if( valueAccessor() ) {
	        $(element).hide().fadeIn(500);
		} else {
	        $(element).fadeOut(500);
		}
    }
};


ko.bindingHandlers.fadeInElement = {
    init: function (element, valueAccessor) {
    },
    update: function (element, valueAccessor) {
        $(element).hide().fadeIn(500);
    }
};
