function Unit(type, sprite) {
    this.type = type;
    this.damage = 5;
    this.health = 20;
    this.x = 100;
    this.y = 100;
    this.height = 5;
    this.width = 5;
    this.image = sprite;
    this.takeDamage = function (damage) {
        this.heath = this.health - damage;
    };
    this.draw = function (ctx) {
        ctx.drawImage(this.image, this.x, this.y);
    }
}

