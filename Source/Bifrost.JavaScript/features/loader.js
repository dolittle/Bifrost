(function ($) {
    $(function () {
        $("*[data-feature]").each(function () {
            var target = $(this);
            var name = $(this).attr("data-feature");
            var feature = Bifrost.features.featureManager.get(name);
            feature.renderTo(target[0]);
        });
    });
})(jQuery);


