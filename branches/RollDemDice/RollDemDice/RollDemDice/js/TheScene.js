function createSceneTuto(engine) {
    //Creation of the scene 
    var scene = new BABYLON.Scene(engine);

    //Adding the light to the scene
    var light = new BABYLON.PointLight("Omni", new BABYLON.Vector3(0, 100, 100), scene);

    //Adding an Arc Rotate Camera
    var camera = new BABYLON.ArcRotateCamera("Camera", 0, 0.8, 100, new BABYLON.Vector3.Zero(), scene);

    var box = BABYLON.Mesh.CreateBox("Box", 6.0, scene);

    var plan = BABYLON.Mesh.CreatePlane("Plane", 50.0, scene);
    plan.rotation.x = Math.PI / 2;

    scene.enablePhysics();
    scene.setGravity(new BABYLON.Vector3(0, -9.81, 0));

    box.position = new BABYLON.Vector3(-10, 10, 0);
    scene.gravity = new BABYLON.Vector3(0, -9.81, 0);
    box.applyGravity = true;

    scene.collisionsEnabled = true;
    box.checkCollisions = true;
    plan.checkCollisions = true;


    return scene;

}