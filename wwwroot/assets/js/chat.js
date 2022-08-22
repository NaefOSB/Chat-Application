
// Function To Change Color From Accounts
function activing(element){
    var account = document.getElementById('accounts');
    var chat = document.getElementById('chating');
    var actives = document.getElementsByClassName('to-remove-active');
    for (let index = 0; index < actives.length; index++) {
        if(actives[index].classList.contains('row-active')){
            actives[index].classList.remove('row-active');
        }
        
    }    
    element.classList.add('row-active');
    // To Show the Chating
    if(window.innerWidth > 640){
        chat.style.display = 'flex';
        account.style.display = 'none';
    }

    
}

// Function To Show The Accounting
function accounts(){
    
    var chat = document.getElementById('chating');
    var accounts = document.getElementById('accounts');
    chat.style.display = 'none';
    accounts.style.display = 'flex';

}

// Function Whene The Width Change
function widthChange(){
    var chat = document.getElementById('chating');
    var accounts = document.getElementById('accounts');
    if(window.innerWidth > 640){
        chat.style.display = 'flex';
        accounts.style.display = 'flex';
    }
    else{
        chat.style.display = 'none';
    }
}

// Function To Show The Icons Of Usrt Account
function showUserAccount(){
    var user_arrow = document.getElementById('user_arrow');
    var edit = document.getElementById('edit');
    var out = document.getElementById('out');
    if(edit.classList.contains('edit') && out.classList.contains('out')){
        edit.classList.remove('edit');
        out.classList.remove('out');
        user_arrow.classList.remove('user-click');
    }
    else{
        edit.classList.add('edit');
        out.classList.add('out');
        user_arrow.classList.add('user-click');

    }
}


// Start Chat Page 
// Function To Change between Group Page And Notification Page
function selectTopBar(element){
    var group = document.getElementById('group');
    var notification = document.getElementById('notification');
    var chatBody = document.getElementById('chat-body');
    if(element.children[2].classList.contains('active')){
        group.classList.remove('active');
        notification.classList.remove('active');
        element.children[2].classList.add('active');
    }
    else{
        group.classList.remove('active');
        notification.classList.remove('active');
        element.children[2].classList.add('active');

    }
    if(element.getAttribute('data') == 'group'){
        chatBody.classList.add('chat-body-click');
    }
    else{
        chatBody.classList.remove('chat-body-click');

    }
}

// Function To Show Chatting Page
function showGropPage(element){
    var topBar = document.getElementById('top-bar');
    var name = element.children[1];
    var groupName = document.getElementById('groub-name');
    var chattingPage = document.getElementById('chating-page');
    var chatBody = document.getElementById('chat-body');
    var topChat = document.getElementById('top-chat');

    chatBody.style.opacity = '0';
    chatBody.style.zIndex = '-1';
    chatBody.style.position = 'absolute';

    topBar.style.opacity = '0';
    topBar.style.zIndex = '-1';
    topBar.style.position = 'absolute';

    chattingPage.style.opacity = '1';
    chattingPage.style.zIndex = '1';
    groupName.textContent = name.textContent;

    topChat.setAttribute('onclick','hideChatingPage(this)');


}
// Function To hide Chatting Page
function hideChatingPage(){
    var topBar = document.getElementById('top-bar');
    var chattingPage = document.getElementById('chating-page');
    var chatBody = document.getElementById('chat-body');

    chatBody.style.opacity = '1';
    chatBody.style.zIndex = '1';
    chatBody.style.position = 'relative';

    topBar.style.opacity = '1';
    topBar.style.zIndex = '1';
    topBar.style.position = 'relative';

    chattingPage.style.opacity = '0';
    chattingPage.style.zIndex = '-1';
}

// Function To Show The Single Note
function showNote(element){
    var notificationBody = document.getElementById('notification-body');
    var notificationBody2 = document.getElementById('notification-body2');
    var notPage = document.getElementById('not-page');

    notPage.children[1].textContent = element.children[1].textContent;
    notPage.children[2].textContent = element.children[2].textContent;

    notPage.style.opacity = '1';
    notPage.style.zIndex = '1';
    notPage.style.position = 'relative';

    notificationBody.style.display = 'none';
    notificationBody2.style.display = 'block';

    // notificationBody.style.opacity = '0';
    // notificationBody.style.zIndex = '-1';
    // notificationBody.style.position = 'absolute';

}

// Function To Hide The Single Note
function hideNote(){
    var notificationBody2 = document.getElementById('notification-body2');
    var notificationBody = document.getElementById('notification-body');
    notificationBody.style.display = 'block';
    notificationBody2.style.display = 'none';
    

}

// Function To Show THe Brach

function showBranch(){
    var arrowBranch = document.getElementById('arrow');
    var optionBranch = document.getElementById('options');
    var back = document.getElementById('back');
    var mainBody = document.getElementById('main-body');

if(!arrowBranch.classList.contains('rotate')){
    arrowBranch.classList.add('rotate');
    optionBranch.style.opacity = '1';
    optionBranch.style.position = 'relative';
    optionBranch.style.zIndex = '1';
    back.style.height = '32%';
    mainBody.style.height = '69%';
}
else{
    arrowBranch.classList.remove('rotate');
    optionBranch.style.opacity = '0';
    optionBranch.style.position = 'absolute';
    optionBranch.style.zIndex = '-1';
    back.style.height = '17%';
    mainBody.style.height = '84%';


}

    
}

// Function To Hide The Branch
function hideBranch(element){
    var nameBranch = document.getElementById('name');
    var arrowBranch = document.getElementById('arrow');
    var optionBranch = document.getElementById('options');
    var back = document.getElementById('back');
    var mainBody = document.getElementById('main-body');

    nameBranch.textContent = element.parentElement.children[0].textContent;
    arrowBranch.classList.remove('rotate');
    optionBranch.style.opacity = '0';
    optionBranch.style.position = 'absolute';
    optionBranch.style.zIndex = '-1';
    back.style.height = '17%';
    mainBody.style.height = "84%";

}

// Function To Show Input Search
function showSearchInput(){
    var options = document.getElementById('options');
    var back = document.getElementById('back');
    var bulding = document.getElementById('bulding');
    var brachSelection = document.getElementById('branch-selection');
    var search = document.getElementById('search');
    var searchInput = document.getElementById('search-input');
    var mainBody = document.getElementById('main-body');

    options.style.opacity = '0';
    options.style.position = 'absolute';
    options.style.zIndex = '-1';
    back.style.height = '17%';

    bulding.style.color = "rgb(164,221,250)";

    brachSelection.style.opacity = '0';
    brachSelection.style.position = 'absolute';
    brachSelection.style.zIndex = '-1';
    
    search.style.color = "rgb(35,169,244)";

    searchInput.style.opacity = '1';
    searchInput.style.position = 'initial';
    searchInput.style.zIndex = '1';

    mainBody.style.height = "84%";
    
}

// Function To Show The Buldeing
function showBuldeing(){
    var arrowBranch = document.getElementById('arrow');
    var options = document.getElementById('options');
    var back = document.getElementById('back');
    var bulding = document.getElementById('bulding');
    var brachSelection = document.getElementById('branch-selection');
    var search = document.getElementById('search');
    var searchInput = document.getElementById('search-input');
    var mainBody = document.getElementById('main-body');

    arrowBranch.classList.add('rotate');
    options.style.opacity = '1';
    options.style.position = 'relative';
    options.style.zIndex = '1';
    back.style.height = '32%';

        
    bulding.style.color = "rgb(35,169,244)";

    brachSelection.style.opacity = '1';
    brachSelection.style.position = 'relative';
    brachSelection.style.zIndex = '0';
    
    search.style.color = "rgb(164,221,250)";

    searchInput.style.opacity = '0';
    searchInput.style.position = 'absolute';
    searchInput.style.zIndex = '-1';

    mainBody.style.height = '69%';
}


// Function To Hide The Select Users Page
function hideSelectUsersPage(){
    var chatBody = document.getElementById('chat-body');
    var topBar = document.getElementById('top-bar');
    var selectUsersPage = document.getElementById('select-users-page');

    chatBody.style.position = "relative";
    chatBody.style.zIndex = '1';
    chatBody.style.opacity = '1';

    topBar.style.position = "relative";
    topBar.style.zIndex = '1';
    topBar.style.opacity = '1';

    selectUsersPage.style.position = "absolute";
    selectUsersPage.style.zIndex = '-1';
    selectUsersPage.style.opacity = '0';

}

// Function To show Users Page
function showUsersPage(){
    var  chatBody = document.getElementById('chat-body');
    var topBar = document.getElementById('top-bar');
    var selectUsersPage = document.getElementById('select-users-page');

    chatBody.style.position = "absolute";
    chatBody.style.zIndex = '-1';
    chatBody.style.opacity = '0';

    topBar.style.position = "absolute";
    topBar.style.zIndex = '-1';
    topBar.style.opacity = '0';

    selectUsersPage.style.position = "realtive";
    selectUsersPage.style.zIndex = '1';
    selectUsersPage.style.opacity = '1';
}


// Function To Show Chatting Page
function showChatingUserPage(element){
    var selectUsersPage = document.getElementById('select-users-page');
    var name = element.children[1].children[0];
    var userName = document.getElementById('groub-name');
    var chattingPage = document.getElementById('chating-page');
    var topChat = document.getElementById('top-chat');

    selectUsersPage.style.opacity = '0';
    selectUsersPage.style.zIndex = '-1';
    selectUsersPage.style.position = 'absolute';

    chattingPage.style.opacity = '1';
    chattingPage.style.zIndex = '1';
    console.log(name.textContent);
    userName.textContent = name.textContent;

    topChat.setAttribute('onclick','hideChatingPage2()');

}


// Function To Hide Chating Page When You Come From User Page
function hideChatingPage2(){
    var chatBody = document.getElementById('chating-page');
    var selectUsersPage = document.getElementById('select-users-page');
    var topBar = document.getElementById('top-bar');
    console.log("tee");
    chatBody.style.opacity = '0';
    chatBody.style.zIndex = '-1';
    chatBody.style.position = 'absolute';

    topBar.style.opacity = '0';
    topBar.style.zIndex = '-1';
    topBar.style.position = 'absolute';

    selectUsersPage.style.opacity = '1';
    selectUsersPage.style.zIndex = '1';
    selectUsersPage.style.position = 'relative';
}

// End Chat Page

