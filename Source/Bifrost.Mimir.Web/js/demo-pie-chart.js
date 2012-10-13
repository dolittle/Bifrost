jQuery(document).ready(function ($) {

	var container = $("#stats-pie-chart");

	var data = [];

	for( var i = 0; i<7; i++)

	{
		data[i] = { label: "Series"+(i+1), data: Math.floor(Math.random()*100)+1 }
	}

    $.plot(container, data, 

	{

		series: {

			pie: { 

				show: true

			}

		},

	});

});