
////////////////////////////////////////////////////////////////////////
//REQUIRED HEADER - Do not change
///////////////////////////////////////////////////////////////////////

window.requestAnimFrame = (function (callback) {
    return window.requestAnimationFrame ||
    window.webkitRequestAnimationFrame ||
    window.mozRequestAnimationFrame ||
    window.oRequestAnimationFrame ||
    window.msRequestAnimationFrame ||
    function (callback) {
        window.setTimeout(callback, 1000 / 60);
    };
})();
///////////////////////////////////////////////////////////////////////
//End Dumb Header--Dont change above
///////////////////////////////////////////////////////////////////////


///////////////////////////////////////////////////////////////////////
//Set up camera and scene
//////////////////////////////////////////////////////////////////////

var WIDTH = 800,
	    HEIGHT = 500;

// set some camera attributes
var VIEW_ANGLE = 45,
    ASPECT = WIDTH / HEIGHT,
    NEAR = 1,
    FAR = 2000;

//create a WebGL renderer and camera
var renderer = new THREE.WebGLRenderer();
var camera = new THREE.PerspectiveCamera(VIEW_ANGLE,
                                ASPECT,
                                NEAR,
                                FAR);
camera.position.set(0, 0, 4);

//And a Scene
var scene = new THREE.Scene();


// start the renderer
renderer.setSize(WIDTH, HEIGHT);

// Toss that shit in the body
document.getElementById("Otrius").appendChild(renderer.domElement)


///////////////////////////////////////////////////////////////////////
//Set up Objects and add to scene
//////////////////////////////////////////////////////////////////////

//var geometry = new THREE.CubeGeometry(100, 100, 100);
//var material = new THREE.MeshBasicMaterial({ color: 0x666666 });
//var cube = new THREE.Mesh(geometry, material);

////Add Cube to scene
//scene.add(cube);


var particleLight, pointLight;
var dae, skin;

var loader = new THREE.ColladaLoader();
loader.options.convertUpAxis = true;
loader.load('/Scripts/monster.js', function (collada) {

    dae = collada.scene;
    skin = collada.skins[0];

    dae.scale.x = dae.scale.y = dae.scale.z = 0.0003;
    dae.rotation.y = 1.5;
    dae.rotation.x = 1.57;

    dae.updateMatrix();
    scene.add(dae);


    particleLight = new THREE.Mesh(new THREE.SphereGeometry(4, 8, 8), new THREE.MeshBasicMaterial({ color: 0xffffff }));
    scene.add(particleLight);

    scene.add(new THREE.AmbientLight(0xcccccc));

    var directionalLight = new THREE.DirectionalLight(/*Math.random() * 0xffffff*/0xeeeeee);
    directionalLight.position.x = Math.random() - 0.5;
    directionalLight.position.y = Math.random() - 0.5;
    directionalLight.position.z = Math.random() - 0.5;
    directionalLight.position.normalize();
    scene.add(directionalLight);

    scene.add(camera);


});

// and the camera


//////////////////////////////////////////////////////////////////////
//Set up lights and add to scene
/////////////////////////////////////////////////////////////////////

var pointLight = new THREE.PointLight(0xFFFFFF);

// set its position
pointLight.position.x = 10;
pointLight.position.y = 50;
pointLight.position.z = 130;

// add to the scene
scene.add(pointLight);

///////////////////////////////////////////////////////////////////
//Render Pipeline
//////////////////////////////////////////////////////////////////

var t = 0;
var clock = new THREE.Clock();
function render() {

    processInput();
    //camera.lookAt(scene.position);
    var delta = clock.getDelta();
    requestAnimFrame(function () {
        render();

        if (LeftDown || RightDown || UpDown || DownDown) {
            if (t > 1) t = 0;

            if (skin) {

                // guess this can be done smarter...

                // (Indeed, there are way more frames than needed and interpolation is not used at all
                //  could be something like - one morph per each skinning pose keyframe, or even less,
                //  animation could be resampled, morphing interpolation handles sparse keyframes quite well.
                //  Simple animation cycles like this look ok with 10-15 frames instead of 100 ;)

                for (var i = 0; i < skin.morphTargetInfluences.length; i++) {

                    skin.morphTargetInfluences[i] = 0;

                }

                skin.morphTargetInfluences[Math.floor(t * 30)] = .9;

                t += delta;

            }
        }
        renderer.render(scene, camera);
    });
}
/////////////////////////////////////////////////////////////////
//Get 'er started, nothing should happen after this line
////////////////////////////////////////////////////////////////





render();
