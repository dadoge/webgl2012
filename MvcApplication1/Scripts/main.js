///////////////////////////////////////////////////////////////////////////////////////
//                           Variables
//////////////////////////////////////////////////////////////////////////////////////


var c = document.getElementById("TestCanvas");
var ctx = c.getContext("2d");

var FPS = 30;

//Background Image
var img = new Image();
img.src = "../Content/funnypictures_91.jpg";

//Ball coordinates
var BallX = 110;
var BallY = 42;

//Paddle coordinates
var PaddleX = 200;
var PaddleY = 390;
var PaddleWidth = 85;
var PaddleSpeed = 12;

//Canvas size, still needs changed when you update canvas tag, would be nice to possibly pull in values here.
var CanvasWidth = 400;
var CanvasHeight = 400;

//Ball Velocity
var SpeedX = 11;
var SpeedY = 13;


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
    }
    if (BallX > CanvasWidth || BallX < 0) {
        SpeedX = SpeedX * -1;
    }


    //Notice Minus...goddamn backwards coords.
    if (BallY >= PaddleY - 10) {
        if (BallX >= (PaddleX - PaddleWidth / 2) && BallX <= (PaddleX + PaddleWidth / 2)) {
            SpeedY = SpeedY * -1;
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
    ctx.lineTo(PaddleX+(PaddleWidth/2), PaddleY);
    ctx.stroke();
    //End Paddle
}

