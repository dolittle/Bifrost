/* Wire up the bodyload handler (set here rather than in the body element
    in order to avoid a HTML Help viewer bug with printing multiple topics */
window.onload = bodyLoad;

/* Set body initially hidden unless we are printing */
document.write("<style media='screen'>body {visibility: hidden}</style>");

/* Begin Common utility functions */

/* This is required for user data support in .chms */
var curURL = document.location + ".";
var pos = curURL.indexOf("mk:@MSITStore");
var scrollPos=null;
if( pos == 0 )
{
    curURL = "ms-its:" + curURL.substring(14,curURL.length-1);
    document.location.replace(curURL);
}

/* Returns a document element using the Firefox friendly getElementById */
function documentElement(id)
{
    return document.getElementById(id);
}

/* Returns the source element of an event */
function sourceElement(e)
{
    if (window.event)
    {
        e = window.event;
    }

    return e.srcElement? e.srcElement : e.target;
}

/* Cancels an event, preventing further bubbling and returning false to cancel default behavior */
function cancelEvent(e)
{
    e.returnValue = false;
    e.cancelBubble = true;

    if (e.stopPropagation)
    {
        e.stopPropagation();
        e.preventDefault();
    }
}

/* Returns an elements absolute position, allowing for the non-scrolling header */
function getElementPosition(e)
{
    var offsetLeft = 0;
    var offsetTop = 0;

    while (e)
    {
        // Allow for the scrolling body region in IE
        if (msieversion() > 4)
        {
            offsetLeft += (e.offsetLeft - e.scrollLeft);
            offsetTop += (e.offsetTop - e.scrollTop);
        }
        else
        {
            offsetLeft += e.offsetLeft ;
            offsetTop += e.offsetTop;
        }

        e = e.offsetParent;
    }

    if (navigator.userAgent.indexOf('Mac') != -1 && typeof document.body.leftMargin != 'undefined')
    {
        offsetLeft += document.body.leftMargin;
        offsetTop += document.body.topMargin;
    }

    return {left:offsetLeft,top:offsetTop};
}

/* Return Microsoft Internet Explorer (major) version number, or 0 for others. */
function msieversion()
{
    var ua = window.navigator.userAgent;
    var msie = ua.indexOf ( "MSIE " );

    if ( msie > 0 ) // is Microsoft Internet Explorer; return version number
    {
        return parseInt ( ua.substring ( msie+5, ua.indexOf ( ".", msie ) ) );
    }
    else
    {
        return 0;    // is other browser
    }
}

/* To hide all popup boxes */
function hideBoxes()
{
    var divs = document.getElementsByTagName("DIV");

    if (divs)
    {
        for(var idiv = 0; idiv < divs.length; idiv++)
        {
            var div = divs[idiv];

            if (div.className)
            {
                if (div.className == "dxpopupbubble" || div.className == "hspopupbubble" || div.className == "hsglossaryreference")
                {
                    div.style.visibility = "hidden";
                }
            }
        }
    }
}

/* Show a popup box */
function showBox(Name,e,button)
{
    if (window.event)
    {
        e = window.event;
    }

    hideBoxes();

    if(!button)
    {
        button = sourceElement(e);
    }
    cancelEvent(e);
    var div = documentElement(Name);

    if (div && button)
    {
        div.style.height = "";
        div.style.left = (getElementPosition(button).left) + "px";
        div.style.top = (getElementPosition(button).top + button.offsetHeight) + "px";
        div.style.visibility = "visible";
    }
}

/* End Common utility functions */


/* Shows a popup parameter box */
function showParamBox(e)
{
    if (window.event)
    {
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

    if (div && button)
    {
        // Have the browser size the box
        div.style.height = "";
        div.style.width = "";

        // Clear any tooltip so it doesn't appear above the popup
        button.title = "";

        pixelTop = getElementPosition(button).top + 14;

        // Check if the box would go off the bottom of the visible area
        documentHeight = document.body.clientHeight;
        boxHeight = div.clientHeight;
        if (pixelTop + boxHeight > documentHeight)
        {
            // If the box won't fit both above and below the link
            //  using the default width then make the box wider
            if (boxHeight >= pixelTop)
            {
                div.style.width = "80%";
            }
            else
            {
                pixelTop = pixelTop - 14 - boxHeight;
            }
        }
        div.style.top = pixelTop + "px";
        documentWidth = document.body.clientWidth;
        boxWidth = div.clientWidth;
        pixelLeft = button.offsetLeft;

                // Check if the box would go off the edge of the visible area           
        if (pixelLeft + boxWidth > documentWidth)
        {
            pixelLeft = documentWidth - boxWidth - 5;
        }
        div.style.left = pixelLeft + "px";
                
                // Show the box
        div.style.visibility = "visible";
    }
}


/* Begin non-scrolling region support */

var isDesignTime = false;

function bodyResize()
{
    resizeBan();
}

function bodyClick(e)
{
    if (window.event)
    {
        e = window.event;
    }
    if(sourceElement(e))
    {
        try
        {
            if(sourceElement(e).tagName != "INPUT")
            {
                if (sourceElement(e).className != "languageFilter" && sourceElement(e).className != "membersFilter" && sourceElement(e).className != "communityFilter")
                {
                hideBoxes();
                resizeBan();
                };
            }
        }
        catch(e)
        { }
    }
}

function resizeBan()
{
    if (msieversion() > 4)
    {
        try
        {
            if (document.body.clientWidth==0)
            {
                return;
            }

            hideBoxes();

            var oBanner= documentElement("pagetop");
            var oText= documentElement("pagebody");
            var oFooter = documentElement("nonscrollingpagefooter");
            var footerHeight = 0;
            if (oFooter != null)
            {
                footerHeight = oFooter.offsetHeight;
            }

            if (oText == null)
            {
                return;
            }

            var oBannerrow1 = documentElement("projectnamebanner");
            var oTitleRow = documentElement("pagetitlebanner");

            if (oBannerrow1 != null)
            {
                var iScrollWidth = document.body.scrollWidth;
                oBannerrow1.style.marginRight = 0 - iScrollWidth;
            }

            if (oTitleRow != null)
            {
                oTitleRow.style.padding = "0px 10px 0px 22px; ";
            }

            if (oBanner != null)
            {
                document.body.scroll = "no";
                oText.style.overflow= "auto";
                oBanner.style.width= document.body.offsetWidth-2;
                oText.style.top=0;

                if (document.body.offsetHeight > oBanner.offsetHeight)
                {
                    oText.style.height= document.body.offsetHeight - (oBanner.offsetHeight+4) - footerHeight;
                }
                else
                {
                    oText.style.height=0;
                }

                if(scrollPos!=null)
                {
                    oText.scrollTop = scrollPos;
                    scrollPos = null;
                }
            }

            try
            {
                documentElement("nstext").setActive();
            }
            catch(e)
            {
            }
        }
        catch(e)
        {
        }
    }
}

/* End non-scrolling region support */


/* Begin Document load/unload/print support */

/* Called before custom body load script */
function bodyLoadCommonBefore()
{
    if (scrollPos == null && curURL.indexOf("#") != -1)
    {
        var oBanner= documentElement("pagetop");
        scrollPos = document.body.scrollTop - oBanner.offsetHeight;
    }

    resizeBan();

    document.body.onclick = bodyClick;
    document.body.onresize = bodyResize;
    window.onbeforeprint = beforePrint;
    window.onafterprint = afterPrint;
    window.onunload = windowUnload;
}

function findFrame(Name)
{
    var frameObject = parent.frames[Name];
    if((!frameObject) && parent.parent)
    {
        frameObject = parent.parent.frames[Name];
    }
    return frameObject;
}

function tocDocument()
{
    try
    {
        return findFrame("webnavbar").document.getElementById("cntNavtoc");
    } catch(e) {}
}

/* Called after custom body load script */
function bodyLoadCommonAfter()
{
    wireUpEventHandlers();
    loadSettings();

    // make body visible, now that we're ready to render
    document.body.style.visibility = "visible";
    
    // if this is web output, sync the TOC
    syncToc();    
}

/* Synchronize the web Table of Contents */
function syncToc()
{
    try
    {
        var tocDoc = tocDocument();
    } catch(e) {}
    
    if (tocDoc)
    {
        try
        {
            tocDoc.contentWindow.pageLoaded(location.href);
        } catch(e) { setTimeout('syncToc();',500); }
    }
}

/* Saves the scroll position */
function windowUnload()
{
    saveSettings();
}

/* Common settings load script */
function loadSettingsCommon()
{
    // load the expand / collapse states
    loadSections();
    dxSetSelectedTabOnTabstrips();
}

/* Common settings save script */
function saveSettingsCommon()
{
    // save the expand / collapse states
    saveSections();
    dxSaveSelectedTabs();
}

/* Removes the non-scrolling region and ensures everything is visible for print */
function beforePrint()
{
    var allElements;

    if (window.text)
    {
        document.all.text.style.height = "auto";
    }

    allElements = document.getElementsByTagName("*");

    for(var i=0; i < allElements.length; i++)
    {
        if (allElements[i].tagName == "BODY")
        {
            allElements[i].scroll = "yes";
        }
        if (allElements[i].id == "pagetop")
        {
            allElements[i].style.margin = "0px 0px 0px 0px";
            allElements[i].style.width = "100%";
        }
        if (allElements[i].id == "pagebody")
        {
            allElements[i].style.overflow = "visible";
            allElements[i].style.top = "5px";
            allElements[i].style.width = "100%";
            allElements[i].style.padding = "0px 10px 0px 30px";
        }
        if (allElements[i].id == "pagetoptable2row1" || allElements[i].id == "feedbacklink")
        {
            allElements[i].style.display = "none";
        }
        if (allElements[i].className == "LanguageSpecific")
        {
            allElements[i].style.display = "block";
        }
    }
}

/* Refresh the page after a print */
function afterPrint()
{
    document.location.reload();
}

/* End Document load/unload/print support */


/* Begin User Data support */

function getDataCache()
{
    if (isDesignTime)
    {
        if (getWindowExternal())
        {
            if (window.external.IsInnovasysDesigner)
            {
                return window.external;
            }
        }
    }
    
    return documentElement("userDataCache");
}

function getWindowExternal()
{
    try
    {
        return window.external;
    }
    catch(e)
    {
    }
}

function load(key)
{
    var dataCacheObject;

    try
    {
        dataCacheObject = getDataCache();
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
    catch(e)
    {
    }
}

function save(key, value)
{
    var dataCacheObject;

    try
    {
        dataCacheObject = getDataCache();
        if (dataCacheObject) {
            dataCacheObject.setAttribute(key, value);
            dataCacheObject.save("innSettings");
        }
        else
            dxSaveCookieValue(key, value, 60, "/", "", "");
        }
    catch(e)
    {
    }
}

function removeAttribute(key)
{
    var dataCacheObject;

    try
    {
        dataCacheObject = getDataCache();
        dataCacheObject.removeAttribute(key);
        dataCacheObject.save("innSettings");
    }
    catch(e)
    {
    }
}

/* End User Data support */


/* Begin Expanding sections support */

var collapsedSections = new Array();

/* Wires up the expand / collapse functionality */
function wireUpEventHandlers()
{
    var elements=document.getElementsByTagName("SPAN");

    for(var i=0;i<elements.length;i++)
    {
        var span = elements[i];
        if(span.className == "expandcollapse")
        {
            span.onclick = toggleExpanded;
            span.onkeypress = toggleExpandedOnKey;
        }
    }

    if(documentElement("toggleExpandedAll"))
    {
        documentElement("toggleExpandedAll").onclick = toggleExpandedAll;
        documentElement("toggleExpandedAll").onkeypress = toggleExpandedAllOnKey;
    }
}

function toggleExpanded(img)
{
    // Firefox passes in the event object from the event handler, so
    //  we check for that and set to null
    if(img)
    {
        if(img.tagName == null)
        {
            e=img;
            img=null;
        }
    }

    // Find the expand & collapse image
    if(!img)
    {
        if (window.event)
            e = window.event;

        var img = sourceElement(e)    
        if(img)
        {
            while(img)
            {
                if(img.className == "expandcollapse" && img.tagName == "SPAN")
                    break;
                else
                    img = img.parentNode;
            }
            if(img)
                img = findExpandCollapseImage(img);
        }
    }


    if(img)
    {
        if(isSectionCollapsed(img.id) == true)
        {
            img.src = documentElement("collapseImage").src;
            expandSection(img);
            removeCollapsedItem(img.id);
            if(img.id.indexOf("Family", 0) == 0)
            {
                protectedMembers = "on";
                configureMembersFilterCheckboxes();
                changeMembersFilterLabel();
            }
        }
        else
        {
            img.src = documentElement("expandImage").src;
            collapseSection(img);
            addCollapsedSection(img.id);
        }

        setCollapseAll();
    }
}

function findExpandCollapseImage(sourceElement)
{
    var e;
    var elements;

    if(sourceElement.tagName == "IMG" && sourceElement.className == "toggle")
    {
        return(sourceElement);
    }
    else
    {
        if(sourceElement)
        {
            elements = sourceElement.getElementsByTagName("IMG");

            for(var i=0;i<elements.length;i++)
            {
                e = elements[i];
                if(e.className == "toggle")
                {
                    return(e);
                }
            }
        }
    }
}

function toggleExpandedAll()
{
    var elements = document.getElementsByName("toggleSwitch");
    var toggleExpandedAllImage = documentElement("toggleExpandedAllImage");

    // All currently collapsed
    if(isSectionCollapsed(toggleExpandedAllImage.id) == true)
    {
        toggleExpandedAllImage.src = documentElement("collapseImage").src;
        removeCollapsedItem(toggleExpandedAllImage.id);

        for(var i=0;i<elements.length;i++)
        {
            expandSection(elements[i]);
            elements[i].src = documentElement("collapseImage").src;
            removeCollapsedItem(elements[i].id);

            if(elements[i].id.indexOf("Protected",0) == 0)
            {
                protectedMembers = "on";
            }
        }

        setToggleExpandedAllLabel(false);
    }
    // All currently expanded
    else
    {
        toggleExpandedAllImage.src = documentElement("expandImage").src;
        addCollapsedSection(toggleExpandedAllImage.id);

        for(var i=0;i<elements.length;i++)
        {
            collapseSection(elements[i]);
            elements[i].src = documentElement("expandImage").src;
            addCollapsedSection(elements[i].id);
        }

        setToggleExpandedAllLabel(true);
    }
}

function toggleExpandedAllOnKey(e)
{
    if(window.event)
    {
        e = window.event;
    }

    if(e.keyCode == 13)
    {
        toggleExpandedAll();
    }
}

function toggleExpandedOnKey(e)
{
    if(window.event)
    {
        e = window.event;
    }

    if(e.keyCode == 13)
    {
        toggleExpanded(findExpandCollapseImage(e.srcElement));
    }
}

function getNextSibling(n)
{
    n=n.nextSibling;
    while(n)
    {
        if(n.nodeType == 1)
            return n;
        else
            n=n.nextSibling;
    }
}

function expandSection(imageItem)
{
    if(imageItem.id != "toggleExpandedAllImage")
    {
        getNextSibling(imageItem.parentNode.parentNode).style.display = "";
    }
}

function collapseSection(imageItem)
{
    if(imageItem.id != "toggleExpandedAllImage")
    {
        getNextSibling(imageItem.parentNode.parentNode).style.display = "none";
    }
}

function setCollapseAll()
{
    var toggleElements = document.getElementsByName("toggleSwitch");
    var allCollapsed = true;

    if(document.getElementById("expandAllLabel"))
    {
        for(var i=0; i < toggleElements.length; i++)
        {
            allCollapsed = allCollapsed && isSectionCollapsed(toggleElements[i].id);
        }

        if(allCollapsed)
        {
            this.src = documentElement("expandAllImage").src;
            addCollapsedSection("toggleExpandedAllImage");
        }
        else
        {
            this.src = documentElement("collapseAllImage").src;
            removeCollapsedItem("toggleExpandedAllImage");
        }

        setToggleExpandedAllLabel(allCollapsed);
    }
}

function setToggleExpandedAllLabel(allCollapsed)
{
    var labelElement;

    labelElement = document.getElementById("collapseAllLabel");

    if(labelElement == null)
    {
        return;
    }

    labelElement.style.display = "none";
    labelElement = document.getElementById("expandAllLabel");
    labelElement.style.display = "none";

    if(allCollapsed)
    {
        labelElement = document.getElementById("expandAllLabel");
        labelElement.style.display = "inline";
    }
    else
    {
        labelElement = document.getElementById("collapseAllLabel");
        labelElement.style.display = "inline";
    }
}

function isSectionCollapsed(imageId)
{
    for(var i=0; i < collapsedSections.length; ++i)
    {
        if(imageId == collapsedSections[i])
        {
            return true;
        }
    }

    return false;
}

function addCollapsedSection(imageId)
{
    if(isSectionCollapsed(imageId) == false)
    {
        collapsedSections[collapsedSections.length] = imageId;
    }
}

function removeCollapsedItem(imageId)
{
    for(var i=0; i < collapsedSections.length; ++i)
    {
        if(imageId == collapsedSections[i])
        {
            collapsedSections.splice(i, 1);
        }
    }
}

function saveSections()
{
    var x = 0;

    cleanUserDataStore();
    for(var i=0; i < collapsedSections.length; ++i)
    {
        if(shouldSave(collapsedSections[i]) == true)
        {
            save("imageValue" + x, collapsedSections[i]);
            x++;
        }
    }
}

function loadSections()
{
    var i = 0;
    var imageId = load("imageValue" + i);

    while(imageId != null)
    {
        var imageItem = document.getElementById(imageId);

        if(imageItem != null)
        {
            if(imageItem.id.indexOf("Family", 0) == 0)
            {
                if(protectedMembers == "on")
                {
                    toggleExpanded(imageItem);
                }
            }
            else
            {
                toggleExpanded(imageItem);
            }
        }
        else
        {
            addCollapsedSection(imageId);
        }

        i++;
        imageId = load("imageValue" + i);
    }
    setCollapseAll();
}

function cleanUserDataStore()
{
    var i = 0;
    var imageId = load("imageValue" + i);

    while(imageId != null)
    {
        removeAttribute("imageValue" + i);
        i++;
        imageId = load("imageValue" + i);
    }
}

function shouldSave(imageId)
{
    var toggleName;

    if(imageId == "toggleExpandedAllImage")
    {
        return false;
    }

    toggleName = "procedureToggle";

    if(imageId.indexOf(toggleName, 0) == 0)
    {
        return false;
    }

    toggleName = "sectionToggle";

    if(imageId.indexOf(toggleName, 0) == 0)
    {
        return false;
    }

    return true;
}
function openSectionById(id)
{
    var e=documentElement(id);

    if(e)
    {
        if(isSectionCollapsed(e.id) == true)
        {
            toggleExpanded(e);
        }
    }
}

/* End Expand / Collapse */


/* Begin save / Restore Scroll Position */

var scrollPos = null;

function loadAll()
{
    var historyObject = getHistoryObject();

    if(historyObject)
    {
        var scrollValue = historyObject.getAttribute("Scroll");

        if(scrollValue)
        {
            if(scrollValue != 0)
            {
                try
                {
                    scrollPos = scrollValue;
                    documentElement("pagebody").scrollTop = scrollPos;
                }
                catch(e)
                {
                }
            }
        }
    }
}
function saveAll()
{
    var historyObject = getHistoryObject();

    if(historyObject)
    {
        try
        {
            historyObject.setAttribute("Scroll", documentElement("pagebody").scrollTop);
        }
        catch(e)
        {
        }
    }
}
function getHistoryObject()
{
    if (isDesignTime)
    {
        try
        {
            var externalObject = window.external;

            if (externalObject)
            {
                if (externalObject.IsInnovasysDesigner)
                {
                    return window.external;
                }
                else
                {
                    externalObject = false;
                }
            }
        }
        catch(e)
        {
        }
    }
    
    if (!externalObject)
    {
        return documentElement("allHistory");
    }
}

/* End save / Restore Scroll Position */


/* Begin Copy Code */

function copyCode(key)
{
    var trElements = getTABLE(key).getElementsByTagName("tr");
    for(var i=0; i < trElements.length; ++i)
    {
        if(getTABLE(key) == getTABLE(trElements[i]) && getTR(key) != trElements[i])
        {
            window.clipboardData.setData("Text", trElements[i].innerText);
            break;
        }
    }
}

function getTR(obj)
{
    while(obj)
    {
        if(obj.tagName == "TR")
            return obj;
        else
            obj = obj.parentElement;
    }
}

function getTABLE(obj)
{
    while(obj)
    {
        if(obj.tagName == "TABLE")
            return obj;
        else
            obj = obj.parentElement;
    }
}

function changeCopyCodeIcon(key,highlight)
{
    var imageElements = document.getElementsByName("ccImage");

    for(var i=0; i<imageElements.length; ++i)
    {
        try
        {
            if(imageElements[i].parentElement == key)
            {
                if(highlight)
                {
                    imageElements[i].src = documentElement("copyHoverImage").src;
                }
                else
                {
                    imageElements[i].src = documentElement("copyImage").src;
                }
            }
        }
        catch(e)
        {
        }
    }
}

function copyCode_CheckKey(key)
{
    var e;

    if (window.event)
    {
        e = window.event;
    }

    if(e.keyCode == 13)
    {
        copyCode(key);
    }
}

/* End Copy Code */


/* Begin XML expand / collapse */

// Detect and switch the display of CDATA and comments from an inline view
//  to a block view if the comment or CDATA is multi-line.
function f(e)
{
    // if this element is an inline comment, and contains more than a single
    //  line, turn it into a block comment.
    if (e.className == "ci")
    {
        if (e.children(0).innerText.indexOf("\n") > 0)
        {
            fix(e, "cb");
        }
    }

    // if this element is an inline cdata, and contains more than a single
    //  line, turn it into a block cdata.
    if (e.className == "di")
    {
        if (e.children(0).innerText.indexOf("\n") > 0)
        {
            fix(e, "db");
        }
    }

    // remove the id since we only used it for cleanup
    e.id = "";
}

// Fix up the element as a "block" display and enable expand/collapse on it
function fix(e, cl)
{
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
function ch(e)
{
    var i;

    // find the +/- symbol
    var mark = e.children(0).children(0);

    // if it is already collapsed, expand it by showing the children
    if (mark.innerText == "+")
    {
        mark.innerText = "-";
        for (i = 1; i < e.children.length; i++)
        {
            e.children(i).style.display = "block";
        }
    }
    
    // if it is expanded, collapse it by hiding the children
    else if (mark.innerText == "-")
    {
        mark.innerText = "+";
        for (i = 1; i < e.children.length; i++)
        {
            e.children(i).style.display="none";
        }
    }
}

// Change the +/- symbol and hide the children.  This function work on "comment"
//  and "cdata" displays
function ch2(e)
{
    var contents;

    // find the +/- symbol, and the "PRE" element that contains the content
    var mark = e.children(0).children(0);
    contents = e.children(1);

    // if it is already collapsed, expand it by showing the children
    if (mark.innerText == "+")
    {
        mark.innerText = "-";

        // restore the correct "block"/"inline" display type to the PRE
        if (contents.className == "db" || contents.className == "cb")
        {
            contents.style.display = "block";
        }
        else
        {
            contents.style.display = "inline";
        }
    }
    // if it is expanded, collapse it by hiding the children
    else
    {
        if (mark.innerText == "-")
        {
            mark.innerText = "+";
            contents.style.display = "none";
        }
    }
}

// Handle a mouse click
function cl()
{
    var e = window.event.srcElement;

    // make sure we are handling clicks upon expandable container elements
    if (e.className != "c")
    {
        e = e.parentElement;
        if (e.className != "c")
        {
            return;
        }
    }
    e = e.parentElement;

    // call the correct funtion to change the collapse/expand state and display
    if (e.className == "e")
    {
        ch(e);
    }
    
    if (e.className == "k")
    {
        ch2(e);
    }
}

// Dummy function for expand/collapse link navigation - trap onclick events instead
function ex()
{
}

// Erase bogus link info from the status window
function h()
{
    window.status=" ";
}

/* End XML Expand / Collapse */


/* .NET specific script */

// Current language
var curLang = "";

// To prevent 'access denied' errors in the authoring environment
function dxErrorHandler(msg,url,line) {
    if(url=="about:blank")
    {
        return true;
    }
}
if (window.location.href == 'about:blank')
{
    window.onerror = dxErrorHandler;
}

function bodyLoad()
{
    var i;
    var b;
    var l;

    bodyLoadCommonBefore();

    // Check the context window for current language.
    try
    {
        for (i=1; i< window.external.ContextAttributes.Count; i++)
        {
            if(window.external.ContextAttributes(i).Name.toUpperCase()=="DEVLANG")
            {
                b = window.external.ContextAttributes(i).Values.toArray();
                curLang = b[0].toUpperCase();
            }
        }
    }
    catch(e)
    {
    }

    if (curLang != null)
    {
        if (curLang.indexOf("VB") != -1)
        {
            curLang = "VB";
        }
        if (curLang.indexOf("VC") != -1)
        {
            curLang = "CPP";
        }
        if (curLang.indexOf("C++") != -1)
        {
            curLang = "CPP";
        }
        if (curLang.indexOf("CSHARP") != -1)
        {
            curLang = "CS";
        }
        if (curLang.indexOf("JSCRIPT") != -1)
        {
            curLang = "JScript";
        }
    }

    if (curLang == null)
    {
        l = "";
        var multipleLang = false;
        var cLang = "";

        try
        {
            l = window.external.Help.FilterQuery.toUpperCase();
        }
        catch(e)
        {
        }

        if (l.indexOf("VB") != -1)
        {
            cLang = "VB";
        }

        if (l.indexOf("VC") != -1)
        {
            if (cLang != null)
            {
                multipleLang = true;
            }
            cLang = "CPP";
        }

        if (l.indexOf("C#") != -1)
        {
            if (cLang != null)
            {
                multipleLang = true;
            }
            cLang = "CS";
        }

        if (l.indexOf("CSHARP") != -1)
        {
            if (cLang != null)
            {
                multipleLang = true;
            }
            cLang = "CS";
        }

        if (l.indexOf("JSCRIPT") != -1)
        {
            if (cLang != null)
            {
                multipleLang = true;
            }
            cLang = "JScript";
        }

        if (l.indexOf("JSHARP") != -1)
        {
            if (cLang != null)
            {
                multipleLang = true;
            }
            cLang = "JSHARP";
        }

        if (multipleLang == false)
        {
            curLang = cLang;
        }
    }

    bodyLoadCommonAfter();
    if (parent)
        parent.loaded = true;
}

function loadSettings()
{
    // load the languages   
    loadLanguages();
    configureLanguageCheckboxes();
    displayLanguages();

    // load the member options
    loadMembersFilter();
    configureMembersFilterCheckboxes();
    changeMembersFilterLabel();
    loadSettingsCommon();
}

function saveSettings()
{
    saveLanguages();
    saveMembersFilter();
    saveSettingsCommon();
}

/* Begin Member Filtering */

var inheritedMembers;
var protectedMembers;

function configureMembersFilterCheckboxes()
{
    var checkbox;
        
    checkbox = document.getElementById("inheritedCheckbox");
    if(checkbox != null)
    {
        if(inheritedMembers == "on")
        {
            checkbox.checked = true;
        }
        else
        {
            checkbox.checked = false;
        }
    }
        
    checkbox = document.getElementById("protectedCheckbox");
    if(checkbox != null)
    {
        if(protectedMembers == "on")
        {
            checkbox.checked = true;
        }
        else
        {
            checkbox.checked = false;
        }
    }
}

function setMembersFilter(key)
{
    if(key.id == "inheritedCheckbox")
    {
        if(key.checked == true)
        {
            inheritedMembers = "on";
        }
        else
        {
            inheritedMembers = "off";
        }
                
        updateInheritedMembers();
    }

    if(key.id == "protectedCheckbox")
    {
        if(key.checked == true)
        {
            protectedMembers = "on";
        }
        else
        {
            protectedMembers = "off";
        }
                
        updateProtectedMembers();
    }
        
    changeMembersFilterLabel();
}

function updateInheritedMembers()
{
    var tablerows = document.getElementsByTagName("tr");
    var i;

    if(inheritedMembers == "off")
    {
        for(i = 0; i < tablerows.length; ++i)
        {
            if (tablerows[i].id == "inheritedMember")
                tablerows[i].style.display = "none";
        }
    }
    else
    {
        for(i = 0; i < tablerows.length; ++i)
        {
            if (tablerows[i].id == "inheritedMember")
                tablerows[i].style.display = "";
        }
    }
}

function updateProtectedMembers()
{
    var toggleImages = document.getElementsByName("toggleSwitch");
    var i;
        
    if(protectedMembers == "off")
    {
        for(i = 0; i < toggleImages.length; ++i)
        {
            if(toggleImages[i].id.indexOf("protected", 0) == 0)
            {
                if(isSectionCollapsed(toggleImages[i].id) == false)
                {
                    toggleExpanded(toggleImages[i]);
                }
            }
        }
    }
    else
    {
        for(i = 0; i < toggleImages.length; ++i)
        {
            if(toggleImages[i].id.indexOf("protected", 0) == 0)
            {
                if(isSectionCollapsed(toggleImages[i].id) == true)
                {
                    toggleExpanded(toggleImages[i]);
                }
            }
        }
    }
}

function changeMembersFilterLabel()
{
    var filtered = false;
        
    if((inheritedMembers=="off") || (protectedMembers=="off"))
    {
        filtered = true;
    }
                
    var labelElement = document.getElementById("showAllMembersLabel");    
    if(labelElement == null)
    {
        return;
    }                
    labelElement.style.display = "none";        

    labelElement = document.getElementById("filteredMembersLabel");
    labelElement.style.display = "none";        

    if(filtered)
    {
        labelElement = document.getElementById("filteredMembersLabel");
        labelElement.style.display = "inline";
    }
    else
    {
        labelElement = document.getElementById("showAllMembersLabel");
        labelElement.style.display = "inline";
    }
}

function loadMembersFilter()
{
    var value = load("inheritedMembers");

    if(value == null)
    {
        inheritedMembers = "on";
    }
    else
    {
        inheritedMembers = value;
    }
                
    value = load("protectedMembers");

    if(value == null)
    {
        protectedMembers = "on";
    }
    else
    {
        protectedMembers = value;
    }
                
    if(inheritedMembers=="off")
    {
        updateInheritedMembers();
    }

    if(protectedMembers=="off")
    {
        updateProtectedMembers();
    }
}

function saveMembersFilter()
{
    save("inheritedMembers", inheritedMembers);
    save("protectedMembers", protectedMembers);
}

/* End Member Filtering */

/* Language Filtering */

var languageNames;
var languageStates;

function configureLanguageCheckboxes()
{
    var checkbox;
        
    for(var i=0;i<languageNames.length;i++)
    {
        checkbox = documentElement(languageNames[i] + "Checkbox");

        if(languageStates[i] == "on")
        {
            checkbox.checked = true;
        }
        else
        {
            checkbox.checked = false;
        }
    }
}

function setLanguage(key)
{
    var languageName = key.id.substring(0,key.id.indexOf("Checkbox"));

    if(getLanguageState(languageName) == "on")
    {
        // Always have at least one selected
        if(getLanguageTickedCount() == 1)
        {
            key.checked = true;
            return;
        }

        setLanguageState(languageName,"off");
        key.checked = false;
    }
    else
    {
        setLanguageState(languageName,"on");
        key.checked = true;
    }
        
    // Update the content to reflect the new language filter
    displayLanguages();

    // Reset the selected tab
    dxRefreshSelectedTabOnTabstrips();
}

function displayLanguages()
{
    var pres = document.getElementsByTagName("DIV");
    var pre;
    var found;
    var languageName;

    if (pres)
    {
        for (var iPre = 0; iPre < pres.length; iPre++)
        {
            pre = pres[iPre];
            
            if (pre.id && pre.className) {
                if (pre.className == "LanguageSpecific"
                    || (pre.className.indexOf("DxTab ") == 0 && pre.className.length == 5)
                    || pre.className.lastIndexOf("DxTab") == pre.className.length - 5) {
                    
                    found = true;

                    if (pre.id.substring(pre.id.length-1,pre.id.length) != "_")
                    {                    
                        for(var i=0;i<languageNames.length;i++)
                        {
                            if(languageStates[i] == "off")
                            {
                                languageName = languageNames[i].toUpperCase();

                                // For each language specific element except the Syntax, treat CPP2005 as CPP
                                if (languageName == "CPP2005" && pre.id.toUpperCase().indexOf("SYNTAX") == -1)
                                {
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
                    
                    if(found)
                    {
                        pre.style.display = "block";
                    }
                    else
                    {
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

function getLanguageState(LanguageName)
{
    for(var i=0;i<languageNames.length;i++)
    {
        if(languageNames[i] == LanguageName)
        {
            return(languageStates[i]);
        }
    }
}

function setLanguageState(LanguageName,Value)
{
    for(var i=0;i<languageNames.length;i++)
    {
        if(languageNames[i] == LanguageName)
        {
            languageStates[i] = Value;
        }
    }
}

function getLanguageTickedCount()
{
    var tickedCount=0;
    var labelElement;
        
    for(var i=0;i<languageNames.length;i++)
    {
        if(languageStates[i] == "on")
        {
            tickedCount++;
        }
    }
        
    return(tickedCount);
}

function changeLanguageFilterLabel()
{
    var tickedCount=0;
    var labelElement;
    var languageName;
        
    if(!document.getElementById("showAllLabel"))
    {
        return;
    }
        
    for(var i=0;i<languageNames.length;i++)
    {
        if(languageStates[i] == "on")
        {
            tickedCount++;
        }
                        
        labelElement = documentElement(languageNames[i] + "label");
        
        if(labelElement != null)
        {
            labelElement.style.display = "none";
        }
    }
        
    document.getElementById("showAllLabel").style.display = "none";
    document.getElementById("multipleLabel").style.display = "none";
        
    if(tickedCount == languageNames.length)
    {
        document.getElementById("showAllLabel").style.display = "inline";
    }
    else if ((tickedCount > 1) && (tickedCount < languageNames.length))
    {
        if((tickedCount == 2) && (getLanguageState("VB") == "on") && (getLanguageState("vbUsage") == "on"))
        {
            document.getElementById("VBLabel").style.display = "inline";
        }
        else
        {
            document.getElementById("multipleLabel").style.display = "inline";
        }
    }
    else if (tickedCount == 1)
    {
        for(var i=0;i<languageNames.length;i++)
        {
            if(languageStates[i] == "on")
            {
                if(languageNames[i] == "vbUsage")
                {
                    languageName = "VB";
                }
                else
                {
                    languageName = languageNames[i];
                }
                
                document.getElementById(languageName + "Label").style.display = "inline";
            }
        }
    }
}

function loadLanguages()
{
    var languageName;
    var language;
    var allNull;

    // Build an array of this pages language names and state
    languageNames = new Array();
    languageStates = new Array();
        
    var elements = document.getElementsByName("languageFilter");
        
    allNull = true;
    
    for(var i=0;i<elements.length;i++)
    {
        var input = elements[i];
                
        languageNames[i] = input.id.substring(0,input.id.indexOf("Checkbox"));
        var value = load("lang_" + languageNames[i]);
        
        if(value == null)
        {
            languageStates[i] = "on";
        }
        else
        {
            allNull = false;
            languageStates[i] = value;
        }
    }
        
        // If no language preference has been established and we have an indicator of the current
        //  language, set the languages filtered to only the current language
    if(allNull && curLang.length > 0)
    {
        for(var i=0;i<elements.length;i++)
        {
            if(languageNames[i].toUpperCase() == curLang.toUpperCase())
            {
                languageStates[i] = "on";
            }
            else
            {
                languageStates[i] = "off";
            }
        }
    }

    dxSetSelectedTabOnTabstrips();
}

function saveLanguages()
{
    if(languageNames)
    {
        for(var i=0;i<languageNames.length;i++)
        {
            var value = languageStates[i];
            save("lang_" + languageNames[i], value);
        }
    }
}

/* End Language Filtering */

/* .NET Framework Help Topic Resolution */

// This function is Copyright 2006 Innovasys Limited. No reproduction or usage
//  allowed other than in documentation generated by Innovasys Document! X v5
function resolveHelp2Keyword(Keyword,OnlineKeyword)
{

    var URL="";

    try
    {
        // Try the current namespace
        URL = findHelp2Keyword(getCurrentHelp2Namespace(),Keyword);
        if (URL == "")
        {
            // Try the likely namespaces first, most recent first
            URL = findHelp2Keyword("MS.VSCC.v80",Keyword);
            if (URL == "")
            {
                URL = findHelp2Keyword("MS.VSCC.2003",Keyword);
                if (URL == "")
                {
                    URL = findHelp2Keyword("MS.VSCC",Keyword);
                }
            }
        }

        // URL found in one of the known VSCC namespaces
        if (URL != "")
        {
            return URL;
        }
        // For future proofing, try other VSCC namespaces
        else
        {
            var RegistryWalker = new ActiveXObject("HxDs.HxRegistryWalker");
            var Namespaces = RegistryWalker.RegisteredNamespaceList("MS.VSCC");
            var Namespace,NamespaceName,Session,Topics,Topic;

            if (Namespaces.Count > 0)
            {
                for (n=1;n<=Namespaces.Count;n++)
                {
                    Namespace = Namespaces.Item(n);
                    NamespaceName = Namespace.Name;
                    if (NamespaceName.substring(0,7) == "MS.VSCC")
                    {
                        switch(NamespaceName)
                        {
                            case "MS.VSCC.v80":
                                break;
                            case "MS.VSCC.2003":
                                break;
                            case "MS.VSCC":
                                break;
                            default:
                                URL = findHelp2Keyword(NamespaceName);
                                if (URL != "")
                                {
                                    return Topics(1).URL;
                                }
                        }
                    }
                }
            }
        }
    }
    catch(e) {}

    // No match found in any applicable namespace
    return "http://msdn.microsoft.com/query/dev10.query?appId=Dev10IDEF1&l=EN-US&k=k(" + OnlineKeyword + ")&rd=true"
}

function findHelp2Keyword(NamespaceName,Keyword)
{
    var Session,Topics;

    if (NamespaceName.length > 0)
    {
        try
        {
            Session = new ActiveXObject("HxDs.HxSession");
            Session.Initialize("ms-help://" + NamespaceName, 0);
            Topics = Session.Query(Keyword, "!DefaultAssociativeIndex", 0, "");
            if (Topics.Count > 0)
            {
                return Topics(1).URL;
            }
        }
        catch(e) {}
    }
    return "";
}

function navigateToHelp2Keyword(Keyword,OnlineKeyword,ReplacePage)
{
    window.status = "Resolving link. Please wait a moment...";
    var URL = resolveHelp2Keyword(Keyword,OnlineKeyword);
    window.status="";
    if (ReplacePage == true)
    {
        location.replace(URL);
    }
    else
    {
        location.href = URL;
    }
}

function getCurrentHelp2Namespace()
{
    var namespace = "";
    var location = window.location;

    if (location.protocol == "ms-help:")
    {
        namespace = location.hostname;
        if (namespace.substring(0, 2) == "//")
            namespace = namespace.substring(2);
    }

    return namespace;
}

/* End .NET Framework Help Topic Resolution */

/* GetSetting SaveSetting Alias */
function dxGetSetting(key) {
    return load(key);
}

function dxSaveSetting(key,value) {
    save(key,value)
}
/* End GetSetting SaveSetting Alias */

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