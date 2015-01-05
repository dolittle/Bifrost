var contextsByName = {};

function given(name, context) {
    /*
    var file = "given/" + name.replaceAll(" ", "_") + ".js";
    print("File : " + file);
    require([file], function () {
        print("Yes we can");
    });*/

    
    if (contextsByName.hasOwnProperty(name)) {
        function weaved() {
            contextsByName[name].prototype = this;
            context.prototype = new contextsByName[name]();
            return new context();
        }

        return weaved;

    } else {
        contextsByName[name] = context;
    }

    return context;    
}