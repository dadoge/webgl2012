startMapEditor = function (templateHelper) {

    var fadeSpeed = 300;
    $('#interactive-inner').fadeOut(fadeSpeed, function () {
        $('#interactive-inner').html(templateHelper.manageCharacter_btns);
        $('#Character-h').html("<h3>Map Editor</h3>");
        $('#Character-inner').html("<div id=\"rootDiv\"><canvas id=\"renderCanvas\"></canvas></div>");

        $('#Character-Cancel').click(function () { CancelConfirm() });

        var canvas = document.getElementById("renderCanvas");

        // Check support
        if (!BABYLON.Engine.isSupported()) {
            window.alert('Browser not supported');
        } else {
            // Babylon
            var engine = new BABYLON.Engine(canvas, true);

            //Creating scene (in "scene_tuto.js")
            scene = MapEditor(engine);

            scene.activeCamera.attachControl(canvas);

            // Once the scene is loaded, just register a render loop to render it
            engine.runRenderLoop(function () {
                scene.render();
            });

            // Resize
            window.addEventListener("resize", function () {
                engine.resize();
            });

            
        }

        $('#interactive-inner').fadeIn(fadeSpeed, function () {
            //Force render
            engine.resize();
        });

    });
};