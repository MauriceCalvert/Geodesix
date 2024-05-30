// features.js
addInitialiser(initialiseFeatures);

let featureinfo;
let kmlLayers = [];
let features = [];
let complained = false;

function adjustBounds() {
    // https://stackoverflow.com/a/20753127/338101
    let newbounds = new google.maps.LatLngBounds();
    for (let i = 0; i < kmlLayers.length; i++) {
        if (kmlLayers[i].getMap() != null) {
            let kmllayer = kmlLayers[i];
            let status = kmllayer.getStatus();
            if (typeof status != 'undefined' && status != 'OK') {
                if (!complained) {
                    complained = true;
                    alert('KML ' + kmllayer['feature'].getProperty('kml') + ' ' + status + ' (cross-domain KML is no longer allowed!)');
                    return;
                }
            }
            let add = kmllayer.getDefaultViewport();
            if (typeof add != "undefined")
                if (typeof newbounds == "undefined")
                    newbounds = add;
                else
                    newbounds.union(add);
        }
    };
    for (let i = 0; i < features.length; i++) {
        let feature = features[i];
        let bounds = feature.Bounds;
        if (bounds) {
            if (typeof newbounds == "undefined")
                newbounds = bounds;
            else
                newbounds.union(bounds);
        } 
    }
    if (!newbounds.isEmpty())
        map.fitBounds(newbounds);
}; //adjustBounds

function dataMouseOver(event) {
    let feature = event.feature;
    let html = feature.getProperty('html');
    if (html) {
        featureinfo.setContent('<div>' + html + '</div>')
    } else {
        return
    };
    featureinfo.setPosition(event.latLng);
    featureinfo.setOptions({ pixelOffset: new google.maps.Size(0, -feature.getProperty('iconSize')) });
    featureinfo.open(map);
}; // dataMouseOver

function getProperty(feature, name, missing) {
    let value = feature.getProperty(name);
    if (!value) value = missing;
    return value
}
function applyStyle(feature) {

    if (feature == undefined) {
        return
    };
    features.push(feature);
    let kml = getProperty(feature,'kml',null);
    if (kml) {
        let opts = {
            url: kml,
            map: map,
            clickable: false,
            preserveViewport: true,
            suppressInfoWindows: true
        };
        let kmlLayer = new google.maps.KmlLayer(opts);
        feature['kmlLayer'] = kmlLayer;
        kmlLayer['feature'] = feature;
        kmlLayers.push(kmlLayer);
    };

    let style = { };
    //feature.forEachProperty(function (value, property) {
    //    style[property] = value
    //})

    let bounds = new google.maps.LatLngBounds();
    let geometry = feature.getGeometry();
    let gt = geometry.getType().toLowerCase();
    let unique = 0;
    switch (gt) {

        case 'point':

            let coordinate = geometry.get('coordinates');
            let latlng = new google.maps.LatLng(coordinate.lat(), coordinate.lng());
            bounds.extend(latlng);
            
            const icon = getProperty(feature, 'icon', 'circle');
            const size = getProperty(feature, 'size', 24);
            let marker = {};

            if (icon.substring(0,4).toLowerCase() == 'http') {

                marker = new google.maps.Marker({
                    animation: google.maps.Animation.DROP,
                    label: {
                        color: getProperty(feature, 'color', 'black'),
                        text: getProperty(feature, 'label', ' ')
                    },
                    title: getProperty(feature, 'title', ' '),
                    icon: {
                        url: icon,
                        anchor: new google.maps.Point(size / 2, size / 2),
                        scaledSize: new google.maps.Size(size, size),
                        labelOrigin: new google.maps.Point(0, 0)
                    }
                })
            } else {
                // function drawGoverlay(id, latlng, icon, title, size, color, align, angle, symbols) {
                unique += 1;
                marker = drawGoverlay(unique, 
                            latlng, 
                            icon, 
                            getProperty(feature, 'title', ''),
                            size,
                            getProperty(feature, 'color', 'Black'),
                            getProperty(feature, 'align', 'c'),
                            0,
                            'outlined');
            }
            style = {marker};
            break;

        case 'linestring':

            let ga = geometry.getArray();
            ga.forEach(
                function (coordinate) {
                    bounds.extend(new google.maps.LatLng(coordinate.lat(), coordinate.lng()));
                }
            );  
            let arrow = getProperty(feature, 'arrow', null)
            if (arrow) {
                let a = ga[0];
                let orig = new google.maps.LatLng(a.lat(), a.lng())
                let b = ga[ga.length - 1];
                let dest = new google.maps.LatLng(b.lat(), b.lng())
                let heading = google.maps.geometry.spherical.computeHeading(orig, dest);
                let s = map.data.getStyle();
                let directionMarker = new google.maps.Marker({
                    position: bounds.getCenter(),
                    map: map,
                    icon: {
                        path: arrow,
                        scale: getProperty(feature, 'arrowScale', 3),
                        strokeColor: getProperty(feature, 'arrowStrokeColor', '#000000'),
                        strokeWeight: getProperty(feature, 'arrowStrokeWeight', 1),
                        fillColor: getProperty(feature, 'arrowFillColor', '#000000'),
                        fillOpacity: getProperty(feature, 'arrowFillOpacity', 1),
                        rotation: heading
                    }
                });
                style = {directionMarker};
            };
            let distance = feature.getProperty('distance');
            if (distance) {
                let distanceMarker = new google.maps.Marker({
                    position: bounds.getCenter(),
                    map: map,
                    icon: ' ',
                    label: {
                        text: distance
                    }
                });
                style = {...style, ...distanceMarker};
            }

            break;

        case 'polygon':
            geometry.forEachLatLng(
                function (latlng) {
                    bounds.extend(latlng)
                }
            );
            break;
        
       default:
            alert("I don't know how to get the bounds of a " + gt);
    };
    feature.Bounds = bounds;
    return style
}; // setStyle
function initialiseFeatures() {

    google.maps.event.addListenerOnce(map, 'tilesloaded', function () {
        adjustBounds();
    });

    let geojson =<#features#>;
    if (geojson)
        map.data.addGeoJson(geojson);

    map.data.setStyle(applyStyle);
    map.data.addListener('mouseover', dataMouseOver);
    featureinfo = new google.maps.InfoWindow({});
    google.maps.event.addListener(map, 'click', function () {
        featureinfo.close();
    });
}; // end initialiseFeatures
// end features.js