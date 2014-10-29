function GameStateHelper() {
    this.gameMap = new GameMap();
    this.mapEditor_Model = {};

}

function GameRules() {
    this.calcModifier = function (attribute) {
        var modValue = Math.floor((attribute - 10) / 2);
        return modValue;
    }
    //Converts a Money object (array of objects with value pieces) to a single Value
    this.convertMoneyToSingle = function (Money) {
        var moneyTotal=0;
        var mult=1;
        for (var i = Money.length-1; i >= 0; i--) {
            moneyTotal += (Money[i].pieces * mult);
            mult *= 1000;
        }
        return moneyTotal;
    }
    //Converts single value money variable to a Money Object
    this.convertMoneyToObject = function (moneyTotal) {
        var Money = [{ pieces: 0 }, { pieces: 0 }, { pieces: 0 }, { pieces: 0 }];
        var mult = 1000*1000*1000;
        for (var i = 0; i< Money.length; i++) {
            Money[i].pieces = Math.floor(moneyTotal / mult);
            moneyTotal -= (Money[i].pieces * mult);
            mult /= 1000;
        }
        return Money;
    }
}