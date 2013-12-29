var globalId = 0;
Bifrost.namespace("Bifrost.interaction.visualStateActions", {
	Opacity: Bifrost.interaction.VisualStateAction.extend(function() {
		var self = this;
		var element = null;
		var id = "opacity"+globalId;
		globalId++;
		//Bifrost.Guid.create().replaceAll("-","");

		this.target = "";
		this.value = "";

function createCSSSelector(selector, style) {
    if(!document.styleSheets) {
        return;
    }

    if(document.getElementsByTagName("head").length == 0) {
        return;
    }

    var stylesheet;
    var mediaType;
    if(document.styleSheets.length > 0) {
        for( i = 0; i < document.styleSheets.length; i++) {
            if(document.styleSheets[i].disabled) {
                continue;
            }
            var media = document.styleSheets[i].media;
            mediaType = typeof media;

            if(mediaType == "string") {
                if(media == "" || (media.indexOf("screen") != -1)) {
                    styleSheet = document.styleSheets[i];
                }
            } else if(mediaType == "object") {
                if(media.mediaText == "" || (media.mediaText.indexOf("screen") != -1)) {
                    styleSheet = document.styleSheets[i];
                }
            }

            if( typeof styleSheet != "undefined") {
                break;
            }
        }
    }

    if( typeof styleSheet == "undefined") {
        var styleSheetElement = document.createElement("style");
        styleSheetElement.type = "text/css";

        document.getElementsByTagName("head")[0].appendChild(styleSheetElement);

        for( i = 0; i < document.styleSheets.length; i++) {
            if(document.styleSheets[i].disabled) {
                continue;
            }
            styleSheet = document.styleSheets[i];
        }

        var media = styleSheet.media;
        mediaType = typeof media;
    }

    if(mediaType == "string") {
        for( i = 0; i < styleSheet.rules.length; i++) {
            if(styleSheet.rules[i].selectorText && styleSheet.rules[i].selectorText.toLowerCase() == selector.toLowerCase()) {
                styleSheet.rules[i].style.cssText = style;
                return;
            }
        }

        styleSheet.addRule(selector, style);
    } else if(mediaType == "object") {
        for( i = 0; i < styleSheet.cssRules.length; i++) {
            if(styleSheet.cssRules[i].selectorText && styleSheet.cssRules[i].selectorText.toLowerCase() == selector.toLowerCase()) {
                styleSheet.cssRules[i].style.cssText = style;
                return;
            }
        }

        styleSheet.insertRule(selector + "{" + style + "}", 0);
    }
}		

		this.initialize = function(namingRoot) {
			element = namingRoot.find(self.target);
		};

		this.onEnter = function(namingRoot, duration) {
			var value = parseFloat(self.value);
			if( isNaN(value) ) value = 0.0;

			createCSSSelector("."+id, "-webkit-transition: opacity 1s ease-in-out; transition: opacity .15s ease-in-out; opacity:"+value+";");

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