Bifrost.namespace("Bifrost.views", {
    Region: function(messengerFactory, operationsFactory, tasksFactory) {
        /// <summary>Represents a region in the visual composition on a page</summary>
        var self = this;

        /// <field name="view" type="Bifrost.views.View">View for the composing</field>
        this.view = null;

        /// <field name="viewModel" type="Bifrost.views.ViewModel">The ViewModel associated with the view</field>
        this.viewModel = null;

        /// <field name="messenger" type="Bifrost.messaging.Messenger">The messenger for the region</field>
        this.messenger = messengerFactory.create();

        /// <field name="globalMessenger" type="Bifrost.messaging.Messenger">The global messenger</field>
        this.globalMessenger = messengerFactory.global();

        /// <field name="operations" type="Bifrost.interaction.Operations">Operations for the region</field>
        this.operations = operationsFactory.create();

        /// <field name="tasks" type="Bifrost.tasks.Tasks">Tasks for the region</field>
        this.tasks = tasksFactory.create();

        /// <field name="parent" type="Bifrost.views.Region">Parent region, null if there is no parent</field>
        this.parent = null;

        /// <field name="children" type="Bifrost.views.Region[]">Child regions within this region</field>
        this.children = ko.observableArray();

        /// <field name="isBusy" type="observable">Indicates wether or not tasks are being performed in this region or any of its child regions</field>
        this.isBusy = ko.computed(function () {
            var isBusy = false;
            self.children().forEach(function (childRegion) {
                if (childRegion.isBusy() === true) {
                    isBusy = true;
                    return;
                }
            });
            
            if (self.tasks.all().length > 0) isBusy = true;

            return isBusy;
        });
    }
});
Bifrost.views.Region.current = null;