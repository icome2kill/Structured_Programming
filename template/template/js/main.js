
// FOR SKILLS GRAPH
jQuery(document).ready(function($){
								
	function isScrolledIntoView(id)
	{
		var elem = "#" + id;
		var docViewTop = $(window).scrollTop();
		var docViewBottom = docViewTop + $(window).height();
	
		if ($(elem).length > 0){
			var elemTop = $(elem).offset().top;
			var elemBottom = elemTop + $(elem).height();
		}

		return ((elemBottom >= docViewTop) && (elemTop <= docViewBottom)
		  && (elemBottom <= docViewBottom) &&  (elemTop >= docViewTop) );
	}

	
	
	function sliding_horizontal_graph(id, speed){
		//alert(id);
		$("#" + id + " li span").each(function(i){
			var j = i + 1; 										  
			var cur_li = $("#" + id + " li:nth-child(" + j + ") span");
			var w = cur_li.attr("title");
			cur_li.animate({width: w + "%"}, speed);
		})
	}
	
	function graph_init(id, speed){
		$(window).scroll(function(){
			if (isScrolledIntoView(id)){
				sliding_horizontal_graph(id, speed);
			}
			else{
				//$("#" + id + " li span").css("width", "0");
			}
		})
		
		if (isScrolledIntoView(id)){
			sliding_horizontal_graph(id, speed);
		}
	}
	
	graph_init("services-graph", 1000);
});


jQuery(document).ready(function ($) {
	$('.social-icons ul li a, .tooltips').tooltip();
	
	$('#nav').tinyNav({
		active: 'current', // String: Set the "active" class
		header: '', // String: Specify text for "header" and show header instead of the active item
		label: '' // String: Sets the <label> text for the <select> (if not set, no label will be added)
	});
	
	$('.carousel').carousel({
		//interval: 2000,
		//pause: "hover",
	});
	
	/*Portfolio Carousel*/
	if($(".portfolio-carousel").length){
		$('ul.portfolio-carousel').jcarousel({ scroll: 1 });
	}
	
	/*Clients Carousel*/
	if($(".clients-carousel").length){
		$('ul.clients-carousel').jcarousel({ scroll: 1 });
	}
	
	$('.sf-menu').superfish({
        delay: 500, // one second delay on mouseout 
        animation: {
            opacity: 'show',
            height: 'show'
        }, // fade-in and slide-down animation 
        speed: 'fast', // faster animation speed
        autoArrows: false, // disable generation of arrow mark-up 
        dropShadows: false, // disable drop shadows 
    });
	
	// Lightbox
	$("a[rel^='prettyPhoto']").prettyPhoto({
		theme:'light_square', 
		autoplay_slideshow: false, 
		overlay_gallery: false, 
		show_title: false,
	});
	
	// Tweets Widget
	if( $.fn.tweet ) {
		$('.tweet-stream').tweet({
			username: "mannatstudio",
			modpath: 'twitter/',
			count: 2,
			template: "{text}{time}",
			loading_text: "loading tweets..."
		});
	}
	
	if( $.fn.jflickrfeed ) {
		$('.flickr-stream ul').jflickrfeed({
			qstrings: {
				id: '52617155@N08'
			}, 
			limit: 9, 
			itemTemplate: '<li><a href="{{link}}" title="{{title}}" target="_blank"><img src="{{image_s}}" alt="{{title}}" /></a></li>'
		});
	}
	
	// hide #back-top first
	$("#back-top").hide();		
	// fade in #back-top
	$(function () {
		$(window).scroll(function () {
			if ($(this).scrollTop() > 100) {
				$('#back-top').fadeIn();
			} else {
				$('#back-top').fadeOut();
			}
		});	
		// scroll body to 0px on click
		$('#back-top a').click(function () {
			$('body,html').animate({
				scrollTop: 0
			}, 800);
			return false;
		});
	});	
	
	//jQuery tab
	jQuery(".tab-content").hide(); //Hide all content
	jQuery("ul.tabs li:first").addClass("active").show(); //Activate first tab
	jQuery(".tab-content:first").show(); //Show first tab content
	//On Click Event
	jQuery("ul.tabs li").click(function() {
		jQuery("ul.tabs li").removeClass("active"); //Remove any "active" class
		jQuery(this).addClass("active"); //Add "active" class to selected tab
		jQuery(".tab-content").hide(); //Hide all tab content
		var activeTab = jQuery(this).find("a").attr("href"); //Find the rel attribute value to identify the active tab + content
		jQuery(activeTab).fadeIn(200); //Fade in the active content
		return false;
	});
	
	//jQuery toggle
	jQuery(".toggle_container").hide();
	jQuery("h2.trigger").click(function(){
		jQuery(this).toggleClass("active").next().slideToggle("slow");
	});
	
	
	// Accordion
	$(".accordion-group").each(function(){
			var that = $(this);
			$(this).find(".accordion-heading a").on("click", function() {
				that.parent().find(".accordion-heading a.active").removeClass("active");
				$(this).toggleClass("active"); 
			});
	  });
	jQuery("ul.ts-accordion li").each(function(){
		if(jQuery(this).index() > 0){
			jQuery(this).children(".accordion-content").css('display','none');
		}else{
			jQuery(this).find(".accordion-title").addClass('active');
		}				
		jQuery(this).children(".accordion-title").bind("click", function(){
			jQuery(this).addClass(function(){
				if(jQuery(this).hasClass("active")) return "";
				return "active";
			});
			jQuery(this).siblings(".accordion-content").slideDown();
			jQuery(this).parent().siblings("li").children(".accordion-content").slideUp();
			jQuery(this).parent().siblings("li").find(".active").removeClass("active");
		});
	});
	
	// Search Field Text
	$(function(){
		$('input[placeholder]').each(function(){
			var $placeInput = $(this);			
			if( 'placeholder' in document.createElement('input') ) {
				var placeholder = true;
			}
			else {
				var placeholder = false;
				$placeInput.val( $placeInput.attr('placeholder') );
			}			
			if( !placeholder ) {
				$placeInput.focusin(function(){
					if( $placeInput.val() === $placeInput.attr('placeholder') ) {				
						$placeInput.val('');				
					}
				})
				.focusout(function(){
					if( $placeInput.val() === '' ) {
						$placeInput.val( $placeInput.attr('placeholder') );
					}
				});
			}		
		});
	});
	
	var $container = $('#content');
		$container.isotope({
			filter: '*',
			animationOptions: {
				duration: 750,
				easing: 'linear',
				queue: false,
			}
		});
		$('#portolfio-filter a').click(function () {
			$('#portolfio-filter a').removeClass('active');
			$(this).addClass('active');
			return false;
		});
		$('#portolfio-filter a').click(function () {
			var selector = $(this).attr('data-filter');
			$container.isotope({
				filter: selector,
				animationOptions: {
					duration: 750,
					easing: 'linear',
					queue: false,
	
				}
			});
			return false;
		});
	
	
});

$(document).ready(function(){
	jQuery("#contact_form").validate({
		meta: "validate",
		submitHandler: function (form) {
			
			var s_name=$("#name").val();
			var s_lastname=$("#lastname").val();
			var s_email=$("#email").val();
			var s_phone=$("#phone").val();
			var s_comment=$("#comment").val();
			$.post("contact.php",{name:s_name,lastname:s_lastname,email:s_email,phone:s_phone,comment:s_comment},
			function(result){
			  $('#sucessmessage').append(result);
			});
			$('#contact_form').hide();
			return false;
		},
		/* */
		rules: {
			name: "required",
			
			lastname: "required",
			// simple rule, converted to {required:true}
			email: { // compound rule
				required: true,
				email: true
			},
			phone: {
				required: true,
			},
			comment: {
				required: true
			}
		},
		messages: {
			name: "Please enter your name.",
			lastname: "Please enter your last name.",
			email: {
				required: "Please enter email.",
				email: "Please enter valid email"
			},
			phone: "Please enter a phone.",
			comment: "Please enter a comment."
		},
	}); /*========================================*/
});
