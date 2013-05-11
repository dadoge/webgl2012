
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
window.addEventListener('keyup', doKeyUp, true);

$('#btnStart').on("click", doStart);
$('#btnStart2').on("click", doStart2);
//$('#btnStart').on("touchstart", doStart);
////////////////////////////////////////////////////////////////////////////
//                        Callbacks
///////////////////////////////////////////////////////////////////////////

function doStart() {
    if (isGameActive == false) {
        isGameActive = true;
        startGame();
    }

    if (leftTeamMoney > leftType.cost) {
        var newLeftType = {
            speed: 5,
            direction: 1,
            health: 10,
            damage: 3,
            cost: 10,
            team: 'left'
        };
        leftTeamUnits.push(new Unit(newLeftType, robotSprite, 64, 68, 4, 0, 400, ++unitCount));
        leftTeamMoney -= leftType.cost;
    }
}
function doStart2() {
    if (isGameActive == false) {
        isGameActive = true;
        startGame();
    }

    if (rightTeamMoney > rightType.cost) {
        var newRightType = {
            speed: 5,
            direction: -1,
            health: 10,
            damage: 3,
            cost: 10,
            team: 'left'
        };
        rightTeamUnits.push(new Unit(newRightType, robotSprite2, 64, 68, 4, Canvas.Width, 400, ++unitCount));
        rightTeamMoney -= rightType.cost;
    }
}
function doKeyDown(evt) {
    switch (evt.keyCode) {     
        case KEYS.LEFT:
			if(leftTeamMoney > leftType.cost)
			{
				var newLeftType = {
					speed:  5,
					direction: 1,
					health: 10,
					damage: 3,
					cost: 10,
					team: 'left'
				};
				leftTeamUnits.push(new Unit(newLeftType, robotSprite, 64, 68, 4, 0, 400, ++unitCount));
				leftTeamMoney -= leftType.cost;
			}
            LeftDown = true;
            break;    
        case KEYS.RIGHT:
			if(rightTeamMoney > rightType.cost)
			{
				var newRightType = {
				speed:  5,
				direction: -1,
				health: 10,
				damage: 3,
				cost: 10,
				team: 'right'
			};
				rightTeamUnits.push(new Unit(newRightType, robotSprite2, 64, 68, 4, Canvas.Width, 400, ++unitCount));
				rightTeamMoney -= rightType.cost;
			}
            RightDown = true;
            break;
        case KEYS.UP:

            UpDown = true;
            break;
        case KEYS.DOWN:

            DownDown = true;
            break;
        case KEYS.SPACE:
            if (isGameActive == false) {
                isGameActive = true;
				startGame();
            }
			
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

function doKeyUp(evt) {
    switch (evt.keyCode) {    
        case KEYS.LEFT:
            LeftDown = false;
            break;     
        case KEYS.RIGHT:
            RightDown = false;
            break;    
        case KEYS.UP:
            UpDown = false;
            break;   
        case KEYS.DOWN:
            DownDown = false;
            break;
    }
}

/////////////////////////////////////////////////////////////////////
//                      Process Input
////////////////////////////////////////////////////////////////////

function processInput() {

}
