$(document).ready(function () {
    var list = "<% _.each(Stats, function(Stats) { %> <li><%= Stats.Name %> : <%= Stats.Value %></li> <% }); %>";
    var Stats = [{ Name: 'DEX', Value: 18 }, { Name: 'STR', Value: 18 }] 
    document.getElementById('character-stats').innerHTML = _.template(list, {Stats:Stats});
    //var list = "<% _.each(people, function(name) { %> <li><%= name %></li> <% }); %>";
    //document.getElementById('character-stats').innerHTML=_.template(list, { people: ['moe', 'curly', 'larry'] });
 //   $.getJSON("http://www.antonmorgan.com/dndsvc/getplayer/1", function (data) {

        //document.getElementById('character-stats').innerHTML = data.Stats;
        //document.getElementById('character-stats').innerHTML = _.template(list, { Stats: [{ Name: 'DEX', Value: 18 }, {Name: 'STR', Value: 18}]});
        //document.getElementById('character-info').innerHTML="Test";
        //$('#character-info').append(data.Name);
 //   });
});