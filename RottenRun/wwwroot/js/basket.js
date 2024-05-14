const modal = document.getElementById('confirmModal');
const openModalBtn = document.getElementById('openModalBtn');
const confirmBtn = document.getElementById('confirmBtn');
const cancelBtn = document.getElementById('cancelBtn');
var form;
var buttons = document.querySelectorAll('.btnDelete');
const mapContainer = document.getElementById('mapContainer');
const btnOpenMap = document.getElementById('btnOpenMap');
const btnCloseMap = document.getElementById('btnCloseMap');
var stringAddress;
btnOpenMap.addEventListener('click', function() {
    mapContainer.style.display = 'block';
});
function CloseMap() {
    mapContainer.style.display = 'none';
}

buttons.forEach(function(button) {
    button.addEventListener('click', function(event) {
        var clickedButton = event.target;
        form = clickedButton.closest('form');
        modal.style.display = 'block';
    });
});
confirmBtn.addEventListener('click', function() {
    if(form) form.submit();
    closeModal();

});
cancelBtn.addEventListener('click', closeModal);

function closeModal() {
    modal.style.display = 'none';
}
var map = L.map('map').setView([53.7223, 91.4431], 13);
L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 19,
    attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
}).addTo(map);
var marker;
function onMapClick(e) {
    if (marker) {
        map.removeLayer(marker); 
    }
    marker = L.marker(e.latlng).addTo(map); 

    
    getAddressFromCoordinates(e.latlng);
}

map.on('click', onMapClick);
function getAddressFromCoordinates(latlng) {
    var xhr = new XMLHttpRequest();
    var url = 'https://nominatim.openstreetmap.org/reverse?format=jsonv2&lat=' + latlng.lat + '&lon=' + latlng.lng;

    xhr.open('GET', url, true);
    xhr.onload = function () {
        if (xhr.status >= 200 && xhr.status < 400) {
            var response = JSON.parse(xhr.responseText);
            var address = response.display_name;
            
            sendAddress(address);
        }
    };
    xhr.send();
}
function sendAddress(address)
{
    console.log(address);
    const parts = address.split(',').map(part => part.trim());
    const houseNumber = parts[0];
    const street = parts[1];
    const city = parts[2];
    stringAddress = city +','+street+','+houseNumber;
    
    document.getElementById('inputCity').textContent = city;
    document.getElementById('inputHome').textContent = houseNumber;
    document.getElementById('inputStreet').textContent = street;
}
async function saveAddress() {
    if(stringAddress === null) 
        return ;
    try {
        let response = await fetch('/Basket/AddDeliveryAddress?', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(stringAddress),
        });

        if (!response.ok) {
            console.log("Error query");
        }
    } catch (error) {
        console.error('Ошибка:', error);
    }
    CloseMap();
    window.location.href = "/Basket";
    
}