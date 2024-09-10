function verify(event) {
    event.preventDefault()
    let inputValue = document.getElementById('input').value.trim().replace(/\s+/g, " ");
    const specialCharcterRgx = /[!~@#$%^&*`()\-_+={}[\]:;"'<>,.?\/|\\]/;
    let message;
    let reverseValue = " ";
    document.getElementById("result").style.color = "red";
    if (inputValue.length == 0) {
        message = "ALert: Please enter a sentence";
    }
    else if (specialCharcterRgx.test(inputValue)) {
        message = "ALert: Special character's not allowed";
    }
    else if (inputValue.length == 1) {
        message = "ALert: Please enter more than one character";
    }
    else {
        let i=inputValue.length-1;
        while(i>=0)
        {
           reverseValue+=inputValue.charAt(i)
            i--
        }
        message=reverseValue
         document.getElementById("result").style.color="green"
     }
    document.getElementById("result").innerHTML = message;
}
function resetAll() {
    document.getElementById("result").innerHTML = "";
}