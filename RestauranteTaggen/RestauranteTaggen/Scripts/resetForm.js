function resetarForm() {
    $('.field-validation-error')
        .removeClass('field-validation-error')
        .addClass('field-validation-valid');

    $('.input-validation-error')
        .removeClass('input-validation-error')
        .addClass('valid');
    var url = window.location.href;
    var url_completa = url.split("/");
    url = url_completa[0] + "/" + url_completa[1] + "/" + url_completa[2] + "/" + url_completa[3];
    window.location.href = url;
}
