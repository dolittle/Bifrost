Bifrost.namespace("Bifrost");
Bifrost.isNumber = function(number) {
    return !isNaN(parseFloat(number)) && isFinite(number);
}
