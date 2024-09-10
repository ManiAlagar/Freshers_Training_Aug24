let checkBoxElement = document.getElementsByName('checkBox');

function checkAll() {
    for (let i = 0; i < checkBoxElement.length; i++) {
        checkBoxElement[i].checked = true;
    }
}
function unCheckAll() {
    for (let i = 0; i < checkBoxElement.length; i++) {
        checkBoxElement[i].checked = false;
    }
}
function checkAndUncheck() {
    for (let i = 0; i < checkBoxElement.length; i++) {
        if (!checkBoxElement[i].checked) {
            checkBoxElement[i].checked = true;
        }
        else {
            checkBoxElement[i].checked = false;
        }
    }
}
