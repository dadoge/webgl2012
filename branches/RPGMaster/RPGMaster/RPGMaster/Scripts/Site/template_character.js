function Character_Stats() {
    return " \
        <% _.each(Stats, function(Stats) { %> \
        <li style=\"list-style-type: none\"> \
            <%= Stats.Name.substring(0,3) %> : <%= Stats.Value %> \
        </li> \
    <% }); %>";
}

function Character_Skills() {
    return " \
        <% _.each(Skills, function(Skills) { %> \
        <li style=\"list-style-type: none\"> \
            <%= Skills.Name %> : <%= Skills.Value %> \
        </li> \
    <% }); %>";
}