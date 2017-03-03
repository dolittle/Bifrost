exports.preTransform = function (model) {
    model.authors = [];
    if (model.author && model.author.length > 0) {
        model.author.split(" ").join("").split(",").forEach(function (author) {
            model.authors.push({
                name: author
            });
        });
    }
    return model;
}

exports.postTransform = function (model) {
    return model;
}