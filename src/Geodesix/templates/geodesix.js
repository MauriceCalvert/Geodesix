// geodesix.js
'use strict';
addInitialiser(initialiseGeodesix);

let myInfoWindow;
let totaldistance;
let clickMarker = null;
let query = "";
let geocoder = null;
let matchlines = null;
let shadow = null;
let clickIcon = null;
let matchmarkers = null;
let midpoints = null;
let infowindow = null;
let overlays = []; // things he's drawn on this page
let MAPFILES_URL = "https://maps.gstatic.com/intl/en_us/mapfiles/";
let ICONFILES_URL = "https://maps.google.com/mapfiles/ms/icons/";

function addOverlay(id, obj, title, latlng) {

    overlays[id] = obj;
    
    if (title) {
    
        obj.set('title', title)

        if (obj.constructor.name == 'goverlay') {
            const div = obj.div;
            google.maps.event.addListener(obj, 'mouseover',
                function (obj) {
                    let content = '<div class="info">' + this.title + '</div>';
                    let iwo = {
                        content: content,
                        position: this.latlng
                    };
                    myInfoWindow.setOptions(iwo);
                    myInfoWindow.open(map);
                }
            );
        } else {
            google.maps.event.addListener(obj, 'mouseover',
                function (obj) {
                    let content = '<div class="info">' + this.title + '</div>';
                    let iwo = {
                        content: content,
                        position: this.latlng
                    };
                    myInfoWindow.setOptions(iwo);
                    myInfoWindow.open(map);
                }
            );
        }
    };
    obj.set('latlng', latlng);
    zoomToContent();
}
function bearing(a, b) {
    // Given 2 LatLng points, compute bearing in degress
    let lat1 = a.lat() * Math.PI / 180;
    let lng1 = a.lng() * Math.PI / 180;
    let lat2 = b.lat() * Math.PI / 180;
    let lng2 = b.lng() * Math.PI / 180;
    let dLon = lng2 - lng1;
    let y = Math.sin(dLon) * Math.cos(lat2);
    let x = Math.cos(lat1) * Math.sin(lat2) -
        Math.sin(lat1) * Math.cos(lat2) * Math.cos(dLon);
    let brng = Math.atan2(y, x) * 180 / Math.PI;
    if (brng < 0) {
        brng = brng + 360;
    }
    return brng;
}
function changeZoom(amount) {

    let myzoom = map.getZoom();
    myzoom = parseInt(myzoom) + parseInt(amount);
    setZoom(myzoom);
}
function clickedRightOnMap(event) {
    let latlonvalue = event.latLng.toUrlValue(7); // 7 = 1.11 cm precision
    sendMessage("rightclick", latlonvalue.toString());
}
function closeInfoWindow() {
    myInfoWindow.close();
};

// https://stackoverflow.com/a/2637079/338101
Number.prototype.toRad = function () {
    return this * Math.PI / 180;
}

Number.prototype.toDeg = function () {
    return this * 180 / Math.PI;
}

function destinationPoint(center, bearing, distance) {
    // 0=Northwards, 90=Eastwards
    distance = distance / 6371000;
    bearing = bearing.toRad();

    let lat1 = center.lat().toRad(), lon1 = center.lng().toRad();

    let lat2 = Math.asin(Math.sin(lat1) * Math.cos(distance) +
        Math.cos(lat1) * Math.sin(distance) * Math.cos(bearing));

    let lon2 = lon1 + Math.atan2(Math.sin(bearing) * Math.sin(distance) *
        Math.cos(lat1),
        Math.cos(distance) - Math.sin(lat1) *
        Math.sin(lat2));

    if (isNaN(lat2) || isNaN(lon2)) return null;

    return new google.maps.LatLng(lat2.toDeg(), lon2.toDeg());
}
function drawArrow(id, orig, dest, arrow, scale, strokeColor, strokeWeight, fillColor, fillOpacity) {

    const heading = google.maps.geometry.spherical.computeHeading(orig, dest);
    const centre = latlngCentre(orig.lat(), orig.lng(), dest.lat(), dest.lng());
    const directionMarker = new google.maps.Marker({
        position: centre,
        map: map,
        icon: {
            url: arrow,
            scale: scale,
            strokeColor: strokeColor,
            strokeWeight: strokeWeight,
            fillColor: fillColor,
            fillOpacity: fillOpacity,
            rotation: heading
        }
    });
    directionMarker.set('ne', orig);
    directionMarker.set('sw', dest);
    addOverlay(id, directionMarker, '', pos);
}
function drawCircle(id, lat, lng, radius, content, strokeColor, strokeOpacity, strokeWeight, fillColor, fillOpacity) {
    let center = new google.maps.LatLng(lat, lng);
    let circle = new google.maps.Circle({
        center: center,
        map: map,
        radius: radius,
        strokeColor: strokeColor,
        strokeOpacity: strokeOpacity,
        strokeWeight: strokeWeight,
        fillColor: fillColor,
        fillOpacity: fillOpacity
    });
    let ne = destinationPoint(center, -45, radius * 1.414);
    circle.set('ne', ne)
    let sw = destinationPoint(center, 135, radius * 1.414);
    circle.set('sw', sw)
    addOverlay(id, circle, content, center);

}
//function drawDirections(id, jsonpoints, mode, strokeColor, strokeOpacity, strokeWeight, suppressInfoWindows, suppressMarkers) {

//    let points = eval(jsonpoints);
//    let origin = new google.maps.LatLng(points[0], points[1]);
//    let destination = new google.maps.LatLng(points[points.length - 2], points[points.length - 1]);
//    let waypoints = [];
//    let path = [];
//    let j = 0;

//    for (let i = 2; i < points.length - 1; i += 2) {
//        let wp = new Object(); // a google.maps.DirectionsWaypoint()
//        let point = new google.maps.LatLng(points[i], points[i + 1]);
//        wp.location = point;
//        expandBounds(point);
//        wp.stopover = true;
//        waypoints[j] = wp;
//        j++;
//    }
//    let polylineOptions = {
//        strokeColor: strokeColor,
//        strokeOpacity: strokeOpacity,
//        strokeWeight: strokeWeight
//    };
//    let renderOptions = {
//        suppressInfoWindows: suppressInfoWindows,
//        suppressMarkers: suppressMarkers,
//        polylineOptions: polylineOptions
//    };
//    let dr = new google.maps.DirectionsRenderer(renderOptions);
//    dr.origin = origin;
//    dr.destination = destination;
//    dr.waypoints = waypoints;
//    dr.travelMode = google.maps.DirectionsTravelMode[mode];
//    let ds = new google.maps.DirectionsService();
//    let request = {
//        origin: origin,
//        destination: destination,
//        waypoints: waypoints,
//        travelMode: google.maps.DirectionsTravelMode[mode.toUpperCase()]
//    };

//    totaldistance = -1;

//    ds.route(request, function (response, status) {
//        if (status === google.maps.DirectionsStatus.OK) {
//            dr.setDirections(response);
//            dr.setMap(map);
//            // get distance
//            let directions = dr.getDirections();
//            let routes = directions.routes[0];
//            let legs = routes.legs;
//            let distance = 0;
//            for (i in legs) {
//                let legdistance = legs[i].distance;
//                if (typeof legdistance !== "undefined")
//                    if (typeof legdistance.value !== "undefined")
//                        distance = distance + legdistance.value;
//            }
//            totaldistance = distance;
//        }
//        else {
//            totaldistance = 0;
//            alert(status);
//        }
//    });
//    addOverlay(id, dr, null);
//}
//function drawInfoWindow(id, lat, lng, content) {

//    let poly = new google.maps.InfoWindow({
//        content: content,
//        map: map,
//        position: new google.maps.LatLng(lat, lng)
//    });
//    addOverlay(id, poly, null);
//}
//function drawLegend(id, html, quadrant) {

//    let name = 'geodesix' + id.toString();
//    let div = document.createElement('div');
//    div.style.backgroundColor = 'transparent';
//    div.setAttribute('id', name);
//    div.style.padding = '0px';
//    div.style.fontFamily = 'Arial,sans-serif';
//    div.innerHTML = html;
//    div.index = 1;
//    map.controls[google.maps.ControlPosition[quadrant.toUpperCase()]].push(div);
//}
function drawLine(id, lat1, lon1, lat2, lon2, title, strokeColor, strokeOpacity, strokeWeight) {
    let jsonpoints = [lat1, lon1, lat2, lon2];
    drawPolyLine(id, jsonpoints, title, strokeColor, strokeOpacity, strokeWeight)
}
function drawMarker(id, lat, lng, icon, title, size) {

    let marker = dropMarker(lat, lng, icon, title, size);
    addOverlay(id, marker, title, marker.position);
}
function drawPolygon(id, jsonpoints, title, strokeColor, strokeOpacity, strokeWeight, fillColor, fillOpacity) {

    let points = eval(jsonpoints);
    let paths = new google.maps.MVCArray;
    let bounds = new google.maps.LatLngBounds();
    for (let i = 0; i < points.length; i += 2) {
        let point = new google.maps.LatLng(points[i], points[i + 1]);
        bounds.extend(point);
        paths.push(point);
    }

    let poly = new google.maps.Polygon({
        path: paths,
        map: map,
        geodesic: true,
        strokeColor: strokeColor,
        strokeOpacity: strokeOpacity,
        strokeWeight: strokeWeight,
        fillColor: fillColor,
        fillOpacity: fillOpacity
    });
    poly.set('ne', bounds.getNorthEast());
    poly.set('sw', bounds.getSouthWest());
    addOverlay(id, poly, title, bounds.getCenter());
}
function drawPolyLine(id, jsonpoints, title, strokeColor, strokeOpacity, strokeWeight) {

    let points = eval(jsonpoints);
    let path = new google.maps.MVCArray;
    let bounds = new google.maps.LatLngBounds();
    let j = 0;
    for (let i = 0; i < points.length; i += 2) {
        let point = new google.maps.LatLng(points[i], points[i + 1]);
        bounds.extend(point);
        path.push(point);
    };
    let poly = new google.maps.Polyline({
        path: path,
        map: map,
        strokeColor: strokeColor,
        strokeOpacity: strokeOpacity,
        strokeWeight: strokeWeight
    });
    poly.set('ne', bounds.getNorthEast());
    poly.set('sw', bounds.getSouthWest());
    addOverlay(id, poly, title, bounds.getCenter());
}
function dropMarker(lat, lng, icon, title, size) {

    let theicon = icon;
    if (icon.substring(0, 1) === "{")
        theicon = eval('theicon = ' + theicon);
    let marker = new google.maps.Marker({
        icon: {
            url: theicon,
            scaledSize: new google.maps.Size(size, size)
        },
        map: map,
        position: new google.maps.LatLng(lat, lng),
        title: title,
        visible: true
    });
    return marker;
}
function expandBounds(latlng) {
    if (latlng) {
        let bounds = map.getBounds();
        if (bounds) {
            if (bounds.contains(latlng)) {
                return;
            } else {
                bounds.extend(latlng);
                map.fitBounds(bounds);
            }
        }
    }
}
function geocode(request) {
    if (request.latlng) {
        clickMarker = new google.maps.Marker({
            'position': request.latlng,
            'map': map,
            'title': request.latlng.toString(),
            'clickable': false,
            'icon': new google.maps.MarkerImage(
                ICONFILES_URL + 'red-pushpin.png'
            )
        });
        map.panTo(latlng);
        setZoom(16);
    } else {
        geocoder.geocode(request, showResults);
    }
}
function getMarkerImageUrl(resultNum) {

    // Get a numbered maker from Google, A, B, C, etc.
    // https://maps.gstatic.com/intl/en_us/mapfiles/markerA.png etc
    return MAPFILES_URL + "marker" + String.fromCharCode(65 + resultNum) + ".png";
}
function getResultDescription(result) {
    let bounds = result.geometry.bounds;
    let html = '<table class="tabContent">';
    html += trs(unBracket(result.formatted_address));
    html += '</table>';
    return html;
}
function getResultsListItem(resultNum, resultDescription) {

    // Make a pretty, clickable HTML description of a result
    let html = '<a onclick="gotoMarker(' + resultNum + ')">';
    html += '<div class="info" id="p' + resultNum + '">';
    html += '<table><tr valign="top">';
    html += '<td style="padding: 2px"><img src="' + getMarkerImageUrl(resultNum) + '"/></td>';
    html += '<td style="padding: 2px">' + resultDescription + '</td>';
    html += '</tr></table>';
    html += '</div></a>';
    return html;
}
function gotoMarker(resultNum) {

    // When he clicks on a result marker, take him to the place

    let position = matchmarkers[resultNum].position;
    let title = matchmarkers[resultNum].title;
    let bounds = matchmarkers[resultNum].bounds;
    resetMap();
    clickMarker = new google.maps.Marker({
        'position': position,
        'map': map,
        'title': title,
        'clickable': false,
        'icon': clickIcon,
        'shadow': shadow
    });
    map.panTo(position);
}
function haversine(pos1, pos2) {
    // Compute the Haversine distance between two places.
    // I know that Vincenty's algorithm is more accurate, but unnecessary here and a fuck-sight faster
    let R = 6371; // km
    let lat1r = toRadians(pos1.lat());
    let lon1r = toRadians(pos1.lng());
    let lat2r = toRadians(pos2.lat());
    let lon2r = toRadians(pos2.lng());
    let d = Math.acos(Math.sin(lat1r) * Math.sin(lat2r) + Math.cos(lat1r) * Math.cos(lat2r) * Math.cos(lon2r - lon1r)) * R;
    return d;
}
function initialiseGeodesix() {

    parseUrlParams();

    geocoder = new google.maps.Geocoder();

    myInfoWindow = new google.maps.InfoWindow({});
    google.maps.event.addListener(map, 'rightclick', clickedRightOnMap);
    google.maps.event.addListener(map, 'zoom_changed', zoomChanged);

    if (query) {
        submitQuery(query);
    };
}
function latlngCentre(lat1, lng1, lat2, lng2) {
    // LatLngBounds has getCenter but it's sensitive to NE/SW
    return new google.maps.LatLng((lat1 + lat2) / 2, (lng1 + lng2) / 2);
}
function latlngToScreen(latlng) {

    let numTiles = 1 << map.getZoom();
    let projection = map.getProjection();
    let worldCoordinate = projection.fromLatLngToPoint(latlng);
    let pixelCoordinate = new google.maps.Point(
        worldCoordinate.x * numTiles,
        worldCoordinate.y * numTiles);

    let topLeft = new google.maps.LatLng(
        map.getBounds().getNorthEast().lat(),
        map.getBounds().getSouthWest().lng()
    );

    let topLeftWorldCoordinate = projection.fromLatLngToPoint(topLeft);
    let topLeftPixelCoordinate = new google.maps.Point(
        topLeftWorldCoordinate.x * numTiles,
        topLeftWorldCoordinate.y * numTiles);

    return {
        x: pixelCoordinate.x - topLeftPixelCoordinate.x,
        y: pixelCoordinate.y - topLeftPixelCoordinate.y
    }
}
function loadScript(file) {
    let script = document.createElement("script");
    script.type = "title/javascript";
    script.src = file;
    document.body.appendChild(script);
};
function overlay(id, onoff) {

    let result = "";
    let obj = overlays[id];

    if (typeof obj === "undefined") {
        alert('Layer object ' + id.toString + ' is not defined');
        return;
    }

    switch (onoff.toLowerCase()) {

        case 'true':
            obj.setMap(map);
            result = 'true';
            break;

        case 'false':
            obj.setMap(null);
            result = 'false';
            break;

        default:
            if (obj.getMap() === null)
                result = 'false';
            else
                result = 'true';
            break;
    }
    return result;
}
function parseUrlParams() {

    if (window.location.search) {
        let args = decodeURIComponent(window.location.search).substring(1).split('&');
        for (let i in args) {
            let param = args[i].split('=');
            switch (param[0].toLowerCase()) {
                case 'api':
                    api = param[1];
                    break;
                case 'key':
                    apikey = param[1];
                    break;
                case 'q':
                    query = param[1];
                    break;
                case 'center':
                    let center = parseLatLng(param[1]);
                    if (center !== null) {
                        startlat = center.lat();
                        startlon = center.lng();
                    }
                    break;
                case 'zoom':
                    let zoom = parseInt(param[1]);
                    if (!isNaN(zoom)) {
                        startzoom = zoom;
                    }
                    break;
                case 'region':
                    region = param[1];
                    break;
                case 'language':
                    language = param[1];
                    break;
                case 'version':
                    version = param[1];
                    break;
                case 'mapstyle':
                    mapstyle = param[1].toLowerCase();
                    break;
            }
        }
    }
}
function parseLatLng(value) {
    value.replace('/\s//g');
    let coords = value.split(',');
    let lat = parseFloat(coords[0]);
    let lng = parseFloat(coords[1]);
    if (isNaN(lat) || isNaN(lng)) {
        return null;
    } else {
        return new google.maps.LatLng(lat, lng);
    }
}
function plotMatchesOnMap(results) {

    matchmarkers = new Array(results.length);
    let resultsListHtml = "";

    // Create a handler to select one of the results when he clicks it
    let hitselector = function (mm) {
        return function () {
            selectHit(mm);
        };
    };

    if (results.length === 0) {
        alert(NOTHINGFOUND);
    }

    if (results.length > 1) {
        for (let i = 0; i < results.length; i++) {

            // We know as a fact the Google's marker images (like )
            // are 20 wide by 34 high
            let icon = new google.maps.MarkerImage(getMarkerImageUrl(i), // URL
                new google.maps.Size(20, 34), // True size
                new google.maps.Point(0, 0), // origin
                new google.maps.Point(10, 34)); // anchor

            matchmarkers[i] = new google.maps.Marker({
                'position': results[i].geometry.location,
                'map': map,
                'icon': icon,
                'shadow': shadow,
                'title': results[i].formatted_address,
                bounds: results[i].geometry.bounds
            });

            google.maps.event.addListener(matchmarkers[i], 'click', hitselector(i));

            resultsListHtml += getResultsListItem(i, getResultDescription(results[i]));
        }
        // Display the matches in a nice little box where he can select one
        document.getElementById("matches").innerHTML = resultsListHtml;
        document.getElementById("p0").style.border = "none";
        document.getElementById("matches").style.display = "none";
    }

    // Now, according to the number of results, display on the map too
    if (results.length > 0) { // OK, there were some
        if (results.length === 1) { // There was exactly one result
            let location = results[0].geometry.location.toString();
            let latlong = location;
            latlong.replace('/\s//g');
            latlong = unBracket(latlong);
            let coords = latlong.split(',');
            let lat = parseFloat(coords[0]).toString();
            let lng = parseFloat(coords[1]).toString();
            // Make a nicely decorated marker
            clickMarker = new google.maps.Marker({
                'position': results[0].geometry.location,
                'map': map,
                'title': results[0].formatted_address,
                'clickable': false,
                'icon': clickIcon,
                'shadow': shadow
            });
            map.panTo(results[0].geometry.location);
            setZoom(15);
        } else {
            // Many results, show them all in the viewport and let him choose one
            document.getElementById("matches").style.display = "block";
            zoomToViewports(results);
        }
    }
}
function resetMap() {
    myInfoWindow.close();
    if (clickMarker !== null) {
        clickMarker.setMap(null);
        clickMarker = null;
    }
    for (let i in matchmarkers) {
        matchmarkers[i].setMap(null);
    }
    matchmarkers = [];
    for (let j in matchlines) {
        matchlines[j].setMap(null);
    }
    matchlines = [];

    document.getElementById("matches").innerHTML = "";
    document.getElementById("matches").style.display = "none";
}
function sendMessage(command, data) {
    let wv = window.chrome.webview;
    if (wv) {
        let json = { 'command': command, 'data': data };
        window.chrome.webview.postMessage(json);
    }
}
function setCenter(lat, lng) {
    let center = new google.maps.LatLng(lat, lng);
    map.setCenter(center);
}
function setZoom(zoom) {
    map.setZoom(zoom);
    zoomChanged();
}
function selectHit(mm) {
    gotoMarker(mm);
}
function setMapStyle(style) {
    let mapstyle = style;
    map.setMapTypeId(mapstyle);
}
function showResults(results, status) {

    if (status === google.maps.GeocoderStatus.OK) {
        plotMatchesOnMap(results);
    } else {
        alert(String(status));
    }
}
function showRoute(origin, destination, mode) {

    let directionsRenderer;
    let directionsService = new google.maps.DirectionsService();
    // draw lines in red, easier to see
    let polylineOptions = {
        strokeColor: '#FF0000'
    };
    let renderOptions = {
        polylineOptions: polylineOptions
    };
    //directionsDisplay = new google.maps.DirectionsRenderer(renderOptions);
    directionsRenderer = new google.maps.DirectionsRenderer({
        preserveViewport: true,
        suppressMarkers: true
    });
    directionsRenderer.setMap(map);
    let request = {
        origin: { query: origin },
        destination: { query: destination },
        travelMode: mode.toUpperCase()
    };
    directionsService.route(request, function (response, status) {
        if (status == google.maps.DirectionsStatus.OK) {
            directionsRenderer.setDirections(response);
            map.fitBounds(response.routes[0].bounds);
        }
        else
            alert(status);
    });
}
function submitQuery(query) {
    resetMap();
    if (query !== "") {
        let latlng = parseLatLng(query);
        if (latlng === null) {
            geocode({
                'address': query
            });
        } else {
            clickMarker = new google.maps.Marker({
                'position': latlng,
                'map': map,
                'title': "",
                'clickable': false,
                'icon': new google.maps.MarkerImage(
                    ICONFILES_URL + 'red-pushpin.png'
                )
            });
            map.panTo(latlng);
            setZoom(12);
        }
    }
}
function toRadians(a) {
    return a * (Math.PI / 180);
}
function tr(key, value) {
    return '<tr>' + '<td class="key">' + key + (key ? ':' : '') + '</td>' + '<td class="value">' + value + '</td>' + '</tr>';
}
function trs(value) {
    return '<tr>' + '<td class="key"></td>' + '<td class="value">' + value + '</td>' + '</tr>';
}
function unBracket(s) {
    // Remove parenthses from start/end of a string
    let a;
    a = s;
    if (a.substring(0, 1) === '(') a = a.substring(1);
    if (a.substring(a.length - 1, 1) === ')') a = a.substring(0, a.length - 2);
    return a;
}
function zoomChanged() {
    sendMessage("zoomChanged", map.getZoom());
}
function zoomToContent() {

    let fit = 0;
    let bounds = new google.maps.LatLngBounds();
    for (let i = 0; i < overlays.length; i++) {
        let obj = overlays[i];
        if (obj) {
            let ne = obj.get('ne');
            let sw = obj.get('sw');
            if (ne && sw) {
                bounds.extend(ne);
                bounds.extend(sw);
                fit += 2;
            } else {
                let latlng = obj.get('latlng');
                if (latlng) {
                    bounds.extend(latlng);
                    fit += 1;
                };
            }
        }
    };
    if (fit < 2) { // Only one object, don't do sily zoom=22
        let centre = bounds.getCenter();
        map.panTo(centre);
        map.setZoom(16);
    } else {
        map.fitBounds(bounds);
    }
}
function zoomToViewports(results) {

    // For a given set of results, zoom the viewport so that they are all visible
    let bounds = new google.maps.LatLngBounds();

    for (let i = 0; i < results.length; i++) {
        bounds.union(results[i].geometry.viewport);
    }
}
// end geodesix.js
