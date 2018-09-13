// Write your JavaScript code.
function OcultarImagenes() {
        $(".carrusel-image").css("display", "none");
    }

    function MostrarImagen(imagen) {
        OcultarImagenes();
        var elemento = $("#" + imagen);
        elemento.fadeIn("1500");
        elemento.children().css("opacity","0").css("left","0px")
        .delay(500)
        .animate({left: '15%', opacity: "1"}, "slow");
    }

    OcultarImagenes();

    var imagenActual = "imagen1";
    MostrarImagen(imagenActual);
    setInterval(function () {
        if (imagenActual == "imagen1") {
            imagenActual = "imagen2";
        } else if (imagenActual == "imagen2") {
            imagenActual = "imagen3";
        } else if (imagenActual == "imagen3") {
            imagenActual = "imagen1"
        }
        MostrarImagen(imagenActual);
    },
        5000
    );

