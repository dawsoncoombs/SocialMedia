// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let menu = document.querySelector('#menu-icon');
let navba = document.querySelector('.navba');

menu.onclick = () => {
    menu.classList.toggle('bx-x');
    navba.classList.toggle('open');
}