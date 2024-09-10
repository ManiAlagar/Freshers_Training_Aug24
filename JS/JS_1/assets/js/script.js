function findLargestSmallestString(event) {
    event.preventDefault();
    let text = document.getElementById("inputField").value.trim().replace(/\s\s+/g, ' ');
    document.getElementById("res").style.color = "red";
    let words = text.split(" ");
    let regexSpclChar = /[^a-zA-Z0-9\s]/;
    let largest = words[0];
    let smallest = words[0];
    let result;

    if (text.length == 0) {
        result = "Please enter a sentence"

    }
    else if (regexSpclChar.test(text)) {
        result = "Special Character(s) not allowed"

    }
    else if (words.length == 1) {
        result = "Please enter a sentence which has more than one word"

    } else {
        for (let i = 1; i < words.length; i++) {
            if (words[i].length > largest.length) {
                largest = words[i];
            }
            if (words[i].length < smallest.length) {
                smallest = words[i];
            }
            if (largest.length == smallest.length) {
                result = "All words are equal in length";

            }
            else {
                document.getElementById("res").style.color = "green";
                result = "Largest string :" + largest + ", Smallest string :" + smallest;
                alert(result);
            }
        }
    }

    document.getElementById("res").innerHTML = result;
}

function resetFunction() {
    document.getElementById("res").innerHTML = "";
    document.getElementById("inputField").value = "";
}

