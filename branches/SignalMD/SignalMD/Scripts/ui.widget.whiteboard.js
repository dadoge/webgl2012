(function ($)
{

    $.widget("ui.whiteboard",
    {

        _coordinates: null,
        _drawingMode: false,
        _isInk: 1,
        _namespace: ".whiteboard",
        options:
        {
            inkcolor: "#000000",
            linethickness: 1
        },

        // Set up the widget
        _create: function ()
        {
            $.connection.hub.start();
            this._createChildren(true);
            this._toggleInk();
            this._bindEvents(true);
        },

        // Use the _setOption method to respond to changes to options
        _bindEvents: function (enable)
        {
            var self = this;
            if (!enable)
            {

                this._getToolboxClearDrawing().unbind("click" + self._namespace);
                this._getDrawingBoardCanvas().unbind("mousedown" + self._namespace);
                this._getDrawingBoardCanvas().unbind("mouseup" + self._namespace);
                this._getDrawingBoardCanvas().unbind("mousemove" + self._namespace);
                this._getDrawingBoardCanvas().unbind("draw" + self._namespace);
                this._getDrawingBoardCanvas().unbind("drawmultiple" + self._namespace);

                return;
            }

            this._getToolboxClearDrawing().bind("click" + self._namespace, function ()
            {
                self._getMessenger().removeDrawing();
                return false;
            });
            this._getDrawingBoardCanvas().bind("draw" + self._namespace, function (event, data)
            {
                self._getMessenger().sendDrawing(data);

            });
            this._getDrawingBoardCanvas().bind("drawmultiple" + self._namespace, function (event)
            {


            });
            this._getDrawingBoardCanvas().bind("mousedown" + self._namespace, function ()
            {
                self._drawingMode = true;
                self._coordinates = "" + self._isInk;
                self._getDrawingContext().beginPath();
            });
            this._getDrawingBoardCanvas().bind("mouseup" + self._namespace, function ()
            {
                self._drawingMode = false;
                self._getDrawingContext().beginPath();
                self._getDrawingBoardCanvas().trigger("draw" + self._namespace, self._coordinates);
                self._coordinates = "" + self._isInk;
            });
            this._getDrawingBoardCanvas().bind("mousemove" + self._namespace, function (event)
            {
                if (event.target.getAttribute("id") == "drawing-canvas")
                {
                    if (self._drawingMode)
                    {
                        self._getDrawingContext().lineTo((event.clientX - self._getDrawingBoardCanvas().offset().left), (event.clientY - self._getDrawingBoardCanvas().offset().top));
                        self._coordinates += "#" + (event.clientX - self._getDrawingBoardCanvas().offset().left) + "," + (event.clientY - self._getDrawingBoardCanvas().offset().top);
                        self._getDrawingContext().stroke();
                    }
                }
            });
            this._getMessenger().clearDrawing = function ()
            {
                self.clearCanvas();
            };

            this._getMessenger().updateDrawing = function (drawingData)
            {
                self._drawPoints(drawingData);
            };
        },
        _createChildren: function (create)
        {
            if (!create)
            {
                this._getDrawingBoardCanvas().remove();
                this._getDrawingBoardCanvasContainer().remove();
                this._getToolboxClearDrawing().remove();
                this._getToolbox().remove();
                return;
            }
            this._createDrawingBoard();
            this._createToolBox();
        },
        _createDrawingBoard: function ()
        {
            var drawingBoard = $("<div/>").attr("id", "drawing-board");
            var drawingBoardCanvas = $("<canvas/>")
                .attr("id", "drawing-canvas")
                .attr("height", "600")
                .attr("width", "1000");
            drawingBoard.append(drawingBoardCanvas);
            this.element.append(drawingBoard);
        },
        _createToolBox: function ()
        {
            var toolbox = $("<div/>")
                .attr("id", "toolbox")
                .attr("class", "btn-group");
            var toolboxClearCanvas = $("<a/>")
                .attr("id", "clear-drawing")
                .attr("class", "btn")
                .attr("href", "#")
                .attr("title", "Clear Drawing");
            var toolboxIcon = $("<i/>")
                .attr("class", "icon-trash");
            toolboxClearCanvas.append(toolboxIcon);
            toolbox.append(toolboxClearCanvas);
            this.element.append(toolbox);
        },
        _drawPoint: function (drawingData)
        {
            var coordinates = drawingData.split(",");
            var xCoordinate = coordinates[0];
            var yCoordinate = coordinates[1];
            this._getDrawingContext().lineTo((xCoordinate), (yCoordinate));
            this._getDrawingContext().stroke();
        },
        _drawPoints: function (drawingData)
        {
            this._getDrawingContext().beginPath();
            var coordinatePairs = drawingData.split("#");
            var drawingUtencil = coordinatePairs[0];

            this._isInk = drawingUtencil;

            if (this._isInk == "1")
                this._toggleInk();
            else
                this._toggleEraser();

            for (var i = 1; i < coordinatePairs.length; i++)
            {
                if (coordinatePairs[i] != "")
                {
                    this._drawPoint(coordinatePairs[i]);
                }
            }

        },



        _getDrawingBoardCanvas: function ()
        {
            return this.element.find("#drawing-canvas");
        },
        _getDrawingBoardCanvasContainer: function ()
        {
            return this.element.find("#drawing-board");
        },
        _getDrawingContext: function ()
        {
            return this._getDrawingBoardCanvas()[0].getContext("2d");
        },

        _getMessenger: function ()
        {
            return $.connection.messenger;
        },
        _getToolbox: function ()
        {
            return this.element.find("#toolbox");
        },
        _getToolboxClearDrawing: function ()
        {
            return this.element.find("#clear-drawing");
        },
        _setOption: function (key, value)
        {

            switch (key)
            {
                case "clear":
                    break;
            }
            $.Widget.prototype._setOption.apply(this, arguments);
        },

        _toggleEraser: function ()
        {
            this._isInk = 0;
            this._getDrawingContext().strokeStyle = "#ffffff";
            this._getDrawingContext().lineWidth = 20;
        },
        _toggleInk: function ()
        {
            this._isInk = 1;
            this._getDrawingContext().strokeStyle = this.options.inkcolor;
            this._getDrawingContext().lineWidth = this.options.linethickness;
        },
        destroy: function ()
        {

            $.Widget.prototype.destroy.call(this);
            this._bindEvents(false);
            this._createChildren(false);
        },
        clearCanvas: function ()
        {
            this._getDrawingContext().clearRect(0, 0, this.element.width(), this.element.height());
        }

    });

} (jQuery));

