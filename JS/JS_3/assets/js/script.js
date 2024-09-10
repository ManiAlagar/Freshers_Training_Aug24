function checkFirstLastCharSame(event) {
    event.preventDefault();
    let string = document.getElementById("inputField").value.trim().replace(/\s+/g, '');
    let str = string.toLowerCase();
    console.log(str);
    let firstChar = str[0];
    let lastChar = str[str.length - 1];
    document.getElementById("res").style.color = "red";
    let regexSpclChar = /[^a-zA-Z\d\s]/;
    let rgxNumber = /\d+/;
    let result;
    if (!str.length) {
        result = "Please enter a string";
    }
    else if (rgxNumber.test(str) || Number(str)) {
        result = "Number(s) not allowed";
    }
    else if (regexSpclChar.test(str)) {
        result = "Special Character(s) not allowed";
    }
    else if (str.length == 1) {
        result = "Please enter more than one character";
    }
    else {
        if (firstChar == lastChar) {
            document.getElementById("res").style.color = "green";
            result = "First and last characters are same";
            alert(result);
        }
        else {
            result = "First and last characters are not same";
        }
    }

    document.getElementById("res").innerHTML = result;
}

function resetFunction() {
    document.getElementById("res").innerHTML = "";
    document.getElementById("inputField").value = "";
}