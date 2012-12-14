jQuery(document).ready(function ($) {

	madmin.init();

	$("#table1").tablesorter({widgets: ['zebra']});

	$('a.help-link').pageslide({
		direction: 'left'
	});

});
