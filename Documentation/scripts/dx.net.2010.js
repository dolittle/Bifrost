/* Begin Common utility functions */

/* This is required for user data support in .chms */
var scrollPos = null;

/* Returns a document element using the Firefox friendly getElementById */
function documentElement(id) {
    return document.getElementById(id);
}

/* Returns the source element of an event */
function sourceElement(e) {
    if (window.event) {
        e = window.event;
    }

    return e.srcElement ? e.srcElement : e.target;
}

/* Cancels an event, preventing further bubbling and returning false to cancel default behavior */
function cancelEvent(e) {
    e.returnValue = false;
    e.cancelBubble = true;

    if (e.stopPropagation) {
        e.stopPropagation();
        e.preventDefault();
    }
}

/* Returns an elements absolute position, allowing for the non-scrolling header */
function getElementPosition(e) {
    var offsetLeft = 0;
    var offsetTop = 0;

    while (e && e.tagName != "DIV") {
        // Allow for the scrolling body region in IE
        offsetLeft += e.offsetLeft;
        offsetTop += e.offsetTop;

        e = e.offsetParent;
    }

    if (navigator.userAgent.indexOf('Mac') != -1 && typeof document.body.leftMargin != 'undefined') {
        offsetLeft += document.body.leftMargin;
        offsetTop += document.body.topMargin;
    }

    return { left: offsetLeft, top: offsetTop };
}

/* Return Microsoft Internet Explorer (major) version number, or 0 for others. */
function msieversion() {
    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ");

    if (msie > 0) // is Microsoft Internet Explorer; return version number
    {
        return parseInt(ua.substring(msie + 5, ua.indexOf(".", msie)));
    }
    else {
        return 0;    // is other browser
    }
}

/* To hide all popup boxes */
function hideBoxes() {
    var divs = document.getElementsByTagName("DIV");

    if (divs) {
        for (var idiv = 0; idiv < divs.length; idiv++) {
            var div = divs[idiv];

            if (div.className) {
                if (div.className == "DxPopupBubble" || div.className == "HsPopupBubble" || div.className == "HsGlossaryReference") {
                    div.style.visibility = "hidden";
                }
            }
        }
    }
}

/* Show a popup box */
function showBox(Name, e, button) {
    if (window.event) {
        e = window.event;
    }

    hideBoxes();

    if (!button) {
        button = sourceElement(e);
    }
    cancelEvent(e);
    var div = documentElement(Name);

    if (div && button) {
        div.style.height = "";
        div.style.left = (getElementPosition(button).left) + "px";
        div.style.top = (getElementPosition(button).top + button.offsetHeight) + "px";
        div.style.visibility = "visible";
    }
}

/* End Common utility functions */


/* Shows a popup parameter box */
function showParamBox(e) {
    if (window.event) {
        e = window.event;
    }

    var button = sourceElement(e);
    var documentWidth;
    var documentHeight;
    var boxWidth;
    var pixelLeft;
    var pixelTop;
    var boxHeight;
    var div = documentElement(button.id + "_box");

    cancelEvent(e);
    hideBoxes();

    if (div && button) {
        // Have the browser size the box
        div.style.height = "";
        div.style.width = "";

        // Clear any tooltip so it doesn't appear above the popup
        button.title = "";

        pixelTop = getElementPosition(button).top + 14;

        // Check if the box would go off the bottom of the visible area
        documentHeight = document.body.clientHeight;
        boxHeight = div.clientHeight;
        if (pixelTop + boxHeight > documentHeight) {
            // If the box won't fit both above and below the link
            //  using the default width then make the box wider
            if (boxHeight >= pixelTop) {
                div.style.width = "80%";
            }
            else {
                pixelTop = pixelTop - 14 - boxHeight;
            }
        }
        div.style.top = pixelTop + "px";
        documentWidth = document.body.clientWidth;
        boxWidth = div.clientWidth;
        pixelLeft = button.offsetLeft;

        // Check if the box would go off the edge of the visible area           
        if (pixelLeft + boxWidth > documentWidth) {
            pixelLeft = documentWidth - boxWidth - 5;
        }
        div.style.left = pixelLeft + "px";

        // Show the box
        div.style.visibility = "visible";
    }
}


/* Begin non-scrolling region support */

var isDesignTime = false;

function bodyClick(e) {
    if (window.event) {
        e = window.event;
    }
    if (sourceElement(e)) {
        try {
            if (sourceElement(e).tagName != "INPUT") {
                if (sourceElement(e).className != "LanguageFilter" && sourceElement(e).className != "MembersFilter" && sourceElement(e).className != "CommunityFilter") {
                    hideBoxes();
                };
            }
        }
        catch (e)
        { }
    }
}

/* End non-scrolling region support */


/* Begin Document load/unload/print support */

/* Called before custom body load script */
function bodyLoadCommonBefore() {
    document.body.onclick = bodyClick;
    window.onbeforeprint = beforePrint;
    window.onafterprint = afterPrint;
    window.onunload = windowUnload;
}

/* Called after custom body load script */
function bodyLoadCommonAfter() {
    wireUpEventHandlers();
    loadSettings();

    // make body visible, now that we're ready to render
    document.body.style.visibility = "visible";
}

/* Saves the scroll position */
function windowUnload() {
    saveSettings();
}

/* Common settings load script */
function dxLoadSettingsCommon() {
    // load the expand / collapse states
    dxLoadSections();
    dxSetSelectedTabOnTabstrips();
}

/* Common settings save script */
function dxSaveSettingsCommon() {
    // save the expand / collapse states
    dxSaveSections();
    dxSaveSelectedTabs();
}

/* Removes the non-scrolling region and ensures everything is visible for print */
function beforePrint() {
    var allElements;

    if (window.text) {
        document.all.text.style.height = "auto";
    }

    allElements = document.getElementsByTagName("*");

    for (var i = 0; i < allElements.length; i++) {
        if (allElements[i].tagName == "BODY") {
            allElements[i].scroll = "yes";
        }
        if (allElements[i].id == "Pagetop") {
            allElements[i].style.margin = "0px 0px 0px 0px";
            allElements[i].style.width = "100%";
        }
        if (allElements[i].id == "Pagebody") {
            allElements[i].style.overflow = "visible";
            allElements[i].style.top = "5px";
            allElements[i].style.width = "100%";
            allElements[i].style.padding = "0px 10px 0px 30px";
        }
        if (allElements[i].id == "Pagetoptable2row1" || allElements[i].id == "FeedbackLink") {
            allElements[i].style.display = "none";
        }
        if (allElements[i].className == "LanguageSpecific") {
            allElements[i].style.display = "block";
        }
    }
}

/* Refresh the page after a print */
function afterPrint() {
    document.location.reload();
}

/* End Document load/unload/print support */


/* Begin User Data support */

function dxGetDataCache() {
    if (isDesignTime) {
        if (dxGetWindowExternal()) {
            if (window.external.IsInnovasysDesigner) {
                return window.external;
            }
        }
    }

    //return documentElement("userDataCache");
}

function dxGetWindowExternal() {
    try {
        return window.external;
    }
    catch (e) {
    }
}

function dxGetSetting(key) {
    var dataCacheObject = dxGetDataCache();
    if (dataCacheObject) {
        dataCacheObject.load("innSettings");
        var value = dataCacheObject.getAttribute(key);
        return value;
    }
    else {
        var value = dxLoadCookieValue(key);
        if (value && value == "undefined")
        {
            return null;
        }
        else
            return value;
    }
}


function load(key) {
    return dxGetSetting(key);
}

function dxSaveSetting(key, value) {
    var dataCacheObject = dxGetDataCache();
    if (dataCacheObject) {
        dataCacheObject.setAttribute(key, value);
        dataCacheObject.save("innSettings");
    }
    else
        dxSaveCookieValue(key, value, 60, "/", "", "");
}

function save(key, value) {
    dxSaveSetting(key, value);
}

function dxRemoveSetting(key) {
    dxSaveSetting(key, null);
}

function removeAttribute(key) {
    dxRemoveSetting(key);
}

/* End User Data support */


/* Begin Expanding sections support */

var collapsedSections = new Array();

/* Wires up the expand / collapse functionality */
function wireUpEventHandlers() {
    var elements = document.getElementsByTagName("SPAN");

    for (var i = 0; i < elements.length; i++) {
        var span = elements[i];
        if (span.className == "DxExpandCollapse") {
            span.onclick = dxToggleExpanded;
            span.onkeypress = dxToggleExpandedOnKey;
        }
    }

    if (documentElement("DxToggleExpandedAll")) {
        documentElement("DxToggleExpandedAll").onclick = dxToggleExpandedAll;
        documentElement("DxToggleExpandedAll").onkeypress = dxToggleExpandedAllOnKey;
    }
}

function dxToggleExpanded(img) {
    // Firefox passes in the event object from the event handler, so
    //  we check for that and set to null
    if (img) {
        if (img.tagName == null) {
            e = img;
            img = null;
        }
    }

    // Find the expand & collapse image
    if (!img) {
        if (window.event)
            e = window.event;

        var img = sourceElement(e)
        if (img) {
            while (img) {
                if (img.className == "DxExpandCollapse" && img.tagName == "SPAN")
                    break;
                else
                    img = img.parentNode;
            }
            if (img)
                img = dxFindExpandCollapseImage(img);
        }
    }


    if (img) {
        if (dxIsSectionCollapsed(img.id) == true) {
            img.src = documentElement("CollapseImage").src;
            dxExpandSection(img);
            dxRemoveCollapsedItem(img.id);
            if (img.id.indexOf("Family", 0) == 0) {
                protectedMembers = "on";
                configureMembersFilterCheckboxes();
                changeMembersFilterLabel();
            }
        }
        else {
            img.src = documentElement("ExpandImage").src;
            dxCollapseSection(img);
            dxAddCollapsedSection(img.id);
        }

        dxSetCollapseAll();
    }
}

function dxFindExpandCollapseImage(sourceElement) {
    var e;
    var elements;

    if (sourceElement.tagName == "IMG" && sourceElement.className == "DxToggle") {
        return (sourceElement);
    }
    else {
        if (sourceElement) {
            elements = sourceElement.getElementsByTagName("IMG");

            for (var i = 0; i < elements.length; i++) {
                e = elements[i];
                if (e.className == "DxToggle") {
                    return (e);
                }
            }
        }
    }
}

function dxToggleExpandedAll() {
    var elements = document.getElementsByName("DxToggleSwitch");
    var toggleExpandedAllImage = documentElement("DxToggleExpandedAllImage");

    // All currently collapsed
    if (dxIsSectionCollapsed(toggleExpandedAllImage.id) == true) {
        toggleExpandedAllImage.src = documentElement("CollapseImage").src;
        dxRemoveCollapsedItem(toggleExpandedAllImage.id);

        for (var i = 0; i < elements.length; i++) {
            dxExpandSection(elements[i]);
            elements[i].src = documentElement("CollapseImage").src;
            dxRemoveCollapsedItem(elements[i].id);

            if (elements[i].id.indexOf("Protected", 0) == 0) {
                protectedMembers = "on";
            }
        }

        dxSetToggleExpandedAllLabel(false);
    }
    // All currently expanded
    else {
        toggleExpandedAllImage.src = documentElement("ExpandImage").src;
        dxAddCollapsedSection(toggleExpandedAllImage.id);

        for (var i = 0; i < elements.length; i++) {
            dxCollapseSection(elements[i]);
            elements[i].src = documentElement("ExpandImage").src;
            dxAddCollapsedSection(elements[i].id);
        }

        dxSetToggleExpandedAllLabel(true);
    }
}

function dxToggleExpandedAllOnKey(e) {
    if (window.event) {
        e = window.event;
    }

    if (e.keyCode == 13) {
        dxToggleExpandedAll();
    }
}

function dxToggleExpandedOnKey(e) {
    if (window.event) {
        e = window.event;
    }

    if (e.keyCode == 13) {
        dxToggleExpanded(dxFindExpandCollapseImage(e.srcElement));
    }
}

function getNextSibling(n) {
    n = n.nextSibling;
    while (n) {
        if (n.nodeType == 1)
            return n;
        else
            n = n.nextSibling;
    }
}

function dxExpandSection(imageItem) {
    if (imageItem.id != "DxToggleExpandedAllImage") {
        getNextSibling(imageItem.parentNode.parentNode).style.display = "";
    }
}

function dxCollapseSection(imageItem) {
    if (imageItem.id != "DxToggleExpandedAllImage") {
        getNextSibling(imageItem.parentNode.parentNode).style.display = "none";
    }
}

function dxSetCollapseAll() {
    var toggleElements = document.getElementsByName("DxToggleSwitch");
    var allCollapsed = true;

    if (document.getElementById("ExpandAllLabel")) {
        for (var i = 0; i < toggleElements.length; i++) {
            allCollapsed = allCollapsed && dxIsSectionCollapsed(toggleElements[i].id);
        }

        if (allCollapsed) {
            this.src = documentElement("ExpandAllImage").src;
            dxAddCollapsedSection("DxToggleExpandedAllImage");
        }
        else {
            this.src = documentElement("CollapseAllImage").src;
            dxRemoveCollapsedItem("DxToggleExpandedAllImage");
        }

        dxSetToggleExpandedAllLabel(allCollapsed);
    }
}

function dxSetToggleExpandedAllLabel(allCollapsed) {
    var labelElement;

    labelElement = document.getElementById("CollapseAllLabel");

    if (labelElement == null) {
        return;
    }

    labelElement.style.display = "none";
    labelElement = document.getElementById("ExpandAllLabel");
    labelElement.style.display = "none";

    if (allCollapsed) {
        labelElement = document.getElementById("ExpandAllLabel");
        labelElement.style.display = "inline";
    }
    else {
        labelElement = document.getElementById("CollapseAllLabel");
        labelElement.style.display = "inline";
    }
}

function dxIsSectionCollapsed(imageId) {
    for (var i = 0; i < collapsedSections.length; ++i) {
        if (imageId == collapsedSections[i]) {
            return true;
        }
    }

    return false;
}

function dxAddCollapsedSection(imageId) {
    if (dxIsSectionCollapsed(imageId) == false) {
        collapsedSections[collapsedSections.length] = imageId;
    }
}

function dxRemoveCollapsedItem(imageId) {
    for (var i = 0; i < collapsedSections.length; ++i) {
        if (imageId == collapsedSections[i]) {
            collapsedSections.splice(i, 1);
        }
    }
}

function dxSaveSections() {
    var x = 0;

    cleanUserDataStore();
    for (var i = 0; i < collapsedSections.length; ++i) {
        if (shouldSave(collapsedSections[i]) == true) {
            dxSaveSetting("imageValue" + x, collapsedSections[i]);
            x++;
        }
    }
}

function dxLoadSections() {
    var i = 0;
    var imageId = dxGetSetting("imageValue" + i);

    while (imageId != null) {
        var imageItem = document.getElementById(imageId);

        if (imageItem != null) {
            if (imageItem.id.indexOf("Family", 0) == 0) {
                if (protectedMembers == "on") {
                    dxToggleExpanded(imageItem);
                }
            }
            else {
                dxToggleExpanded(imageItem);
            }
        }
        else {
            dxAddCollapsedSection(imageId);
        }

        i++;
        imageId = dxGetSetting("imageValue" + i);
    }
    dxSetCollapseAll();
}

function cleanUserDataStore() {
    var i = 0;
    var imageId = dxGetSetting("imageValue" + i);

    while (imageId != null) {
        dxRemoveSetting("imageValue" + i);
        i++;
        imageId = dxGetSetting("imageValue" + i);
    }
}

function shouldSave(imageId) {
    var toggleName;

    if (imageId == "DxToggleExpandedAllImage") {
        return false;
    }

    toggleName = "procedureToggle";

    if (imageId.indexOf(toggleName, 0) == 0) {
        return false;
    }

    toggleName = "sectionToggle";

    if (imageId.indexOf(toggleName, 0) == 0) {
        return false;
    }

    return true;
}
function dxOpenSectionById(id) {
    var e = documentElement(id);

    if (e) {
        if (dxIsSectionCollapsed(e.id) == true) {
            dxToggleExpanded(e);
        }
    }
}

/* End Expand / Collapse */


/* Begin save / Restore Scroll Position */

var scrollPos = null;

function loadAll() {
    var historyObject = getHistoryObject();

    if (historyObject) {
        var scrollValue = historyObject.getAttribute("Scroll");

        if (scrollValue) {
            if (scrollValue != 0) {
                try {
                    scrollPos = scrollValue;
                    documentElement("OH_outerContent").scrollTop = scrollPos;
                }
                catch (e) {
                }
            }
        }
    }
}
function saveAll() {
    var historyObject = getHistoryObject();

    if (historyObject) {
        try {
            historyObject.setAttribute("Scroll", documentElement("OH_outerContent").scrollTop);
        }
        catch (e) {
        }
    }
}
function getHistoryObject() {
    if (isDesignTime) {
        try {
            var externalObject = window.external;

            if (externalObject) {
                if (externalObject.IsInnovasysDesigner) {
                    return window.external;
                }
                else {
                    externalObject = false;
                }
            }
        }
        catch (e) {
        }
    }

    if (!externalObject) {
        return documentElement("AllHistory");
    }
}

/* End save / Restore Scroll Position */


/* Begin Copy Code */

function copyCode(key) {
    var trElements = getTABLE(key).getElementsByTagName("tr");
    for (var i = 0; i < trElements.length; ++i) {
        if (getTABLE(key) == getTABLE(trElements[i]) && getTR(key) != trElements[i]) {
            window.clipboardData.setData("Text", trElements[i].innerText);
            break;
        }
    }
}

function getTR(obj) {
    while (obj) {
        if (obj.tagName == "TR")
            return obj;
        else
            obj = obj.parentElement;
    }
}

function getTABLE(obj) {
    while (obj) {
        if (obj.tagName == "TABLE")
            return obj;
        else
            obj = obj.parentElement;
    }
}

function changeCopyCodeIcon(key, highlight) {
    var imageElements = document.getElementsByName("CcImage");

    for (var i = 0; i < imageElements.length; ++i) {
        try {
            if (imageElements[i].parentElement == key) {
                if (highlight) {
                    imageElements[i].src = documentElement("CopyHoverImage").src;
                }
                else {
                    imageElements[i].src = documentElement("CopyImage").src;
                }
            }
        }
        catch (e) {
        }
    }
}

function copyCode_CheckKey(key) {
    var e;

    if (window.event) {
        e = window.event;
    }

    if (e.keyCode == 13) {
        copyCode(key);
    }
}

/* End Copy Code */


/* Begin XML expand / collapse */

// Detect and switch the display of CDATA and comments from an inline view
//  to a block view if the comment or CDATA is multi-line.
function f(e) {
    // if this element is an inline comment, and contains more than a single
    //  line, turn it into a block comment.
    if (e.className == "ci") {
        if (e.children(0).innerText.indexOf("\n") > 0) {
            fix(e, "cb");
        }
    }

    // if this element is an inline cdata, and contains more than a single
    //  line, turn it into a block cdata.
    if (e.className == "di") {
        if (e.children(0).innerText.indexOf("\n") > 0) {
            fix(e, "db");
        }
    }

    // remove the id since we only used it for cleanup
    e.id = "";
}

// Fix up the element as a "block" display and enable expand/collapse on it
function fix(e, cl) {
    var j;
    var k;

    // change the class name and display value
    e.className = cl;
    e.style.display = "block";

    // mark the comment or cdata display as a expandable container
    j = e.parentElement.children(0);
    j.className = "c";

    // find the +/- symbol and make it visible - the dummy link enables tabbing
    k = j.children(0);
    k.style.visibility = "visible";
    k.href = "#";
}

// Change the +/- symbol and hide the children.  This function works on "element"
//  displays
function ch(e) {
    var i;

    // find the +/- symbol
    var mark = e.children(0).children(0);

    // if it is already collapsed, expand it by showing the children
    if (mark.innerText == "+") {
        mark.innerText = "-";
        for (i = 1; i < e.children.length; i++) {
            e.children(i).style.display = "block";
        }
    }

    // if it is expanded, collapse it by hiding the children
    else if (mark.innerText == "-") {
        mark.innerText = "+";
        for (i = 1; i < e.children.length; i++) {
            e.children(i).style.display = "none";
        }
    }
}

// Change the +/- symbol and hide the children.  This function work on "comment"
//  and "cdata" displays
function ch2(e) {
    var contents;

    // find the +/- symbol, and the "PRE" element that contains the content
    var mark = e.children(0).children(0);
    contents = e.children(1);

    // if it is already collapsed, expand it by showing the children
    if (mark.innerText == "+") {
        mark.innerText = "-";

        // restore the correct "block"/"inline" display type to the PRE
        if (contents.className == "db" || contents.className == "cb") {
            contents.style.display = "block";
        }
        else {
            contents.style.display = "inline";
        }
    }
    // if it is expanded, collapse it by hiding the children
    else {
        if (mark.innerText == "-") {
            mark.innerText = "+";
            contents.style.display = "none";
        }
    }
}

// Handle a mouse click
function cl() {
    var e = window.event.srcElement;

    // make sure we are handling clicks upon expandable container elements
    if (e.className != "c") {
        e = e.parentElement;
        if (e.className != "c") {
            return;
        }
    }
    e = e.parentElement;

    // call the correct funtion to change the collapse/expand state and display
    if (e.className == "e") {
        ch(e);
    }

    if (e.className == "k") {
        ch2(e);
    }
}

// Dummy function for expand/collapse link navigation - trap onclick events instead
function ex() {
}

// Erase bogus link info from the status window
function h() {
    window.status = " ";
}

/* End XML Expand / Collapse */


/* .NET specific script */

// Current language
var curLang = "";

// To prevent 'access denied' errors in the authoring environment
function dxErrorHandler(msg, url, line) {
    if (url == "about:blank") {
        return true;
    }
}
if (window.location.href == 'about:blank') {
    window.onerror = dxErrorHandler;
}

function bodyLoad() {
    var i;
    var b;
    var l;

    bodyLoadCommonBefore();

    bodyLoadCommonAfter();
}

function loadSettings() {
    // load the languages   
    loadLanguages();
    configureLanguageCheckboxes();
    displayLanguages();

    // load the member options
    loadMembersFilter();
    configureMembersFilterCheckboxes();
    changeMembersFilterLabel();
    dxLoadSettingsCommon();
}

function saveSettings() {
    saveLanguages();
    saveMembersFilter();
    dxSaveSettingsCommon();
}

/* Begin Member Filtering */

var inheritedMembers;
var protectedMembers;

function configureMembersFilterCheckboxes() {
    var checkbox;

    checkbox = document.getElementById("InheritedCheckbox");
    if (checkbox != null) {
        if (inheritedMembers == "on") {
            checkbox.checked = true;
        }
        else {
            checkbox.checked = false;
        }
    }

    checkbox = document.getElementById("ProtectedCheckbox");
    if (checkbox != null) {
        if (protectedMembers == "on") {
            checkbox.checked = true;
        }
        else {
            checkbox.checked = false;
        }
    }
}

function setMembersFilter(key) {
    if (key.id == "InheritedCheckbox") {
        if (key.checked == true) {
            inheritedMembers = "on";
        }
        else {
            inheritedMembers = "off";
        }

        updateInheritedMembers();
    }

    if (key.id == "ProtectedCheckbox") {
        if (key.checked == true) {
            protectedMembers = "on";
        }
        else {
            protectedMembers = "off";
        }

        updateProtectedMembers();
    }

    changeMembersFilterLabel();
}

function updateInheritedMembers() {
    var tablerows = document.getElementsByTagName("tr");
    var i;

    if (inheritedMembers == "off") {
        for (i = 0; i < tablerows.length; ++i) {
            if (tablerows[i].id == "InheritedMember")
                tablerows[i].style.display = "none";
        }
    }
    else {
        for (i = 0; i < tablerows.length; ++i) {
            if (tablerows[i].id == "InheritedMember")
                tablerows[i].style.display = "";
        }
    }
}

function updateProtectedMembers() {
    var toggleImages = document.getElementsByName("DxToggleSwitch");
    var i;

    if (protectedMembers == "off") {
        for (i = 0; i < toggleImages.length; ++i) {
            if (toggleImages[i].id.indexOf("protected", 0) == 0) {
                if (dxIsSectionCollapsed(toggleImages[i].id) == false) {
                    dxToggleExpanded(toggleImages[i]);
                }
            }
        }
    }
    else {
        for (i = 0; i < toggleImages.length; ++i) {
            if (toggleImages[i].id.indexOf("protected", 0) == 0) {
                if (dxIsSectionCollapsed(toggleImages[i].id) == true) {
                    dxToggleExpanded(toggleImages[i]);
                }
            }
        }
    }
}

function changeMembersFilterLabel() {
    var filtered = false;

    if ((inheritedMembers == "off") || (protectedMembers == "off")) {
        filtered = true;
    }

    var labelElement = document.getElementById("ShowAllMembersLabel");
    if (labelElement == null) {
        return;
    }
    labelElement.style.display = "none";

    labelElement = document.getElementById("FilteredMembersLabel");
    labelElement.style.display = "none";

    if (filtered) {
        labelElement = document.getElementById("FilteredMembersLabel");
        labelElement.style.display = "inline";
    }
    else {
        labelElement = document.getElementById("ShowAllMembersLabel");
        labelElement.style.display = "inline";
    }
}

function loadMembersFilter() {
    var value = dxGetSetting("inheritedMembers");

    if (value == null) {
        inheritedMembers = "on";
    }
    else {
        inheritedMembers = value;
    }

    value = dxGetSetting("protectedMembers");

    if (value == null) {
        protectedMembers = "on";
    }
    else {
        protectedMembers = value;
    }

    if (inheritedMembers == "off") {
        updateInheritedMembers();
    }

    if (protectedMembers == "off") {
        updateProtectedMembers();
    }
}

function saveMembersFilter() {
    dxSaveSetting("inheritedMembers", inheritedMembers);
    dxSaveSetting("protectedMembers", protectedMembers);
}

/* End Member Filtering */

/* Language Filtering */

var languageNames;
var languageStates;

function configureLanguageCheckboxes() {
    var checkbox;

    for (var i = 0; i < languageNames.length; i++) {
        checkbox = documentElement(languageNames[i] + "Checkbox");

        if (languageStates[i] == "on") {
            checkbox.checked = true;
        }
        else {
            checkbox.checked = false;
        }
    }
}

function setLanguage(key) {
    var languageName = key.id.substring(0, key.id.indexOf("Checkbox"));

    if (getLanguageState(languageName) == "on") {
        // Always have at least one selected
        if (getLanguageTickedCount() == 1) {
            key.checked = true;
            return;
        }

        setLanguageState(languageName, "off");
        key.checked = false;
    }
    else {
        setLanguageState(languageName, "on");
        key.checked = true;
    }

    // Update the content to reflect the new language filter
    displayLanguages();

    // Reset the selected tab
    dxRefreshSelectedTabOnTabstrips();
}

function displayLanguages() {
    var pres = document.getElementsByTagName("DIV");
    var pre;
    var found;
    var languageName;

    if (pres) {
        for (var iPre = 0; iPre < pres.length; iPre++) {
            pre = pres[iPre];

            if (pre.id && pre.className) {
                if (pre.className == "LanguageSpecific"
                    || (pre.className.indexOf("DxTab ") == 0 && pre.className.length == 5)
                    || pre.className.lastIndexOf("DxTab") == pre.className.length - 5) {
                    
                    found = true;

                    if (pre.id.substring(pre.id.length - 1, pre.id.length) != "_") {
                        for (var i = 0; i < languageNames.length; i++) {
                            if (languageStates[i] == "off") {
                                languageName = languageNames[i].toUpperCase();

                                // For each language specific element except the Syntax, treat CPP2005 as CPP
                                if (languageName == "CPP2005" && pre.id.toUpperCase().indexOf("SYNTAX") == -1) {
                                    languageName = "CPP";
                                }

                                if (doesIdContainLanguageName(pre.id, languageName)) {
                                    found = false;
                                    break;
                                }
                                else if (languageName == "VB" && doesIdContainLanguageName(pre.id, "VBALL")) {
                                    found = false;
                                    break;
                                }
                            }
                        }
                    }

                    if (found) {
                        pre.style.display = "block";
                    }
                    else {
                        pre.style.display = "none";
                    }
                }
            }
        }
    }

    changeLanguageFilterLabel();
}

function doesIdContainLanguageName(id, languageName)
{
    return ((id.toUpperCase().indexOf(languageName) != -1 
             && id.toUpperCase().indexOf(languageName) == (id.length - languageName.length)) 
           || (id.toUpperCase().indexOf(languageName + ".NET") != -1 
             && id.toUpperCase().indexOf(languageName + ".NET") == (id.length - languageName.length - 4)))
}

function getLanguageState(LanguageName) {
    for (var i = 0; i < languageNames.length; i++) {
        if (languageNames[i] == LanguageName) {
            return (languageStates[i]);
        }
    }
}

function setLanguageState(LanguageName, Value) {
    for (var i = 0; i < languageNames.length; i++) {
        if (languageNames[i] == LanguageName) {
            languageStates[i] = Value;
        }
    }
}

function getLanguageTickedCount() {
    var tickedCount = 0;
    var labelElement;

    for (var i = 0; i < languageNames.length; i++) {
        if (languageStates[i] == "on") {
            tickedCount++;
        }
    }

    return (tickedCount);
}

function changeLanguageFilterLabel() {
    var tickedCount = 0;
    var labelElement;
    var languageName;

    if (!document.getElementById("ShowAllLabel")) {
        return;
    }

    for (var i = 0; i < languageNames.length; i++) {
        if (languageStates[i] == "on") {
            tickedCount++;
        }

        labelElement = documentElement(languageNames[i] + "label");

        if (labelElement != null) {
            labelElement.style.display = "none";
        }
    }

    document.getElementById("ShowAllLabel").style.display = "none";
    document.getElementById("MultipleLabel").style.display = "none";

    if (tickedCount == languageNames.length) {
        document.getElementById("ShowAllLabel").style.display = "inline";
    }
    else if ((tickedCount > 1) && (tickedCount < languageNames.length)) {
        if ((tickedCount == 2) && (getLanguageState("VB") == "on") && (getLanguageState("vbUsage") == "on")) {
            document.getElementById("VBLabel").style.display = "inline";
        }
        else {
            document.getElementById("MultipleLabel").style.display = "inline";
        }
    }
    else if (tickedCount == 1) {
        for (var i = 0; i < languageNames.length; i++) {
            if (languageStates[i] == "on") {
                if (languageNames[i] == "vbUsage") {
                    languageName = "VB";
                }
                else {
                    languageName = languageNames[i];
                }

                document.getElementById(languageName + "Label").style.display = "inline";
            }
        }
    }
}

function loadLanguages() {
    var languageName;
    var language;
    var allNull;

    // Build an array of this pages language names and state
    languageNames = new Array();
    languageStates = new Array();

    var elements = document.getElementsByName("LanguageFilter");

    allNull = true;

    for (var i = 0; i < elements.length; i++) {
        var input = elements[i];

        languageNames[i] = input.id.substring(0, input.id.indexOf("Checkbox"));
        var value = dxGetSetting("lang_" + languageNames[i]);

        if (value == null) {
            languageStates[i] = "on";
        }
        else {
            allNull = false;
            languageStates[i] = value;
        }
    }

    // If no language preference has been established and we have an indicator of the current
    //  language, set the languages filtered to only the current language
    if (allNull && curLang.length > 0) {
        for (var i = 0; i < elements.length; i++) {
            if (languageNames[i].toUpperCase() == curLang.toUpperCase()) {
                languageStates[i] = "on";
            }
            else {
                languageStates[i] = "off";
            }
        }
    }
}

function saveLanguages() {
    if (languageNames) {
        for (var i = 0; i < languageNames.length; i++) {
            var value = languageStates[i];
            dxSaveSetting("lang_" + languageNames[i], value);
        }
    }
}

/* End Language Filtering */



/* Tab Strip */

function dxSetSelectedTabOnTabstrips() {
    // Set the default tab on any tab strips
    var tabDivs = document.getElementsByTagName("DIV")
    for (var i = 0; i < tabDivs.length; i++) {
        var div = tabDivs[i];
        if (div.className == "DxTabStripContainer") {
            // Load the selected tab from the session cookie
            var defaultTabName = dxGetSetting(div.id + "_SelectedTab");
            if (defaultTabName)
                dxSetActiveTabById(div.id, defaultTabName);
            // Check that a tab is selected
            var selectedTabName = dxGetSelectedTabName(div);
            if (!selectedTabName) {
                var tabs = dxGetTabsFromTabContainer(div, true);
                if (tabs)
                    dxSetActiveTab(div, tabs[0]);
            }
        }
    }
}

function dxRefreshSelectedTabOnTabstrips() {
    // Refresh the selected tab on any tab strips
    var tabDivs = document.getElementsByTagName("DIV")
    for (var i = 0; i < tabDivs.length; i++) {
        var div = tabDivs[i];
        if (div.className == "DxTabStripContainer") {
            // Get the currently selected tab
            var selectedTabName = dxGetSelectedTabName(div);
            dxSetActiveTabById(div.id, selectedTabName);
            // Now check we have a selection
            var selectedTabName = dxGetSelectedTabName(div);
            if (!selectedTabName) {
                var tabs = dxGetTabsFromTabContainer(div, true);
                if (tabs)
                    dxSetActiveTab(div, tabs[0]);
            }
        }
    }
}

function dxSaveSelectedTabs() {
    // Save the current tab on any tab strips
    var tabDivs = document.getElementsByTagName("DIV");
    for (var i = 0; i < tabDivs.length; i++) {
        var div = tabDivs[i];
        if (div.className == "DxTabStripContainer") {
            var selectedTabName = dxGetSelectedTabName(div);
            dxSaveSetting(div.id + "_SelectedTab", selectedTabName);        
        }
    }
}

function dxSetActiveTabById(containerName, tabName) {
    if (containerName && tabName) {
        var container = document.getElementById(containerName);
        var childNodes = container.childNodes;
        for (var i = 0; i < childNodes.length; i++) {
            var childNode = childNodes[i];
            if (childNode.id == tabName) {
                return dxSetActiveTab(container, childNode);
            }
        }
    }

    dxSaveSelectedTabs();
}

function dxGetNextDiv(div) {
    do {
        if (div.nextSibling) {
            div = div.nextSibling;
            if (div.tagName && div.tagName == "DIV" && div.style.display != "none")
                return div;
        }
    } while (div.nextSibling);
}

function dxGetPreviousDiv(div) {
    do {
        if (div.previousSibling) {
            div = div.previousSibling;
            if (div.tagName && div.tagName == "DIV" && div.style.display != "none")
                return div;
        }
    } while (div.previousSibling);
}

function dxSetActiveTab(container, tab) {
    if (container && tab) {
        var childNodes = container.childNodes;
        for (var i = 0; i < childNodes.length; i++) {
            var childNode = childNodes[i];
            if (childNode.tagName && childNode.tagName == "DIV" && (childNode.className == "DxTab" || childNode.className.indexOf(" DxTab") == childNode.className.length - 6)) {
                var isActiveTab = (childNode == tab);
                if (isActiveTab)
                    childNode.className = "DxTabActive DxTab";
                else
                    childNode.className = "DxTab";
                // If this tab is visible, update the adjacent left / right end 
                if (childNode.style.display != 'none') {
                    if (dxGetNextDiv(childNode) && dxGetNextDiv(childNode).className && dxGetNextDiv(childNode).className.indexOf("DxTabRightEnd") != -1) {
                        /* Last tab, update right end */
                        var rightEnd = dxGetNextDiv(childNode);
                        if (isActiveTab)
                            rightEnd.className = "DxTabRightEndActive DxTabRightEnd";
                        else
                            rightEnd.className = "DxTabRightEnd";
                    }
                    if (dxGetPreviousDiv(childNode) && dxGetPreviousDiv(childNode).className && dxGetPreviousDiv(childNode).className.indexOf("DxTabLeftEnd") != -1) {
                        /* First tab, update right end */
                        var leftEnd = dxGetPreviousDiv(childNode);
                        if (isActiveTab)
                            leftEnd.className = "DxTabLeftEndActive DxTabLeftEnd";
                        else
                            leftEnd.className = "DxTabLeftEnd";
                    }
                }
                /* Find related content div and show/hide */
                if (childNode.id == "SyntaxTab_VBAll") {
                    dxShowOrHideTabContentSection("SyntaxTab_VB_Content", isActiveTab);
                    dxShowOrHideTabContentSection("SyntaxTab_VBUsage_Content", isActiveTab);
                }
                else {
                    dxShowOrHideTabContentSection(childNode.id + "_Content", isActiveTab);
                }
            }
        }
    }
}

function dxShowOrHideTabContentSection(id, isVisible) {
    var contentDiv = document.getElementById(id);
    if (contentDiv) {
        if (isVisible)
            contentDiv.style.display = "block";
        else
            contentDiv.style.display = "none";
    }
}

function dxGetSelectedTabName(container) {
    var tabs = dxGetTabsFromTabContainer(container, true);
    if (tabs) {
        for (var tabIndex = 0; tabIndex < tabs.length; tabIndex++) {
            var tab = tabs[tabIndex];
            if (tab.className.indexOf("DxTabActive") != -1)
                return tab.id;
        }
    }
}

function dxGetTabsFromTabContainer(container, visibleOnly) {
    var tabs = [];
    var childNodes = container.childNodes;
    for (var i = 0; i < childNodes.length; i++) {
        var childNode = childNodes[i];
        if (childNode.tagName && childNode.tagName == "DIV" && (childNode.className == "DxTab" || (childNode.className.indexOf(" DxTab") == childNode.className.length - 6))) {
            if (!visibleOnly || childNode.style.display != "none")
                tabs[tabs.length] = childNode;
        }
    }
    return tabs;
}

/* End Tab Strip */

/* Cookie functionality */
function dxLoadCookieValue(sName) {
    var CookieValues = document.cookie.split("; ");
    for (var i = 0; i < CookieValues.length; i++) {
        var aCrumb = CookieValues[i].split("=");

        if (sName == aCrumb[0])
            return unescape(aCrumb[1])
    }
    return null;
}

function dxSaveCookieValue(name, value, expiry, path, domain, secure) {
    var Now = new Date();
    Now.setTime(Now.getTime());

    if (expiry) {
        expiry = expiry * 1000 * 60 * 60 * 24;
    }
    var ExpiresOn = new Date(Now.getTime() + (expiry));

    document.cookie = name + "=" + escape(value) +
    ((expiry) ? ";expires=" + ExpiresOn.toGMTString() : "") +
    ((path) ? ";path=" + path : "") +
    ((domain) ? ";domain=" + domain : "") +
    ((secure) ? ";secure" : "");
}

/* End Cookie functionality */