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
