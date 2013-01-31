var sprite = function sprite(img,x,y)
{
    this.x = x;
    this.y = y;
    this.Image = img;
    this.draw = function draw()
    {
        ctx.drawImage(this.Image,this.x,this.y);
    }();
}();