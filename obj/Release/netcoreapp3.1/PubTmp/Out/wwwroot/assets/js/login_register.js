// Function To Display The Sign In Page
function showSignIn(){
    var firstPage = document.getElementById('first-page');
    var sign_in = document.getElementById('sign_in');
    var register = document.getElementById('register');
    firstPage.style.opacity = '0';
    sign_in.style.opacity = '1';
    register.style.opacity = '0';

    firstPage.style.zIndex = '-1';
    sign_in.style.zIndex = '10';
    register.style.zIndex = '-1';
}
// Function To Dispaly Register Page
function showRegister(){
    var firstPage = document.getElementById('first-page');
    var sign_in = document.getElementById('sign_in');
    var register = document.getElementById('register');
    firstPage.style.opacity = '0';
    sign_in.style.opacity = '0';
    register.style.opacity = '1';

    firstPage.style.zIndex = '-1';
    sign_in.style.zIndex = '-1';
    register.style.zIndex = '10';
}

// Function To Display The Password And Hide
function displayPassword(element){
    var passwordInput = element.parentElement.children[1];
    if(passwordInput.getAttribute('type') == 'password' ){
        passwordInput.setAttribute('type', 'text');
        element.style.color = 'black';
        element.classList.remove("fa-eye");
        element.classList.add("fa-eye-slash");
    }
    else{
        passwordInput.setAttribute('type', 'password');
        element.style.color = 'rgb(3,169,244)';
        element.classList.add('fa-eye');
        element.classList.remove('fa-eye-slash');
    }
}

// Function TO Display The Branch In Register Page
function showBranch(){
    var arrow = document.getElementById('arrow');
    var branch = document.getElementById('branch');

    if(branch.classList.contains('branch-active')){
        branch.classList.remove('branch-active');
        arrow.style.transform = ''
    }
    else{
        branch.classList.add('branch-active');
        arrow.style.transform = 'rotate(180deg)'
    }
}

// Function To Change The Text To See The Branch Selected
function selectedBranch(element){
    var branchText = document.getElementById('branch-text');
    branchText = branchText.textContent = "فرع " + element.value;
}


// Function To Hide User Edit Dialog
function hideUserEdit(){
    var userEdit = document.getElementById('user-edit');
    userEdit.style.zIndex = "-1";
    userEdit.style.opacity = "0";
    userEdit.style.position = "absolute";
}

// Function To Show User Edit Dialog
function showUserEdit(){
    var userEdit = document.getElementById('user-edit');
    userEdit.style.zIndex = "1";
    userEdit.style.opacity = "1";
    userEdit.style.position = "fixed";
}