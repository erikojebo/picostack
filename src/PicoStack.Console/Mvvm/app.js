document.addEventListener("DOMContentLoaded", function () {

    pico.getJSON('http://localhost:8002/users', function(data) {
        console.log(data);
    }, function() {
        console.error("Error occurred when loading JSON data from the server");
    });

});