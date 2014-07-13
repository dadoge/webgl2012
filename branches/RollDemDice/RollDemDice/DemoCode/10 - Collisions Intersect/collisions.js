var createSceneTutoCollisions = function (engine) {

    var scene = new BABYLON.Scene(engine);
    var light0 = new BABYLON.PointLight("Omni", new BABYLON.Vector3(0, 10, 10), scene);
    var camera = new BABYLON.ArcRotateCamera("Camera", 1, 0.8, 70, new BABYLON.Vector3(5, 0, 0), scene);

    var matPlan = new BABYLON.StandardMaterial("matPlan1", scene);
    matPlan.backFaceCulling = false;
    matPlan.emissiveColor = new BABYLON.Color4(0.2, 1, 0.2, 1);

    //origin
    var pointToIntersect = new BABYLON.Vector3(-30, 0, 0);
    var origin = BABYLON.Mesh.CreateSphere("origin", 4, 0.3, scene);
    origin.position = pointToIntersect;
    origin.material = matPlan;

    //2 planes
    var plan1 = new BABYLON.Mesh.CreatePlane("plane1", 20, scene);
    plan1.position = new BABYLON.Vector3(13, 0, 0);
    plan1.rotation.x = -Math.PI / 4;
    plan1.material = matPlan;

    var plan2 = new BABYLON.Mesh.CreatePlane("plane2", 20, scene);
    plan2.position = new BABYLON.Vector3(-13, 0, 0);
    plan2.rotation.x = -Math.PI/4;
    plan2.material = matPlan;
    

    //AABB
    var planAABB = new BABYLON.Mesh.CreateBox("AABB", 20, scene);
    planAABB.material = new BABYLON.StandardMaterial("matPlan2", scene);
    planAABB.position = new BABYLON.Vector3(13, 0, 0);
    planAABB.scaling = new BABYLON.Vector3(1, Math.cos(Math.PI / 4), Math.cos(Math.PI / 4));
    planAABB.material.wireframe = true;

    //OBB
    var planOBB = new BABYLON.Mesh.CreateBox("OBB", 20, scene);
    planOBB.scaling = new BABYLON.Vector3(1, 1, 0.05);
    planOBB.parent = plan2;
    planOBB.material = new BABYLON.StandardMaterial("matPlan3", scene);
    planOBB.material.wireframe = true;

    //balloons
    var balloon1 = BABYLON.Mesh.CreateSphere("balloon1", 10, 2.0, scene);
    var balloon2 = BABYLON.Mesh.CreateSphere("balloon2", 10, 2.0, scene);
    var balloon3 = BABYLON.Mesh.CreateSphere("balloon3", 10, 2.0, scene);
    balloon1.material = new BABYLON.StandardMaterial("matball1", scene);
    balloon2.material = new BABYLON.StandardMaterial("matball2", scene);
    balloon3.material = new BABYLON.StandardMaterial("matball2", scene);
    balloon1.material.diffuseTexture = new BABYLON.Texture("ball.jpg", scene);
    balloon2.material.diffuseTexture = new BABYLON.Texture("ball.jpg", scene);
    balloon3.material.diffuseTexture = new BABYLON.Texture("ball.jpg", scene);

    balloon1.position = new BABYLON.Vector3(6, 5, 0);
    balloon2.position = new BABYLON.Vector3(-6, 5, 0);
    balloon3.position = new BABYLON.Vector3(-30, 5, 0);

    //Animation
    var alpha = Math.PI;
    scene.registerBeforeRender(function () {

        //Baloon 1 collision -- Precise = false
        if (balloon1.intersectsMesh(plan1, false)) {
            balloon1.material.emissiveColor = new BABYLON.Color4(1, 0, 0, 1);
        } else {
            balloon1.material.emissiveColor = new BABYLON.Color4(1, 1, 1, 1);
        }

        //Baloon 2 collision -- Precise = true
        if (balloon2.intersectsMesh(plan2, true)) {
            balloon2.material.emissiveColor = new BABYLON.Color4(1, 0, 0, 1);
        } else {
            balloon2.material.emissiveColor = new BABYLON.Color4(1, 1, 1, 1);
        }

        //baloon 3 collision on single point
        if (balloon3.intersectsPoint(pointToIntersect)) {
            balloon3.material.emissiveColor = new BABYLON.Color4(1, 0, 0, 1);
        } else {
            balloon3.material.emissiveColor = new BABYLON.Color4(1, 1, 1, 1);
        }

        alpha += 0.01;
        balloon1.position.y += Math.cos(alpha) / 10;
        balloon2.position.y += Math.cos(alpha) / 10;
        balloon3.position.y += Math.cos(alpha) / 10;
    });

    return scene;
};