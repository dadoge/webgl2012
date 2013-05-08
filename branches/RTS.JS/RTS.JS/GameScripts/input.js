//Key States
var LeftDown = false;
var RightDown = false;
var UpDown = false;
var DownDown = false;

//KEYS
var KEYS = {
    SPACE: 32,
    LEFT: 37,
    UP: 38,
    RIGHT: 39,
    DOWN: 40,
    P: 80
};
var ImageUp = new Image();
ImageUp.src ='Ot_BackSprite_1.png';
             
var ImageDown = new Image();
ImageDown.src ='Ot_FrontSprite_1.png';

//Key Listeners
window.addEventListener('keydown', doKeyDown, true);
window.addEventListener('keyup', doKeyUp, true);


////////////////////////////////////////////////////////////////////////////
//                        Callbacks
///////////////////////////////////////////////////////////////////////////


function doKeyDown(evt) {
    switch (evt.keyCode) {     
        case KEYS.LEFT:
            if(!LeftDown)
            {
                if(hero.Image.src != 'Ot_LeftSprite_1.png')
                {
                    hero.Image.src = 'Ot_LeftSprite_1.png';
                }
            }
            LeftDown = true;
            break;    
        case KEYS.RIGHT:
            if(!RightDown)
            {
                if(hero.Image.src != 'Ot_RightSprite_1.png')
                {
                    hero.Image.src = 'Ot_RightSprite_1.png';
                }
            }
            RightDown = true;
            break;
        case KEYS.UP:
            if(!UpDown)
            {
                if(hero.Image.src != 'Ot_BackSprite_1.png')
                {
                    hero.Image.src = 'Ot_BackSprite_1.png';
                }
            }
            UpDown = true;
            break;
        case KEYS.DOWN:
            if(!DownDown)
            {
                if(hero.Image.src != 'Ot_FrontSprite_1.png')
                {
                    hero.Image.src = 'Ot_FrontSprite_1.png';
                }
            }
            DownDown = true;
            break;
        case KEYS.SPACE:
            break;
        case KEYS.P:
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
    if(LeftDown && !RightDown)
    {
        hero.X -= 5;
    }
    if(RightDown && !LeftDown)
    {
        hero.X += 5;
    }
    if(UpDown && !DownDown)
    {
        hero.Y -= 5;
    }
    if(DownDown && !UpDown)
    {
        hero.Y += 5;
    }
}
