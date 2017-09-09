(() => {

    // Fade images in as they load
    const pics = document.getElementsByTagName("img");

    for (let img of pics) {

        img.onload = (e) => {
            e.target.className = "loaded";
        };

        if (img.complete) {
            setTimeout((elm) => {
                elm.className = "loaded";
            }, 200, img);
        }
    }

    // Keyboard navigation
    const keyMap = {
        37: document.querySelector("a[rel=prev]"), // left
        39: document.querySelector("a[rel=next]") // right
    };

    window.addEventListener("keyup", (e) => {
        if (e.altKey || e.shiftKey || e.ctrlKey)
            return;

        if (link = keyMap[e.keyCode]) {
            location.href = link.href;
        }
    }, false);

})();