
jQuery(document).ready(function ($) {
    
    $('.mapbutton').hover(function () {
       
        $('.mapbutton').find('.mapbutton_sec').removeClass('active');
        /* Stuff to do when the mouse enters the element */
        $(this).find('.mapbutton_sec').addClass('active');
        $(this).addClass('active');
    }, function() {
       $(this).removeClass('active');
    });
    $('.mapbutton_box').mouseleave(function(event) {
        $('.mapbutton_sec').removeClass('active')
    });
    $('.mapbutton_sec').hover(function() {
        $(this).find('div').addClass('changecolor');
    }, function() {
        if(!$(this).find('div').hasClass('active')){
       $(this).find('div').removeClass('changecolor');
       }
    });
    $('.function_button').click(function(){
        $('.function_button').removeClass('active');
        if($(this).hasClass('active')){
            $(this).removeClass('active');
        }else{
            $(this).addClass('active');
        }
    });
    $('.mapbutton_sec div').click(function(){
        
        if($(this).hasClass('active')){
            $(this).removeClass('changecolor');
            $(this).removeClass('active');
        }else{
            $(this).addClass('changecolor');
            $(this).addClass('active');
        }
    });
    /*图例操作*/
    $('.fa-chevron-down').click(function(event) {
        $(this).hide();
        
        $('.legend h4').hide();
        $('.fa-chevron-up').show();
        /* Act on the event */
        $('.legend_box').slideUp('fast',function(){$('.legend').addClass('active');});

    });
    $('.fa-chevron-up').click(function(event) {
        $(this).hide();
        $('.fa-chevron-down').show();
        $('.legend').removeClass('active');
        $('.legend h4').show();
        /* Act on the event */
        $('.legend_box').slideDown('fast');
    });
   
});