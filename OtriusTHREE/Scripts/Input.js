///////////////////////////////////////////////////////////////////////
//INPUT VARIABLES
///////////////////////////////////////////////////////////////////////

//Key States
var LeftDown = false;
var RightDown = false;
var UpDown = false;
var DownDown = false;

//KEYS
var KEYS = {
    SPACE: 32,
    LEFT: 37,
    UP: 38,
    RIGHT: 39,
    DOWN: 40,
    P: 80
};

//Key Listeners
window.addEventListener('keydown', doKeyDown, true);
window.addEventListener('keyup', doKeyUp, true);


////////////////////////////////////////////////////////////////////////////
//                        Callbacks
///////////////////////////////////////////////////////////////////////////


function doKeyDown(evt) {
    switch (evt.keyCode) {

        //Should be left arrow key      
        case KEYS.LEFT:
            if (!LeftDown) {
                dae.rotation.y += 1.57;
            }
            LeftDown = true;
            break;
            //right      
        case KEYS.RIGHT:
            if (!RightDown) {
                dae.rotation.y -= 1.57;
            }
            RightDown = true;
            break;
        case KEYS.UP:
            UpDown = true;
            break;
        case KEYS.DOWN:
            if (!DownDown) {
                dae.rotation.y += 3.14;
            }
            DownDown = true;
            break;
    }
}

function doKeyUp(evt) {
    switch (evt.keyCode) {

        //Should be left arrow key      
        case KEYS.LEFT:
            LeftDown = false;
            dae.rotation.y -= 1.57;
            break;
            //right      
        case KEYS.RIGHT:
            RightDown = false;
            dae.rotation.y += 1.57
            break;
        case KEYS.UP:
            UpDown = false;
            break;
        case KEYS.DOWN:
            DownDown = false;
            dae.rotation.y -= 3.14;
            break;
    }
}

/////////////////////////////////////////////////////////////////////
//                      Process Input
////////////////////////////////////////////////////////////////////

function processInput() {

    //Move Paddle if keys are pressed
    if (LeftDown && !RightDown) {
        dae.position.x -= .01;
    }
    else if (RightDown && !LeftDown) {
        dae.position.x += .01;
    }
    if (UpDown && !DownDown) {
        dae.position.y += .01;
    }
    else if (DownDown && !UpDown) {
        dae.position.y -= .01;
    }

}

