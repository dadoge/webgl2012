function Unit(type) {
    this.type = type;
    this.damage = 5;
    this.health = 20;
    this.height = 10;
    this.x = 0;
    this.y = 100;
    this.width = 5;
    this.animate = function (ctx) {
        ctx.fillStyle = "#FFFFFF";
        ctx.fillRect(this.x, this.y, this.height, this.width);
        this.x++;
    };
    this.takeDamage = function (damage) {
        this.heath = this.health - damage;
    };
}

