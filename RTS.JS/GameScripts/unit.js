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
        ctx.drawImage(this.image, 0 + this.state, 0, 60, 60, this.x, this.y, 60, 60);
        this.state += 50;
        this.x += 10;
        if (this.x > 500) {
            this.x = 0;
        }
        if (this.state > 200) {
            this.state = 0
        }
    }
}

