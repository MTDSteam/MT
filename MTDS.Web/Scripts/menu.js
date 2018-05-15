
var _gImageIndex = 0;
var anchors =[]; 
var currentLayer = undefined;
var editmarker = [];

(function() {
  L.Control.ImageLoader = L.Control.extend({
    options: {
      position: 'topleft',
      imgTitle: "导入厂区图",
      endTitle: "结束编辑厂区图",
	  drawTitle: "绘制厂区图区域"
    },

    onAdd: function(map) {

      // Set options
      if (!this.options.center) {
        this.options.center = map.getCenter();
      }
      if (!this.options.zoom) {
        this.options.zoom = map.getZoom();
      }
      var options = this.options;

      // Create toolbar
      var controlName = 'leaflet-control-menu',
      container = L.DomUtil.create('div', controlName + ' leaflet-bar');

      // Add toolbar buttons
      this._loadButton = this._createButton(options.imgTitle, controlName + '-loadimg', container, this._loadImg);
      this._endButton = this._createButton(options.endTitle, controlName + '-end', container, this._endImg);
	  this._superButton = this._createButton(options.drawTitle, controlName + '-draw', container, this._startDraw);
      // Initialize view history and index
      //this._viewHistory = [{center: this.options.center, zoom: this.options.zoom}];
      this._curIndx = 0;
      //map.once('moveend', function() {this._map.on('moveend', this._updateHistory, this);}, this);
      // Set intial view to home
      //map.setView(options.center, options.zoom);

      return container;
    },

    onRemove: function(map) {
      //map.off('moveend', this._updateHistory, this);
    },

	 _loadImg: function() {
     		document.getElementById("loadfile").click();
    },
	
	_startDraw: function() {
     		var Layers = map.editTools.startPolygon();
			UpdataLegend("绘制厂区"+_gImageIndex,Layers);
			_gImageIndex++;
    },
	
    _endImg: function() {
    		if(_gImageIndex > 0)
    		{
    			var legend = SelectLegend("导入图片"+_gImageIndex);
		    	if(legend != undefined)
		    	{
		    		legend.elements[0].style['background-color'] = 'lightgreen';
		    		legend.opacity =1;
		    	}
		    	ReRenderLegend();
		    	for(var n = 0; n < editmarker.length;n++)
		    	{
		    		editmarker[n].remove(map);
		    	}
		    	editmarker.splice(0,editmarker.length);
		    	
		    	
    		}
    },
    _transferImage: function(map,file) {
    	
    	this._endImg();
    	var bounds = map.getBounds();
    	var xMin = bounds.getWest();
    	var xMax = bounds.getEast();
    	var yMin = bounds.getSouth();
    	var yMax = bounds.getNorth();
    	var cx = xMax - xMin;
    	var cy = yMax - yMin;
    	
    	//var anchors = [[90,-180.0], [90,180.0], [-90,180.0], [ -90,-180]];
    	anchors = [[yMax-cy/4,xMin+cx/4], [yMax-cy/4,xMax - cx/4], [yMin + cy/4,xMax - cx/4], [yMin + cy/4,xMin+cx/4]];
    	currentLayer = L.imageTransform(file, anchors, { opacity: 1}).addTo(map);
    	_gImageIndex++;
    	UpdataLegend("导入图片"+_gImageIndex,currentLayer);
    	var legend = SelectLegend("导入图片"+_gImageIndex);
    	if(legend != undefined)
    	{
    		legend.elements[0].style['background-color'] = 'red';
    		legend.opacity =0.7;
    		for(var a = 0; a < anchors.length;a++)
    		{
    			var markers = L.marker(anchors[a],{draggable: true}).addTo(map);
    			markers.enableEdit();
    			markers.on('drag', updateAnchors);
    			editmarker.push(markers);
    		}
    		
    	}
    	ReRenderLegend();
    },

    _createButton: function(title, className, container, fn) {
      // Modified from Leaflet zoom control
     
      var link = L.DomUtil.create('a', className, container);
      link.href = '#';
      link.title = title;
      L.DomEvent
      .on(link, 'mousedown dblclick', L.DomEvent.stopPropagation)
      .on(link, 'click', L.DomEvent.stop)
      .on(link, 'click', fn, this)
      .on(link, 'click', this._refocusOnMap, this);

      return link;
    }
  });

  L.control.imgload = function(options) {
    return new L.Control.ImageLoader(options);
  };

})();


var updateAnchors = function()
{
	if(currentLayer == undefined)
		return;
	anchors.splice(0,anchors.length);
	for(var i =0; i < editmarker.length;i++)
	{
		anchors.push(editmarker[i].getLatLng());
	}
	currentLayer.setAnchors(anchors);
};
