var c = document.getElementById("TestCanvas");
var ctx = c.getContext("2d");

var FPS = 30;

//Ball coordinates
var BallX = 110;
var BallY = 42;

//Canvas size, still needs changed when you update canvas tag, would be nice to possibly pull in values here.
var CanvasWidth = 400;
var CanvasHeight = 400;

//Ball Velocity
var SpeedX = 15;
var SpeedY = 15;

setInterval(function () {
    update();
    draw();
}, 1000 / FPS);

function update() {

    //Positive Y = ball goes down
    BallX += SpeedX;
    BallY += SpeedY;

    if (BallY > CanvasHeight || BallY < 0) {
        SpeedY = SpeedY * -1;
    }
    if (BallX > CanvasWidth || BallX < 0) {
        SpeedX = SpeedX * -1;
    }
}


function draw() {

    //Clear Screen
    ctx.clearRect(0, 0, CanvasWidth, CanvasHeight);

    //Item Color
    ctx.fillStyle = "#FF0000";

    //Draw ball
    ctx.beginPath();
    ctx.arc(BallX, BallY, 10, 0, Math.PI * 2, true);
    ctx.closePath();
    ctx.fill();
    //End ball
}

