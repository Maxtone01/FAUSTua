const toggleSwitch = document.getElementById('styleSwitcher');
const listView = document.querySelector('.library-grid');

toggleSwitch.addEventListener('change', () => {
    listView.classList.toggle('library-list');
    listView.classList.toggle('library-grid');
});