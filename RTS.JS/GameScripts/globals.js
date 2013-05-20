function Global(){
    this.intervalID;
    this.FPS = 30;
    this.isGameActive = false;
    this.isPaused = false;
    this.groundHeight = 50;

    this.leftTeamMoney = 100;
    this.rightTeamMoney = 100;
    this.leftTeamExp = 0;
    this.rightTeamExp = 0;
    this.leftBaseHealth = 300;
    this.rightBaseHealth = 300;
}