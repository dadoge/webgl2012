<?php

    include("STLStats.php");

    $allowedExts = array("stl");
    $extension = strtolower( end(explode(".", $_FILES["file"]["name"])) );
//    if (($_FILES["file"]["size"] < 200000)
//    && in_array($extension, $allowedExts))
		if(in_array($extension, $allowedExts))
      {
        if ($_FILES["file"]["error"] > 0)
          {
              echo "Error: " . $_FILES["file"]["error"] . "<br />";
          }
        else
          {
            echo "<H1>STLStats report</H1><BR><BR>";
            $fname = $_FILES["file"]["tmp_name"];
            $obj = new STLStats($fname);
            echo "<u><H2>Basic Usage</H2></u>";
            $unit = "cm";
            $vol = $obj->getVolume($unit);
            echo "Volume: " . $vol . " cubic " . $unit . "<BR>";
            $weight = $obj->getWeight();
            echo "Weight: " . $weight . " gm<BR>";
            $den = $obj->getDensity();
            echo "Density: " . $den . " gm/cc" . "<BR>";
            $tcount = $obj->getTrianglesCount();
            echo "Triangles Count: " . $tcount . " triangles read<BR>";

            echo "<BR>";

            echo "<u><H2>Units -> inch</H2></u>";
            $unit = "inch";
            $vol = $obj->getVolume($unit);
            echo "Volume: " . $vol . " cubic " . $unit . "<BR>";
            $weight = $obj->getWeight();
            echo "Weight: " . $weight . " gm<BR>";
            $den = $obj->getDensity();
            echo "Density: " . $den . " gm/cc" . "<BR>";
            $tcount = $obj->getTrianglesCount();
            echo "Triangles Count: " . $tcount . " triangles read<BR>";

            echo "<BR>";

            echo "<u><H2>Change Density (default 1.04g/cc -> 2.44g/cc)</H2></u>";
            $obj->setDensity(2.44);
            $unit = "cm";
            $vol = $obj->getVolume($unit);
            echo "Volume: " . $vol . " cubic " . $unit . "<BR>";
            $weight = $obj->getWeight();
            echo "Weight: " . $weight . " gm<BR>";
            $den = $obj->getDensity();
            echo "Density: " . $den . " gm/cc" . "<BR>";
            $tcount = $obj->getTrianglesCount();
            echo "Triangles Count: " . $tcount . " triangles read<BR>";
          }
      }
    else
      {
        echo "File too large or bad file extension.";
      }

    function __autoload($class_name) {
        include 'classes/'.$class_name . '.php';
    }

?>
