Bifrost.namespace("Bifrost.views", {
    DataViewModelFileAttributeElementVisitor: Bifrost.markup.ElementVisitor.extend(function () {
        this.visit = function (element, actions) {

            var dataView = element.attributes.getNamedItem("data-viewmodel-file");
            if (!Bifrost.isNullOrUndefined(dataView)) {
                var dataBindString = "";
                var dataBind = element.attributes.getNamedItem("data-bind");
                if (!Bifrost.isNullOrUndefined(dataBind)) {
                    dataBindString = dataBind.value + ", ";
                } else {
                    dataBind = document.createAttribute("data-bind");
                }
                dataBind.value = dataBindString + "viewModel: '" + dataView.value + "'";
                element.attributes.setNamedItem(dataBind);
                element.attributes.removeNamedItem("data-viewmodel-file");
            }
        };
    })
});