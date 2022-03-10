const app = require('express')();
const http = require('http').createServer(app);
const io = require('socket.io')(http);
const Pla = require('./models/Player');

const { nanoid } = require('nanoid');
const Player = require('./models/Player')
const FoodPosition = require('./models/FoodPosition')

var lsPlayer = [];
var lsFood = [];
for (var i = 0; i < 100; i++) {
    var position = new FoodPosition();
    position.x = randomPositonFood();
    position.y = randomPositonFood();
    lsFood.push(position);
}
io.sockets.on('connection', (socket) => {
    //spawn
    lsPlayer.forEach(function(item, index, array) {
        //console.log("send player all" + item);
        socket.emit('new_player', item.id);
        socket.emit('updatePosition', item.positionToString());
        socket.emit('updateStatus', item.statusToString());
        console.log("zxc" + item.statusToString() + "asdddasdasd");
    })

    lsFood.forEach(function(item, index, array) {
            //console.log("send player all" + item);
            socket.emit('changeFood', index + "|" + item.toString());
        })
        //end spawn
    var player = new Player();
    player.id = nanoid();
    console.log(player.id + ' connect');
    socket.emit('new_player_id', player.id);
    socket.broadcast.emit('new_player', player.id);

    socket.on('updatePosition', function(dataGet) {
        const data = dataGet.split('|');
        player.position.x = data[0];
        player.position.y = data[1];
        socket.broadcast.emit('updatePosition', player.positionToString());
    });
    socket.on('updateRotation', function(dataGet) {
        player.rotation = dataGet;
        socket.broadcast.emit('updateRotation', player.id + "|" + player.rotation);
    });
    socket.on('attack', function(dataGet) {
        socket.broadcast.emit('attack', player.id + "|" + dataGet);
    });
    socket.on('loseHp', function(dataGet) {
        const data = dataGet.split('|');
        lsPlayer.forEach(function(item, index, array) {
            if (item.id == data[0]) {
                item.status.hpNow -= data[1];
                if (item.status.hpNow <= 0) {
                    item.new();
                    io.emit('loseHp', data[0] + "|0");
                } else {
                    io.emit('loseHp', data[0] + "|" + Math.round(((item.status.hpNow / item.status.hp) * 10) * 100) / 100);
                }
            }
        })
    });
    socket.on('updateHp', function(dataGet) {
        const data = dataGet.split('|');
        player.status.hpNow = data[0];
        player.status.hp = data[1];
        socket.broadcast.emit('loseHp', player.id + "|" + Math.round(((player.status.hpNow / player.status.hp) * 10) * 100) / 100);
    });
    socket.on('restoreHp', function(dataGet) {
        player.status.hpNow += 1;
        io.emit('restoreHp', player.id + "|" + Math.round(((player.status.hpNow / player.status.hp) * 10) * 100) / 100);
    });
    //
    socket.on('changeFood', function(dataGet) {
        lsFood[dataGet].x = randomPositonFood();
        lsFood[dataGet].y = randomPositonFood();
        io.emit('changeFood', dataGet + "|" + lsFood[dataGet].toString());
    });
    socket.on('disconnect', () => {
        console.log(player.id + ' disconnected');
        lsPlayer.splice(lsPlayer.indexOf(player), 1);
        socket.broadcast.emit('player_disconnect', player.id);
    });
    lsPlayer.push(player);
    // console.log(player.id + ' connect ok \n');

});

function randomPositonFood() {
    return Math.floor(Math.random() * 50) - 24;
}

app.get('/', (req, res) => {
    res.sendFile(__dirname + '/index.html');
});
http.listen(process.env.PORT || 3000, () => {
    console.log('Connected at 3000');
});