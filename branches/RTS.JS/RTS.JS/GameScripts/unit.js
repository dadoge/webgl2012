function Unit(type) {
    this.type = type;
    this.damage = 5;
    this.health = 20;
    this.x = 0;
    this.y = 100;
    this.height = 5;
    this.width = 5;
    this.takeDamage = function (damage) {
        this.heath = this.health - damage;
    };
}

