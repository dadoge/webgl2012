var createSceneTutoCollisions = function (engine) {

    //Load a basic scene (see tuto1 and tuto2)
    var scene = new BABYLON.Scene(engine);
    var light0 = new BABYLON.DirectionalLight("Omni", new BABYLON.Vector3(-2, -5, 2), scene);
    var light1 = new BABYLON.PointLight("Omni", new BABYLON.Vector3(2, -5, -2), scene);
    var camera = new BABYLON.FreeCamera("FreeCamera", new BABYLON.Vector3(0, 0, -20), scene);

    //Ground
    var ground = BABYLON.Mesh.CreatePlane("ground", 20.0, scene);
    ground.material = new BABYLON.StandardMaterial("groundMat", scene);
    ground.material.diffuseColor = new BABYLON.Color3(1,1,1);
    ground.material.backFaceCulling = false;
    ground.position = new BABYLON.Vector3(5, -10, -15);
    ground.rotation = new BABYLON.Vector3(Math.PI / 2, 0, 0);


    //Simple crate
    var box = new BABYLON.Mesh.CreateBox("crate", 2, scene);
    box.material = new BABYLON.StandardMaterial("Mat", scene);
    box.material.diffuseTexture = new BABYLON.Texture("crate.png", scene);
    box.material.diffuseTexture.hasAlpha = true;
    box.position = new BABYLON.Vector3(5,-9,-10);


    //COLLISIONS BY GRAVITY
    //---------------------

    //Set gravity to the scene (G force like, on Y-axis. Very low here, welcome on moon)
    scene.gravity = new BABYLON.Vector3(0, -0.1, 0);
   
    // Enable Collisions
    scene.collisionsEnabled = true;

    //Then apply collisions and gravity to the active camera
    camera.checkCollisions = true;
    camera.applyGravity = true;
    //Set the ellipsoid around the camera (e.g. your player's size)
    camera.ellipsoid = new BABYLON.Vector3(1, 1, 1);

    //finally, say which mesh will be collisionable
    ground.checkCollisions = true;
    box.checkCollisions = true;

    return scene;
};