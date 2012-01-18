var isDesignTime = false;

/* Utility */

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

/* End Utility */

/* Event wiring */

function hsAddLoadEvent(loadFunc) 
{
  var oldFunc = window.onload;
  if (typeof window.onload != 'function') 
  {
    window.onload = loadFunc;
  } 
  else {
    window.onload = function() 
    {
      if (oldFunc) {
        oldFunc();
      }
      loadFunc();
    }
  }
}

/* End Event wiring */