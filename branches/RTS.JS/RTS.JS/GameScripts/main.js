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
var baseLeftTypes = {
    robotType : new UnitType(5,1,10,3,35,10,'left'),
    archerType : new UnitType(5,1,10,3,100,10,'left')
}
var baseRightTypes = {
    robotType : new UnitType(5,-1,10,3,35,10,'right'),
    archerType : new UnitType(5,-1,10,3,100,10,'right')
}
var game = new Game();
var leftPlayer = new Player(100,0, [],0,400, baseLeftTypes);
var rightPlayer = new Player(100,0,[],Canvas.Width,400, baseRightTypes);

var leftBaseHealth = 300;
var rightBaseHealth = 300;


//////////////////////////////////////////////////////////////////////////////////
//                           Pre-Init
/////////////////////////////////////////////////////////////////////////////////

//CREATE SPRITES AND INVOKE PREINIT AS FIRST METHOD
var rightType = {
    speed:  5,
    direction: -1,
    health: 10,
    damage: 3,
	cost: 10,
    team: 'right'

};
var leftType = {
    speed:  5,
    direction: 1,
    health: 10,
    damage: 3,
	cost: 10,
    team: 'left'

};

var robotImage = new Image();
robotImage.src = 'robot.png';
var blueRobotSprite = new Sprite(robotImage, 64, 68, 4, 4);

var robotImage2 = new Image();
robotImage2.src = 'robot2.png';
var pinkRobotSprite = new Sprite(robotImage2, 64, 68, 4, 4);

var archerImage = new Image();
archerImage.src = 'archer.png';
var archerSprite = new Sprite(archerImage, 32, 48, 5, 5);

var archer2Image = new Image();
archer2Image.src = 'archer2.png';
var archer2Sprite = new Sprite(archer2Image, 32, 48, 5, 5);


var leftBase = new Image();
leftBase.src = 'base-left.png'
var rightBase = new Image();
rightBase.src = 'base-right.png'

var unitCount = 0;

preinit();

function preinit() {

    //Draw bottom pit
    ctx.fillStyle = "#000000";
    ctx.fillRect(0, 0, Canvas.Width, Canvas.Height);

    ctx.fillStyle = "#00FF00";
    ctx.fillRect(0, Canvas.Height - game.groundHeight, Canvas.Width, Canvas.Height);

    leftPlayer.money = 100;
    rightPlayer.money = 100;
    leftPlayer.experience = 0;
    rightPlayer.experience = 0;
    leftPlayer.units = [];
    rightPlayer.units = [];

    leftBaseHealth = 300;
    rightBaseHealth = 300;

    unitCount = 0;
}


///////////////////////////////////////////////////////////////////////////////////
//                    Start Up Game
///////////////////////////////////////////////////////////////////////////////////
function startGame() {
    game.isGameActive = true;
        game.intervalID = setInterval(gameLoop, 1000 / game.FPS);
}

function gameLoop() {
    draw2();
    handleBaseCollision();
}

function draw2() {

    //Clear Screen
    ctx.clearRect(0, 0, Canvas.Width, Canvas.Height);

    //Draw bottom pit
    ctx.fillStyle = "#000000";
    ctx.fillRect(0, 0, Canvas.Width, Canvas.Height);

    ctx.fillStyle = "#00FF00";
    ctx.fillRect(0, Canvas.Height - game.groundHeight, Canvas.Width, Canvas.Height);
	
	//Place left base on screen
	ctx.drawImage(leftBase, -100, 260);
	//Place right base on screen
	ctx.drawImage(rightBase, 730, 260);
	

    for(i = 0;  i < leftPlayer.units.length; i++)
    {
        leftPlayer.units[i].draw(ctx);
    }
    
    for (i = 0; i < rightPlayer.units.length; i++) {
        rightPlayer.units[i].draw(ctx);
    }

    var max = _.max(leftPlayer.units, function (ltu) { return ltu.x; });
    var min = _.min(rightPlayer.units, function (rtu) { return rtu.x; });
    //Draw Score and lives
    ctx.font = "20pt Arial";
    ctx.textBaseline = "top";
    ctx.fillStyle = "#FFFFFF";
    ctx.fillText(max.x + "," + min.x, 0, 0);
	
	ctx.fillStyle = "#FFFFFF";
	ctx.fillText("Money: " + leftPlayer.money, 0, 30);
	
	ctx.fillStyle = "#FFFFFF";
	ctx.fillText("Money: " + rightPlayer.money, 660, 30);
	
	ctx.fillStyle = "#FFFFFF";
	ctx.fillText("Experience: " + leftPlayer.experience, 0, 60);
	
	ctx.fillStyle = "#FFFFFF";
	ctx.fillText("Experience: " + rightPlayer.experience, 620, 60);
	
	ctx.fillStyle = "#FFFFFF";
	ctx.fillText("Health: " + leftBaseHealth, 0, 90);
	
	ctx.fillStyle = "#FFFFFF";
	ctx.fillText("Health: " + rightBaseHealth, 660, 90);
	
	
    if (game.isPaused) {
        ctx.fillText("Game Paused. Press 'p' to resume.", 50, 200);
        clearInterval(game.intervalID);
    }
	
	if (rightBaseHealth <= 0 || leftBaseHealth <= 0) {
        game.isGameActive = false;
		ctx.fillText("Game Over. Press 'Space' to start over.", 50, 200);
        clearInterval(game.intervalID);
    }
}

function handleBaseCollision() {
	
	var leftTeamMax = _.max(leftPlayer.units, function (ltu) { return ltu.x; });
    var rightTeamMin = _.min(rightPlayer.units, function (rtu) { return rtu.x; });
	
	if (leftTeamMax.x > 690) {
	// Player is attacking base
	leftTeamMax.speed = 0;
	rightBaseHealth -= leftTeamMax.damage;
	}
	
	if (rightTeamMin.x < 60) {
	// Player is attacking base
	rightTeamMin.speed = 0;
	leftBaseHealth -= rightTeamMin.damage;
	}
	
}