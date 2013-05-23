function UnitType(speed, direction, health, damage, range, cost, team) {
    this.speed = speed;
    this.direction = direction;
    this.health = health;
    this.damage = damage;
    this.range = range;
    this.cost = cost;
    this.team = team;
    this.getBehavior = function(){
        var self = this;
        if(self.canMove(self.getClosestUnit(leftPlayer.units.concat(rightPlayer.units)))){
            return self.walkBehavior;
        }
    }
}
   
