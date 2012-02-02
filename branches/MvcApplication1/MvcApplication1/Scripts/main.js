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
// Score counter
var GameScore = 0;

//Background Image
var img = new Image();
img.src = "../Content/incomingbaby.jpg";

//Key States
var LeftDown = false;
var RightDown = false;

//Ball coordinates
var Ball = {
    X: 200,
    Y: 351,
    Radius: 8,
    SpeedX: -10,
    SpeedY: -12
};

//Paddle coordinates
var Paddle = {
    X: 200,
    Y: 365,
    Width: 85,
    Speed: 12
};

//Canvas size, still needs changed when you update canvas tag, would be nice to possibly pull in values here.
var Canvas = {
    Height: 400,
    Width: 400
};

//KEYS
var KEYS = {
    SPACE: 32,
    LEFT: 37,
    RIGHT: 39
};

//Key Listener
window.addEventListener('keydown', doKeyDown, true);
window.addEventListener('keyup', doKeyUp, true);



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

    Ball.X = 200;
    Ball.Y = 365 - Ball.Radius - 6;

    Paddle.X = 200;
    Paddle.Y = 365;
    Paddle.Width = 85;
    Paddle.Speed = 12;

    Ball.SpeedX = -10;
    Ball.SpeedY = -12;
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
    if (LeftDown && !RightDown && Paddle.X > 0) {
        Paddle.X -= Paddle.Speed;
    }
    else if (RightDown && !LeftDown && Paddle.X < Canvas.Width) {
        Paddle.X += Paddle.Speed;
    }

    //Positive Y = ball goes down
    Ball.X += Ball.SpeedX;
    Ball.Y += Ball.SpeedY;


    // check if you missed the ball, end game
    if (Ball.Y > Canvas.Height) {
        isGameActive = false;
        clearInterval(intervalID);
        preloadGame();
    }

    //inverse ball direction when hitting boundries, need real colision function.
    if (Ball.Y < 0) {
        Ball.SpeedY = Ball.SpeedY * -1;
        playsound();
    }
    if (Ball.X > Canvas.Width || Ball.X < 0) {
        Ball.SpeedX = Ball.SpeedX * -1;
        playsound();
    }


    //Notice Minus...goddamn backwards coords.
    if (Ball.Y >= Paddle.Y - 10) {
        if (Ball.X >= (Paddle.X - Paddle.Width / 2) && Ball.X <= (Paddle.X + Paddle.Width / 2)) {
            Ball.SpeedY = Ball.SpeedY * -1;
            setScore();
            playsound();
        }
    }

//    //This was done already above, so I put set score above.
//    if (Ball.Y >= Paddle.Y - 10) {
//        if (Ball.X >= (Paddle.X - Paddle.Width / 2) && Ball.X <= (Paddle.X + Paddle.Width / 2)) {

//        }
//    }
}


function draw() {

    //Clear Screen
    ctx.clearRect(0, 0, Canvas.Width, Canvas.Height);

    //Draw background img
    ctx.drawImage(img, 0, 0);

    //Draw ball
    ctx.fillStyle = "#FF0000";
    ctx.beginPath();
    ctx.arc(Ball.X, Ball.Y, Ball.Radius, 0, Math.PI * 2, true);
    ctx.closePath();
    ctx.fill();
    //End ball

    //Draw Paddle
    ctx.strokeStyle = "#888888";
    ctx.lineCap = "round";
    ctx.lineWidth = 6;
    ctx.beginPath();
    ctx.moveTo(Paddle.X-(Paddle.Width/2), Paddle.Y);
    ctx.lineTo(Paddle.X + (Paddle.Width / 2), Paddle.Y);
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