Bifrost.namespace("Bifrost", {
    taskFactory: Bifrost.Singleton(function () {
        var self = this;

        this.createHttpPost = function (url, payload) {
            var task = Bifrost.tasks.HttpPostTask.create({
                url: url,
                payload: payload
            });
            return task;
        };

        this.createHttpGet = function () {
            var task = Bifrost.tasks.HttpGetTask.create({
                url: url,
                payload: payload
            });
            return task;
        };
    })
});