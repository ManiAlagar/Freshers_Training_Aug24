function checkVowelOrNot(event) {
    event.preventDefault();
    let charIp = document.getElementById("inputField").value.trim();
    let charInput=charIp.toLowerCase();
    const arr = ['a','e','i','o','u'] ;
    document.getElementById("res").style.color = "red";
    let regexSpclChar = /[^a-zA-Z0-9\s]/;
    let result;
    if (!charInput.length) {
        result = "Please enter a character";
    }
    else if (!isNaN(charInput)){
        result="Number(s) not allowed";
    }
    else if (regexSpclChar.test(charInput)) {
        result = "Special Character(s) not allowed";
    }
    else if (charInput.length > 1) {
        result = "Please enter only one character";
    }
    else {
        if(arr.includes(charInput)) {
            document.getElementById("res").style.color = "green";
            result="Given character is a Vowel"
        }
        else {
            result="Given character is not a vowel"
        }
    }

    document.getElementById("res").innerHTML = result;
}

function resetFunction() {
    document.getElementById("res").innerHTML = "";
    document.getElementById("inputField").value = "";
}