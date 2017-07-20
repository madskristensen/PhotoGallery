(function () {

    // File upload
    var filesUpload = document.getElementById("files");
    var btnfiles = document.getElementById("btnfiles");

    if (filesUpload && btnfiles) {
        filesUpload.addEventListener("change", function () {
            btnfiles.disabled = this.files.length === 0;
        }, false);
    }

    // Delete album
    var deletealbum = document.querySelector("#deletealbum");

    if (deletealbum) {
        deletealbum.addEventListener("click", function (e) {
            if (!confirm("Are you sure you want to delete the album?")) {
                e.preventDefault();
            }
        }, false)
    };

    // Delete photo
    var deletephoto = document.querySelector("#deletephoto");

    if (deletephoto) {
        deletephoto.addEventListener("click", function (e) {
            if (!confirm("Are you sure you want to delete the photo?")) {
                e.preventDefault();
            }
        }, false)
    };
})();