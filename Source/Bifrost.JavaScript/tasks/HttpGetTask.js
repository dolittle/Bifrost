Bifrost.namespace("Bifrost.tasks", {
    HttpGetTask: Bifrost.tasks.Task.extend(function (server, url, payload) {
        /// <summary>Represents a task that can perform Http Get requests</summary>
        var self = this;

        this.execute = function () {
            var promise = Bifrost.execution.Promise.create();
            server
                .get(url, payload)
                    .continueWith(function (result) {
                        promise.signal(result);
                    })
                    .onFail(function (error) {
                        promise.fail(error);
                    });
            return promise;
        };
    })
});