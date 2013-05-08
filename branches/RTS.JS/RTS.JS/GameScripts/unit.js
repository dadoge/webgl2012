function Unit(type, sprite) {
    this.type = type;
    this.damage = 5;
    this.health = 20;
    this.x = 100;
    this.y = 100;
    this.height = 5;
    this.width = 5;
    this.image = sprite;
    this.state = 0;
    this.takeDamage = function (damage) {
        this.heath = this.health - damage;
    };
    this.draw = function (ctx) {
        ctx.drawImage(this.image, 0 + this.state, 0, 50, 50, this.x, this.y, 50, 50);
        this.state += 50;
        if (this.state > 200) {
            this.state = 0
        }
    }
}

