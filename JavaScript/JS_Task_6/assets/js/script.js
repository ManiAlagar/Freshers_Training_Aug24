arrayCollection = document.getElementsByTagName("input")
function check() {
    for (i = 0; i < arrayCollection.length; i++)
        arrayCollection[i].checked = true;
}
function uncheck() {
    for (i = 0; i < arrayCollection.length; i++)
        arrayCollection[i].checked = false;
}
function checkUncheck() {
    for (i = 0; i < arrayCollection.length; i++) {
        if (arrayCollection[i].checked == false)
            arrayCollection[i].checked = true;

        else
            arrayCollection[i].checked = false;
    }
}