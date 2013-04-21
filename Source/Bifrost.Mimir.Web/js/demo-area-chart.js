jQuery(document).ready(function ($) {

	// this should come from the server
	var datapoints = [];

	function random_datapoints(length, min, max) {
		var result = [fix((min + max) / 2 + Math.random() * 10 - 5)];
		for (var i = 1; i < length; ++i) {
			result[i] = fix(result[i - 1] + Math.random() * 10 - 5);
		}
		function fix(x) {
			if (x < min) return min;
			else if (x > max) return max;
			else return x;
		}
		return result;
	}

	// convert the datapoints in an array of [x, y] pairs
	function make_dataseries(datapoints) {
		var result = [];
		for (var i = 0; i < datapoints.length; ++i)
			result.push([i, datapoints[i]]);
		return result;
	}

	// setup plot
	var options = {
		// series: { shadowSize: 0 },
		yaxis: { min: 0, max: 100 },
		xaxis: { min: 0, max: 100 },
		colors: ["#48e", "#222", "#666", "#BBB"],
		series: {
			shadowSize: 0,
			lines: { 
				lineWidth: 2, 
				fill: true,
				fillColor: { colors: [ { opacity: 0.6 }, { opacity: 0.2 } ] },
				steps: false
			}
		}
	};
	
	
	$.plot($("#stats-area-chart"), [make_dataseries(random_datapoints(100, 0, 100))], options);

});