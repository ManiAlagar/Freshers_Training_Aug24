function smallAndLarge(event) {
    event.preventDefault()
    let lineInput = document.getElementById('line').value.trim().replace(/\s+/g, ' ');
    const arrayInput = lineInput.split(" ");
    let shortString = longString = arrayInput[0];
    const specialCharacterRgx = /[!~@#$%^&*`()\-+={}[\]:_;"'<>,.?\/|\\]/g;
    let alertMessage;
    document.getElementById("output").style.color = "red";
    if (lineInput.length === 0) {
        alertMessage = "Alert: Please enter a sentence";
    }
    else {
        if (specialCharacterRgx.test(lineInput)) {
            alertMessage = "Alert: Special character not allowed";
        }
        else if (arrayInput.length === 1) {
            alertMessage = "Alert: Please enter a sentence which has more than one word";
        }
        else {
            for (let i = 1; i < arrayInput.length; i++) {
                if (longString.length < arrayInput[i].length) {
                    longString = arrayInput[i]
                }
                if (shortString.length > arrayInput[i].length) {
                    shortString = arrayInput[i]
                }
            }
            if (shortString == longString) {
                alertMessage = "Alert: All words are equal in length";
            }
            else {
                document.getElementById("output").style.color = "green";
                alertMessage = "Short string is \"" + shortString + "\" and long string is \"" + longString + "\"";
            }
        }
    }
    document.getElementById("output").innerHTML = alertMessage;
}
function resetAll() {
    document.getElementById('output').innerHTML = "";
}

