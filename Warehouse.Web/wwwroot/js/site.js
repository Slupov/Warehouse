// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function() {
    $('.dropdown-menu a.dropdown-toggle').on('click',
        function(e) {
            if (!$(this).next().hasClass('show')) {
                $(this).parents('.dropdown-menu').first().find('.show').removeClass("show");
            }

            var $subMenu = $(this).next(".dropdown-menu");
            $subMenu.toggleClass('show');

            $(this).parents('li.nav-item.dropdown.show').on('hidden.bs.dropdown',
                function(e) {
                    $('.dropdown-submenu .show').removeClass("show");
                });

            return false;
        });
});


(function($) {
    'use strict';


    // Preloader js    
    $(window).on('load', function() {
        $('.preloader').fadeOut(700);
    });

    //   navfixed
    $(window).on('scroll', function() {
        var scrolling = $(this).scrollTop();

        if (scrolling > 10) {
            $('.naviagtion').addClass('nav-bg');
        } else {
            $('.naviagtion').removeClass('nav-bg');
        }
    });

    // Background-images
    $('[data-background]').each(function() {
        $(this).css({
            'background-image': 'url(' + $(this).data('background') + ')'
        });
    });

    // venobox popup 
    $('.venobox').venobox();

    //  Count Up
    function counter() {
        var oTop;
        if ($('.counter').length !== 0) {
            oTop = $('.counter').offset().top - window.innerHeight;
        }
        if ($(window).scrollTop() > oTop) {
            $('.counter').each(function() {
                var $this = $(this),
                    countTo = $this.attr('data-count');
                $({
                    countNum: $this.text()
                }).animate({
                    countNum: countTo
                }, {
                    duration: 2000,
                    easing: 'swing',
                    step: function() {
                        $this.text(Math.floor(this.countNum));
                    },
                    complete: function() {
                        $this.text(this.countNum);
                    }
                });
            });
        }
    }
    $(window).on('scroll', function() {
        counter();
    });


    // testimonial
    $('.testimonial-slider-single').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        autoplay: true,
        dots: false,
        arrows: true,
        nextArrow: '<buttton class="nextarrow"></buttton>',
        prevArrow: '<buttton class="prevarrow"></buttton>'
    });


    // blog slider
    $('.blog-slider').slick({
        slidesToShow: 3,
        slidesToScroll: 1,
        autoplay: true,
        dots: false,
        arrows: true,
        nextArrow: '<buttton class="nextarrow"></buttton>',
        prevArrow: '<buttton class="prevarrow"></buttton>',
        responsive: [{
                breakpoint: 1024,
                settings: {
                    slidesToShow: 2
                }
            },
            {
                breakpoint: 600,
                settings: {
                    slidesToShow: 2
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 1
                }
            }
        ]
    });

    $('.category-slider').slick({
        slidesToShow: 4,
        slidesToScroll: 1,
        autoplay: true,
        dots: false,
        arrows: true,
        nextArrow: '<buttton class="nextarrow"></buttton>',
        prevArrow: '<buttton class="prevarrow"></buttton>',
        responsive: [{
                breakpoint: 1024,
                settings: {
                    slidesToShow: 3
                }
            },
            {
                breakpoint: 600,
                settings: {
                    slidesToShow: 2
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 1
                }
            }
        ]
    });

    $('.testimonial-slider').slick({
        infinite: true,
        slidesToShow: 3,
        slidesToScroll: 1,
        arrows: false,
        dot: false,
        autoplay: true,
        responsive: [{
                breakpoint: 1024,
                settings: {
                    slidesToShow: 2
                }
            },
            {
                breakpoint: 600,
                settings: {
                    slidesToShow: 2
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 1
                }
            }
        ]
    });

    // Aos js
    AOS.init({
        once: true,
        offset: 250,
        easing: 'ease',
        duration: 800
    });


})(jQuery);