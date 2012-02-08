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

/////////////////////////////////////////////////////////////////////
//                      Process Input
////////////////////////////////////////////////////////////////////

function processInput() {

    //Move Paddle if keys are pressed
    if (LeftDown && !RightDown && Paddle.X > 0) {
        Paddle.X -= Paddle.Speed;
    }
    else if (RightDown && !LeftDown && Paddle.X < Canvas.Width) {
        Paddle.X += Paddle.Speed;
    }

    //Positive Y = ball goes down
    Ball.X += Ball.SpeedX;
    Ball.Y += Ball.SpeedY;
}