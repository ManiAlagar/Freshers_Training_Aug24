function validate() {
    var characterInput = document.getElementById('input').value.trim().replace(/\s+/g, " ");
    const specialCharcterRgx = /[!~@#$%^&*`()\-_+={}[\]:;"'<>,.?\/|\\]/;
    const vowelsRgx = /['a','e','i','o','u']/g;
    let message;
    document.getElementById("output").style.color = "red";
    let checkNumber = Number(characterInput)
    if (characterInput.length == 0) {
        message = "ALert: Please enter a character";
    }
    else if (Number.isInteger(checkNumber)) {
        message = "ALert: Numbers are not allowed";
    }
    else if (characterInput.length !== 1) {
        message = "ALert: Please enter only one letter";
    }
    else if (specialCharcterRgx.test(characterInput)) {
        message = "ALert: Special character's not allowed";
    }
    else {
        if (vowelsRgx.test(characterInput.toLowerCase())) {
            document.getElementById("output").style.color = "green";
            message = "\"" + characterInput + "\" is  a vowel character";
        }
        else {
            message = "\"" + characterInput + "\" is not a vowel character";
        }
    }
    document.getElementById('output').innerHTML = message;
}
function reset() {
    document.getElementById('input').value = "";
    document.getElementById('output').innerHTML = "";
}

