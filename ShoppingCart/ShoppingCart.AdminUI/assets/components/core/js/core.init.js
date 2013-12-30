/* Required for iframe origin policy in our Live Preview - safe to remove in production */
if (window.location != window.parent.location) 
{
	if (document.domain == 'demo.mosaicpro.biz') {
        document.domain = "mosaicpro.biz";
    }
}

var randNum,
	equalHeight,
	genSparklines,
	beautify,
	mt_rand;

(function($, window)
{

	if (!Modernizr.touch && $('[href="#template-options"][data-auto-open]').length)
		$('#template-options').collapse('show');

	// generate a random number
	window.randNum = function()
	{
		return (Math.floor( Math.random()* (1+40-20) ) ) + 20;
	}

	window.equalHeight = function(boxes, substract)
	{
		if (typeof substract == 'undefined')
			var substract = 0;
		
		boxes.height('auto');
		if (parseInt($(window).width()) <= 400)
			return;
			
		var maxHeight = Math.max.apply( Math, boxes.map(function(){ return $(this).height() - substract; }).get());
		boxes.height(maxHeight);
	}

	function parse_url (str, component) {
		var query, key = ['source', 'scheme', 'authority', 'userInfo', 'user', 'pass', 'host', 'port',
		                  'relative', 'path', 'directory', 'file', 'query', 'fragment'],
		                  ini = (this.php_js && this.php_js.ini) || {},
		                  mode = (ini['phpjs.parse_url.mode'] &&
		                		  ini['phpjs.parse_url.mode'].local_value) || 'php',
		                		  parser = {
		                	  php: /^(?:([^:\/?#]+):)?(?:\/\/()(?:(?:()(?:([^:@]*):?([^:@]*))?@)?([^:\/?#]*)(?::(\d*))?))?()(?:(()(?:(?:[^?#\/]*\/)*)()(?:[^?#]*))(?:\?([^#]*))?(?:#(.*))?)/,
		                	  strict: /^(?:([^:\/?#]+):)?(?:\/\/((?:(([^:@]*):?([^:@]*))?@)?([^:\/?#]*)(?::(\d*))?))?((((?:[^?#\/]*\/)*)([^?#]*))(?:\?([^#]*))?(?:#(.*))?)/,
		                	  loose: /^(?:(?![^:@]+:[^:@\/]*@)([^:\/?#.]+):)?(?:\/\/\/?)?((?:(([^:@]*):?([^:@]*))?@)?([^:\/?#]*)(?::(\d*))?)(((\/(?:[^?#](?![^?#\/]*\.[^?#\/.]+(?:[?#]|$)))*\/?)?([^?#\/]*))(?:\?([^#]*))?(?:#(.*))?)/ // Added one optional slash to post-scheme to catch file:/// (should restrict this)
		                  };

		                  var m = parser[mode].exec(str),
		                  uri = {},
		                  i = 14;
		                  while (i--) {
		                	  if (m[i]) {
		                		  uri[key[i]] = m[i];
		                	  }
		                  }

		                  if (component) {
		                	  return uri[component.replace('PHP_URL_', '').toLowerCase()];
		                  }
		                  if (mode !== 'php') {
		                	  var name = (ini['phpjs.parse_url.queryKey'] &&
		                			  ini['phpjs.parse_url.queryKey'].local_value) || 'queryKey';
		                	  parser = /(?:^|&)([^&=]*)=?([^&]*)/g;
		                	  uri[name] = {};
		                	  query = uri[key[12]] || '';
		                	  query.replace(parser, function ($0, $1, $2) {
		                		  if ($1) {uri[name][$1] = $2;}
		                	  });
		                  }
		                  delete uri.source;
		                  return uri;
	}

	function PDFTarget(target)
	{
		var doc = $('html').clone();
		var target = $(target).clone();
		var form = $('#PDFTargetForm');
		if (!form.length) {
			$('<form id="PDFTargetForm"></form>').appendTo('body');
			form = $('#PDFTargetForm');
		}
		
		form.attr('action', rootPath + 'admin/php/pdf.php');
		form.attr('method', 'POST');
		$('<input type="hidden" name="target" value="" />').appendTo(form);
		
		target.find('.hidden-print').remove();
		doc.find('body').html(target);
		var html = doc.html();
		
		form.find('input').val(html);
		form.submit();
	}

	// save to PDF
	$('[data-toggle*="pdf"]').on('click', function(e){
		e.preventDefault();
		PDFTarget($(this).attr('data-target'));
	});

	window.beautify = function (source)
	{
		var output,
			opts = {};

	 	opts.preserve_newlines = false;
		output = html_beautify(source, opts);
	    return output;
	}

	// generate a random number within a range (PHP's mt_rand JavaScript implementation)
	window.mt_rand = function (min, max) 
	{
		var argc = arguments.length;
		if (argc === 0) {
			min = 0;
			max = 2147483647;
		}
		else if (argc === 1) {
			throw new Error('Warning: mt_rand() expects exactly 2 parameters, 1 given');
		}
		else {
			min = parseInt(min, 10);
			max = parseInt(max, 10);
		}
		return Math.floor(Math.random() * (max - min + 1)) + min;
	}

	// scroll to element animation
	function scrollTo(id)
	{
		if ($(id).length)
			$('html,body').animate({scrollTop: $(id).offset().top},'slow');
	}

	function menuSlimScroll()
	{
		var menu_max_height = parseInt($('#menu .slim-scroll').attr('data-scroll-height'));
		var menu_real_max_height = parseInt($('#wrapper').height());
		// var menu_real_max_height = $(window).height();
		var height = menu_real_max_height;
			height -= $('#menu .slim-scroll').offset().top;
		
		if ($('html').is('.sidebar.sticky-sidebar'))
			height -= $('#footer').height();
		else
			height = menu_max_height > menu_real_max_height ? menu_real_max_height : menu_max_height;
		
		//alert(height);
		$('#menu .slim-scroll').slimScroll({
			height: (height) + "px",
			allowPageScroll : true,
			railDraggable: ($.fn.draggable ? true : false)
	    });
		
		setTimeout(function(){
			$('#menu .slim-scroll').trigger('mouseenter');
		}, 200);
	}

	// handle menu toggle button action
	function toggleMenuHidden(onload)
	{
		if (typeof onload == 'undefined')
			var onload = false;

		if (!$('html.content-transitions').length)
			$('html').addClass('content-transitions');
		
		if ($('html.sidebar-width-mini').length && !onload || !$('html.sidebar-width-mini').length)
			$('.container-fluid:first').toggleClass('menu-hidden sidebar-hidden-phone');
		
		if (typeof $.cookie != 'undefined')
			$.cookie('menuHidden', $('.container-fluid:first').is('.menu-hidden'));
		
		if ($('html.sidebar').length && !$('.container-fluid:first.menu-hidden').length)
			menuSlimScroll();
	}

	// handle generate sparkline charts
	function genSparklines()
	{
		if (typeof $.fn.sparkline == 'undefined') 
			return;

		if ($('.sparkline').length)
		{
			$.each($('#content .sparkline'), function(k,v)
			{
				var size = { w: 150, h: 28 };
				if ($(this).parent().is('.widget-stats'))
					size = { w: 150, h: 35 }
				
				var color = primaryColor;
				if ($(this).is('.danger')) color = dangerColor;
				if ($(this).is('.success')) color = successColor;
				if ($(this).is('.warning')) color = warningColor;
				if ($(this).is('.inverse')) color = inverseColor;
				
				var data = [[1, 3+randNum()], [2, 5+randNum()], [3, 8+randNum()], [4, 11+randNum()],[5, 14+randNum()],[6, 17+randNum()],[7, 20+randNum()], [8, 15+randNum()], [9, 18+randNum()], [10, 22+randNum()]];
			 	$(v).sparkline(data, 
				{ 
					type: 'bar',
					width: size.w,
					height: size.h,
					stackedBarColor: ["#444", color],
					lineWidth: 2
				});
			});
			$.each($('#menu .sparkline'), function(k,v)
			{
				var size = { w: 150, h: 20 };
				if ($(this).parent().is('.widget-stats-3'))
					size = { w: 150, h: 35 }
				
				var color = primaryColor;
				if ($(this).is('.danger')) color = dangerColor;
				if ($(this).is('.success')) color = successColor;
				if ($(this).is('.warning')) color = warningColor;
				if ($(this).is('.inverse')) color = inverseColor;
				
				var data = [[1, 3+randNum()], [2, 5+randNum()], [3, 8+randNum()], [4, 11+randNum()],[5, 14+randNum()],[6, 17+randNum()],[7, 20+randNum()], [8, 15+randNum()], [9, 18+randNum()], [10, 22+randNum()]];
			 	$(v).sparkline(data, 
				{ 
					type: 'bar',
					width: size.w,
					height: size.h,
					stackedBarColor: ["#dadada", color],
					lineWidth: 2
				});
			});
		}
	}

	if (typeof Holder != 'undefined')
	{
		Holder.add_theme("dark", {background:"#424242", foreground:"#aaa", size:9}).run();
		Holder.add_theme("white", {background:"#fff", foreground:"#c9c9c9", size:9}).run();
	}

	// if ($('html.sidebar').length && !$('.container-fluid:first.menu-hidden').length)
	// 	menuSlimScroll();
	
	if (Modernizr.touch)
	{
		$('html.sidebar .container-fluid:first').on('swipeleft swiperight', function(e){
			if ($(this).is('.sidebar-hidden-phone') && e.type == 'swiperight')
				return toggleMenuHidden();
			if (!$(this).is('.sidebar-hidden-phone') && e.type == 'swipeleft')
				return toggleMenuHidden();
			
			e.preventDefault();
		});
	
		$('html.sidebar .container-fluid').on('movestart', function(e) {
			// If the movestart is heading off in an upwards or downwards
			// direction, prevent it so that the browser scrolls normally.
			if ((e.distX > e.distY && e.distX < -e.distY) ||
				(e.distX < e.distY && e.distX > -e.distY)) {
				e.preventDefault();
			}
		});
	}

	if (!Modernizr.touch)
	{
		$('html.sidebar.sidebar-hat #menu').on('mouseenter', function(){
			if ($(this).parents('.menu-hidden').length)
				toggleMenuHidden();
		}).on('mouseleave', function(){
			if (!$(this).parents('.menu-hidden').length)
			{
				$(this).find('li.dropdown.open > [data-toggle="dropdown"]').click();
				toggleMenuHidden();
			}
		});
		
		// dropdown menus
		$('html.sidebar.sidebar-dropdown').find('.slim-scroll > ul, > ul').on('mouseenter', '> li.dropdown:not(.open)', function(){
			$(this).find('> [data-toggle="dropdown"]').click();
		});
		
		// Dropdowns
		$('.navbar.main ul.topnav').on('mouseenter', '> li.dropdown:not(.open)', function(){
			$(this).find('> [data-toggle="dropdown"]').click();
		});
		$('.navbar.main').on('mouseleave', 'li.dropdown.open', function(){
			$(this).find('> [data-toggle="dropdown"]').click();
		});
		
		// Mega menus
		$('.navbar.main ul.topnav').on('mouseenter', '> li.mega-menu:not(.mega-menu-open)', function(){
			$(this).find('> a').click();
		});
		$('.navbar.main').on('mouseleave', 'li.mega-menu.mega-menu-open', function(){
			$('body').click();
		});
	}
	
	// Sidebar menu collapsibles
	if ($('html.sidebar-collapsible').length)
	{
		$('#menu .collapse').on('show.bs.collapse', function(e)
		{
			e.stopPropagation();
			$(this).parents('.hasSubmenu:first').addClass('active');
			
			if ($('html.sidebar-collapsible .container-fluid:first').is('.menu-hidden'))
				toggleMenuHidden();
		})
		.on('hidden.bs.collapse', function(e)
		{
			e.stopPropagation();
			$(this).parents('.hasSubmenu:first').removeClass('active');
		});
	}
	
	// main menu visibility toggle
	$('.navbar.main .btn-navbar, #menu .btn-navbar').click(function()
	{
		var disabled = typeof toggleMenuButtonWhileTourOpen != 'undefined' ? toggleMenuButtonWhileTourOpen(true) : false;
		if (!disabled)
			toggleMenuHidden();
	});

	// multi-level top menu
	$('.submenu').hover(function()
	{
        $(this).children('ul').removeClass('submenu-hide').addClass('submenu-show');
    }, function()
    {
    	$(this).children('ul').removeClass('submenu-show').addClass('submenu-hide');
    });
	
	// tooltips
	$('body').tooltip({ selector: '[data-toggle="tooltip"]' });
	
	// popovers
	$('[data-toggle="popover"]').popover();
	
	// loading state for buttons
	$('[data-toggle*="btn-loading"]').click(function () {
        var btn = $(this);
        btn.button('loading');
        setTimeout(function () {
        	btn.button('reset')
        }, 3000);
    });
	$('[data-toggle*="button-loading"]').click(function () {
        var btn = $(this);
        btn.button('loading');
    });
	
	// print
	$('[data-toggle="print"]').click(function(e)
	{
		e.preventDefault();
		window.print();
	});
	
	// carousels
	$('.carousel').carousel();
	
	// generate sparkline charts
	genSparklines();
	
	// Google Code Prettify
	if ($('.prettyprint').length && typeof prettyPrint != 'undefined')
		prettyPrint();
	
	// view source toggle buttons
	$('.btn-source-toggle').click(function(e){
		e.preventDefault();
		$('.code:not(.show)').toggleClass('hide');
	});
	
	// show/hide toggle buttons
	$('[data-toggle="hide"]').click(function()
	{
		if ($(this).is('.bootboxTarget'))
			bootbox.alert($($(this).attr('data-target')).html());
		else {
			$($(this).attr('data-target')).toggleClass('hide');
			if ($(this).is('.scrollTarget') && !$($(this).attr('data-target')).is('.hide'))
				scrollTo($(this).attr('data-target'));
		}
	});
	
	// handle persistent menu visibility on page load
	if (typeof $.cookie != 'undefined' && $.cookie('menuHidden') && $.cookie('menuHidden') == 'true' || (!$('.container-fluid').is('.menu-hidden') && !$('#menu').is(':visible')))
		toggleMenuHidden(true);
	
	/* Other non-widget Slim Scroll areas */
	$('#content .slim-scroll').each(function(){
		var scrollSize = $(this).attr('data-scroll-size') ? $(this).attr('data-scroll-size') : "7px";
		$(this).slimScroll({
			height: $(this).attr('data-scroll-height'),
			allowPageScroll : false,
			railVisible: false,
			// size: '0',
			railDraggable: ($.fn.draggable ? true : false)
	    });
	});
})(jQuery, window);