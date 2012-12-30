
function feature(icon, name, title, children) {
    var self = this;
    this.icon = icon;
    this.name = name;
    this.title = title;
    this.children = children || [];

    $.each(this.children, function (index, item) {
        item.parent = self;
    });
}

feature.prototype.toString = function () {
    return this.name;
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

$(function () {
    var max_h = 0;

    this.sidebar_fix_init = function () {
        var self = this;

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

    this.sidebar_fix_init();
});

Bifrost.features.featureManager.get("SideBar").defineViewModel(function () {
    var self = this;
    this.features = [
        new feature("home", "Dashboard", "Dashboard"),
        new feature("tasks", "Tasks", "Tasks"),
        new feature("bar-chart", "Statistics", "Statistics"),
        new feature("reorder", "EventViewer", "Event Viewer"),
        new feature("table", "EventSubscriptions", "Event Subscriptions"),
        new feature("group", "", "Users", [
            new feature("cog", "Users/Accounts", "Accounts"),
            new feature("cog", "Users/Settings", "Settings"),
        ])
    ];

    this.currentFeature = ko.observableMessage("currentFeatureChanged", self.features[0]);
    this.currentSubFeature = ko.observable(new feature("", "", ""));

    this.navigateTo = function (selectedFeature) {
        if (selectedFeature.children.length > 0) {
            return;
        }

        if (typeof selectedFeature.parent !== "undefined") {
            self.currentSubFeature(selectedFeature);
            self.currentFeature(selectedFeature.parent);
        } else {
            self.currentFeature(selectedFeature);
            self.currentSubFeature(new feature("", "", ""));
        }
    }
});