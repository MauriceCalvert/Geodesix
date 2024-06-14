# Geodesix README 
 
Geodesix is an add-in to display Google Maps in Excel:

<a href="https://mauricecalvert.github.io/Geodesix/src/Geodesix/help/paristour.jpg" target="_blank">
	<img style='width:800px' src="https://mauricecalvert.github.io/Geodesix/src/Geodesix/help/paristour.jpg" title="Click to enlarge" />
</a>

Applications
============

*   Exports from fitness applications [sample](https://mauricecalvert.github.io/Geodesix/src/Geodesix/help/workout.jpg)
*   Forensics, to plot movements of mobile phones with cellular triangulation from CDR records
*   Google Takeout History [sample](https://mauricecalvert.github.io/Geodesix/src/Geodesix/help/takeout.jpg)
*   Route/trip planning and analysis [sample](https://mauricecalvert.github.io/Geodesix/src/Geodesix/help/japan.jpg)
*   Surveying [sample](https://mauricecalvert.github.io/Geodesix/src/Geodesix/help/centralpark.jpg)
*   Topical maps of offices, natural phenomena, ... [sample](https://mauricecalvert.github.io/Geodesix/src/Geodesix/help/chopes.html)

Features
========

*   Draws maps automatically for any worksheet with Latitude and Longitude columns.
*   Icons, markers, lines, distances, popups and legends can be individually formatted.
*   Maps can be exported as self-contained HTML files suitable for sharing [sample](https://mauricecalvert.github.io/Geodesix/src/Geodesix/help/paristour.htm).
*   Imports structured data in JSON or XML formats with a [generic parser](https://mauricecalvert.github.io/Geodesix/src/Geodesix/help/templateeditor.jpg) for any data structure. Ready-built parsers for GeoJSON, GPX, TCX & Google Takeout.
*   Exports to HTML, GeoJSON, KML (Google Earth) and tabbed-without-quotes
*   Travelling Salesman Solver: Given a list of places, what is the shortest possible route that visits each place exactly once?
*   Example workbooks, demonstrating all the functionalities. Comprehensive [Help](https://mauricecalvert.github.io/Geodesix/src/Geodesix/help/index.html)
*   Complete VBA interface.
*   Extensive library of Excel functions for geodesics:
    
    [Area](https://mauricecalvert.github.io/Geodesix/src/Geodesix/help/ExcelHelp.html#Area) Calculate the area of a polygon
    
    [Azimuth](https://mauricecalvert.github.io/Geodesix/src/Geodesix/help/ExcelHelp.html#Azimuth) Calculate the initial and final bearing from one point to another
    
    [Color](https://mauricecalvert.github.io/Geodesix/src/Geodesix/help/ExcelHelp.html#Color) Formats a hexadecimal Color from the ARGB components
    
    [Displace](https://mauricecalvert.github.io/Geodesix/src/Geodesix/help/ExcelHelp.html#Displace) Gets a point at a bearing and distance from another point
    
    [Distance](https://mauricecalvert.github.io/Geodesix/src/Geodesix/help/ExcelHelp.html#Distance) Calculate the distance in metres between 2 latitude/longitude pairs, as the crow flies
    
    [DMS](https://mauricecalvert.github.io/Geodesix/src/Geodesix/help/ExcelHelp.html#DMS) Convert Degrees-Minutes-Seconds to decimal degrees
    
    [Draw](https://mauricecalvert.github.io/Geodesix/src/Geodesix/help/ExcelHelp.html#Draw) Draw circles/lines/markers on the map
    
    [Formulae](https://mauricecalvert.github.io/Geodesix/src/Geodesix/help/ExcelHelp.html#Formulae) Get the formula of a cell, showing the values
    
    [Geocode](https://mauricecalvert.github.io/Geodesix/src/Geodesix/help/ExcelHelp.html#Geocode) Get Geodesic information about a place
    
    [Geodesix](https://mauricecalvert.github.io/Geodesix/src/Geodesix/help/ExcelHelp.html#Geodesix) User interface and VBA drawing functions
    
    [GeoReverse](https://mauricecalvert.github.io/Geodesix/src/Geodesix/help/ExcelHelp.html#GeoReverse) Performs a reverse geocode
    
    [Heat](https://mauricecalvert.github.io/Geodesix/src/Geodesix/help/ExcelHelp.html#Heat) Gets the 'temperature' of a value in the range 'min' to 'max'. The Color ranges from purple (cold) to red (hot)
    
    [JavaScript](https://mauricecalvert.github.io/Geodesix/src/Geodesix/help/ExcelHelp.html#JavaScript) Execute a JavaScript function in the map pane
    
    [Pluscode](https://mauricecalvert.github.io/Geodesix/src/Geodesix/help/ExcelHelp.html#Pluscode) Translate latitude & longitude to/from Pluscodes
    
    [Regex](https://mauricecalvert.github.io/Geodesix/src/Geodesix/help/ExcelHelp.html#Regex) Extracts strings using regular expressions
    
    [TimeOffset](https://mauricecalvert.github.io/Geodesix/src/Geodesix/help/ExcelHelp.html#TimeOffset) Gets the time offset from UTC from a latitude/longitude
    
    [TimeZone](https://mauricecalvert.github.io/Geodesix/src/Geodesix/help/ExcelHelp.html#TimeZone) Gets the time zone from UTC from a latitude/longitude
    
    [Travel](https://mauricecalvert.github.io/Geodesix/src/Geodesix/help/ExcelHelp.html#Travel) Estimates distance or duration to travel from one place to another; walking, cycling, driving or transit

Getting started
===============

Geodesix uses Google Maps to get geographical information. This requires an API key. 
Obtaining a key from Google is free and it takes a couple of minutes.
[Instructions](https://mauricecalvert.github.io/Geodesix/apikey.md).

Download and install the latest version of GeodesixInstaller.msi from the
[Releases page](https://github.com/MauriceCalvert/Geodesix/releases)

At the end of the installation Excel will start and you will be prompted for the API key.
    
License
========
    
Open Source, [GPLv3](https://www.gnu.org/licenses/gpl-3.0.en.html)

![](https://mauricecalvert.github.io/Geodesix/src/Geodesix/help/swissmadesoftware-logo.png)