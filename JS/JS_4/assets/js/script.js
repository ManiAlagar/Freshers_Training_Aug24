function reverseStr(event) {
    event.preventDefault();
    let str = document.getElementById("inputField").value.trim().replace(/\s\s+/g, ' ');
    document.getElementById("res").style.color = "red";
    let regexSpclChar = /[^a-zA-Z\d\s]/;
    let result;
    if (!str.length) {
        result = "Please enter a sentence";
    }
    else if (str.length == 1) {
        result = "Please enter more than one character";
    }
    else if (regexSpclChar.test(str)) {
        result = "Special Character(s) not allowed";
    }
    else{
        document.getElementById("res").style.color = "green";
        result = str.split("").reverse().join("");
    }
    document.getElementById("res").innerHTML = result;
}

function resetFunction() {
    document.getElementById("res").innerHTML = "";

}