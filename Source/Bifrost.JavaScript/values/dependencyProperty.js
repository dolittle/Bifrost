Bifrost.namespace("Bifrost.values", {
    dependencyProperty: Bifrost.Type.extend(function (propertyName) {
        this.initialize = function (UIElement) { };
        this.dispose = function (UIElement) {};

        this.set = function (UIElement, value) {
            UIElement[propertyName] = value;
        };

        this.get = function (UIElement) {
            return UIElement[propertyName];
        };
    })
});

Bifrost.values.DependencyProperty.register = function (owningType, name, dependencyPropertyType) {
};

Bifrost.namespace("Bifrost.DOM", {
    inputValueDependencyProperty: Bifrost.values.dependencyProperty.extend(function() {

        function inputChanged(e) {
            if( Bifrost.isFunction(e.target._changed) ) {
                e.target._changed(e.value);
            }
        }

        this.initialize = function (UIElement, changed) {
            UIElement._changed = changed;
            UIElement.addEventListener("change", inputChanged);
        };

        this.dispose = function (UIElement) {
            UIElement.removeEventListener("change", inputChanged);
        };
    })
});


Bifrost.values.DependencyProperty.register(HTMLInputElement, "value", Bifrost.DOM.inputValueDependencyProperty);
