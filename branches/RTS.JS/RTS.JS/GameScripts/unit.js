function Unit(type,sprite, spriteW, spriteH, spriteFrames, x, y, id) {
    this.type = type;
    this.damage = type.damage;
    this.health = type.health;
    this.x = x;
    this.y = y;
    this.height = 5;
    this.width = 5;
    this.image = sprite;
    this.state = 0;
    this.fightState = 0;
    this.counter = 0;
    this.id = id;
    this.takeDamage = function (damage) {
        this.health = this.health - damage;
		if(this.health <= 0)
		{
			if(this.type.team == "left")
			{
				leftTeamUnits = _.reject(leftTeamUnits, function (unitA) { return unitA.id == this.id;}, this);
				
			}
			else
			{
				rightTeamUnits = _.reject(rightTeamUnits, function (unitA) { return unitA.id == this.id;}, this);
			}
			return 1;
		}
		return 0;
    };
    this.draw = function (ctx) {
        var everyone = leftTeamUnits.concat(rightTeamUnits);
        var closestUnit;

        if (this.type.direction == 1) {
            var everyoneElse = _.reject(everyone, function (unitA) { return unitA.id == this.id || unitA.x <= this.x; }, this);
            closestUnit = _.min(everyoneElse, function (unitA) { return unitA.x - this.x }, this);
        }
        else 
		{
			var everyoneElse = _.reject(everyone, function (unitA) { return unitA.id == this.id || unitA.x >= this.x; }, this);
			closestUnit = _.min(everyoneElse, function (unitA) { return this.x - unitA.x }, this);        
		}
		if(closestUnit && this.type.direction == 1 && closestUnit.x - this.x + this.type.speed > 35) {
            this.x += this.type.speed * this.type.direction;

            if (this.x % 20 == 0) {
                this.state += spriteW;
            }
            if (this.x > Canvas.Width) {
                this.x = 0;
            }
            if (this.x < 0) {
                this.x = Canvas.Width;
            }
            if (this.state > spriteW * (spriteFrames - 1)) {
                this.state = 0;
            }
            ctx.drawImage(this.image, 0 + this.state, 0, spriteW, spriteH, this.x, this.y, spriteW, spriteH);
        }
        else if (closestUnit && this.type.direction == -1 && (this.x - this.type.speed) - closestUnit.x  > 35) {
            this.x += this.type.speed * this.type.direction;

            if (this.x % 20 == 0) {
                this.state += spriteW;
            }
            if (this.x > Canvas.Width) {
                this.x = 0;
            }
            if (this.x < 0) {
                this.x = Canvas.Width;
            }
            if (this.state > spriteW * (spriteFrames - 1)) {
                this.state = 0;
            }
            ctx.drawImage(this.image, 0 + this.state, 0, spriteW, spriteH, this.x, this.y, spriteW, spriteH);
        }
        else if (closestUnit == Infinity) {
            this.x += this.type.speed * this.type.direction;

            if (this.x % 20 == 0) {
                this.state += spriteW;
            }
            if (this.x > Canvas.Width) {
                this.x = 0;
            }
            if (this.x < 0) {
                this.x = Canvas.Width;
            }
            if (this.state > spriteW * (spriteFrames - 1)) {
                this.state = 0;
            }
            ctx.drawImage(this.image, 0 + this.state, 0, spriteW, spriteH, this.x, this.y, spriteW, spriteH);
        }
        else if(this.type.team != closestUnit.type.team){
            ctx.drawImage(this.image, 128 + this.fightState, 136, spriteW, spriteH, this.x, this.y, spriteW, spriteH);

            if (this.counter == 5) {
                this.fightState += 64;
                this.counter = 0;
				var killed = closestUnit.takeDamage(this.damage);
				if(this.type.team == "left")
				{
					leftTeamMoney += killed * 50;
				}
				else
				{
					rightTeamMoney += killed * 50;
				}
            }
            if (this.fightState > 64) {
                this.fightState = 0;
            }
            this.counter++;
        }
		else
		{
			ctx.drawImage(this.image, 0 + this.state, 0, spriteW, spriteH, this.x, this.y, spriteW, spriteH);
		}
		
    }
}


