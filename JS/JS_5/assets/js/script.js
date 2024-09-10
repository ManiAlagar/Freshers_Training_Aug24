function findLargestNumber(event) {
    event.preventDefault();
    let numberInput = document.getElementById("inputField").value.trim().replace(/\s+/g, '').replace(/,,+/g, ",");
    document.getElementById("res").style.color = "red";
    let regexSpclChar = /[^a-zA-Z0-9\s,-.]/;
    let rgxNumber = /[^\d+\s,-.]/;
    let result;

    if (!numberInput.length) {
        result = "Please enter a number";
    }
    else if (regexSpclChar.test(numberInput)) {
        result = "Special Character(s) not allowed";
    }
    else if (rgxNumber.test(numberInput)) {
        result = "letter(s) not allowed";
    }
    else {
        let inputNumbers = numberInput.split(",")
        result = validation(Number(inputNumbers[0]), Number(inputNumbers[1]));
    }
    document.getElementById("res").innerHTML = result;
}
 
function resetFunction() {
    document.getElementById("res").innerHTML = "";
}

function validation(numberOne, numberTwo) {
    if (!numberOne || !numberTwo) {
        result = "Invalid input,make sure the input format is (1,2)"
    }
    else if (numberOne == numberTwo) {
        result = "both numbers are same"
    }
    else if (numberOne > numberTwo) {
        document.getElementById("res").style.color = "green";
        result = "Largest number is: " + numberOne
    }
    else {
        document.getElementById("res").style.color = "green";
        result = "Largest number is: " + numberTwo
    }
    return result;
}


