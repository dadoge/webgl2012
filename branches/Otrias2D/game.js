var Canvas = document.getElementById("GameCanvas");
var ctx = Canvas.getContext("2d");
var intervalID;

ctx.fillStyle="#333333";

var CanvasWidth = Canvas.width;
var CanvasHeight = Canvas.height;
var count=0;

var heroImage = new Image();
heroImage.src = 'Ot_FrontSprite_1.png';

    var hero = {
        Image: heroImage,
        X: 20,
        Y: 30,
        draw : function(){
            ctx.drawImage(this.Image,this.X,this.Y);
        }
    };
startGame();

function startGame() {
    intervalID = setInterval(gameLoop, 1000 / 30);
}

function gameLoop(){
    InitFloor();
    processInput();
    hero.draw();
    
}
function InitFloor()
{
    ctx.fillStyle="#ffffff";
    ctx.fillRect(0,0,CanvasWidth,CanvasHeight);
    var tileheight=20;
    var tilewidth=20;
    var x_tiles=Math.floor(CanvasHeight/tileheight);
    var y_tiles=Math.floor(CanvasWidth/tilewidth);
    for (var i=0;i<x_tiles;i++)
    {	
        ctx.fillStyle="#7E5B52";
        for (var j=0;j<y_tiles;j++)
        {
            ctx.fillRect(i*tileheight+i,j*tilewidth+j,tileheight,tilewidth);
            
        }
    }
}

