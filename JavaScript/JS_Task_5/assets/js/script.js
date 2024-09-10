function verify(event) {
    event.preventDefault()
    let numberInputs = document.getElementById('input').value.trim().replace(/\s+/g, "").replace(/,+/g, ",");
    numbercollection = numberInputs.split(",")
    const specialCharcterRgx = /[!~@#$%^&*`()\_+={}[\]:;"'.<>?\/|\\]/;
    const letterRgx = /[A-Za-z]/
    let message;
    document.getElementById("result").style.color = "red";
    if (numbercollection[0] == "") {
        numbercollection.shift()
    }
    if (numbercollection[numbercollection.length - 1] == "") {
        numbercollection.pop()
    }
    if (numbercollection[0] == "-" || numbercollection[1] == "-") {
        message = "Alert: Special character's not allowed";
    }
    else if (numbercollection.length == 0) {
        message = "Alert: Please enter a number";
    }
    else if (letterRgx.test(numberInputs)) {
        message = "Alert: Letter's not allowed";
    }
    else if (specialCharcterRgx.test(numberInputs)) {
        message = "Alert: Special character's not allowed";
    }
    else if (numbercollection.length == 1) {
        message = "Alert: Please enter more than one Number";
    }
    else if (numbercollection.length > 2 && numbercollection[numbercollection.length - 1] !== "") {
        message = "Alert: Please enter only two Numbers like --> (1,55)";
    }
    else {
        message = findLargest(numbercollection[0], numbercollection[1])
    }
    document.getElementById("result").innerHTML = message;
}
function resetAll() {
    document.getElementById("result").innerHTML = "";
}

function findLargest(firstNumber, secondNumber) {
    if (firstNumber == secondNumber) {
        message = "Alert: Both numbers are Same";
        return message
    }
    else if (Number(firstNumber) < Number(secondNumber)) {
        largestNumber = secondNumber
        document.getElementById("result").style.color = "green"
        message = "Largest Number is \"" + secondNumber + "\"";
        return message
    }
    else {
        message = "Largest Number is \"" + firstNumber + "\"";
        document.getElementById("result").style.color = "green"
        return message

    }
}