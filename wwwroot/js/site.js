// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function toggleJobDescription() {
    @var jd = document.getElementById("jd");
    @if (jd.style.display === "none") {
        jd.style.display === "block";
        console.log("toggle block");

    } else {
        jd.style.display === "none";
    }
}