/*price range*/

 $('#sl2').slider();

	var RGBChange = function() {
	  $('#RGB').css('background', 'rgb('+r.getValue()+','+g.getValue()+','+b.getValue()+')')
	};	
		
/*scroll to top*/

$(document).ready(function(){
	$(function () {
		$.scrollUp({
	        scrollName: 'scrollUp', // Element ID
	        scrollDistance: 300, // Distance from top/bottom before showing element (px)
	        scrollFrom: 'top', // 'top' or 'bottom'
	        scrollSpeed: 300, // Speed back to top (ms)
	        easingType: 'linear', // Scroll to top easing (see http://easings.net/)
	        animation: 'fade', // Fade, slide, none
	        animationSpeed: 300, // Animation in speed (ms)
	        scrollTrigger: false, // Set a custom triggering element. Can be an HTML string or jQuery object
					//scrollTarget: false, // Set a custom target element for scrolling to the top
	        scrollText: '<i class="fa fa-angle-up"></i>', // Text for element, can contain HTML
	        scrollTitle: false, // Set a custom <a> title if required.
	        scrollImg: false, // Set true to use image
	        activeOverlay: false, // Set CSS color to display scrollUp active point, e.g '#00FFFF'
	        zIndex: 2147483647 // Z-Index for the overlay
		});
	});

	const added_click = function () {
		const p_id = $(this).attr('id');
		const thisElem = $(this);
		$.ajax({
			type: "POST",
			url: location.protocol + '//' + location.host + "/FavoriteProducts/RemoveFromFavorite/",
			contentType: "application/json; charset=utf-8",
			data: p_id,
			success: function () {
				thisElem.replaceWith(' <a class="add_favorite" id="' + p_id + '" style="cursor: pointer;"> <i class="fa fa-plus-square"></i>Сохранить</a>');
				$('#' + p_id).click(add_click);
			},
			error: function (m) {
				alert(m.responseText);
			}
		});
	};

	const add_click = function () {
		const p_id = $(this).attr('id');
		const thisElem = $(this);
		$.ajax({
			type: "POST",
			url: location.protocol + '//' + location.host + "/FavoriteProducts/AddToFavorite/",
			contentType: "application/json; charset=utf-8",
			data: p_id,
			success: function () {
				thisElem.replaceWith('<a class="added_favorite" id="' + p_id + '" style="cursor: pointer; color: #FFD700"><i class="fa fa-check" ></i >Сохранено</a >');
				$('#' + p_id).click(added_click);
			},
			error: function (m) {
				alert(m.responseText);
			}
		});
	};

	$(".add_favorite").click(add_click);
	$(".added_favorite").click(added_click);
});
