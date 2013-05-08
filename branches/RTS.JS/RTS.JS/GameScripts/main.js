///////////////////////////////////////////////////////////////////////////////////////
//                           Variables
//////////////////////////////////////////////////////////////////////////////////////


var c = document.getElementById("RTS_BG");
var ctx = c.getContext("2d");

//Canvas size
var Canvas = {
    Height: c.height,
    Width: c.width
};

//Game Variables
var intervalID;
var FPS = 30;
var isGameActive = false;
var isPaused = false;
var groundHeight = 50;
var robotSprite = new Image();
robotSprite.src = 'robot.png';
var robotSprite2 = new Image();
robotSprite2.src = 'robot2.png';

var bro = new Image();
bro.src = 'bro.png';

//////////////////////////////////////////////////////////////////////////////////
//                           Pre-Init
/////////////////////////////////////////////////////////////////////////////////

//INVOKE PREINIT AS FIRST METHOD
var rightType = {
    speed:  5,
    direction: -1,
    heath: 10,
    damage: 5

};
var leftType = {
    speed:  5,
    direction: 1,
    heath: 10,
    damage: 5

};
var newUnit = new Unit(leftType, robotSprite, 64, 68, 4, 100,400);
var newUnit2 = new Unit(rightType, robotSprite2, 64, 68, 4, 100,400);
preinit();

function preinit() {



    //Draw bottom pit
    ctx.fillStyle = "#000000";
    ctx.fillRect(0, 0, Canvas.Width, Canvas.Height);

    ctx.fillStyle = "#00FF00";
    ctx.fillRect(0, Canvas.Height - groundHeight, Canvas.Width, Canvas.Height);

    //Draw Score and lives
    ctx.font = "20pt Arial";
    ctx.textBaseline = "top";
    ctx.fillStyle = "#FFFFFF";
    ctx.fillText(newUnit.health, 0, 0);
	
	ctx.drawImage(bro, 0, 200);
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

    //Draw bottom pit
    ctx.fillStyle = "#000000";
    ctx.fillRect(0, 0, Canvas.Width, Canvas.Height);

    ctx.fillStyle = "#00FF00";
    ctx.fillRect(0, Canvas.Height - groundHeight, Canvas.Width, Canvas.Height);

    newUnit.draw(ctx);
    newUnit2.draw(ctx);

    if (isPaused) {
        ctx.fillText("Game Paused. Press 'p' to resume.", 50, 200);
        clearInterval(intervalID);
    }
}

