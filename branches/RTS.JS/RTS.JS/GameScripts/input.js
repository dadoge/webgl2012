
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
        case KEYS.LEFT:
            leftTeamUnits.push(new Unit(leftType, robotSprite, 64, 68, 4, 0, 400, ++unitCount));
            LeftDown = true;
            break;    
        case KEYS.RIGHT:
            rightTeamUnits.push(new Unit(rightType, robotSprite2, 64, 68, 4, Canvas.Width, 400, ++unitCount));
            RightDown = true;
            break;
        case KEYS.UP:

            UpDown = true;
            break;
        case KEYS.DOWN:

            DownDown = true;
            break;
        case KEYS.SPACE:
            if (isGameActive == false) {
                startGame();
            }
            break;
        case KEYS.P:
            if (isPaused == true) {
                isPaused = false;
                startGame();
            }
            else {
                isPaused = true;
            }
            break;
    }
}

function doKeyUp(evt) {
    switch (evt.keyCode) {    
        case KEYS.LEFT:
            LeftDown = false;
            break;     
        case KEYS.RIGHT:
            RightDown = false;
            break;    
        case KEYS.UP:
            UpDown = false;
            break;   
        case KEYS.DOWN:
            DownDown = false;
            break;
    }
}

/////////////////////////////////////////////////////////////////////
//                      Process Input
////////////////////////////////////////////////////////////////////

function processInput() {

}
