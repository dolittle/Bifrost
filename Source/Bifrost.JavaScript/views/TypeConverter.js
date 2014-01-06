Bifrost.namespace("Bifrost.views", {
	TypeConverter: Bifrost.Type.extend(function() {
		this.supportedType = null;
		this.convert = function(value) {
			return value;
		};
	})
})