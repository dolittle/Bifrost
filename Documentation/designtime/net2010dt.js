function dxDTLoad(){
    // set the scroll position
    try { scrollPos = load("dtscrollpos"); HSShowAllCSections();}
    catch(e){}      
}

function dxDTSave(){
    // save the scroll position
    if (documentElement("OH_outerContent"))
        save("dtscrollpos",documentElement("OH_outerContent").scrollTop);
    saveSettings(); 
}

var isDesignTime = true;