// remove active class
const list = document.querySelectorAll('.list')
function activeLink() {
    list.forEach((item) =>
        item.classList.remove('active'));
    this.classList.add('active');
    debugger
    let id = this.getAttribute("id");
    sessionStorage.setItem("side-nav", id);

}
list.forEach((item) =>
    item.addEventListener('click', activeLink));

//change active class
debugger
const sideNav = sessionStorage.getItem("side-nav");
if (sideNav) {
    list.forEach((item) =>
        item.classList.remove('active'));
    const elem = document.getElementById(sideNav);
    elem.classList.add('active');
}






/*.........................
Profile-dropdown styling
.........................*/

function showMenu() {
    var showDropMenu = document.getElementById("show-dropdown");
    if (showDropMenu.style.display == "none") {
        showDropMenu.style.display = "block";
    }
    else {
        showDropMenu.style.display = "none"
    }
}

$(document).ready(function () {
    $('#example').DataTable();
});
