///////////////////////////////////////////////////////////////////////////////////////
//                           Variables
//////////////////////////////////////////////////////////////////////////////////////


var c = document.getElementById("TestCanvas");
var ctx = c.getContext("2d");

var FPS = 30;

//Background Image
var img = new Image();
img.src = "../Content/incomingbaby.jpg";

//Sounds


//Ball coordinates
var BallX = 110;
var BallY = 42;

//Paddle coordinates
var PaddleX = 200;
var PaddleY = 365;
var PaddleWidth = 85;
var PaddleSpeed = 18;

//Canvas size, still needs changed when you update canvas tag, would be nice to possibly pull in values here.
var CanvasWidth = 400;
var CanvasHeight = 400;

//Ball Velocity
var SpeedX = 11;
var SpeedY = 13;

// Score counter
var GameScore = 0;


//////////////////////////////////////////////////////////////////////////////////////
//                    Methods that are invoked upon initialization
/////////////////////////////////////////////////////////////////////////////////////
setInterval(function () {
    update();
    draw();
}, 1000 / FPS);
//Key Listener
window.addEventListener('keydown', doKeyDown, true);

////////////////////////////////////////////////////////////////////////////
//                        Callbacks
///////////////////////////////////////////////////////////////////////////


function doKeyDown(evt) {
    switch (evt.keyCode) {

        //Should be left arrow key
        case 37:
            PaddleX -= PaddleSpeed;
            break;
        //right
        case 39:
            PaddleX += PaddleSpeed;
            Sound.play();
            break;
    }
}


///////////////////////////////////////////////////////////////////////////////////////
//                                Update and draw methods
///////////////////////////////////////////////////////////////////////////////////////
function update() {

    //Positive Y = ball goes down
    BallX += SpeedX;
    BallY += SpeedY;

    //inverse ball direction when hitting boundries, need real colision function.
    if (BallY > CanvasHeight || BallY < 0) {
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
            playsound();
        }
    }

    //Ball collision with paddle, add to score
    if (BallY == PaddleY - 10) {
        if (BallX >= (PaddleX - PaddleWidth / 2) && BallX <= (PaddleX + PaddleWidth / 2)) {
            setScore();
        }
    }
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