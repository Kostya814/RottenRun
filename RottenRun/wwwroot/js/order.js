window.onload = function() {
    var columns = document.querySelectorAll('.order > div');

    for (var i = 0; i < columns.length; i++) {
        var maxColumnWidth = 0;
        var columnElements = document.querySelectorAll('.order > div:nth-child(' + (i + 1) + ')');

        for (var j = 0; j < columnElements.length; j++) {
            var width = columnElements[j].offsetWidth;
            if (width > maxColumnWidth) {
                maxColumnWidth = width;
            }
        }

        for (var j = 0; j < columnElements.length; j++) {
            columnElements[j].style.width = maxColumnWidth + 'px';
        }
    }
}