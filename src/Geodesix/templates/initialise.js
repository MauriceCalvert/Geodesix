// initialise.js
'use strict';
let map;
let minimalStyle;
<#!minimalstyle.js#>
let mapOptions;
let initialisers = [];
<#settings#>

// dynamic library import

  (g=>{var h,a,k,p="The Google Maps JavaScript API",c="google",l="importLibrary",q="__ib__",m=document,b=window;b=b[c]||(b[c]={});var d=b.maps||(b.maps={}),r=new Set,e=new URLSearchParams,u=()=>h||(h=new Promise(async(f,n)=>{await (a=m.createElement("script"));e.set("libraries",[...r]+"");for(k in g)e.set(k.replace(/[A-Z]/g,t=>"_"+t[0].toLowerCase()),g[k]);e.set("callback",c+".maps."+q);a.src=`https://maps.${c}apis.com/maps/api/js?`+e;d[q]=f;a.onerror=()=>h=n(Error(p+" could not load."));a.nonce=m.querySelector("script[nonce]")?.nonce||"";m.head.append(a)}));d[l]?console.warn(p+" only loads once. Ignoring:",g):d[l]=(f,...n)=>r.add(f)&&u().then(()=>d[l](f,...n))})({
    key: apikey,
    v: "weekly",
    language: language,
    region: region,
    libraries: 'geometry'
  });

function addInitialiser(f) {
    initialisers = initialisers.concat(f);
};

async function initMap() {
    // Are we connected to the Internet ?
    if (typeof google === "undefined") { 
        let text = '<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">' +
            '<html><head><title>No Internet connection</title></head>' +
            '<body>Looks like you are not connected to the Internet!</body></html>';
        let encodedText = encodeURIComponent(text);
        document.location.href = "javascript:(function(){document.open();document.write('" + encodedText + "');document.close();})();";
        return;
    };

    const { Map } = await google.maps.importLibrary("maps");
    map = new google.maps.Map(document.getElementById('map'), {});


    let minimalMapOptions = { name: "minimal" };
    let minimalMapType = new google.maps.StyledMapType(minimalStyle, minimalMapOptions);
    map.mapTypes.set('minimal', minimalMapType);

    mapOptions = {

        zoom: startzoom,
        center: (new google.maps.LatLng(startlat, startlong)),

        draggableCursor: 'crosshair',
        draggingCursor: 'move',

        disableDefaultUI: true,

        panControl: false,
        streetViewControl: false,
        zoomControl: false,
        scaleControl: false,
        navigationControl: false,
        mapTypeControl: false,

        scaleControlOptions: {
            position: google.maps.ControlPosition.BOTTOM_LEFT
        },

        navigationControlOptions: {
            style: google.maps.NavigationControlStyle.ZOOM_PAN,
            position: google.maps.ControlPosition.TOP_LEFT
        },

        mapTypeId: mapstyle,
        mapTypeControlOptions: {
            mapTypeIds: [
                google.maps.MapTypeId.hybrid,
                google.maps.MapTypeId.roadmap,
                google.maps.MapTypeId.satellite,
                google.maps.MapTypeId.terrain,
                'minimal'
            ]
        }
    };
    initialisers.forEach(function (q) {
        q()
    });
    map.setOptions(mapOptions);
}; // initMap
// end initialise.js
