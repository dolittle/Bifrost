Bifrost.namespace("Bifrost.interaction", {
    VisualStateManagerElementVisitor: Bifrost.markup.ElementVisitor.extend(function() {
        var visualStateActionTypes = Bifrost.interaction.VisualStateAction.getExtenders();

        function parseActions(namingRoot, stateElement, state) {
            if( stateElement.hasChildNodes() ) {
                var child = stateElement.firstChild;
                while( child ) {
                    visualStateActionTypes.forEach(function(type) {
                        if( type._name.toLowerCase() == child.localName ) {
                            var action = type.create();

                            for( var attributeIndex=0; attributeIndex<child.attributes.length; attributeIndex++ ) {
                                var name = child.attributes[attributeIndex].localName;
                                var value = child.attributes[attributeIndex].value;
                                if( action.hasOwnProperty(name) ) {
                                    action[name] = value;
                                }
                            }
                            action.initialize(namingRoot);
                            state.addAction(action);
                        }
                    });
                    child = child.nextSibling;
                }
            }
        }

        function parseStates(namingRoot, groupElement, group) {
            if( groupElement.hasChildNodes() ) {
                var child = groupElement.firstChild;
                while( child ) {
                    if( child.localName === "visualstate" ) {
                        var state = Bifrost.interaction.VisualState.create();
                        state.name = child.getAttribute("name");
                        group.addState(state);
                        parseActions(namingRoot, child, state);
                    }
                    child = child.nextSibling;
                }
            }
        }


        this.visit = function(element, actions) {
            if( element.localName === "visualstatemanager" ) {
                var visualStateManager = Bifrost.interaction.VisualStateManager.create();
                var namingRoot = element.parentElement.namingRoot;
                element.parentElement.visualStateManager = visualStateManager;

                if( element.hasChildNodes() ) {
                    var child = element.firstChild;
                    while( child ) {
                        if( child.localName === "visualstategroup" ) {
                            var group = Bifrost.interaction.VisualStateGroup.create();
                            visualStateManager.addGroup(group);

                            var duration = child.getAttribute("duration");
                            if( !Bifrost.isNullOrUndefined(duration) ) {
                                duration = parseFloat(duration);
                                if( !isNaN(duration) ) {
                                    duration = duration * 1000;
                                    timespan = Bifrost.TimeSpan.fromMilliseconds(duration);
                                    group.defaultDuration = timespan;
                                }
                            }

                            parseStates(namingRoot, child, group);
                        }
                        child = child.nextSibling;
                    }
                }
            }
        }

    })
});