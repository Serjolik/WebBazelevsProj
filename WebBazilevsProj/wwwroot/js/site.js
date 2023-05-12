
//Send

document.getElementById("myForm").addEventListener("submit", function (event) {
    event.preventDefault(); // prevent default form submission behavior
    var formData = new FormData(event.target); // get form data
    var inputString = formData.get("myInput"); // get input field value
    var xhr = new XMLHttpRequest(); // create new HTTP request
    xhr.open("POST", "/myaction"); // set request URL
    xhr.setRequestHeader("Content-Type", "application/json"); // set request content type

    var data = JSON.stringify(inputString); // convert input string to JSON format
    xhr.send(data); // send request data

    xhr.onload = function () {
        if (xhr.status == 200) {
            var response = JSON.parse(xhr.responseText); // parse response JSON
            document.getElementById("myOutput").textContent = response.message; // update page with response data
        } else {
            console.log("Error: " + xhr.status); // log error status to console
        }
    };
});