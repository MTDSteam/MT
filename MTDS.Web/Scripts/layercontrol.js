var _glayerLengend = undefined;
var InitLegend = function(layername,layers){
	_glayerLengend = L.control.htmllegend({
			position: 'topright',
			legends: [{
				name: layername,
				layer: layers,
				opacity: 1,
				elements: [{
					label: '',
					html: '',
					style: {
						'background-color': 'lightgreen',
						'width': '2px',
						'height': '10px'
					}
				}]
			}],
			collapseSimple: true,
			detectStretched: true,
			collapsedOnInit: true,
			defaultOpacity: 1,
			visibleIcon: 'icon icon-eye',
			hiddenIcon: 'icon icon-eye-splash'
		});
};

var UpdataLegend = function(layername,layers){
	_glayerLengend.addLegend({
        name: layername,
        layer: layers,
        opacity: 1,
        elements: [{
            html: '',
            style: {
                'background-color': 'lightgreen',
							'width': '2px',
							'height': '10px'
            }
        }]
    });
};

var SelectLegend = function(layername)
{
	for(var i =0; i < _glayerLengend.options.legends.length;i++)
	{
		if(_glayerLengend.options.legends[i].name == layername)
			return _glayerLengend.options.legends[i];
	}
	return undefined;
};

var ReRenderLegend = function()
{
	_glayerLengend.render();
};

var LegendCount = function()
{
	return _glayerLengend.options.legends.length;
};

var LegendName = function(layerIndex)
{
	if(layerIndex >= _glayerLengend.options.legends.length)
		return undefined;
	return _glayerLengend.options.legends[layerIndex].name;
};



var addToMap = function(map){
	map.addControl(_glayerLengend);	
}

