Bifrost.namespace("Bifrost.views", {
    DataNavigationFrameAttributeElementVisitor: Bifrost.views.ElementVisitor.extend(function () {
        this.visit = function (element, actions) {

            var dataView = element.attributes.getNamedItem("data-navigation-frame");
            if (!Bifrost.isNullOrUndefined(dataView)) {

                var configurationItems = ko.expressionRewriting.parseObjectLiteral(configurationString);

                var configuration = {};

                for (var index = 0; index < configurationItems.length; index++) {
                    var item = configurationItems[index];
                    configuration[item.key.trim()] = item.value.trim();
                }

                if (typeof configuration.uriMapper !== "undefined") {
                    var mapper = Bifrost.uriMappers[configuration.uriMapper];
                    var frame = Bifrost.navigation.NavigationFrame.create({
                        stringMapper: mapper,
                        home: configuration.home || ''
                    });
                    frame.setContainer(element);

                    element.navigationFrame = frame;
                }


                var dataBindString = "";
                var dataBind = element.attributes.getNamedItem("data-bind");
                if (!Bifrost.isNullOrUndefined(dataBind)) {
                    dataBindString = dataBind.value + ", ";
                } else {
                    dataBind = document.createAttribute("data-bind");
                }
                dataBind.value = dataBindString + "view: '" + dataView.value + "'";
                element.attributes.setNamedItem(dataBind);
                element.attributes.removeNamedItem("data-navigation-frame");
            }
        }
    })
});
    
