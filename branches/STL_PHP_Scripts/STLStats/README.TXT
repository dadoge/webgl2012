README.TXT [v1.1]
-----------

Introduction
------------

STL format is popularly used for representing 3D
object information for 3D printing applications. It is useful to know the volume
and hence the weight of the material that may go into making an object on a 3D
printer before one prints it. This may aid calculating the cost of printing that
particular object.

This software uses a tetrahedron volume counting algorithm to determine the
volume of the 3D model represented in a binary STL file.

The math is wrapped inside an easy to use PHP class that exposes getter
functions (i.e. getVolume, getWeight, etc) to calculate common statistics of a
3D object. This class will be useful to a website which deals in web based 3D
printing applications.

The PHP code has been commented extensively. Example usage has been given at the
end of this documentation as well as at the end of the class and should be
commented out when the class is deployed on your server.

Install Notes
-------------

Follow these steps to start using this PHP class in
 your applications:

1. Extract the contents of the STLStats.zip folder.

2. Copy the STLStats.php file to your webserver folder such that your
application can resolve the path and find this class file.

3. A sample binary STL file - 40mmcube.stl has been provided to help you test
the class right away. Copy this file such that the PHP class can find this path.
Pass this path as an argument when you instantiate the STLStats class. For
example: if the 40mmcube.stl file is in the directory "/var/www/mystlfiles/"
then call this class from within your code like this:

    $mypath = "/var/www/mystlfiles/40mmcube.stl";
    $obj = new STLStats($mypath);
    $unit = "cm";
    echo $obj->getVolume($unit);

4. A sample ascii STL file - nut-ascii.stl has been provided to help you test
the class right away. Copy this file such that the PHP class can find this path.
Pass this path as an argument when you instantiate the STLStats class. For
example: if the 40mmcube.stl file is in the directory "/var/www/mystlfiles/"
then call this class from within your code like this:

    $mypath = "/var/www/mystlfiles/nut-ascii.stl";
    $obj = new STLStats($mypath);
    $unit = "cm";
    echo $obj->getVolume($unit);

Thats all!
