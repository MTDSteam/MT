var timeAxis = undefined;
var clockID = undefined;
function axisStart()
{
	if(timeAxis == undefined)
		return;
	if(timeAxis.options.end ==1)
		window.clearInterval(clockID);	
	timeAxis._play(50);	
}


(function() {
	L.Control.TimeAxis =L.Control.extend({
		 options: {
      position: 'topleft',
      map:undefined,
      startPosion:undefined,//起始位置
      endPosion:undefined,//终点位置
      maxTime:undefined,//总时长 秒
      currentTime:0,		//当前时长,秒
      constFps:20, //每秒帧数,int
      TotalFps:undefined,//总帧数
      speed:undefined,
      end:0,
      start:0
    },
    _Init: function()
    {
    	this.options.TotalFps = parseInt(this.options.maxTime) * this.options.constFps;
    	this.options.speed = 0;
    	this.options.start = 1;
    	this.options.end = 0;
    	this.options.currentTime = 0;
    	for(var a = 0; a < parseInt(this.options.maxTime);a++)
    	{
    		this.options.speed += a+1;
    	}
    },
    
    _startTimer: function() {
    	if(this.options.start == 1)
    		return;
     timeAxis = this;
     this._Init();
     clockID = self.setInterval("axisStart();",50);
   },
    
    _play: function(times) {
    	if(this.options.end == 1)
    	{
    		return;
    	}
    	
    	zoom = this.options.map.zoom;
    	this.options.currentTime += 1.0*times/1000;
    	if(this.options.currentTime >= this.options.maxTime)
    	{
    		this.options.end =1;
    		this.options.start =0;
    		map.flyTo(this.options.endPosion,zoom);
    		return;
    	}
    	var sx = this.options.startPosion[1];
    	var sy = this.options.startPosion[0];
    	var ex = this.options.endPosion[1];
    	var ey = this.options.endPosion[0];
    	var cx = (ex - sx)/this.options.speed;
    	var cy = (ey - sy)/this.options.speed;
    	var step = parseInt(this.options.currentTime)+1;//总共分为多少速度级别
    	var currentMove = 0;
    	for(var a = 0; a < step;a++)
    	{
    		currentMove += a+1;
    	}
    	var Posion = [sy + cy*currentMove , sx + cx*currentMove];
    	map.flyTo(Posion,zoom);
    },
    onAdd: function(map) {
			this.options.map = map;
      var options = this.options;
      // Create toolbar
      var controlName = 'leaflet-control-timeaxis',
      container = L.DomUtil.create('div', controlName + ' leaflet-bar');
      this._endButton = this._createButton(options.endTitle, controlName + '-play', container, this._startTimer);
      return container;
    },

    onRemove: function(map) {
      //map.off('moveend', this._updateHistory, this);
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
    
    L.control.axis = function(options) {
    return new L.Control.TimeAxis(options);
  };
})();