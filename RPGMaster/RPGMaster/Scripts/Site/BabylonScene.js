function MapEditor(engine) {
    //Creation of the scene 
    var scene = new BABYLON.Scene(engine);
    scene.clearColor = new BABYLON.Color4(0, 0, 0, 0);

    //Adding the light to the scene
    var light = new BABYLON.PointLight("Omni", new BABYLON.Vector3(0, 100, 100), scene);

    //Adding an Arc Rotate Camera
    //var camera = new BABYLON.FreeCamera("FreeCamera", new BABYLON.Vector3(0, 1, -15), scene);
    var camera = new BABYLON.ArcRotateCamera("Camera", -Math.PI / 3, 1.2, 30, new BABYLON.Vector3.Zero(), scene);
    
    scene.tileMaterialFloor = new BABYLON.StandardMaterial("tile-texture-Floor", scene);
    scene.tileMaterialFloor.diffuseColor = new BABYLON.Color3(0.6, 0.6, 0.58);

    scene.tileMaterialWall = new BABYLON.StandardMaterial("tile-texture-Wall", scene);
    scene.tileMaterialWall.diffuseColor = new BABYLON.Color3(0.2, 0.2, 0.18);

    scene.tile = [];
    scene.updateQueue = [];
    scene.loadMap = 0;
    scene.updateQueue.tileId = [];
    gameStateHelper.gameMap.Tiles = [];
    var MapHeight = 20;
    var MapLength = 20
    var tileZ = 0; var tileX = -(MapHeight/2 - 1) * 2.7;
    zOffset = -(MapLength / 2 - 1) * 2.7;
    for (var i = 0; i < (MapHeight*MapLength); i++) {
        if (tileZ > (MapLength - 1)) {
            tileZ = 0;
            tileX += 2.7;
        }
        scene.tile[i] = BABYLON.Mesh.CreateCylinder("tile-" + i, .25, 3, 3, 6, scene, false);
        scene.tile[i].material = scene.tileMaterialFloor;
        scene.tile[i].tileId = i;
        scene.tile[i].position = new BABYLON.Vector3(tileX - 1.35 * (tileZ % 2), 0, tileZ * 2.35 + zOffset);
        var mapTile = new MapTile();
        mapTile.insertTileData(scene.tile[i]);
        gameStateHelper.gameMap.Name = "Map-00";
        gameStateHelper.gameMap.Tiles.push(mapTile);
        tileZ++;
    }

    //BABYLON.SceneLoader.ImportMesh("", "../BabylonModels/", "golem_clean.babylon", scene, function (meshes) {
    //    var m = meshes[0];
    //    m.isVisible = true;
    //    //m.scaling = new BABYLON.Vector3(20, 20, 20);
    //    m.position = new BABYLON.Vector3(0, 0, 0);
    //    WOLF_MODEL = m;
    //});
    //var newMeshes;
    //BABYLON.SceneLoader.ImportMesh("", "../BabylonModels/", "Crow-rev001.babylon", scene, function (NewMeshes) {
    //    this.newMeshes = NewMeshes;
    //});

    //var test = newMeshes;

    scene.registerBeforeRender(function () {
        //if (box.intersectsMesh(plan, true)) {

        //} else {
        //    box.position.y -= 1;
        //}
    });

    //When click event is raised
    //document.getElementById('interactive-inner').removeEventListener("click",arguments.callee,false);
    document.getElementById('interactive-inner').addEventListener("click", function (evt) {
        //alert("Screen Width: " + screen.width + "\nScreen Height: " + screen.height + "\nInner Width: " + window.innerWidth + "\nOuter Width: " + window.outerWidth + "\n\nClick X: " + evt.clientX + "\nClick Y: " + evt.clientY);
        //alert("Click X: " + evt.clientX + "\nClick Y: " + evt.clientY + "\n\nOffset X: " + evt.offsetX + "\nOffset Y: " + evt.offsetY + "\n\nTotal X: " + (evt.clientX + evt.offsetX) + "\nTotal Y: " + (evt.clientY + evt.offsetY));
        var scale = getScale();
        if (scale == null) {
            scale = 1;
        }
        // We try to pick an object
        //evt.clientY*scale -
        //evt.clientX*scale -
        var pickResult = scene.pick(evt.offsetX, evt.offsetY);

        // if the click hits the ground object, we change the impact position
        if (pickResult.hit) {
            $.updateTile(pickResult.pickedMesh.tileId);
        }
    });

    return scene;

}

this.getScale = function () {
    this.viewportScale = undefined;
    // Calculate viewport scale 
    this.viewportScale = screen.width / window.innerWidth;
    return this.viewportScale;
};