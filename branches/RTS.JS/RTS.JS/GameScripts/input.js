
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

$('#btn_leftA').on("click", sendLeftUnit);
$('#btn_rightA').on("click", sendRightUnit);

$('#btn_start').on("click", start);
////////////////////////////////////////////////////////////////////////////
//                        Callbacks
///////////////////////////////////////////////////////////////////////////

function doKeyDown(evt) {
    switch (evt.keyCode) {     
        case KEYS.LEFT:
			sendLeftUnit();
            break;    
        case KEYS.RIGHT:
			sendRightUnit();
			break;
        case KEYS.SPACE:
			startGame();
            break;
        case KEYS.UP:
            sendLeftArcher();
            break;
        case KEYS.P:
            if (isPaused == true) {
                isPaused = false;
                startGame();
            }
            else {
                isPaused = true;
            }
            break;
    }
}
///////////////////////////////////////////////////////////////
//Methods that do the work of input
//////////////////////////////////////////////////////////////
//this should really be sendunit(type), but we'll get there.
function sendLeftUnit()
{
    if (leftTeamMoney > leftType.cost) {
		var newLeftType = new UnitType(5,1,10,3,10,'left');
		leftTeamUnits.push(new Unit(newLeftType, blueRobotSprite, 0, 400, ++unitCount));
		leftTeamMoney -= leftType.cost;
	}
}
function sendRightUnit()
{
    if (rightTeamMoney >= rightType.cost) {
		var newRightType = new UnitType(5,-1,10,3,10,'right');
		rightTeamUnits.push(new Unit(newRightType, pinkRobotSprite, Canvas.Width, 400, ++unitCount));
		rightTeamMoney -= rightType.cost;
    }
}
function sendLeftArcher()
{
    if(leftTeamMoney > 20){
        var leftArcherType = new UnitType(5,1,10,3,10,'left');
        leftTeamUnits.push(new Unit(leftArcherType, archerSprite, 0, 400, ++unitCount));
        leftTeamMoney -= 20;
    }

}
function start(){
    if (isGameActive == false) {
        isGameActive = true;
		startGame();
    }
}

