Bifrost.namespace("Bifrost.views", {
    Region: function() {
        /// <summary>Represents a region in the visual composition on a page</summary>
        var self = this;

        /// <field name="view" type="Bifrost.views.View">View for the composing</field>
        this.view = null;

        /// <field name="viewModel" type="Bifrost.views.ViewModel">The ViewModel associated with the view</field>
        this.viewModel = null;

        /// <field name="messenger" type="Bifrost.messaging.Messenger">The messenger for the region</field>
        this.messenger = Bifrost.messaging.Messenger.create();

        /// <field name="globalMessenger" type="Bifrost.messaging.Messenger">The global messenger</field>
        this.globalMessenger = Bifrost.messaging.Messenger.global;

        /// field name="parent" type="Bifrost.views.Region">Parent region, null if there is no parent</field>
        this.parent = null;

        /// field name="children" type="Bifrost.views.Region[]">Child regions within this region</field>
        this.children = [];
    }
});
