const { type } = require("express/lib/response");
module.exports = class Vector2 {
    constructor(X = 0, Y = 0) {
        this.x = X;
        this.y = Y;
    }
    toString() {
        return this.x + "|" + this.y;
    }
}