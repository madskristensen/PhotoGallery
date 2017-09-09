(() => {

    // Button click
    const form = document.querySelector("form");

    if (form) {
        form.addEventListener("submit", (e) => {
            var elm = e.target;

            if (elm.checkValidity && elm.checkValidity()) {
                const input = elm.querySelector("input[data-progress]");

                if (input) {
                    input.disabled = true;
                    input.value = input.getAttribute("data-progress");
                }
            }
        });
    }

    // Delete album
    const deletealbum = document.querySelector("#deletealbum");

    if (deletealbum) {
        deletealbum.addEventListener("click", (e) => {
            if (!confirm("Are you sure you want to delete the album?")) {
                e.preventDefault();
            }
        }, false);
    }

    // Delete photo
    const deletephoto = document.querySelector("#deletephoto");

    if (deletephoto) {
        deletephoto.addEventListener("click", (e) => {
            if (!confirm("Are you sure you want to delete the photo?")) {
                e.preventDefault();
            }
        }, false);
    }
})();