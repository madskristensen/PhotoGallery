(function () {

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