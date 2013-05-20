function Unit(type, sprite, x, y, id) {
    this.type = type;
    this.damage = type.damage;
    this.health = type.health;
    this.x = x;
    this.y = y;
    this.height = 5;
    this.width = 5;
    this.image = sprite.image;
    this.spriteW = sprite.frameWidth;
    this.spriteH = sprite.frameHeight;
    this.spriteFrames = sprite.xFrames;
    this.state = 0;
    this.fightState = 0;
    this.counter = 0;
    this.id = id;
    this.range = type.range;
    this.takeDamage = function (damage) {
        this.health = this.health - damage;
        if (this.health <= 0) {
            if (this.type.team == "left") {
                leftPlayer.units = _.reject(leftPlayer.units, function (unitA) {
                    return unitA.id == this.id;
                }, this);

            }
            else {
                rightPlayer.units = _.reject(rightPlayer.units, function (unitA) {
                    return unitA.id == this.id;
                }, this);
            }
            return 1;
        }
        return 0;
    };
    this.draw = function (ctx) {
        var everyone = leftPlayer.units.concat(rightPlayer.units);
        var closestUnit = this.getClosestUnit(everyone);
        var closestEnemy = this.getClosestEnemy(everyone);

        if (this.canMove(closestUnit)) {

            this.x += this.type.speed * this.type.direction;

            if (this.x % 20 == 0) {
                this.state += this.spriteW;
            }
            if (this.x > Canvas.Width) {
                this.x = 0;
            }
            if (this.x < 0) {
                this.x = Canvas.Width;
            }
            if (this.state > this.spriteW * (this.spriteFrames - 1)) {
                this.state = 0;
            }
            ctx.drawImage(this.image, 0 + this.state, 0, this.spriteW, this.spriteH, this.x, this.y, this.spriteW, this.spriteH);
        }
        else if (this.type.team != closestUnit.type.team) {
            this.attack(ctx, closestUnit);
        }
        else if(closestEnemy.length > 0)
        {
            this.attack(ctx,closestEnemy[0]);
        }
        else {
            ctx.drawImage(this.image, 0 + this.state, 0, this.spriteW, this.spriteH, this.x, this.y, this.spriteW, this.spriteH);
        }
    }
    this.getClosestUnit = function (everyone) {
        if (this.type.direction == 1) {
            var everyoneElse = _.reject(everyone, function (unitA) {
                return unitA.id == this.id || unitA.x <= this.x;
            }, this);
            var closestUnit = _.min(everyoneElse, function (unitA) {
                return unitA.x - this.x
            }, this);
        }
        else {
            var everyoneElse = _.reject(everyone, function (unitA) {
                return unitA.id == this.id || unitA.x >= this.x;
            }, this);
            var closestUnit = _.min(everyoneElse, function (unitA) {
                return this.x - unitA.x
            }, this);
        }
        return closestUnit
    };
    this.getClosestEnemy = function(everyone){
        if (this.type.direction == 1) {
            var closestEnemy = _.reject(everyone, function (unitA) {
                return unitA.type.team == this.type.team ||
                    unitA.x - this.x > this.range;
            }, this);
        }
        else {
            var closestEnemy = _.reject(everyone, function (unitA) {
                return unitA.type.team == this.type.team ||
                    this.x - unitA.x > this.range;
            }, this);
        }
        return closestEnemy;
    }
    this.canMove = function (closestUnit) {
        return (closestUnit && this.type.direction == 1 && closestUnit.x - (this.x + this.type.speed) > 35) ||
            (closestUnit && this.type.direction == -1 && (this.x - this.type.speed) - closestUnit.x > 35) ||
            (closestUnit == Infinity);
    }

    this.attack = function (ctx, closestUnit) {
        ctx.drawImage(this.image, 2*this.spriteW + this.fightState, 2*this.spriteH, this.spriteW, this.spriteH, this.x, this.y, this.spriteW, this.spriteH);

        if (this.counter == 5) {
            this.fightState += this.spriteW;
            this.counter = 0;
            var killed = closestUnit.takeDamage(this.damage);
            if (this.type.team == "left") {
                leftPlayer.money += killed * 8;
            }
            else {
                rightPlayer.money += killed * 8;
            }
        }
        if (this.fightState > 64) {
            this.fightState = 0;
        }
        this.counter++;
    }
}


