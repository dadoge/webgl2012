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

/////////////////////////////////////////////////////////////////////
//                        Power-up Creation
/////////////////////////////////////////////////////////////////////

function createPowerup(i, j, imagename, t) {
    var powerup;
    var image = new Image();
    var currBlock = level[i][j];
    image.src = "../Content/" + imagename;
    powerup = { x: Ball.X, y: Ball.Y, img: image, speedY: 5, type: t };
    return powerup;
}

// If powerup granted, draw it
function drawPowerup() {
    if (isPowerupEnabled) {
        processPowerup();
        ctx.drawImage(currentPowerup.img, currentPowerup.x, currentPowerup.y);

        if (currentPowerup.y > Paddle.Y + currentPowerup.speedY) {
            isPowerupEnabled = false;
            isPowerupAllowed = false;
            setTimeout(function () { isPowerupAllowed = true; }, 10000);
        }
        else if (currentPowerup.y >= Paddle.Y - 20) {
            if (currentPowerup.x >= (Paddle.X - Paddle.Width / 2) && currentPowerup.x <= (Paddle.X + Paddle.Width / 2)) {
                //you got the powerup!
                if (currentPowerup.type == "life") {
                    lives++;
                    isPowerupEnabled = false;
                    isPowerupAllowed = false;
                    setTimeout(function () { isPowerupAllowed = true; }, 10000);
                }
                else if (currentPowerup.type == "size") {
                    Paddle.Width = Paddle.Width * 2;
                    isPowerupEnabled = false;
                    isPowerupAllowed = false;
                    setTimeout(function () { Paddle.Width = 85; isPowerupAllowed = true; }, 10000);
                }
            }
        }
    }
}

function determineGrantPowerup(i, j) {
    var num = Math.floor(Math.random() * 10 + 1);
    if (num >= 6) {
        isPowerupEnabled = true;
        num = Math.floor(Math.random() * 10 + 1);
        if (num >= 6) {
            lifePowerup = createPowerup(i, j, "th_heart_pic_tiny.gif", "life");
            currentPowerup = lifePowerup;
        }
        else {
            sizePowerup = createPowerup(i, j, "2times.png", "size");
            currentPowerup = sizePowerup;
        }
    }
}

function processPowerup() {
    currentPowerup.y += currentPowerup.speedY;
}

/////////////////////////////////////////////////////////////////////
//                        Block Creation
/////////////////////////////////////////////////////////////////////

function createBlock(extX, extY, extHealth, extColor, extBroken) {
    var block;
    block = { health: extHealth, x: extX, y: extY, color: extColor, broken: extBroken };
    return block;
}

function setupLevel() {
    for (var i = 0; i < level.length; i++) {
        for (var j = 0; j < level[i].length; j++) {
            if (level[i][j] == "purple") {
                level[i][j] = createBlock(j, i, 1, "#660099", false);
                numBlocks++;
            }
            else if (level[i][j] == "orange") {
                level[i][j] = createBlock(j, i, 1, "#FF9900", false);
                numBlocks++;
            }
            else if (level[i][j] == "red") {
                level[i][j] = createBlock(j, i, 2, "#FF0000", false);
                numBlocks++;
            }
            else if (level[i][j] == "blue") {
                level[i][j] = createBlock(j, i, 3, "#0000FF", false);
                numBlocks++;
            }
            else if (level[i][j] == "empty") {
                level[i][j] = null;
            }
        }
    }
}

//////////////////////////////////////////////////////////////////////////////////////
//                                  Brick collision
//////////////////////////////////////////////////////////////////////////////////////
function collisionXWithBlocks() {
    var bumpedX = false;
    var currBlock;
    for (var i = 0; i < level.length; i++) {
        for (var j = 0; j < level[i].length; j++) {
            currBlock = level[i][j];

            if (currBlock != null) {
                if (currBlock.health > 0) { // if brick is still visible
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
                            //breakBlock(currBlock);

                            bumpedX = true;
                        }
                    }
                }
            }

        }
    }
    return bumpedX;
}

function collisionYWithBlocks() {
    var bumpedY = false;
    var currBlock;
    for (var i = 0; i < level.length; i++) {
        for (var j = 0; j < level[i].length; j++) {
            currBlock = level[i][j];
            if (currBlock != null) {
                if (currBlock.health > 0) { // if brick is still visible
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
                            //breakBlock(currBlock);

                            bumpedY = true;
                        }
                    }
                }
            }

        }
    }
    return bumpedY;
}

function explodeBlock(i, j) {
    // First weaken the block (0 means block is gone)
    var currBlock;
    currBlock = level[i][j];
    currBlock.health--;

    if (currBlock.health > 0) {
        // The block is weakened but still around. Give a single point.
        GameScore++;
    } else {
        // give player an extra point when the block disappears
        GameScore += 2;
        currBlock.broken = true;
        numBlocks--;
        if (!isPowerupEnabled && isPowerupAllowed) {
            determineGrantPowerup(i, j);
        }
    }
}
