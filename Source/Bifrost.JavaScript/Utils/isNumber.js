Bifrost.namespace("Bifrost", {
	isNumber : function(number) {
    	return !isNaN(parseFloat(number)) && isFinite(number);
    }
}
