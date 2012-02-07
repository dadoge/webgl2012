///////////////////////////////////////////////////////////////////////////////////////
//                           Variables
//////////////////////////////////////////////////////////////////////////////////////


var c = document.getElementById("TestCanvas");
var ctx = c.getContext("2d");
var cwidth = c.width;
var cheight = c.height;

var intervalID;
var FPS = 30;
var isGameActive = false;
var timer;
var startMessage;
// Score counter
var lives = 8;
var GameScore = 0;
var gamewon = false;
var numBlocks = 0;

//Background Image
var img = new Image();
img.src = "../Content/incomingbaby.jpg";

//Canvas size, still needs changed when you update canvas tag, would be nice to possibly pull in values here.
var Canvas = {
    Height: cheight,
    Width: cwidth
};

var blocksPerRow = 8;
var blockHeight = 20;
var blockWidth = Canvas.Width / blocksPerRow;

var level = [
    ["empty", "purple", "purple", "purple", "purple", "purple", "purple", "empty"],
    ["empty", "orange", "orange", "orange", "orange", "orange", "orange", "empty"],
    ["empty", "orange", "orange", "orange", "orange", "orange", "orange", "empty"],
    ["empty", "purple", "purple", "purple", "purple", "purple", "purple", "empty"]
];

//var level = [
//    [9, 9, 9, 9, 9, 9, 9, 9],
//    [9, 9, 9, 9, 9, 9, 9, 9],
//    [9, 9, 9, 9, 9, 9, 9, 9],
//    [1, 1, 1, 1, 3, 1, 1, 2],
//    [1, 2, 1, 2, 1, 1, 3, 2],
//    [2, 1, 3, 1, 2, 3, 1, 2],
//    [9, 2, 1, 3, 1, 2, 1, 9]
//];


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

    LeftDown = false;
    RightDown = false;

    Ball.X = 200;
    Ball.Y = 365 - Ball.Radius - 6;

    Paddle.X = 200;
    Paddle.Y = 365;
    Paddle.Width = 85;
    Paddle.Speed = 12;

    Ball.SpeedX = -7;
    Ball.SpeedY = -8;

    if (lives == 0) {
        GameScore = 0;
        numBlocks = 0;
        lives = 8;
        level = [
            ["empty", "purple", "purple", "purple", "purple", "purple", "purple", "empty"],
            ["empty", "orange", "orange", "orange", "orange", "orange", "orange", "empty"],
            ["empty", "orange", "orange", "orange", "orange", "orange", "orange", "empty"],
            ["empty", "purple", "purple", "purple", "purple", "purple", "purple", "empty"]
        ];
    }

    draw();
}

//////////////////////////////////////////////////////////////////////////////////////
//                    Methods that are invoked upon initialization
/////////////////////////////////////////////////////////////////////////////////////
function gameLoop() {
    update();
    draw();

}

//////////////////////////////////////////////////////////////////////////////////////
//                                  Level creation
//////////////////////////////////////////////////////////////////////////////////////
function createLevel() {
    for (var i = 0; i < level.length; i++) {
        for (var j = 0; j < level[i].length; j++) {
            //drawBlock(j, i, level[i][j].id, level[i][j].color);
            var currBlock = level[i][j];
            if (currBlock != null) {
                if (!currBlock.broken) {
                    ctx.fillStyle = currBlock.color;

                    //Draw rectangle with fillStyle color selected earlier
                    ctx.fillRect(currBlock.x * blockWidth, currBlock.y * blockHeight, blockWidth, blockHeight);
                    // Also draw blackish border around the brick
                    ctx.strokeStyle = "#111111";
                    ctx.lineCap = "round";
                    ctx.lineWidth = 1;
                    ctx.strokeRect(currBlock.x * blockWidth + 1, currBlock.y * blockHeight + 1, blockWidth - 2, blockHeight - 2);
                }
                
            }
        }
    }
}

// draw a single block
//Unused
//function drawBlock(x, y, type, color) {
//    switch (type) {
//        case "purple":
//            ctx.fillStyle = color;
//            break;
//        case 2:
//            ctx.fillStyle = 'rgb(100,200,100)';
//            break;
//        case 3:
//            ctx.fillStyle = 'rgba(50,100,50,.5)';
//            break;
//        default:
//            //ctx.clearRect(x * blockWidth, y * blockHeight, blockWidth, blockHeight);
//            break;

//    }
//    if (type != "empty") {
//        //Draw rectangle with fillStyle color selected earlier
//        ctx.fillRect(x * blockWidth, y * blockHeight, blockWidth, blockHeight);
//        // Also draw blackish border around the brick
//        ctx.strokeStyle = "#111111";
//        ctx.lineCap = "round";
//        ctx.lineWidth = 1;
//        ctx.strokeRect(x * blockWidth + 1, y * blockHeight + 1, blockWidth - 2, blockHeight - 2);
//    }
//}

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
    if (Ball.Y > Paddle.Y + (Ball.SpeedY + Ball.Radius)) {
        lives--;
        isGameActive = false;
        clearInterval(intervalID);
        preloadGame();
    }

    //inverse ball direction when hitting boundries, need real colision function.
    if (Ball.Y < 0 || collisionYWithBlocks()) {
        Ball.SpeedY = Ball.SpeedY * -1;
        playsound();
    }
    if (Ball.X > Canvas.Width || Ball.X < 0 || collisionXWithBlocks()) {
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

    if (numBlocks == 0) {
        isGameActive = false;
        clearInterval(intervalID);
        gamewon = true;
    }

}
var isNonZero = function(x) {
   return !x==0;
}
function draw() {

    //Clear Screen
    ctx.clearRect(0, 0, Canvas.Width, Canvas.Height);

    //Draw background img
    ctx.drawImage(img, 0, 0);

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

    if (gamewon) {

        ctx.font = "20pt Arial";
        ctx.textBaseline = "top";
        ctx.fillStyle = "#FFFFFF";
        ctx.fillText("You won you lucky basturd!!" + GameScore, 10, 200);
    }
}


// Handling the score
function setScore() {
    GameScore += 1;
}