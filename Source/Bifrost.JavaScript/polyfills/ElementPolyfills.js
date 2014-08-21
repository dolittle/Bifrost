// From: http://www.jonathantneal.com/blog/faking-the-future/
this.Element && (function (ElementPrototype, polyfill) {
    function NodeList() { [polyfill] }
    NodeList.prototype.length = Array.prototype.length;

    ElementPrototype.matchesSelector = ElementPrototype.matchesSelector ||
    ElementPrototype.mozMatchesSelector ||
    ElementPrototype.msMatchesSelector ||
    ElementPrototype.oMatchesSelector ||
    ElementPrototype.webkitMatchesSelector ||
    function matchesSelector(selector) {
        var results = this.parentNode.querySelectorAll(selector);
        var resultsIndex = -1;

        while (results[++resultsIndex] && results[resultsIndex] != this) {}

        return !!results[resultsIndex];
    };

    ElementPrototype.ancestorQuerySelectorAll = ElementPrototype.ancestorQuerySelectorAll ||
    ElementPrototype.mozAncestorQuerySelectorAll ||
    ElementPrototype.msAncestorQuerySelectorAll ||
    ElementPrototype.oAncestorQuerySelectorAll ||
    ElementPrototype.webkitAncestorQuerySelectorAll ||
    function ancestorQuerySelectorAll(selector) {
        for (var cite = this, newNodeList = new NodeList(); cite = cite.parentElement;) {
            if (cite.matchesSelector(selector)) Array.prototype.push.call(newNodeList, cite);
        }
 
        return newNodeList;
    };
 
    ElementPrototype.ancestorQuerySelector = ElementPrototype.ancestorQuerySelector ||
    ElementPrototype.mozAncestorQuerySelector ||
    ElementPrototype.msAncestorQuerySelector ||
    ElementPrototype.oAncestorQuerySelector ||
    ElementPrototype.webkitAncestorQuerySelector ||
    function ancestorQuerySelector(selector) {
        return this.ancestorQuerySelectorAll(selector)[0] || null;
    };
})(Element.prototype);