var Bifrost = Bifrost || {};

(function ($) {
	function ViewModel(definition, isSingleton) {
		var self = this;
		
		self.definition = definition;
		self.isSingleton = isSingleton;
		
		self.getInstance = function() {
			if( isSingleton ) {
				if( !self.instance ) {
					self.instance = new self.definition();
				}
				return self.instance;
			}
			
			return new self.definition();
		}
	}
	
		
	function Feature(name, view, viewModel, path) {
		
		this.name = name;
		this.view = view;
		this.viewModel = viewModel;
		this.path = path;
		
		var self = this;

		this.load = function(loaded) {
		
			//var path = "/Features/"+self.name;
			var view = "text!"+self.path+"/"+self.view+".html!strip";
			//var styles = "text!"+path+"/views.css";
			var viewModelPath = self.path+"/"+self.viewModel+".js";

//			require([view, styles, viewModelPath], function(v,s) {
			require([view, viewModelPath], function(v) {
				self.view = v;
				//self.stylesheet = s;
			
				if( loaded ) {
					loaded(self);
				}
			});
		}
		
		this.defineViewModel = function(viewModel, isSingleton) {
			self.viewModel = new ViewModel(viewModel, isSingleton);
		}
		
		this.renderTo = function(target) {
			$(target).append(self.view);
			var viewModel = self.viewModel.getInstance();
			ko.applyBindings(viewModel, target);
		}
	}
	
	function Container(name) {
		this.name = name;
		var self = this;
		
		this.isRoot = function() {
			return self.name === "root";
		}
		
		this.getBasePath = function(isAdministration) {
			var path = isAdministration ? "/administration" : "/features";
			if( self.isRoot() ) {
				return path;
			}
			return path+"/"+self.name;
		}
		
		this.addFeature = function(isAdministration, name, loaded) {
			var view = "view"
			var viewModel = "viewModel";
			
			var path = self.getBasePath(isAdministration);
			
			if( !self.isRoot() ) {
				view = name;
				viewModel = name;
			} else {
				path = path+"/"+name;
			}
			
			var feature = new Feature(name, view, viewModel, path);
			self[name] = feature;
			feature.load(loaded);
		}
	}

	Bifrost.features = $.extend(new Container("root"), {
		addOrGetContainer: function(name, isAdministration) {
			if( Bifrost.features[name] != undefined ) {
				return Bifrost.features[name];
			}
			var container = new Container(name, isAdministration);
			Bifrost.features[name] = container;
			return container;
		}
	});

	
	$(function() {
		$("*[data-feature]").each(function() {
			var target = $(this);
			var feature = $(this).attr("data-feature");
			var isAdministration = $(this).attr("data-admin") != undefined ? true : false;

			var container = feature;
			var name = feature;
			var root = Bifrost.features;
			
			if( feature.indexOf('/') > 0 ) {
				var elements = feature.split('/');
				container = elements[0];
				name = elements[1];
				root = Bifrost.features.addOrGetContainer(container);
			}
			
			root.addFeature(isAdministration, name, function(f) {
				f.renderTo(target[0]);
			});
		});
	});
})(jQuery);


