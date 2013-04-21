jQuery(document).ready(function ($) {

	// this should come from the server
	var datapoints = [];

	var plot_container = $("#dashboard-dynamic-chart");

	function random_datapoints(length, min, max) {
		var result = [fix((min + max) / 2 + Math.random() * 10 - 5, min, max)];
		for (var i = 1; i < length; ++i) {
			result[i] = fix(result[i - 1] + Math.random() * 10 - 5, min, max);
		}
		return result;
	}
	function update_w_random_datapoint(datapoints) {
		datapoints.shift();
		datapoints.push(fix(datapoints[datapoints.length - 1] + Math.random() * 10 - 5, 0, 100));
		return datapoints;
	}

	function fix(x, min, max) {
		if (x < min) return min;
		else if (x > max) return max;
		else return x;
	}

	// convert the datapoints in an array of [x, y] pairs
	function make_dataseries(datapoints) {
		var result = [];
		for (var i = 0; i < datapoints.length; ++i)
			result.push([i, datapoints[i]]);
		return result;
	}

	var datapoints = random_datapoints(100, 0, 100);

	// setup plot
	var options = {
		series: { shadowSize: 0 },
		yaxis: { min: 0, max: 100 },
		xaxis: { show: false },
		colors: ["#48e", "#222", "#666", "#BBB"]
	};
	
	var plot = $.plot(
		plot_container,
		[make_dataseries(datapoints)],
		options);

	function update() {
		plot.setData([make_dataseries(update_w_random_datapoint(datapoints))]);
		plot.draw();
		setTimeout(update, 100);
	}

	update();

});