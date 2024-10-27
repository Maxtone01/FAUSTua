const forms = document.querySelector(".forms");
const pwShowHide = document.querySelectorAll(".eye-icon");
const links = document.querySelectorAll(".link");  

const forgotPassLink = document.querySelector(".forgot-pass a");
const orLink = document.querySelector(".or-link");
const submitResetLink = document.querySelector(".submit-reset");

pwShowHide.forEach(eyeIcon => {
    eyeIcon.addEventListener("click", () => {
        let pwFields = eyeIcon.parentElement.parentElement.querySelectorAll(".password");
        
        pwFields.forEach(password => {
            if(password.type === "password"){
                password.type = "text";
                eyeIcon.classList.replace("bx-hide", "bx-show");
                return;
            }
            password.type = "password";
            eyeIcon.classList.replace("bx-show", "bx-hide");
        })
        
    })
})  

const loginForm = document.querySelector('.login');
const signupForm = document.querySelector('.signup');
const passResetForm = document.querySelector('.pass-reset');
const passResetPopup = document.querySelector('.pass-reset-popup');

function showForm(formToShow) {
  loginForm.classList.remove('active');
  signupForm.classList.remove('active');
  passResetForm.classList.remove('active');
  passResetPopup.classList.remove('active');
  formToShow.classList.add('active');
}

showForm(loginForm);

document.querySelector('.signup-link').addEventListener('click', () => {
  showForm(signupForm);
});

document.querySelector('.forgot-pass-link').addEventListener('click', () => {
  showForm(passResetForm);
});

document.querySelector('.login-link').addEventListener('click', () => {
  showForm(loginForm);
});

document.querySelector('#closePopupButton').addEventListener('click', () => {
    showForm(loginForm);
});

document.querySelector('#closeResetButton').addEventListener('click', () => {
    showForm(loginForm);
});

$(function () {
    $('#forgotPasswordForm').on('submit', function (e) {

        e.preventDefault();

        $('#forgotPasswordValidation').hide();

        let button = $('#forgotPasswordForm button').first();
        button.prop('disabled', true);

        $.ajax({
            type: 'POST',
            url: $(this).attr('action'),
            data: $(this).serialize(),
            success: function (response) {
                showForm(passResetPopup);
                button.prop('disabled', false);
            },
            error: function (xhr) {
                $('#forgotPasswordValidation').show();
                button.prop('disabled', false);
            }
        });
    });
});