const { type } = require("express/lib/response");

module.exports = class Player {
    constructor() {
        this.username = '';
        this.id = '';
        this.position = new Vector2();
        this.rotation = 0;
        this.status = new Status();
    }
    new() {
        this.position = new Vector2();
        this.status = new Status();
    }
    statusToString() {
        return this.id + "|" + ((this.status.hpNow / this.status.hp) * 10);
    }
    positionToString() {
        return this.id + "|" + this.position.toString();
    }
}
class Vector2 {
    constructor(X = 0, Y = 0) {
        this.x = X;
        this.y = Y;
    }
    toString() {
        return this.x + "|" + this.y;
    }
}
class Status {
    constructor(hp = 10) {
        this.hp = hp;
        this.hpNow = hp;
    }

    toString() {
        return this.hp;
        return this.hpNow;
    }
}