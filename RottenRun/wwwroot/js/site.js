var IsOpen =false;
var IsClick = false;
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
