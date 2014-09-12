function MapEditor(engine) {
    //Creation of the scene 
    var scene = new BABYLON.Scene(engine);
    scene.clearColor = new BABYLON.Color4(0, 0, 0, 0);

    //Adding the light to the scene
    var light = new BABYLON.PointLight("Omni", new BABYLON.Vector3(0, 100, 100), scene);

    //Adding an Arc Rotate Camera
    var camera = new BABYLON.ArcRotateCamera("Camera", -Math.PI / 3, 1.2, 30, new BABYLON.Vector3.Zero(), scene);


    //var freeCamera = new BABYLON.FreeCamera("FreeCamera", new BABYLON.Vector3(0, 0, -30), scene);

    //var box = BABYLON.Mesh.CreateBox("Box", 6.0, scene);

    //scene.enablePhysics();
    //scene.setGravity(new BABYLON.Vector3(0, -9.81, 0));

    var tileMaterialFloor = new BABYLON.StandardMaterial("texture1", scene);
    tileMaterialFloor.diffuseColor = new BABYLON.Color3(0.6, 0.6, 0.58);

    var tileMaterialWall = new BABYLON.StandardMaterial("texture1", scene);
    tileMaterialWall.diffuseColor = new BABYLON.Color3(0.2, 0.2, 0.18);

    var tile = [];
    var tileZ = 0; var tileX = -4 * 2.7;
    zOffset = -4 * 2.7;
    for (var i = 0; i < 100; i++) {
        if (tileZ > 9) {
            tileZ = 0;
            tileX += 2.7;
        }
        tile[i] = BABYLON.Mesh.CreateCylinder("cylinder", .25, 3, 3, 6, scene, false);
        tile[i].material = tileMaterialFloor;
        tile[i].position = new BABYLON.Vector3(tileX - 1.35 * (tileZ % 2), 0, tileZ * 2.35 + zOffset);
        tileZ++;
    }



    //scene.gravity = new BABYLON.Vector3(0, -9.81, 0);
    //scene.collisionsEnabled = true;
    //box.checkCollisions = true;
    //plan.checkCollisions = true;

    //box.applyGravity = true;


    scene.registerBeforeRender(function () {
        //if (box.intersectsMesh(plan, true)) {

        //} else {
        //    box.position.y -= 1;
        //}
    });

    //When click event is raised
    window.addEventListener("click", function (evt) {
        //alert("Screen Width: " + screen.width + "\nScreen Height: " + screen.height + "\nInner Width: " + window.innerWidth + "\nOuter Width: " + window.outerWidth + "\n\nClick X: " + evt.clientX + "\nClick Y: " + evt.clientY);
        //alert("Click X: " + evt.clientX + "\nClick Y: " + evt.clientY + "\n\nOffset X: " + evt.offsetX + "\nOffset Y: " + evt.offsetY + "\n\nTotal X: " + (evt.clientX + evt.offsetX) + "\nTotal Y: " + (evt.clientY + evt.offsetY));
        var scale = getScale();
        if (scale == null) {
            scale = 1;
        }
        $.sendMessage('jel', 'jel');
        // We try to pick an object
        //evt.clientY*scale -
        //evt.clientX*scale -
        var pickResult = scene.pick(evt.offsetX, evt.offsetY);

        var test = pickResult.pickedMesh;

        // if the click hits the ground object, we change the impact position
        if (pickResult.hit) {
            if (pickResult.pickedMesh.scaling.y == 1) {
                pickResult.pickedMesh.scaling.y = 10;
                pickResult.pickedMesh.position.y += pickResult.pickedMesh.scaling.y * .125;
                pickResult.pickedMesh.material = tileMaterialWall;
            }
            else {
                pickResult.pickedMesh.position.y -= pickResult.pickedMesh.scaling.y * .125;
                pickResult.pickedMesh.scaling.y = 1;
                pickResult.pickedMesh.material = tileMaterialFloor;
            }
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