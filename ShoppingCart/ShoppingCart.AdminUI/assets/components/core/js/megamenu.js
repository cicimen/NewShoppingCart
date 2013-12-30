var megamenu = (function() 
{
 
    var $listItems = $( '.navbar.main li.mega-menu' ),
        $menuItems = $listItems.children( 'a' ),
        $body = $( 'body' );
 
    function init() 
    {
        $menuItems.on( 'hover', open );
        $listItems.on( 'click', function( event ) { event.stopPropagation(); } );
	}
 
	function open( event ) 
    {
    	$listItems.removeClass( 'mega-menu-open' );
        var $item = $( event.currentTarget ).parent( 'li' );
        $item.addClass( 'mega-menu-open' );
        $body.off( 'click' ).on( 'click', close );
        return false;
    }
 
    function close( event ) 
    {
    	$listItems.removeClass( 'mega-menu-open' );
    }
 
    return { init : init };
 
})();

$(function()
{
	megamenu.init();
});