var createSceneTutoCollisions = function (engine) {

    //Load basic scene
    var scene = new BABYLON.Scene(engine);
    var light0 = new BABYLON.PointLight("Omni", new BABYLON.Vector3(0, 10, 20), scene);
    var freeCamera = new BABYLON.FreeCamera("FreeCamera", new BABYLON.Vector3(0, 0, -30), scene);


    //impact
    var impact = BABYLON.Mesh.CreatePlane("impact", 1, scene);
    impact.material = new BABYLON.StandardMaterial("impactMat", scene);
    impact.material.diffuseTexture = new BABYLON.Texture("impact.png", scene);
    impact.material.diffuseTexture.hasAlpha = true;
    impact.position = new BABYLON.Vector3(0, 0, -0.1);


    //Wall
    var wall = BABYLON.Mesh.CreatePlane("wall", 20.0, scene);
    wall.material = new BABYLON.StandardMaterial("wallMat", scene);
    wall.material.emissiveColor = new BABYLON.Color3(1, 1, 1);

    //When click event is raised
    window.addEventListener("click", function (evt) {

        // We try to pick an object
        var pickResult = scene.pick(evt.clientX, evt.clientY);

        // if the click hits the ground object, we change the impact position
        if (pickResult.hit) {
            impact.position.x = pickResult.pickedPoint.x;
            impact.position.y = pickResult.pickedPoint.y;
        }

    });


    return scene;
};