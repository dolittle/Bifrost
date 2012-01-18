function hsDTLoad(){
    // set the scroll position
    try { scrollPos = load("dtscrollpos"); HSShowAllCSections(); resizeBan(); }
    catch(e){}
}

function hsDTSave(){
    // save the scroll position
    save("dtscrollpos",documentElement("pagebody").scrollTop);
    saveSettings();
    try {ic_saveCommunityFeatureStates();} catch (ex) {}
}

var isDesignTime = true;