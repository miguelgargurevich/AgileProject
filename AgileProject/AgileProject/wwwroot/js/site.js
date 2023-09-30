// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', function () {

    var modal = document.getElementById("ventanaModal");
    // Botón que abre el modal
    var boton = document.getElementById("abrirModal");
    // Hace referencia al elemento <span> que tiene la X que cierra la ventana
    var span = document.getElementsByClassName("cerrar")[0];

    if (boton != null) {

        boton.addEventListener("click", function () {
            modal.style.display = "block";
        });
    }
   
    //// Si el usuario hace clic en la x, la ventana se cierra
    //span.addEventListener("click", function () {
    //    modal.style.display = "none";
    //});
    // Si el usuario hace clic fuera de la ventana, se cierra.
    window.addEventListener("click", function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    });


    $(document).ready(function () {

        //getEventTypes();



    });






});
