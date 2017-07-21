(function () {

    // Button click
    var submit = document.querySelector("input[type=submit]");

    if (submit) {
        submit.addEventListener("click", function (e) {
            var elm = e.target;

            if (elm.form.checkValidity && elm.form.checkValidity()) {
                elm.disabled = true;

                if (elm.hasAttribute("data-progress")) {
                    elm.value = elm.getAttribute("data-progress");
                }
            }
        });
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