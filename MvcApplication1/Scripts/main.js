///////////////////////////////////////////////////////////////////////////////////////
//                           Variables
//////////////////////////////////////////////////////////////////////////////////////


var c = document.getElementById("TestCanvas");
var ctx = c.getContext("2d");

var intervalID;
var FPS = 30;
var isGameActive = false;
var timer;
var startMessage;

//Background Image
var img = new Image();
img.src = "../Content/incomingbaby.jpg";

//Key States
var LeftDown = false;
var RightDown = false;

//Ball coordinates
var BallX = 110;
var BallY = 42;

//Paddle coordinates
var PaddleX = 200;
var PaddleY = 365;
var PaddleWidth = 85;
var PaddleSpeed = 12;

//Canvas size, still needs changed when you update canvas tag, would be nice to possibly pull in values here.
var CanvasWidth = 400;
var CanvasHeight = 400;

//Ball Velocity
var SpeedX = 11;
var SpeedY = 13;

// Score counter
var GameScore = 0;

//Key Listener
window.addEventListener('keydown', doKeyDown, true);
window.addEventListener('keyup', doKeyUp, true);

//KEYS
var KEYS = new Object();
KEYS.SPACE = 32,
KEYS.LEFT = 37,
KEYS.RIGHT = 39;

///////////////////////////////////////////////////////////////////////////////////
//                    Start Up Game
///////////////////////////////////////////////////////////////////////////////////
function startGame() {
    intervalID = setInterval(gameLoop, 1000 / FPS);
}


///////////////////////////////////////////////////////////////////////////////////
//                    Pre-load Game
///////////////////////////////////////////////////////////////////////////////////
function preloadGame() {
    isGameActive = false;
    GameScore = 0;

    LeftDown = false;
    RightDown = false;

    BallX = 110;
    BallY = 42;

    PaddleX = 200;
    PaddleY = 365;
    PaddleWidth = 85;
    PaddleSpeed = 12;

    SpeedX = 10;
    SpeedY = 12;
    draw();
}

//////////////////////////////////////////////////////////////////////////////////////
//                    Methods that are invoked upon initialization
/////////////////////////////////////////////////////////////////////////////////////
function gameLoop() {
    update();
    draw();

}


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
                isGameActive = true;
                startGame();
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


///////////////////////////////////////////////////////////////////////////////////////
//                                Update and draw methods
///////////////////////////////////////////////////////////////////////////////////////
function update() {


    //Move Paddle if keys are pressed
    if (LeftDown && !RightDown && PaddleX > 0) {
        PaddleX -= PaddleSpeed;
    }
    else if (RightDown && !LeftDown && PaddleX < CanvasWidth) {
        PaddleX += PaddleSpeed;
    }

    //Positive Y = ball goes down
    BallX += SpeedX;
    BallY += SpeedY;


    // check if you missed the ball, end game
    if (BallY > CanvasHeight) {
        isGameActive = false;
        clearInterval(intervalID);
        preloadGame();
    }

    //inverse ball direction when hitting boundries, need real colision function.
    if (BallY < 0) {
        SpeedY = SpeedY * -1;
        playsound();
    }
    if (BallX > CanvasWidth || BallX < 0) {
        SpeedX = SpeedX * -1;
        playsound();
    }


    //Notice Minus...goddamn backwards coords.
    if (BallY >= PaddleY - 10) {
        if (BallX >= (PaddleX - PaddleWidth / 2) && BallX <= (PaddleX + PaddleWidth / 2)) {
            SpeedY = SpeedY * -1;
            setScore();
            playsound();
        }
    }

//    //This was done already above, so I put set score above.
//    if (BallY >= PaddleY - 10) {
//        if (BallX >= (PaddleX - PaddleWidth / 2) && BallX <= (PaddleX + PaddleWidth / 2)) {

//        }
//    }
}


function draw() {

    //Clear Screen
    ctx.clearRect(0, 0, CanvasWidth, CanvasHeight);

    //Draw background img
    ctx.drawImage(img, 0, 0);

    //Draw ball
    ctx.fillStyle = "#FF0000";
    ctx.beginPath();
    ctx.arc(BallX, BallY, 8, 0, Math.PI * 2, true);
    ctx.closePath();
    ctx.fill();
    //End ball

    //Draw Paddle
    ctx.strokeStyle = "#888888";
    ctx.lineCap = "round";
    ctx.lineWidth = 6;
    ctx.beginPath();
    ctx.moveTo(PaddleX-(PaddleWidth/2), PaddleY);
    ctx.lineTo(PaddleX + (PaddleWidth / 2), PaddleY);
    ctx.stroke();
    ctx.closePath();
    //End Paddle

    //Draw Score
    ctx.font = "14pt Arial";
    ctx.textBaseline = "top";
    ctx.fillStyle = "#FFFFFF";
    ctx.fillText("Score: " + GameScore, 300, 10);

    //Draw bottom pit
    ctx.fillStyle = "#000000";
    ctx.fillRect(0, 370, 400, 30);
}


// Handling the score
function setScore() {
    GameScore += 1;
}