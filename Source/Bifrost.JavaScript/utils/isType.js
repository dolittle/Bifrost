Bifrost.namespace("Bifrost", {
    isType: function (o) {
        if (Bifrost.isNullOrUndefined(o)) return false;
		return typeof o._typeId != "undefined";
	}
});