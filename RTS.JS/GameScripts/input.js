
//KEYS
var KEYS = {
    SPACE: 32,
    LEFT: 37,
    UP: 38,
    RIGHT: 39,
    DOWN: 40,
    P: 80
};

//Key Listeners
window.addEventListener('keydown', doKeyDown, true);

//$('#btn_leftA').on("click", sendLeftUnit);
//$('#btn_leftB').on("click", sendLeftArcher);
//$('#btn_rightA').on("click", sendRightUnit);
//$('#btn_rightB').on("click", sendRightArcher);

//$('#btn_start').on("click", start);
////////////////////////////////////////////////////////////////////////////
//                        Callbacks
///////////////////////////////////////////////////////////////////////////

function doKeyDown(evt) {
    switch (evt.keyCode) {     
        case KEYS.LEFT:
            var newLeftType = new UnitType(5,1,10,3,35,10,'left');
			sendUnit(leftPlayer,newLeftType,blueRobotSprite)
            break;    
        case KEYS.RIGHT:
            var newRightType = new UnitType(5,-1,10,3,35,10,'right');
			sendUnit(rightPlayer,newRightType,pinkRobotSprite);
			break;
        case KEYS.SPACE:
            start();
            break;
        case KEYS.UP:
            var leftArcherType = new UnitType(5,1,10,3,100,10,'left');
            sendUnit(leftPlayer,leftArcherType,archerSprite);
            break;
        case KEYS.DOWN:
            var rightArcherType = new UnitType(5,-1,10,3,100,10,'right');
            sendUnit(rightPlayer, rightArcherType, archer2Sprite);
            break;
        case KEYS.P:
            if (game.isPaused == true) {
                game.isPaused = false;
                startGame();
            }
            else {
                game.isPaused = true;
            }
            break;
    }
}
///////////////////////////////////////////////////////////////
//Methods that do the work of input
//////////////////////////////////////////////////////////////
//this should really be sendunit(type), but we'll get there.
function sendUnit(player,type,sprite){
    if(player.money >= type.cost)
    {
        player.units.push(new Unit(type,sprite,player.baseX,player.baseY, ++unitCount))
    }
    player.money -= type.cost;
}
function start(){
    if(!game.isGameActive)
    {
        preinit();
        startGame();
        game.isGameActive = true;
    }
}

