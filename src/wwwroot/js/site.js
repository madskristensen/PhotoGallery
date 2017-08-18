(function () {

    // Fade images in as they load
    var pics = document.getElementsByTagName("img");

    for (var i = 0; i < pics.length; i++) {
        var img = pics[i];

        img.onload = function () {
            this.className = "loaded";
        };

        if (img.complete) {
            setTimeout(function (elm) {
                elm.className = "loaded";
            }, 200, img);
        }
    }

    // Keyboard navigation
    var keyMap = {
        "37": document.querySelector("a[rel=prev]"), // left
        "39": document.querySelector("a[rel=next]") // right
    };

    window.addEventListener("keyup", function (e) {
        if (e.altKey || e.shiftKey || e.ctrlKey)
            return;

        var link = keyMap[e.keyCode];

        if (link) {
            location.href = link.href;
        }
    }, false);

})();