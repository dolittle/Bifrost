(function ($) {
    $(function () {
		Bifrost.navigation.navigationManager.hookup($("body")[0]);
        Bifrost.features.featureManager.hookup($);
    });
})(jQuery);


