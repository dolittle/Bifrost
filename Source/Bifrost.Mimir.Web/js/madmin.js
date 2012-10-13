var madmin = (function () {

	var max_h = 0;

	var self = {
		init: function () {

			this.menu_init();
			this.panel_pages_init();
			this.sidebar_fix_init();
		},
		menu_init: function () {

			$menu_lis = $('.sidebar-menu.on-click li');
			$menu_links = $('.sidebar-menu.on-click li > div > a, .sidebar-menu.on-click li > a');

			$menu_links.on('click.madmin-menu_init', function (e) {

				$this =  $(this);

				$parent_li = $this.parent();
				if ($parent_li.prop('tagName') == 'DIV')  {

					$parent_li = $parent_li.parent();
					var $root_li = $parent_li;

				} else {
					var $root_li = $parent_li.parent().parent().parent();
				}

				if (!$parent_li.hasClass('parent') || $parent_li.hasClass('open')) {

					if ($this.attr('href') != '#' || $this.attr('data-target-page')) {

						$parent_li
							.addClass('active').removeClass('inactive')
							.removeClass('open')
							.siblings()
								.removeClass('active').addClass('inactive')
								.removeClass('open')
								.find('li')
									.removeClass('active').addClass('inactive');
						$root_li
							.addClass('active').removeClass('inactive')
							.removeClass('open')
							.siblings()
								.removeClass('active').addClass('inactive')
								.removeClass('open')
								.find('li')
									.removeClass('active').addClass('inactive');
						self.fix_sidebar();
						return true;
					}
					else $parent_li.removeClass('open');
				}
				else {
					$menu_lis.removeClass('open');
					if ($parent_li.hasClass('parent')) $parent_li.addClass('open');
				}
				self.fix_sidebar();
				return false;
			});

			$('body').on('click.madmin-menu_init', function (e) {
				$target = $(e.target);
				$open = $('.open');
				if (!$target.is($open) && !$target.is($('.open *'))) $open.removeClass('open');
			})
		},
		menu_unset: function () {

			$('.sidebar-menu li.parent > div > a, .sidebar-menuli > a')
				.off('click.madmin-menu_init');

			$('body').off('click.madmin-menu_init');
		},
		panel_pages_init: function () {

			$('a[data-target-page]').on('click', function (e) {

				$('.page').addClass('hidden');
				$('#' + $(this).attr('data-target-page')).removeClass('hidden')

				return false;
			});
		},
		sidebar_fix_init: function() {

			$active_li = $('.sidebar-menu > li.active');

			$('.sidebar-menu > li').each(function (i) {
				$(this)
					.addClass('active').removeClass('inactive)')
					.siblings()
						.addClass('inactive').removeClass('active');
				var h = $('#sidebar')[0].scrollHeight
				if (h > max_h) max_h = h;
			});

			max_h += 50;

			$active_li
				.addClass('active').removeClass('inactive')
				.siblings()
					.addClass('inactive').removeClass('active');

			this.fix_sidebar();

			var timer_fix_sidebar;
			$(window).resize(function (e) {
				if (timer_fix_sidebar) clearTimeout(timer_fix_sidebar);
				timer_fix_sidebar = setTimeout(self.fix_sidebar, 100);
			});

			$('#sidebar-resizer').on('click', function (e) {
				var $body = $('body');
				if ($body.hasClass('sidebar-max')) {
					$body.addClass('sidebar-min').removeClass('sidebar-max');
				} else {
					$body.addClass('sidebar-max').removeClass('sidebar-min');
				}
				self.fix_sidebar();
			});
		},
		fix_sidebar: function () {

			var $sidebar = $('#sidebar');
			
			if ($(window).height() < max_h)
				$sidebar.removeClass('fixed');
			else
				$sidebar.addClass('fixed');
		}
	};

	return self;

})();