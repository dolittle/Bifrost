if (typeof ko !== 'undefined') {
    ko.bindingHandlers.command = {
        init: function (element, valueAccessor, allBindingAccessor, viewModel) {
			var value = valueAccessor();
			var command;
			var contextBound = false;
			if( typeof value.canExecute === "undefined" ) {
				command = value.target;
				
				command.parameters = command.parameters || {};
				var parameters = value.parameters || {};
				
				for( var parameter in parameters ) {
					var parameterValue = parameters[parameter];
					
					if( command.parameters.hasOwnProperty(parameter) &&
						ko.isObservable(command.parameters[parameter]) ) {
						command.parameters[parameter](parameterValue);
					} else {
						command.parameters[parameter] = ko.observable(parameterValue);
					}
				}
				contextBound = true;
			} else {
				command = value;
			}
            ko.applyBindingsToNode(element, { click: function() {
				// TODO: Investigate further - idea was to support a "context-sensitive" way of dynamically inserting 
				// parameters before execution of the command
				/*
				if( !contextBound ) {
					command.parameters = command.parameters || {};					
					for( var parameter in command.parameters ) {
						if( viewModel.hasOwnProperty(parameter) ) {
							var parameterValue = viewModel[parameter];
							if( ko.isObservable(command.parameters[parameter]) ) {
								command.parameters[parameter](parameterValue);
							} else {
								command.parameters[parameter] = parameterValue;								
							}
						}
					}
				}
				*/
	
				command.execute();
			}}, viewModel);
        }
    };
}