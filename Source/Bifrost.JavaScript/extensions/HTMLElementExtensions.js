HTMLElement.prototype.getChildElements = function () {
    var children = [];
    this.childNodes.forEach(function (node) {
        if (node.nodeType == 1) children.push(node);
    });
    return children;
};
