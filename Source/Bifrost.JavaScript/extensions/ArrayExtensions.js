
function polyfillForEach() {
    if (typeof Array.prototype.forEach !== "function") {
        Array.prototype.forEach = function (callback, thisArg) {
            if( typeof thisArg == "undefined" ) thisArg = window;
            for (var i = 0; i < this.length; i++) {
                callback.call(thisArg, this[i], i, this);
            }
        };
    }
}

function polyFillClone() {
    if (typeof Array.prototype.clone !== "function") {
        Array.prototype.clone = function () {
            return this.slice(0);
        }
    }
}

(function () {
    polyfillForEach();
    polyFillClone();
})();