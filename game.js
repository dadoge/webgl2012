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
    
    
    //Things that have to do with processing input or conditionals based upon input parameters need to be in processinput() or a function it calls
//    if(DownDown && !UpDown)
//    {
//        //if you never change step whats the point?
//        var step=3;
//        
//        //step * 1 means nothings
//        if (count<step*1){
//            //Please only update the hero object, not the hero image
//            heroImage.src = 'Ot_FrontSprite_1.png';
//            
//            //replace your N hero.draws() with a single hero.draw()
//            hero.draw();
//        }
//        else if(count>=step*1 && count<step*2){
//            //Also lets put all images into string varables
//            heroImage.src = 'Ot_FrontSprite_2.png';
//            hero.draw();
//        }
//        else if(count>=step*2 && count<step*3){
//            heroImage.src = 'Ot_FrontSprite_3.png';
//            hero.draw();
//        }
//        else if(count>=step*3 && count<step*4){
//            heroImage.src = 'Ot_FrontSprite_4.png';
//            hero.draw();
//        }
//        else if(count>=step*4){
//            heroImage.src = 'Ot_FrontSprite_1.png';
//            hero.draw();
//            count=0;
//        }
//        count=count+1;
//    }
//    else{
//        heroImage.src = 'Ot_FrontSprite_1.png';
//        hero.draw();
//    }
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

