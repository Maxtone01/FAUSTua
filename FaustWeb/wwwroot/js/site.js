// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let linksMatching = Array.from(document.getElementById("buttons-container").getElementsByTagName("a"))
    .filter(q => q.href == window.location.href)

linksMatching.filter(q => q.classList.contains("nav-icon-group")).forEach(q => q.classList.add("active-link"));

linksMatching.filter(q => q.classList.contains("no-text-decor")).forEach(q => Array.from(q.getElementsByTagName("div")).forEach(w => w.classList.add("active-link")));

linksMatching.filter(q => q.classList.contains("nav-link-container")).forEach(q => q.parentElement.classList.add("active-link"));

function goToMainPage() {
    window.location.href = '/';
}

function openNav() {
    document.getElementById("side-nav").style.width = "100%";
}

function closeNav() {
    document.getElementById("side-nav").style.width = "0";
}

function toggleLibraryContext() {
    let libContext = document.getElementById("library-context");
    let libContextButton = document.getElementById("lib-context-button");

    if (libContext.style.height == "0px") {
        libContext.style.height = "100px";
        libContextButton.textContent = "<";
    }
    else {
        libContext.style.height = "0px";
        libContextButton.textContent = ">";
    }
}

function toggleAboutContext() {
    let aboutContext = document.getElementById("about-us-context");
    let aboutContextButton = document.getElementById("about-context-button");

    if (aboutContext.style.height == "0px") {
        aboutContext.style.height = "135px";
        aboutContextButton.textContent = "<";
    }
    else {
        aboutContext.style.height = "0px";
        aboutContextButton.textContent = ">";
    }
}

document.addEventListener('DOMContentLoaded', function () {
    setupLoginForm();
    setupRegisterForm();

    function setupLoginForm() {
        const loginForm = document.getElementById('loginForm');
        const submitLoginButton = document.getElementById('submitLogin');
        if (loginForm && submitLoginButton) {
            submitLoginButton.addEventListener('click', function () {
                loginForm.submit();
            });
        }
    }

    function setupRegisterForm() {
        const registerForm = document.getElementById('registerForm');
        const submitRegisterButton = document.getElementById('submitRegister');
        if (registerForm && submitRegisterButton) {
            submitRegisterButton.addEventListener('click', function () {
                registerForm.submit();
            });
        }
    }
});
