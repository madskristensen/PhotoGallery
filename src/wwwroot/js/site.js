(function () {

    var keyMap = {
        "36": document.querySelector("link[rel=index]"), // home
        "37": document.querySelector("a[rel=prev]"), // left
        "39": document.querySelector("a[rel=next]") // right
    };

    window.addEventListener("keypress", function (e) {
        if (e.altKey || e.shiftKey || e.ctrlKey)
            return;

        var link = keyMap[e.keyCode];

        if (link) {
            location.href = link.href;
        }
    }, false);

})();