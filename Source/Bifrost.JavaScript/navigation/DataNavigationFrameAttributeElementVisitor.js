Bifrost.namespace("Bifrost.navigation", {
    DataNavigationFrameAttributeElementVisitor: Bifrost.markup.ElementVisitor.extend(function (documentService) {
        this.visit = function (element, actions) {
            var dataNavigationFrame = element.attributes.getNamedItem("data-navigation-frame");
            if (!Bifrost.isNullOrUndefined(dataNavigationFrame)) {
                var dataBindString = "";
                var dataBind = element.attributes.getNamedItem("data-bind");
                if (!Bifrost.isNullOrUndefined(dataBind)) {
                    dataBindString = dataBind.value + ", ";
                } else {
                    dataBind = document.createAttribute("data-bind");
                }
                dataBind.value = dataBindString + "navigation: '" + dataNavigationFrame.value + "'";
                element.attributes.setNamedItem(dataBind);

                element.attributes.removeNamedItem("data-navigation-frame");
            }
        }
    })
});
    
