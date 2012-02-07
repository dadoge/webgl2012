//Ball coordinates
var Ball = {
    X: 200,
    Y: 351,
    Radius: 6,
    SpeedX: -7,
    SpeedY: -8
};

//Paddle coordinates
var Paddle = {
    X: 200,
    Y: 365,
    Width: 85,
    Speed: 12
};

//Block
var Block = {
    a: {
        id: "purple",
        health: 1,
        color: "#512555",
        broken: false
    },
    z: {
        id: "empty",
        health: 0,
        color: null,
        broken: true
    }
};