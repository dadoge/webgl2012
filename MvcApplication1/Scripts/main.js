var c = document.getElementById("TestCanvas");
var ctx = c.getContext("2d");

var FPS = 30;

//Ball coordinates
var BallX = 110;
var BallY = 42;

//Ball Velocity
var SpeedX = 3;
var SpeedY = 3;

setInterval(function () {
    update();
    draw();
}, 1000 / FPS);

function update() {

    //Positive Y = ball goes down
    BallX += SpeedX;
    BallY += SpeedY;

    if (BallY > 200 || BallY < 0) {
        SpeedY = SpeedY * -1;
    }
    if (BallX > 200 || BallX < 0) {
        SpeedX = SpeedX * -1;
    }
}


function draw() {

    //Clear Screen
    ctx.clearRect(0, 0, 200, 200);

    //Item Color
    ctx.fillStyle = "#FF0000";

    //Draw ball
    ctx.beginPath();
    ctx.arc(BallX, BallY, 10, 0, Math.PI * 2, true);
    ctx.closePath();
    ctx.fill();
    //End ball
}

