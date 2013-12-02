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

        /// <field name="commands" type="observableArray">Array of commands inside the region</field>
        this.commands = ko.observableArray();

        /// <field name="isValid" type="observable">Indiciates wether or not region or any of its child regions are in an invalid state</field>
        this.isValid = ko.computed(function () {
            isValid = true;

            self.children().forEach(function (childRegion) {
                if (childRegion.isValid() === false) {
                    isValid = false;
                    return;
                }
            });

            self.commands().forEach(function (command) {
                if( command.isValid() === false ) isValid = false;
            });

            // Walk through all operations and find ones with commands and see if we are invalid
            return isValid;
        });

        /// <field name="validationMessages" type="observableArray">Holds the regions and any of its child regions validation messages</field>
        this.validationMessages = ko.computed(function () {
            var messages = [];

            self.children().forEach(function (childRegion) {
                if (childRegion.isValid() === false) {
                    childRegion.validationMessages().forEach(function (message) {
                        messages.push(message);
                    });
                }
            });

            self.commands().forEach(function (command) {
                if (command.isValid() === false) {
                    command.validators().forEach(function (validator) {
                        if (validator.isValid() === false) {
                            messages.push(validator.message());
                        }
                    });
                }
            });

            return messages; 
        });

        function thisOrChildHasTaskType(taskType, propertyName) {
            var hasTask = false;
            self.children().forEach(function (childRegion) {
                if (childRegion[propertyName]() === true) {
                    hasTask = true;
                    return;
                }
            });

            self.tasks.all().forEach(function (task) {
                if (task instanceof taskType) hasTask = true;
            });

            return hasTask;
        }

        /// <field name="isExecuting" type="observable">Indiciates wether or not execution tasks are being performend in this region or any of its child regions</field>
        this.isExecuting = thisOrChildHasTaskType(Bifrost.tasks.ExecutionTask, "isExecuting");

        /// <field name="isComposing" type="observable">Indiciates wether or not execution tasks are being performend in this region or any of its child regions</field>
        this.isComposing = thisOrChildHasTaskType(Bifrost.views.ComposeTask, "isComposing");

        /// <field name="isLoading" type="observable">Indiciates wether or not loading tasks are being performend in this region or any of its child regions</field>
        this.isLoading = thisOrChildHasTaskType(Bifrost.tasks.LoadTask, "isLoading");

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