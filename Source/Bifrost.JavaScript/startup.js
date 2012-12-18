Bifrost.namespace("Bifrost", {
    

});
(function ($) {
    $(function () {
        Bifrost.assetsManager.initialize();
        Bifrost.navigation.navigationManager.hookup();
        Bifrost.features.featureManager.hookup($);
    });
})(jQuery);