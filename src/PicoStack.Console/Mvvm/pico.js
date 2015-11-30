var pico = pico || {};

pico.getJSON = function(url, success, error) {
    
    var xhr = new XMLHttpRequest();
    xhr.open('GET', url);

    xhr.onreadystatechange = function() {
        if (xhr.readyState === 4) { // DONE
            if (xhr.status === 200) {
                var data = JSON.parse(xhr.responseText);
                success(data);
            } else {
                error();
            }
        }
    };

    xhr.send();
};
