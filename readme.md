# Geodesix README 
 
Geodesix is an add-in to display Google Maps in Excel:

<a href="src/GeodesiX/help/paristour.jpg" target="_blank">
	<img style='width:800px' src="src/GeodesiX/help/paristour.jpg" title="Click to enlarge" />
</a>

Applications
============

*   Exports from fitness applications [sample](src/GeodesiX/help/workout.jpg)
*   Forensics, to plot movements of mobile phones with cellular triangulation from CDR records
*   Google Takeout History [sample](src/GeodesiX/help/takeout.jpg)
*   Route/trip planning and analysis [sample](src/GeodesiX/help/japan.jpg)
*   Surveying [sample](src/GeodesiX/help/centralpark.jpg)
*   Topical maps of offices, natural phenomena, ... [sample](src/GeodesiX/help/chopes.html)

Features
========

*   Draws maps automatically for any worksheet with Latitude and Longitude columns.
*   Icons, markers, lines, distances, popups and legends can be individually formatted.
*   Maps can be exported as self-contained HTML files suitable for sharing [sample](src/GeodesiX/help/paristour.htm).
*   Imports structured data in JSON or XML formats with a [generic parser](src/GeodesiX/help/templateeditor.jpg) for any data structure. Ready-built parsers for GeoJSON, GPX, TCX & Google Takeout.
*   Exports to HTML, GeoJSON, KML (Google Earth) and tabbed-without-quotes
*   Travelling Salesman Solver: Given a list of places, what is the shortest possible route that visits each place exactly once?
*   Example workbooks, demonstrating all the functionalities. Comprehensive [Help](src/GeodesiX/help/index.html)
*   Complete VBA interface.
*   Extensive library of Excel functions for geodesics:
    
    [Area](src/GeodesiX/help/ExcelHelp.html#Area) Calculate the area of a polygon
    
    [Azimuth](src/GeodesiX/help/ExcelHelp.html#Azimuth) Calculate the initial and final bearing from one point to another
    
    [Color](src/GeodesiX/help/ExcelHelp.html#Color) Formats a hexadecimal Color from the ARGB components
    
    [Displace](src/GeodesiX/help/ExcelHelp.html#Displace) Gets a point at a bearing and distance from another point
    
    [Distance](src/GeodesiX/help/ExcelHelp.html#Distance) Calculate the distance in metres between 2 latitude/longitude pairs, as the crow flies
    
    [DMS](src/GeodesiX/help/ExcelHelp.html#DMS) Convert Degrees-Minutes-Seconds to decimal degrees
    
    [Draw](src/GeodesiX/help/ExcelHelp.html#Draw) Draw circles/lines/markers on the map
    
    [Formulae](src/GeodesiX/help/ExcelHelp.html#Formulae) Get the formula of a cell, showing the values
    
    [Geocode](src/GeodesiX/help/ExcelHelp.html#Geocode) Get Geodesic information about a place
    
    [Geodesix](src/GeodesiX/help/ExcelHelp.html#Geodesix) User interface and VBA drawing functions
    
    [GeoReverse](src/GeodesiX/help/ExcelHelp.html#GeoReverse) Performs a reverse geocode
    
    [Heat](src/GeodesiX/help/ExcelHelp.html#Heat) Gets the 'temperature' of a value in the range 'min' to 'max'. The Color ranges from purple (cold) to red (hot)
    
    [JavaScript](src/GeodesiX/help/ExcelHelp.html#JavaScript) Execute a JavaScript function in the map pane
    
    [Pluscode](src/GeodesiX/help/ExcelHelp.html#Pluscode) Translate latitude & longitude to/from Pluscodes
    
    [Regex](src/GeodesiX/help/ExcelHelp.html#Regex) Extracts strings using regular expressions
    
    [TimeOffset](src/GeodesiX/help/ExcelHelp.html#TimeOffset) Gets the time offset from UTC from a latitude/longitude
    
    [TimeZone](src/GeodesiX/help/ExcelHelp.html#TimeZone) Gets the time zone from UTC from a latitude/longitude
    
    [Travel](src/GeodesiX/help/ExcelHelp.html#Travel) Estimates distance or duration to travel from one place to another; walking, cycling, driving or transit

Getting started
===============

Geodesix uses Google Maps to get geographical information. This requires an API key. 
Obtaining a key from Google is free and it takes a couple of minutes.
[Instructions](apikey.md).

Download and install the latest version of GeodesixInstaller.msi from the
[Releases page](https://github.com/MauriceCalvert/Geodesix/releases)

At the end of the installation Excel will start and you will be prompted for the API key.
    
License
========
    
Open Source, [GPLv3](https://www.gnu.org/licenses/gpl-3.0.en.html)

![](src/GeodesiX/help/swissmadesoftware-logo.png)