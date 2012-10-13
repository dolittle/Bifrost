
function feature(icon, name, children) {
    var self = this;
    this.icon = icon;
    this.name = name;
    this.children = children || [];

    $.each(this.children, function (index, item) {
        item.parent = self;
    });
}

ko.bindingHandlers.childMenu = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var children = valueAccessor();
        if (children.length > 0) {
            $(element).click(function () {
                $(element).addClass("open");
                return false;
            });
        }
    }
}


Bifrost.features.featureManager.get("SideBar").defineViewModel(function () {
    var self = this;
    var max_h = 0;

    this.features = [
        new feature("home", "Dashboard"),
        new feature("bar-chart", "Statistics"),
        new feature("group", "Users", [ 
            new feature("cog", "Accounts"),
            new feature("cog", "Settings"),
        ])
    ];

    this.currentFeature = ko.observable(self.features[0]);
    this.currentSubFeature = ko.observable(new feature("",""));

    this.navigateTo = function (feature) {
        if (feature.children.length > 0) {
            return;
        }

        if (typeof feature.parent !== "undefined") {
            self.currentSubFeature(feature);
            self.currentFeature(feature.parent);
        } else {
            self.currentFeature(feature);
            self.currentSubFeature(new feature("", ""));
        }
    }

    this.menu_init = function () {

        $menu_lis = $('.sidebar-menu.on-click li');
        $menu_links = $('.sidebar-menu.on-click li > div > a, .sidebar-menu.on-click li > a');

        $menu_links.on('click.madmin-menu_init', function (e) {

            $this = $(this);

            $parent_li = $this.parent();
            if ($parent_li.prop('tagName') == 'DIV') {

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
    };

    this.menu_unset = function () {

        $('.sidebar-menu li.parent > div > a, .sidebar-menuli > a')
            .off('click.madmin-menu_init');

        $('body').off('click.madmin-menu_init');
    };
    this.panel_pages_init = function () {

        $('a[data-target-page]').on('click', function (e) {

            $('.page').addClass('hidden');
            $('#' + $(this).attr('data-target-page')).removeClass('hidden')

            return false;
        });
    };

    this.sidebar_fix_init = function () {

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
    };
    this.fix_sidebar = function () {

        var $sidebar = $('#sidebar');

        if ($(window).height() < max_h)
            $sidebar.removeClass('fixed');
        else
            $sidebar.addClass('fixed');
    };

    this.menu_init();
    this.panel_pages_init();
    this.sidebar_fix_init();

});