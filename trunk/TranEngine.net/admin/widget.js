/*-----------------------------------------------------------------------------
                              WIDGET FRAMEWORK
-----------------------------------------------------------------------------*/

TrainEngine.widgetAdmin = {
    zoneNames: [],
    moveContainer: null,
    moveDropdown: null,
    moveContainerCurrentLocationWidgetId: null,
    moveContainerCurrentLocationWidgetZone: null
    ,
    positionMoveDropdown: function() {

        // Make sure the following conditions are met.
        if (!TrainEngine.widgetAdmin.moveContainer ||
            !TrainEngine.widgetAdmin.moveContainerCurrentLocationWidgetId ||
            !TrainEngine.widgetAdmin.moveContainerCurrentLocationWidgetZone)
            return;

        var oWidget = TrainEngine.$("widget" + TrainEngine.widgetAdmin.moveContainerCurrentLocationWidgetId);
        if (oWidget) {
            oWidget.insertBefore(TrainEngine.widgetAdmin.moveContainer, oWidget.firstChild);
            TrainEngine.widgetAdmin.moveContainer.style.display = '';  // unhide if hidden.
        }
    }
    ,
    moveComplete: function(response) {

        if (response) {

            var data = response.split(',');
            if (data.length == 4) {

                var oldZone = data[0];
                var widgetToMove = data[1];
                var newZone = data[2];
                var moveBeforeWidget = data[3];

                var divOldZone = TrainEngine.$("widgetzone_" + oldZone);
                var divWidgetToMove = TrainEngine.$("widget" + widgetToMove);
                var divNewZone = TrainEngine.$("widgetzone_" + newZone);
                var divMoveBeforeWidget = moveBeforeWidget && moveBeforeWidget.length > 0 ? TrainEngine.$("widget" + moveBeforeWidget) : null;

                // note: not actually using divOldZone.
                if (divOldZone && divWidgetToMove && divNewZone) {

                    // Move widget in DOM.
                    if (divMoveBeforeWidget)
                        divNewZone.insertBefore(divWidgetToMove, divMoveBeforeWidget);
                    else
                        divNewZone.appendChild(divWidgetToMove);

                    TrainEngine.widgetAdmin.clearWidgetList();
                }
            }
        }
    }
    ,
    processMoveWidget: function() {

        var selectedValue = TrainEngine.widgetAdmin.moveDropdown.value;
        if (selectedValue && selectedValue.length > 0) {

            // The selected value might either be a ZoneName or a Widget.  If it is a ZoneName,
            // the widget will be placed at the bottom of that Zone.  If it is a Widget, the widget
            // will be moved in front of the selected Widget.

            if (selectedValue != TrainEngine.widgetAdmin.moveContainerCurrentLocationWidgetId) {

                var sZoneNamePrefix = "zone_";
                var sNewZoneName = null;
                var iIndex = TrainEngine.widgetAdmin.moveDropdown.selectedIndex;

                // Find the zone of the selected item by going up the list.  If the selected item
                // is a zone, then sNewZoneName will be the selected zone.
                while (!sNewZoneName && iIndex >= 0) {

                    if (TrainEngine.widgetAdmin.moveDropdown[iIndex].value &&
                        TrainEngine.widgetAdmin.moveDropdown[iIndex].value.indexOf(sZoneNamePrefix) == 0) {

                        sNewZoneName = TrainEngine.widgetAdmin.moveDropdown[iIndex].value.substr(sZoneNamePrefix.length);
                    }
                    iIndex--;
                }

                if (sNewZoneName) {

                    // If a Zone was selected from the dropdown list, then set moveBeforeWidgetId to an empty string.
                    var moveBeforeWidgetId = selectedValue.indexOf(sZoneNamePrefix) == 0 ? "" : selectedValue;

                    // Pass 4 pieces of data back to the server.
                    // (1) Old Zone, (2) Widget Id to move, (3) New Zone, (4) Move Before Widget Id.
                    var sData =
                        TrainEngine.widgetAdmin.moveContainerCurrentLocationWidgetZone + "," +
                        TrainEngine.widgetAdmin.moveContainerCurrentLocationWidgetId + "," +
                        sNewZoneName + "," + moveBeforeWidgetId;

                    TrainEngine.createCallback(TrainEngine.webRoot + "admin/WidgetEditor.aspx?move=" + sData + "&rnd=" + Math.random(), TrainEngine.widgetAdmin.moveComplete);
                }
            }
        }
    }
    ,
    hideMoveToList: function() {
        if (TrainEngine.widgetAdmin.moveContainer) {
            TrainEngine.widgetAdmin.moveContainer.style.display = 'none';  // hide dropdown container.
        }

        TrainEngine.widgetAdmin.moveContainerCurrentLocationWidgetId = null;
        TrainEngine.widgetAdmin.moveContainerCurrentLocationWidgetZone = null;
    }
    ,
    clearWidgetList: function() {
        // After a widget is added, edited, moved, or removed, the moveDropdown list is cleared so
        // it can be reloaded with updated data the next time it is needed.  Also hide dropdown container.

        TrainEngine.widgetAdmin.hideMoveToList();

        if (TrainEngine.widgetAdmin.moveDropdown && TrainEngine.widgetAdmin.moveDropdown.firstChild) {
            while (TrainEngine.widgetAdmin.moveDropdown.firstChild) {
                TrainEngine.widgetAdmin.moveDropdown.removeChild(TrainEngine.widgetAdmin.moveDropdown.firstChild);
            }
        }
    }
    ,
    createDropdownOption: function(text, value) {
        var oOption = document.createElement('option');
        oOption.value = value;
        oOption.appendChild(document.createTextNode(text));
        return oOption;
    }
    ,
    loadMoveDropdownItems: function(response) {

        if (!response || response.length == 0) {
            alert('Failed to retrieve list of zones and widgets.');
            return;
        }

        var oResponse = eval("(" + response + ")");

        if (!oResponse || typeof oResponse.zones == 'undefined') {
            alert('Failed to retrieve list of zones and widgets.');
            return;
        }

        var zoneAndChildWidgets = oResponse.zones;

        if (!TrainEngine.widgetAdmin.moveContainer) {
            TrainEngine.widgetAdmin.moveContainer = document.createElement('div');
            TrainEngine.widgetAdmin.moveContainer.id = 'moveWidgetToContainer';
        }

        if (!TrainEngine.widgetAdmin.moveDropdown) {

            TrainEngine.widgetAdmin.moveDropdown = document.createElement('select');
            TrainEngine.widgetAdmin.moveDropdown.id = 'moveWidgetTo';

            var oMoveBtn = document.createElement('input');
            oMoveBtn.id = 'moveWidgetToBtn';
            oMoveBtn.type = 'button';
            oMoveBtn.value = 'Move';
            oMoveBtn.onclick = TrainEngine.widgetAdmin.processMoveWidget;

            TrainEngine.widgetAdmin.moveContainer.appendChild(TrainEngine.widgetAdmin.moveDropdown);
            TrainEngine.widgetAdmin.moveContainer.appendChild(oMoveBtn);
        }

        TrainEngine.widgetAdmin.moveDropdown.appendChild(TrainEngine.widgetAdmin.createDropdownOption(oResponse.moveWidgetTo, ''));

        for (var z = 0; z < zoneAndChildWidgets.length; z++) {

            var zone = zoneAndChildWidgets[z];
            TrainEngine.widgetAdmin.moveDropdown.appendChild(TrainEngine.widgetAdmin.createDropdownOption("[" + zone.zoneName + "]", 'zone_' + zone.zoneName));

            for (var w = 0; w < zone.widgets.length; w++) {
                TrainEngine.widgetAdmin.moveDropdown.appendChild(TrainEngine.widgetAdmin.createDropdownOption(String.fromCharCode(160) + String.fromCharCode(160) + '-' + String.fromCharCode(160) + zone.widgets[w].desc, zone.widgets[w].id));
            }
        }

        TrainEngine.widgetAdmin.positionMoveDropdown();
    }
    ,
    getWidgetZoneNameFromElement: function(el) {
        // If the element passed in is a widgetzone, the zone name is extracted from the element's id and returned.
        var sIdPrefix = "widgetzone_";
        if (el.id && el.id.indexOf(sIdPrefix) == 0 && el.className && el.className == "widgetzone") {
            return el.id.substr(sIdPrefix.length);
        }
        return null;
    }
    ,
    sendRequestForMoveItems: function() {

        // The zoneNames need to be loaded only one time since new zones can't be added while
        // the document is loaded.
        if (TrainEngine.widgetAdmin.zoneNames.length == 0) {
            var oDivs = document.getElementsByTagName('div');
            for (var i = 0; i < oDivs.length; i++) {

                if (TrainEngine.widgetAdmin.getWidgetZoneNameFromElement(oDivs[i]))
                    TrainEngine.widgetAdmin.zoneNames[TrainEngine.widgetAdmin.zoneNames.length] = TrainEngine.widgetAdmin.getWidgetZoneNameFromElement(oDivs[i]);

            }
        }

        TrainEngine.createCallback(TrainEngine.webRoot + "admin/WidgetEditor.aspx?getmoveitems=" + TrainEngine.widgetAdmin.zoneNames.join(",") + "&rnd=" + Math.random(), TrainEngine.widgetAdmin.loadMoveDropdownItems);
    }
    ,
    getWidgetsZoneName: function(id) {
        var oWidget = TrainEngine.$("widget" + id);
        var zoneName = null;
        if (oWidget) {
            while (!zoneName && oWidget.parentNode) {
                zoneName = TrainEngine.widgetAdmin.getWidgetZoneNameFromElement(oWidget.parentNode);
                oWidget = oWidget.parentNode;
            }
        }
        return zoneName;
    }
    ,
    initiateMoveWidget: function(id) {

        var sWidgetZoneName = TrainEngine.widgetAdmin.getWidgetsZoneName(id);

        if (TrainEngine.widgetAdmin.moveContainerCurrentLocationWidgetId &&
            TrainEngine.widgetAdmin.moveContainerCurrentLocationWidgetZone &&
            id == TrainEngine.widgetAdmin.moveContainerCurrentLocationWidgetId &&
            sWidgetZoneName == TrainEngine.widgetAdmin.moveContainerCurrentLocationWidgetZone) {

            // Move link was clicked on the same widget the moveContainer is already at.  Just hide.
            TrainEngine.widgetAdmin.hideMoveToList();
            return;
        }

        TrainEngine.widgetAdmin.moveContainerCurrentLocationWidgetId = id;
        TrainEngine.widgetAdmin.moveContainerCurrentLocationWidgetZone = sWidgetZoneName;

        if (!TrainEngine.widgetAdmin.moveDropdown || TrainEngine.widgetAdmin.moveDropdown.childNodes.length == 0)
            TrainEngine.widgetAdmin.sendRequestForMoveItems();
        else
            TrainEngine.widgetAdmin.positionMoveDropdown();
    }
    ,
    editWidget: function(name, id) {
        window.scrollTo(0, 0);
        var width = document.documentElement.clientWidth + document.documentElement.scrollLeft;
        var height = document.documentElement.clientHeight + document.documentElement.scrollTop;

        var zone = TrainEngine.widgetAdmin.getWidgetsZoneName(id);

        var layer = document.createElement('div');
        layer.style.zIndex = 1002;
        layer.id = 'layer';
        layer.style.position = 'absolute';
        layer.style.top = '0px';
        layer.style.left = '0px';
        layer.style.height = document.documentElement.scrollHeight + 'px';
        layer.style.width = width + 'px';
        layer.style.backgroundColor = 'black';
        layer.style.opacity = '.6';
        layer.style.filter += ("progid:DXImageTransform.Microsoft.Alpha(opacity=60)");
        document.body.style.position = 'static';
        document.body.appendChild(layer);

        var size = { 'height': 500, 'width': 750 };
        var iframe = document.createElement('iframe');
        iframe.name = 'Widget Editor';
        iframe.id = 'WidgetEditor';
        iframe.src = TrainEngine.webRoot + 'admin/WidgetEditor.aspx?widget=' + name + '&id=' + id + "&zone=" + zone;
        iframe.style.height = size.height + 'px';
        iframe.style.width = size.width + 'px';
        iframe.style.position = 'fixed';
        iframe.style.zIndex = 1003;
        iframe.style.backgroundColor = 'white';
        iframe.style.border = '4px solid silver';
        iframe.frameborder = '0';

        iframe.style.top = ((height + document.documentElement.scrollTop) / 2) - (size.height / 2) + 'px';
        iframe.style.left = (width / 2) - (size.width / 2) + 'px';

        document.body.appendChild(iframe);
    }
    ,
    addWidget: function(type, zone) {
        TrainEngine.createCallback(TrainEngine.webRoot + "admin/WidgetEditor.aspx?add=" + type + "&zone=" + zone + "&rnd=" + Math.random(), TrainEngine.widgetAdmin.appendWidget);
    }
    ,
    appendWidget: function(response) {
        if (response == "reload") {
            location.reload();
        }
        else {
            var delimiterPos = response.indexOf('?');
            if (delimiterPos != -1) {
                var zoneId = response.substr(0, delimiterPos);
                response = response.substr(delimiterPos + 1);
                var zone = TrainEngine.$('widgetzone_' + zoneId);
                if (zone) {
                    // clearWidgetList() BEFORE using innerHTML.  If done after,
                    // causes problems with losing a handle to the moveContainer
                    // if the moveContainer is currently visible.
                    TrainEngine.widgetAdmin.clearWidgetList();
                    zone.innerHTML += response;
                }
            }
        }
    }
    ,
    widgetRemoved: function(response) {
        if (!response || response.length < 36) {
            alert('Failed to remove the widget.');
            return;
        }

        var widgetId = response.substr(0, 36);
        var zoneName = response.substr(36);
        var zone = TrainEngine.$("widgetzone_" + zoneName);
        var widget = TrainEngine.$("widget" + widgetId);

        if (zone && widget) {
            TrainEngine.widgetAdmin.clearWidgetList();
            zone.removeChild(widget);
        }
    }
    ,
    removeWidget: function(id) {
        if (confirm('Are you sure you want to remove the widget?')) {
            var zone = TrainEngine.widgetAdmin.getWidgetsZoneName(id);
            TrainEngine.createCallback(TrainEngine.webRoot + "admin/WidgetEditor.aspx?remove=" + id + "&zone=" + zone + "&rnd=" + Math.random(), TrainEngine.widgetAdmin.widgetRemoved);
        }
    }
    ,
    closeEditor: function() {
        document.body.removeChild(TrainEngine.$('WidgetEditor'));
        document.body.removeChild(TrainEngine.$('layer'));
        document.body.style.position = '';
    }
};
