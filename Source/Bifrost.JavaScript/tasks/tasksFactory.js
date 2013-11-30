Bifrost.namespace("Bifrost.tasks", {
    tasksFactory: Bifrost.Singleton(function () {
        this.create = function () {
            var tasks = Bifrost.tasks.Tasks.create();
            return tasks;
        };
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.tasksFactory = Bifrost.tasks.tasksFactory;