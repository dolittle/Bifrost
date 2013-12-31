var globalId = 0;
Bifrost.namespace("Bifrost.interaction.visualStateActions", {
	Opacity: Bifrost.interaction.VisualStateAction.extend(function(documentService) {
		var self = this;
		var element = null;
		var id = documentService.getUniqueStyleName("opacity");

		this.target = "";
		this.value = "";


		this.initialize = function(namingRoot) {
			element = namingRoot.find(self.target);
		};

		this.onEnter = function(namingRoot, duration) {
			var value = parseFloat(self.value);
			if( isNaN(value) ) value = 0.0;

			var actualDuration = duration.totalMilliseconds() / 1000;

			documentService.addStyle("."+id, {
				"-webkit-transition": "opacity "+actualDuration+"s ease-in-out",
				"transition": "opacity "+actualDuration+"s ease-in-out",
				"opacity":value
			})

			element.classList.add(id);


			/*
			if( element != null ) { 
				element.style.opacity = value;
			}*/
		};

		this.onExit = function(namingRoot, duration) {
			element.classList.remove(id);
		};
	})
})