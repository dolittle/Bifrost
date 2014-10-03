Bifrost.namespace("Web.Controls.Dialog", {
    ElementVisitor: Bifrost.markup.ElementVisitor.extend(function () {
        this.visit = function (element, actions) {
            if (element.localName != "dialog") {
                return;
            }

            var configuration = {
                view: null,
                width: 500,
                height: 500,
                completed: null,
                title: "A magic dialog",
                openmessage: "",
                closemessage: ""
            };

            var widthAttribute = element.attributes["width"];
            if (!Bifrost.isNullOrUndefined(widthAttribute)) {
                configuration.width = parseInt(widthAttribute.value);
            }

            var heightAttribute = element.attributes["height"];
            if (!Bifrost.isNullOrUndefined(heightAttribute)) {
                configuration.height = parseInt(heightAttribute.value);
            }

            var completedAttribute = element.attributes["completed"];
            if (!Bifrost.isNullOrUndefined(completedAttribute)) {
                configuration.completed = completedAttribute.value;
            }

            var titleAttribute = element.attributes["title"];
            if (!Bifrost.isNullOrUndefined(titleAttribute)) {
                configuration.title = titleAttribute.value;
            }

            var viewAttribute = element.attributes["view"];
            if (!Bifrost.isNullOrUndefined(viewAttribute)) {
                configuration.view = viewAttribute.value;
            }

            var openmessageAttribute = element.attributes["openmessage"];
            if (!Bifrost.isNullOrUndefined(openmessageAttribute)) {
                configuration.openmessage = openmessageAttribute.value;
            }

            var closemessageAttribute = element.attributes["closemessage"];
            if (!Bifrost.isNullOrUndefined(closemessageAttribute)) {
                configuration.closemessage = closemessageAttribute.value;
            }

            var container = document.createElement("div");
            var bindingAttribute = document.createAttribute("data-bind");
            bindingAttribute.value = "view: 'Controls/Dialog/Index', viewModelParameters: { configuration: "+JSON.stringify(configuration)+"}";
            container.attributes.setNamedItem(bindingAttribute);

            element.parentElement.replaceChild(container, element);
        }
    })
});