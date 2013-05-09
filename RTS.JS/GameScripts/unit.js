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
    this.id = id;
    this.takeDamage = function (damage) {
        this.heath = this.health - damage;
    };
    this.draw = function (ctx) {
        ctx.drawImage(this.image, 0 + this.state, 0, spriteW, spriteH, this.x, this.y, spriteW, spriteH);
        var everyone = leftTeamUnits.concat(rightTeamUnits);
        var everyoneElse = _.reject(everyone, function(unitA) { return unitA.id == this.id; }, this);
        var closestUnit = _.min(everyoneElse, function (unitA) { return Math.abs(unitA.x - this.x) }, this);
        this.x += this.type.speed * this.type.direction;

        if(this.x % 20 == 0)
        {
            this.state += spriteW;
        }
        if (this.x > Canvas.Width) {
            this.x = 0;
        }
        if (this.x < 0) {
            this.x = Canvas.Width;
        }
        if (this.state > spriteW * (spriteFrames-1)) {
            this.state = 0
        }
    }
}

