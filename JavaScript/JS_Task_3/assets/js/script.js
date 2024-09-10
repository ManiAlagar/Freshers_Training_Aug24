function verify(event) {
    event.preventDefault()
    var inputLine = document.getElementById('line').value.trim().replace(/\s+/g, " ");
    const arrayCollection = inputLine.split(" ")
    const specialCharcterRgx = /[!~@#$%^&*`()\-_+={}[\]:;"'<>,.?\/|\\]/;
    const numberRgx = /[0-9]/
    let message;
    document.getElementById("result").style.color = "red";
    if (inputLine.length == 0) {
        message = "ALert: Please enter a sentence";
    }
    else if (specialCharcterRgx.test(inputLine)) {
        message = "ALert: Special character's not allowed";
    }
    else if (numberRgx.test(inputLine)) {
        message = "ALert: Numbers not allowed";
    }
    else if (arrayCollection.length == 1) {
        message = "ALert: Please enter more than one word";
    }
    else {
        if (inputLine[0].toLowerCase() === inputLine[inputLine.length - 1].toLowerCase()) {
            document.getElementById("result").style.color = "green"
            message = "First and last character are same";
        }
        else {
            message = "Alert: First and last character are not same";
        }
    }
    document.getElementById("result").innerHTML = message;
}
function resetAll() {
    document.getElementById("result").innerHTML = "";
}
