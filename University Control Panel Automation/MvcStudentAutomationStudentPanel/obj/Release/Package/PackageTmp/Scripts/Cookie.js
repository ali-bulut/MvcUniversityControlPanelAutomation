function changeLang(lang) {
    jQuery.cookie('lang', lang);
    window.location.reload();
    return false;
}