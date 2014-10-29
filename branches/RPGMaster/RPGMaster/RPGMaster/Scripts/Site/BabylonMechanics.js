startMapEditor = function (templateHelper) {

    var fadeSpeed = 300;
    $('#interactive-inner').fadeOut(fadeSpeed, function () {
        $('#interactive-inner').html(templateHelper.mapEditor_btns);
        $('#Character-h').html("<h3>Map Editor</h3>");
        $('#Character-inner').html("<div id=\"rootDiv\"> \
            <input class=\"input-MapName\" data-bind=\"value: ActiveMapName\" />\
            <canvas id=\"renderCanvas\"></canvas></div>");

        var UName = $('#UID').html();
        $.getJSON(["http://www.antonmorgan.com/rpgsvc/rpgsvc/GetUserMaps/" + UName], function (data) {
            var maps = [];
            for (var i = 0; i < data.MapID.length; i++) {
                maps.push({ MapID: data.MapID[i], MapNames: data.Name[i] })
            }
            gameStateHelper.mapEditor_Model = new MapEditor_Model(data,maps);
            ko.applyBindings(gameStateHelper.mapEditor_Model, document.getElementById('Character-inner'));
        });
        $('#mapEditor-Cancel').click(function () { CancelConfirm() });
        $('#mapEditor-Save').click(function () {
            //create dummy gameMap
            if ($.inArray(gameStateHelper.mapEditor_Model.ActiveMapName(), gameStateHelper.mapEditor_Model.MapNames()) > -1) {
                alertBox("Name already exists. Please choose another name.");
            }
            else {
                var activeGameMap = gameStateHelper.gameMap;

                activeGameMap.Name = gameStateHelper.mapEditor_Model.ActiveMapName();
                activeGameMap.TilesData = JSON.stringify(gameStateHelper.gameMap.Tiles);
                activeGameMap.UserName = $('#UID').html();
                gameStateHelper.mapEditor_Model.Maps.push({ MapID: 0, MapNames: gameStateHelper.mapEditor_Model.ActiveMapName() })

                $.ajax({
                    url: 'http://www.antonmorgan.com/rpgsvc/rpgsvc/SaveMap',
                    type: 'POST',
                    data: JSON.stringify(activeGameMap),
                    contentType: 'application/json',
                    dataType: 'json'
                });
                alertBox(["Map " + gameStateHelper.mapEditor_Model.ActiveMapName() + " saved!"]);
            }
        });
        $('#mapEditor-Load').click(function () {
            //get mapID
            alertBox("Select a map to load:<div id=\"alertBox-Dropdown\"> \
                <select data-bind=\"options: Maps, value: ActiveMapID, optionsValue: 'MapID', optionsText: 'MapNames'\" id=\"Map-options\" class=\"form-control input-sm  info-Description\"> \
                </div>", function () {
                    $.loadMap(gameStateHelper.mapEditor_Model.ActiveMapID())
                });
            ko.applyBindings(gameStateHelper.mapEditor_Model, document.getElementById('alertBox-Dropdown'));
        });


        var canvas = document.getElementById("renderCanvas");

        // Check support
        if (!BABYLON.Engine.isSupported()) {
            window.alert('Browser not supported');
        } else {
            // Babylon
            var engine = new BABYLON.Engine(canvas, true);

            //Creating scene (in "scene_tuto.js")
            scene = MapEditor(engine);

            scene.activeCamera.attachControl(canvas);

            // Once the scene is loaded, just register a render loop to render it
            engine.runRenderLoop(function () {
                //create an update queue for various events
                //example: the click event handler passes to everyone trhough signalR
                //update here and clear updateQueue
                if (scene.updateQueue.tileId.length != 0) {

                    for (var i_loop = 0; i_loop < scene.updateQueue.tileId.length ; i_loop++) {
                        var tileToUpdate = scene.tile[scene.updateQueue.tileId[i_loop]];
                        if (tileToUpdate.scaling.y == 1) {
                            tileToUpdate.scaling.y = 10;
                            tileToUpdate.position.y += tileToUpdate.scaling.y * .1125;
                            tileToUpdate.material = scene.tileMaterialWall;
                            gameStateHelper.gameMap.Tiles[scene.updateQueue.tileId[i_loop]].tileType = 1;
                        }
                        else {
                            tileToUpdate.position.y -= tileToUpdate.scaling.y * .1125;
                            tileToUpdate.scaling.y = 1;
                            tileToUpdate.material = scene.tileMaterialFloor;
                            gameStateHelper.gameMap.Tiles[scene.updateQueue.tileId[i_loop]].tileType = 0;
                        }
                    }
                    scene.updateQueue.tileId = [];
                }

                //Handle Pan
                if (isShiftKeyPressed == 1 && isMouseDown == 1) {
                    //camera.inertialBetaOffset = 0;
                    //camara.inertialAlphaOffset = 0;
                    //$.sendMessage("test", "shift");
                }

                if (scene.loadMap == 1) {
                    scene.loadMap = 0;
                    $.getJSON(["http://www.antonmorgan.com/rpgsvc/rpgsvc/LoadMap/" + scene.mapID], function (data) {
                        //Clear some memory

                        var numOfLocalTiles = gameStateHelper.gameMap.Tiles.length;
                        var numOfNewTiles = data.Tiles.length;
                        $('.input-MapName').val(data.Name).change();
                        gameStateHelper.gameMap.Name = data.Name;

                        var i_loop = 0;
                        //dispose of all existing tile Meshes
                        for (i_loop = 0; i_loop < numOfLocalTiles; i_loop++) {
                            scene.tile[i_loop].dispose();
                        }
                        gameStateHelper.gameMap.Tiles = [];
                        scene.tile = [];
                        //Create new Tile Meshes
                        for (i_loop = 0; i_loop < numOfNewTiles; i_loop++) {
                            scene.tile[i_loop] = BABYLON.Mesh.CreateCylinder("tile-" + i_loop, .25, 3, 3, 6, scene, false);
                            scene.tile[i_loop].tileId = i_loop;
                            scene.tile[i_loop].position = new BABYLON.Vector3(data.Tiles[i_loop].posX, 0, data.Tiles[i_loop].posZ);
                            if (data.Tiles[i_loop].tileType == 0) {
                                scene.tile[i_loop].material = scene.tileMaterialFloor;
                            }
                            else {
                                scene.tile[i_loop].material = scene.tileMaterialWall;
                                scene.tile[i_loop].scaling.y = 10;
                                scene.tile[i_loop].position.y += scene.tile[i_loop].scaling.y * .1125;
                            }
                            var mapTile = new MapTile();
                            mapTile.insertTileData(scene.tile[i_loop]);
                            gameStateHelper.gameMap.Tiles.push(mapTile);
                        }
                    });
                }

                //Render scene and any changes
                scene.render();
            });

            // Resize
            window.addEventListener("resize", function () {
                engine.resize();
            });

            
        }

        $('#interactive-inner').fadeIn(fadeSpeed, function () {
            //Force render
            engine.resize();
        });

    });
};