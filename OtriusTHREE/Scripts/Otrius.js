
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
var VIEW_ANGLE = 100,
    ASPECT = WIDTH / HEIGHT,
    NEAR = 0.1,
    FAR = 10000;

// create a WebGL renderer and camera
var renderer = new THREE.WebGLRenderer();
var camera = new THREE.PerspectiveCamera(VIEW_ANGLE,
                                ASPECT,
                                NEAR,
                                FAR);
// the camera starts at 0,0,0 so pull it back
camera.position.z = 300;

//And a Scene
var scene = new THREE.Scene();


// start the renderer
renderer.setSize(WIDTH, HEIGHT);

// Toss that shit in the body
document.getElementById("Otrius").appendChild(renderer.domElement)


///////////////////////////////////////////////////////////////////////
//Set up Objects and add to scene
//////////////////////////////////////////////////////////////////////

var geometry = new THREE.CubeGeometry(100, 100, 100);
var material = new THREE.MeshBasicMaterial({ color: 0x666666 });
var cube = new THREE.Mesh(geometry, material);

//Add Cube to scene
scene.add(cube);

// and the camera
scene.add(camera);

//////////////////////////////////////////////////////////////////////
//Set up lights and add to scene
/////////////////////////////////////////////////////////////////////

var pointLight = new THREE.PointLight( 0xFFFFFF );

// set its position
pointLight.position.x = 10;
pointLight.position.y = 50;
pointLight.position.z = 130;

// add to the scene
scene.add(pointLight);

///////////////////////////////////////////////////////////////////
//Render Pipeline
//////////////////////////////////////////////////////////////////

function render()
{

    processInput();

    renderer.render(scene, camera);
    requestAnimFrame(function () {
        render();
    });
}
/////////////////////////////////////////////////////////////////
//Get 'er started, nothing should happen after this line
////////////////////////////////////////////////////////////////
render();
