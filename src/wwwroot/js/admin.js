(function () {

    // Button click
    var form = document.querySelector("form");

    if (form) {
        form.addEventListener("submit", function (e) {
            var elm = e.target;

            if (elm.checkValidity && elm.checkValidity()) {
                var input = elm.querySelector("input[data-progress]");

                if (input) {
                    input.disabled = true;
                    input.value = input.getAttribute("data-progress");
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
        }, false);
    }

    // Delete photo
    var deletephoto = document.querySelector("#deletephoto");

    if (deletephoto) {
        deletephoto.addEventListener("click", function (e) {
            if (!confirm("Are you sure you want to delete the photo?")) {
                e.preventDefault();
            }
        }, false);
    }
})();