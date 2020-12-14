// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
getImage = document.querySelector('input[type="file"]') // document.getElementById("ImageFile");
ShowImage = document.getElementById("ImageShow")
getImage.addEventListener("change", function () {
    const reader = new FileReader()
    reader.onload = function () {
        ShowImage.src = reader.result;
    }
    reader.readAsDataURL(getImage.files[0])
}, false)
