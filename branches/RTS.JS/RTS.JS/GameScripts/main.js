///////////////////////////////////////////////////////////////////////////////////////
//                           Variables
//////////////////////////////////////////////////////////////////////////////////////


var c = document.getElementById("RTS_BG");
var ctx = c.getContext("2d");

//Canvas size
var Canvas = {
    Height: c.width,
    Width: c.height
};

//Game Variables
var intervalID;
var FPS = 30;
var isGameActive = false;
var isPaused = false;


//Background Image
var backgroundImg = new Image();
backgroundImg.src = "../Content/incomingbaby.jpg";


//////////////////////////////////////////////////////////////////////////////////
//                           Pre-Init
/////////////////////////////////////////////////////////////////////////////////

//INVOKE PREINIT AS FIRST METHOD
preinit();

function preinit() {

    //Draw bottom pit
    ctx.fillStyle = "#000000";
    ctx.fillRect(0, 0, Canvas.Height, Canvas.Width);

    //Draw Score and lives
    ctx.font = "20pt Arial";
    ctx.textBaseline = "top";
    ctx.fillStyle = "#FFFFFF";
    ctx.fillText("Oh herro!  Press space to pray!", 10, (Canvas.Height / 2 - 10));
}


///////////////////////////////////////////////////////////////////////////////////
//                    Start Up Game
///////////////////////////////////////////////////////////////////////////////////
function startGame() {
    intervalID = setInterval(gameLoop, 1000 / FPS);
}

function gameLoop() {
    draw();

}

function draw() {

    //Clear Screen
    ctx.clearRect(0, 0, Canvas.Width, Canvas.Height);

    //Draw background img
    ctx.drawImage(backgroundImg, 0, 0);

    //create level
    createLevel();

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
    ctx.moveTo(Paddle.X - (Paddle.Width / 2), Paddle.Y);
    ctx.lineTo(Paddle.X + (Paddle.Width / 2), Paddle.Y);
    ctx.stroke();
    ctx.closePath();
    //End Paddle

    //Draw bottom pit
    ctx.fillStyle = "#000000";
    ctx.fillRect(0, 370, 400, 30);

    //Draw Score and lives
    ctx.font = "14pt Arial";
    ctx.textBaseline = "top";
    ctx.fillStyle = "#FFFFFF";
    ctx.fillText("Score: " + GameScore, 10, 370);
    ctx.fillText("Lives: " + lives, 300, 370);
    ctx.fillText("Level: " + (currentLevel + 1), 150, 370);

    drawPowerup();

    if (isPaused) {
        ctx.fillText("Game Paused. Press 'p' to resume.", 50, 200);
        clearInterval(intervalID);
    }

    if (gamewon) {

        ctx.font = "20pt Arial";
        ctx.textBaseline = "top";
        ctx.fillStyle = "#FFFFFF";
        ctx.fillText("You won you lucky basturd!!" + GameScore, 10, 200);
    }
}

