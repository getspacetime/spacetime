export function log(message, args) {
    console.log(message, args);
}

export function copyToClipboard(contents) {
    return navigator.clipboard.writeText(contents).then(() => {
        log("copied to clipboard!");
        return true;
    }).catch(err => {
        log("failed to copy to clipboard", err);
        return false;
    });
}

export function getOffset(el) {
    var _x = 0;
    var _y = 0;
    while (el && !isNaN(el.offsetLeft) && !isNaN(el.offsetTop)) {
        _x += el.offsetLeft - el.scrollLeft;
        _y += el.offsetTop - el.scrollTop;
        el = el.offsetParent;
    }
    return { top: _y, left: _x };
}