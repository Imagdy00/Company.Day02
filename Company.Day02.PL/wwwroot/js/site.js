// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


let InputSearch = document.getElementById("SearchInput");
InputSearch.addEventListener("keyup"() => {
    let xhr = new XMLHttpRequest();

    let url = `https://localhost:44327/Employee?SearchInput${InputSearch.value}`;
    xhr.open("GET", url, true);

    xhr.onreadystatechange = function () {
        if (xhr.onreadystatechange == 4 && this.state == 200) {
            console.log(this.responseText);
        }
    }

    xhr.send();

});
