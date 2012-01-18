/* Returns a document element using the Firefox friendly getElementById */
function documentElement(id) {
    return document.getElementById(id);
}

/* Locates the first meta tag with a specific name */
function FindMetaTag(name) {
    var AllMetaTags = document.getElementsByTagName('meta');
    for (var i = 0; i < AllMetaTags.length; i++) {
        var MetaTag = AllMetaTags[i];
        if (MetaTag.name == name)
            return MetaTag;
    }
}

/* Gets the MSHS base url for resources */
function ResourceBaseUrl() {

    if (isDesignTime) {
        return '';
    }
    else {
        // Get the first script tag
        var script = document.getElementById('mshs_support_script');

        // Extract the src which is a full resource url to within our origin .mshc
        var scriptSrc = script.src;

        // Get the portion up to the ; (the base url for resource references)
        var startIndex = scriptSrc.indexOf(';') + 1;
        var scriptUrl = scriptSrc.substring(0, startIndex);

        return scriptUrl;
    }
}

function ToggleSection(id) {
    var element;
    var img;

    // Find the element
    element = documentElement(id);
    img = documentElement(id + "_Image");
    if (element) {
        if (element.className == "hs-collapsed") {
            element.className = "hs-expanded";
            if (img) {
                img.src = ResourceBaseUrl() + "images/hs-expanded.gif";
                if (getStyleAttribute(img, "hs-hidealltext"))
                    img.alt = getStyleAttribute(img, "hs-hidealltext");
                else
                    img.alt = "Hide";
            }
        }
        else {
            element.className = "hs-collapsed";
            if (img) {
                img.src = ResourceBaseUrl() + "images/hs-collapsed.gif";
                if (getStyleAttribute(img, "hs-showalltext"))
                    img.alt = getStyleAttribute(img, "hs-showalltext");
                else
                    img.alt = "Show";
            }
        };
    }
}
function getStyleAttribute(element, styleProp) {
    if (element.currentStyle)
        return element.currentStyle[styleProp];
    else if (window.getComputedStyle)
        return document.defaultView.getComputedStyle(element, null).getPropertyValue(styleProp);
}
function HideOrShowAllCSections(show) {
    var spans
    var divs

    spans = document.getElementsByTagName("SPAN");
    if (spans) {
        for (var spanindex = 0; spanindex < spans.length; spanindex++) {
            if ((spans[spanindex].className == "hs-collapsed" && show) || (spans[spanindex].className == "hs-expanded" && !show)) {
                ToggleSection(spans[spanindex].id)
            }
        }
    }
    divs = document.getElementsByTagName("DIV")
    if (divs) {
        for (var divindex = 0; divindex < divs.length; divindex++) {
            if ((divs[divindex].className == "hs-collapsed" && show) || (divs[divindex].className == "hs-expanded" && !show)) {
                ToggleSection(divs[divindex].id)
            }
        }
    }
}
function HideAllCSections() {
    var HSHideAll = documentElement("HSHideAll");
    var HSShowAll = documentElement("HSShowAll");

    HideOrShowAllCSections(false)
    if (HSHideAll) {
        HSHideAll.style.display = "none";
        if (HSShowAll) {
            HSShowAll.style.display = "block";
        }
    }
}
function ShowAllCSections() {
    var HSHideAll = documentElement("HSHideAll");
    var HSShowAll = documentElement("HSShowAll");

    HideOrShowAllCSections(true)
    if (HSShowAll) {
        HSShowAll.style.display = "none";
        if (HSHideAll) {
            HSHideAll.style.display = "block";
        }
    }
}

function ShowPageAddress() {
    alert(location.href);
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

function bodyLoadMain() {

    if (!isDesignTime) {

        // Fix double line breaks
        var linebreaks = document.getElementsByTagName("BR");
        if (linebreaks) {
            for (var linebreakindex = 0; linebreakindex < linebreaks.length; linebreakindex++) {
                var linebreak = linebreaks[linebreakindex];
                var sibling = getNextSibling(linebreak);
                if (sibling && sibling.tagName == "BR")
                    linebreak.parentNode.removeChild(linebreak);
            }
        }

        // Fix double captions and bookmark links
        var anchors = document.getElementsByTagName("A");
        if (anchors) {
            for (var anchorindex = 0; anchorindex < (anchors.length); anchorindex++) {
                var anchor = anchors[anchorindex];
                var anchorCaption = anchor.innerText;
                if (anchorCaption && anchorCaption.substring(0, anchorCaption.length / 2) == anchorCaption.substring(anchorCaption.length / 2))
                    anchor.innerHTML = anchorCaption.substring(anchorCaption.length / 2);
                    
                // Check for bookmark links - currently prefixed with the full page url
                var anchorHref = anchor.href;
                if (anchorHref.indexOf('#') != -1) {
                    var bookmark = anchorHref.substring(anchorHref.indexOf('#'));
                    if (anchorHref.substring(0, anchorHref.indexOf('#')) == location.href) {
                        // Bookmark in this document
                        anchor.target = "_self";
                    }
                }
            }
        }

        // Fix Javascript rules using urls
        var stylesheets = document.styleSheets;
        if (stylesheets && stylesheets.length > 0) {
            for (var stylesheetindex = 0; stylesheetindex < (stylesheets.length); stylesheetindex++) {
                var stylesheet = stylesheets[stylesheetindex];
                var rules;
                if (stylesheet.rules) {
                    rules = stylesheet.rules;
                }
                else {
                    rules = stylesheet.cssRules;
                }
                if (rules) {
                    for (var ruleindex = 0; ruleindex < rules.length; ruleindex++) {
                        var rule = rules[ruleindex];
                        if (rule.style.backgroundImage) {
                            if (rule.style.backgroundImage.substring(0, 4) == 'url(') {
                                var backgroundText = rule.style.backgroundImage;
                                var originalUrl
                                if (rule.style.backgroundImage.indexOf('127.0.0.1') != -1) {
                                    // Chrome - rule returned as full url
                                    originalUrl = backgroundText.substring(backgroundText.indexOf('/', backgroundText.indexOf('127.0.0.1')) + 5, backgroundText.lastIndexOf(')'));
                                }
                                else {
                                    // IE - rule returned as original, with a .. prefix
                                    originalUrl = backgroundText.substring(backgroundText.indexOf('../') + 2, backgroundText.lastIndexOf(')'));
                                }
                                originalUrl = originalUrl.replace("\"", "");
                                var newUrl = 'url(\"' + ResourceBaseUrl() + originalUrl + '\")';
                                backgroundText = newUrl + backgroundText.substring(backgroundText.indexOf(')') + 1);
                                rule.style.backgroundImage = backgroundText;
                            }
                        }
                    }
                }
            }
        }
    }

    if (!isDesignTime) {
        bodyLoad();

        // Show the main content section
        document.getElementById('mainBody').style.display = 'block';
    }

}