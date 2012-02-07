//Key States
var LeftDown = false;
var RightDown = false;

//KEYS
var KEYS = {
    SPACE: 32,
    LEFT: 37,
    RIGHT: 39
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
            LeftDown = true;
            break;
        //right     
        case KEYS.RIGHT:
            RightDown = true;
            break;
        //space bar 
        case KEYS.SPACE:
            if (isGameActive == false) {
                setupLevel();
                preloadGame();
                isGameActive = true;
                startGame();
                gamewon = false;
            }
            break;
    }
}

function doKeyUp(evt) {
    switch (evt.keyCode) {

        //Should be left arrow key     
        case KEYS.LEFT:
            LeftDown = false;
            break;
        //right     
        case KEYS.RIGHT:
            RightDown = false;
            break;
    }
}