var message = '@TempData["ToastrMessage"]';
var messageType = '@TempData["ToastrType"]';

if (message) {
    if (messageType === "error") {
        toastr.error(message);
    } else if (messageType === "success") {
        toastr.success(message);
    } else if (messageType === "warning") {
        toastr.warning(message);
    } else if (messageType === "info") {
        toastr.info(message);
    }
}