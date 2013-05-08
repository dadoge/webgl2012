function Unit(type) {
    this.type = type;
    this.damage = 5;
    this.health = 20;
    this.takeDamage = function (damage) {
        this.heath = this.health - damage;
    };
}

