Bifrost.namespace("Bifrost.features");
Bifrost.features.UriNotSpecified = function(message) {
	this.prototype = Error.prototype;
	this.name = "UriNotSpecified";
	this.message = message || "Uri was not specified";
}
Bifrost.features.MappedUriNotSpecified = function(message) {
	this.prototype = Error.prototype;
	this.name = "MappedUriNotSpecified";
	this.message = message || "Mapped Uri was not specified";
}