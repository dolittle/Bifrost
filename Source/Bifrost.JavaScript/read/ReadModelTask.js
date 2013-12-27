Bifrost.namespace("Bifrost.read", {
    ReadModelTask: Bifrost.tasks.LoadTask.extend(function (readModelOf, propertyFilters, taskFactory) {
        var url = "/Bifrost/ReadModel/InstanceMatching?_rm=" + readModelOf.generatedFrom;
        var payload = {
            descriptor: {
                readModel: readModelOf.name,
                generatedFrom: readModelOf.generatedFrom,
                propertyFilters: propertyFilters
            }
        };

        this.readModel = readModelOf.generatedFrom;

        var innerTask = taskFactory.createHttpPost(url, payload);

        this.execute = function () {
            var promise = innerTask.execute();
            return promise;
        };
    })
});