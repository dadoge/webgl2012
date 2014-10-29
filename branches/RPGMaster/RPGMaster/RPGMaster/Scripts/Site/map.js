function MapEditor_Model(data,maps) {
    var self = this;
    self.ActiveMapName = ko.observable('Map-0');
    self.ActiveMapID = ko.observable(1);
    self.MapID = ko.observableArray(data.MapID);
    self.MapNames = ko.observableArray(data.Name);
    self.Maps = ko.observableArray(maps);

}

function GameMap() {
    this.Name;
    this.UserName;
    this.CreatedByPlayerID = 0;
    this.Tiles = [];
    this.TilesData = "";
    this.DateCreated = "";
    this.isActive = 1;
}

function MapTile() {
    this.posX;
    this.posY;
    this.posZ;
    this.tileType;
    this.diffuseColor;
    this.materialName="";

    this.insertTileData = function (babylonTile) {
        this.posX = babylonTile.position.x;
        this.posY = babylonTile.position.y;
        this.posZ = babylonTile.position.z;
        if (babylonTile.scaling.y == 1) {
            this.tileType = 0;
        }
        else {
            this.tileType = 1;
        }
        this.diffuseColor = babylonTile.material.diffuseColor;
    }

}

function UserMaps() {
    this.Name = [];
    this.MapID = [];
}