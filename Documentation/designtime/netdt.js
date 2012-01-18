function dxDTLoad(){
    // set the scroll position
    try { scrollPos = load("dtscrollpos"); resizeBan(); HSShowAllCSections(); }
    catch(e){}    
}

function dxDTSave(){
    // save the scroll position
    save("dtscrollpos",documentElement("pagebody").scrollTop);
    saveSettings();
    try { ic_saveCommunityFeatureStates(); } catch (ex) { }
}

var isDesignTime = true;