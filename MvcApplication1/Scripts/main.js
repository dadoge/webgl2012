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
var lives = 3;
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
    Radius: 6,
    SpeedX: -7,
    SpeedY: -8
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
    Height: cheight,
    Width: cwidth
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

var blocksPerRow = 8;
var blockHeight = 20;
var blockWidth = Canvas.Width / blocksPerRow;

var level = [
    [1, 1, 1, 1, 3, 1, 1, 2],
    [1, 2, 1, 2, 1, 1, 3, 2],
    [2, 1, 3, 1, 2, 3, 1, 2],
    [1, 2, 1, 3, 1, 2, 1, 2]
];




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
        lives = 3;
        level = [
            [1, 1, 1, 1, 3, 1, 1, 2],
            [1, 2, 1, 2, 1, 1, 3, 2],
            [2, 1, 3, 1, 2, 3, 1, 2],
            [1, 2, 1, 3, 1, 2, 1, 2]
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
                preloadGame();
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

//////////////////////////////////////////////////////////////////////////////////////
//                                  Level creation
//////////////////////////////////////////////////////////////////////////////////////
function createLevel() {
    for (var i = 0; i < level.length; i++) {
        for (var j = 0; j < level[i].length; j++) {
            drawBlock(j, i, level[i][j]);
        }
    }
}

// draw a single block
function drawBlock(x, y, type) {
    switch (type) {
        case 1:
            ctx.fillStyle = 'orange';
            break;
        case 2:
            ctx.fillStyle = 'rgb(100,200,100)';
            break;
        case 3:
            ctx.fillStyle = 'rgba(50,100,50,.5)';
            break;
        default:
            ctx.clearRect(x * blockWidth, y * blockHeight, blockWidth, blockHeight);
            break;

    }
    if (type) {
        //Draw rectangle with fillStyle color selected earlier
        ctx.fillRect(x * blockWidth, y * blockHeight, blockWidth, blockHeight);
        // Also draw blackish border around the brick
        ctx.strokeStyle = "#111111";
        ctx.lineCap = "round";
        ctx.lineWidth = 1;
        ctx.strokeRect(x * blockWidth + 1, y * blockHeight + 1, blockWidth - 2, blockHeight - 2);
    }
}

//////////////////////////////////////////////////////////////////////////////////////
//                                  Brick collision
//////////////////////////////////////////////////////////////////////////////////////
function collisionXWithBlocks() {
    var bumpedX = false;
    for (var i = 0; i < level.length; i++) {
        for (var j = 0; j < level[i].length; j++) {
            if (level[i][j]) { // if brick is still visible
                var blockX = j * blockWidth;
                var blockY = i * blockHeight;
                if (
                // barely touching from left
                    ((Ball.X + Ball.SpeedX + Ball.Radius >= blockX) &&
                    (Ball.X + Ball.Radius <= blockX))
                    ||
                // barely touching from right
                    ((Ball.X + Ball.SpeedX - Ball.Radius <= blockX + blockWidth) &&
                    (Ball.X - Ball.Radius >= blockX + blockWidth))
                    ) {
                    if ((Ball.Y + Ball.SpeedY - Ball.Radius <= blockY + blockHeight) &&
                        (Ball.Y + Ball.SpeedY + Ball.Radius >= blockY)) {
                        // weaken block and increase score
                        explodeBlock(i, j);

                        bumpedX = true;
                    }
                }
            }
        }
    }
    return bumpedX;
}

function collisionYWithBlocks() {
    var bumpedY = false;
    for (var i = 0; i < level.length; i++) {
        for (var j = 0; j < level[i].length; j++) {
            if (level[i][j]) { // if brick is still visible
                var blockX = j * blockWidth;
                var blockY = i * blockHeight;
                if (
                // barely touching from below
                    ((Ball.Y + Ball.SpeedY - Ball.Radius <= blockY + blockHeight) &&
                    (Ball.Y - Ball.Radius >= blockY + blockHeight))
                    ||
                // barely touching from above
                    ((Ball.Y + Ball.SpeedY + Ball.Radius >= blockY) &&
                    (Ball.Y + Ball.Radius <= blockY))) {
                    if (Ball.X + Ball.SpeedX + Ball.Radius >= blockX &&
                        Ball.X + Ball.SpeedX - Ball.Radius <= blockX + blockWidth) {
                        // weaken block and increase score
                        explodeBlock(i, j);
                        bumpedY = true;
                    }
                }
            }
        }
    }
    return bumpedY;
}

function explodeBlock(i, j) {
    // First weaken the block (0 means block is gone)
    level[i][j]--;

    if (level[i][j] > 0) {
        // The block is weakened but still around. Give a single point.
        GameScore++;
    } else {
        // give player an extra point when the block disappears
        GameScore += 2;
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
}


// Handling the score
function setScore() {
    GameScore += 1;
}