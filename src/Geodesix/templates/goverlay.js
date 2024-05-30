/*  goverlay.js */
class goverlay extends google.maps.OverlayView {

    id;
    type;
    latlng;
    icon;
    title;
    size;
    color;
    align;
    angle;
    symbols;

    div;
    span;
    fixed;

    constructor(id, type, latlng, icon, title, size, color, align, angle, symbols) {
        super();
        this.id = id;
        this.type = type;
        this.latlng = latlng;
        this.icon = icon;
        this.title = title;
        this.size = size;
        this.color = color;
        this.align = align;
        this.angle = angle;
        if (symbols) 
            this.symbols = symbols
        else
            this.symbols = 'outlined';
        this.fixed = false;

        this.div = document.createElement('div');
        this.div.className = 'go'+this.type; 

        switch (this.type) {

            case 'arrow': // Arrows
                this.fixed = true;
                this.span = document.createElement('span');
                this.div.appendChild(this.span);
                this.span.className = 'material-symbols-outlined';
                this.span.style.fontSize = this.size + 'px';
                this.span.innerHTML = this.icon;
                if (this.angle != 0)
                    // 1px is a hack to get rotatated material symbols to align correctly
                    this.span.style.transform = 'rotate('+this.angle+'deg) translate(1px)'
                break;

            case 'box': // text in a box
                this.div.innerHTML = this.icon;
                break;

            case 'circle': // material icon in a circle
                this.fixed = true;
                this.div.style.fontSize = this.size + 'px';
                this.div.innerHTML = this.icon;
                this.div.classList.add("material-symbols-outlined");
                const padding = 5;
                this.div.style.padding = padding + 'px';
                // increase size from icon size to circle size
                this.size = this.size * 2 + padding;
                break;

            case 'distance': // CSS .goverlay .distance will look after formatting
                this.div.innerHTML = this.icon;
                break;

            case 'free': // Replace div with a Span
                this.div.innerHTML = this.icon;
                break;

            case 'marker':
                const marker = dropMarker(this.latlng.lat(), this.latlng.lng(), icon, title, size)
                this.div.appendChild(marker);
                break;

        };
        if (this.color)
            this.div.style.color = this.color;

        //console.log('create '+this.type+' @ '+this.latlng.lat+','+this.latlng.lng+
        //    ' cw='+this.div.clientWidth+' ch='+this.div.clientHeight+
        //    ' w=2'+this.div.style.width+' h='+this.div.style.height);


    }

    // onAdd is called when the map's panes are ready and the overlay has been added to the map.
    onAdd() {

        // Add the element to the "overlayLayer" pane.
        const panes = this.getPanes();

        panes.overlayMouseTarget.appendChild(this.div);

        // Hack for mousemove
        // https://stackoverflow.com/a/3402480/338101
        let me = this;
        google.maps.event.addDomListener(me.div, 'mouseover', function() {
            google.maps.event.trigger(me, 'mouseover');
        });
    }
    draw() {
        const overlayProjection = this.getProjection();
        const center = overlayProjection.fromLatLngToDivPixel(this.latlng);

        if (this.type == 'free') {
            return;
        }

        let height;
        let width;
        if (this.fixed) {
            height = this.size;
            width = this.size;
        } else {
            height = this.div.clientHeight;
            width = this.div.clientWidth;
        }
        let height2 = height / 2;
        let width2 = width / 2;

        let left = center.x - width2;
        let top = center.y - height2;

        //console.log('draw '+this.type+' @ '+this.latlng.lat+','+this.latlng.lng+
        //            ' l='+left+' top='+top+' w='+width+' h='+height);

        switch(this.align.toLowerCase().substring(0, 1)) {
            case 'b':
                top = top + height2 + 2;
                break;
            case 't':
                top = top - height2 - 2;
                break
            case 'l':
                left = left - width2 - 2;
                break
            case 'r':
                left = left + width2 + 2;
                break
        };

        this.div.style.left = left + "px";
        this.div.style.top = top + "px";
    }
    /**
     * The onRemove() method will be called automatically from the API if
     * we ever set the overlay's map property to 'null'.
     */
    onRemove() {
        if (this.div) {
            this.div.parentNode.removeChild(this.div);
            delete this.div;
        }
    }
    /**
     *  Set the visibility to 'hidden' or 'visible'.
     */
    hide() {
        if (this.div) {
            this.div.style.visibility = "hidden";
        }
    }
    show() {
        if (this.div) {
            this.div.style.visibility = "visible";
        }
    }
    toggle() {
        if (this.div) {
            if (this.div.style.visibility === "hidden") {
                this.show();
            } else {
                this.hide();
            }
        }
    }
    toggleDOM(map) {
        if (this.getMap()) {
            this.setMap(null);
        } else {
            this.setMap(map);
        }
    }
}

function drawGoverlay(id, type, latlng, icon, title, size, color, align, angle, symbols) {
    let mi = new goverlay(id, type, latlng, icon, title, size, color, align, angle, symbols);
    mi.setMap(map);
    addOverlay(id, mi, title, latlng);
    return mi
};
/*  end goverlay.js */