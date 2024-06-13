var IsOpen =false;
var IsClick = false;
var searchText = document.getElementById('searchText');
function clickProfile(){
        document.getElementById('containerProfile').style.display = "block";
        IsClick = true;
        IsOpen=true;
   
}
document.addEventListener('click', function(event) {
    var form = document.getElementById('contentProfile');
    if(!form.contains(event.target) && IsOpen && !IsClick) {
        document.getElementById('containerProfile').style.display = "none";
        IsOpen = false;
        
    }
    IsClick = false;
});
searchText.onkeyup = function ()
{
    filterDivs();
};
function filterDivs() {
    var filter, productsList, i;
    filter = searchText.value.toUpperCase();
    productsList = document.getElementsByClassName("product");    
    for (i = 0; i < productsList.length; i++) {
        var productInfo = productsList[i].querySelector('.product-info');
        var contentSpan = productInfo.querySelector('span').textContent;
        if(contentSpan.toUpperCase().indexOf(filter)>-1)
        {
            productsList[i].style.display = "";
        } 
        else {
            productsList[i].style.display = "none";            
        }
    }
}

var divNotify = document.getElementById("divNotify");
var spanNotify = divNotify.querySelector("span");
function fadeOutAndHide(element) {
    element.classList.add('hidden');
    // Установим display: none после окончания анимации
    element.addEventListener('transitionend', function() {
        element.style.display = 'none';
        spanNotify.textContent = '';
    }, { once: true });
}
if(spanNotify.textContent !== "")
{
    divNotify.style.display = 'block';
    setTimeout(() => {
        fadeOutAndHide(divNotify);
    }, 5000);
}

function clearFilter()
{
    var BtnCancelFilter = document.getElementById('BtnCancelFilter');
    productsList = document.getElementsByClassName("product");
    for (i = 0; i < productsList.length; i++) {
        var contentSpan = productsList[i].querySelector('span').textContent;
        productsList[i].style.display = "";
        
    }
    BtnCancelFilter.style.display="none";
}
function onSelect() {
    var BtnCancelFilter = document.getElementById('BtnCancelFilter');
    const selectElement = document.getElementById('options');
    const selectedValue = selectElement.value.toUpperCase();
    productsList = document.getElementsByClassName("product");
    for (i = 0; i < productsList.length; i++) {
        var contentSpan = productsList[i].querySelector('span').textContent;
        if(contentSpan.toUpperCase().indexOf(selectedValue)>-1)
        {
            productsList[i].style.display = "";
        }
        else {
            productsList[i].style.display = "none";
        }
    }
    BtnCancelFilter.style.display="flex";    
}
var btnEdit = document.getElementById("btnEdit");
btnEdit.addEventListener("click",function (event)
{
    var profileDiv = document.getElementById("profile");
    var isEdit = false;
    if(btnEdit.textContent === "Редактировать")
    {
        btnEdit.textContent = "Сохранить изменения";
        isEdit = true;
    }
    else
    {
        isEdit = false;
        var form = document.getElementsByClassName("form-button");
        form[0].submit();
    }
    var inputs = document.getElementsByClassName("form-control");
    for (i = 0; i < inputs.length; i++) {
        if(isEdit)
        {
            inputs[i].removeAttribute("readonly");
        }  
    }    
})
    