const vid = document.getElementById("botonVideo")
const com = document.getElementById("botonComic")
const fvid = document.getElementById("formularioVideo")
const fcom = document.getElementById("formularioComic")

vid.addEventListener("click", function () {
    com.classList.add("d-none");
    fvid.classList.remove("d-none");
});

com.addEventListener("click", function () {
    vid.classList.add("d-none");
    fcom.classList.remove("d-none");
});
