var Canvas = document.getElementById("GameCanvas");
var ctx = Canvas.getContext("2d");
var intervalID;

ctx.fillStyle="#333333";

var CanvasWidth = Canvas.width;
var CanvasHeight = Canvas.height;

var heroImage = new Image();
heroImage.src = 'Ot_FrontSprite_0.png';

    var hero = {
        Image: heroImage,
        X: 20,
        Y: 30,
        draw : function(){
            ctx.drawImage(this.Image,this.X,this.Y)
        }
    };

startGame();

function startGame() {
    intervalID = setInterval(gameLoop, 1000 / 30);
}

function gameLoop(){
    InitFloor();
    hero.draw();
    processInput();
}
function InitFloor()
{
    var tileheight=20;
    var tilewidth=20;
    var x_numtiles=Math.floor(CanvasHeight/tileheight);
    var y_numtiles=Math.floor(CanvasWidth/tilewidth);
    for (var i=0;i<x_numtiles;i++)
    {	
        for (var j=0;j<y_numtiles;j++)
        {
            ctx.fillRect(i*tileheight+i,j*tilewidth+j,tileheight,tilewidth);
        }
    }
}

