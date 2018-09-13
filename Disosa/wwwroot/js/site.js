// Write your JavaScript code.
function OcultarImagenes() {
    $(".carrusel-image").css("display", "none");
}

function MostrarImagen(imagen) {
    OcultarImagenes();
    var elemento = $("#" + imagen);
    elemento.fadeIn("1500");
    elemento.children().css("opacity", "0").css("left", "0px")
        .delay(500)
        .animate({ left: '15%', opacity: "1" }, "slow");
}

OcultarImagenes();

var imagenActual = "imagen1";
MostrarImagen(imagenActual);
setInterval(function () {
    if (imagenActual === "imagen1") {
        imagenActual = "imagen2";
    } else if (imagenActual === "imagen2") {
        imagenActual = "imagen3";
    } else if (imagenActual === "imagen3") {
        imagenActual = "imagen1"
    }
    MostrarImagen(imagenActual);
},
    5000
);


$(document).ready(function () {

    /* Every time the window is scrolled ... */
    $(window).scroll(function () {

        /* Check the location of each desired element */
        $('.hideme').each(function (i) {

            var bottom_of_object = $(this).offset().top + $(this).outerHeight();
            var bottom_of_window = $(window).scrollTop() + $(window).height();

            /* If the object is completely visible in the window, fade it it */
            if (bottom_of_window > bottom_of_object) {                
                $(this).animate({ 'opacity': '1' , 'margin-top':'0px' }, 500);
            }

        });

    });

});
