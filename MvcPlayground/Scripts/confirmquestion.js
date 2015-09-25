$('*[data-confirmprompt]').click(function (event) {
    var promptText = $(this).attr('data-confirmprompt');
    if (!confirm(promptText)) {
        event.preventDefault();
    }
});