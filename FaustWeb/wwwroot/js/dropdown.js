const dropdownBtn = document.getElementById("expandDropdown");
const dropdownMenu = document.getElementById("m_dropdown");

let expanded = false;


const toggleDropdown = function () {
  dropdownMenu.classList.toggle("show");
};

const showCheckboxes = function (id) {
  let checkboxes = document.getElementById("checkboxes-" + id);
  if (!expanded) {
    checkboxes.style.display = "block";
    expanded = true;
  } 
  else {
    checkboxes.style.display = "none";
    expanded = false;
  }
};

dropdownBtn.addEventListener("click", function (e) {
  e.stopPropagation();
  toggleDropdown();
});