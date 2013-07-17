Bifrost.namespace("Bifrost.read", {
    PagingInfo: Bifrost.Type.extend(function (size, number) {
        var self = this;

        this.size = size;
        this.number = number;
    })
});