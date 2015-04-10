Bifrost.namespace("Bifrost.markup", {
    bindingContextManager: Bifrost.Singleton(function () {

        this.ensure = function (element) {
            // If there is specific bindingContext for element, return it

            // If no specific, find nearest from parent element

            // If no parent element has one either, there is none - return null

            // If element has an attribute of bindingContext - we can now change it to what it is pointing at

            // If bindingContext changes due to a binding being related to the context from the attribute on the element, it should fire the changed thing on the binding context

            // Inherit from parent - always - parent is prototype of current, point back to parent
        };

        this.hasFor = function (element) {

        };

        this.getFor = function (element) {

        };
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.bindingContextManager = Bifrost.markup.bindingContextManager;