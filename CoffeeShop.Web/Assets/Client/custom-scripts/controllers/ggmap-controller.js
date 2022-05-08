var contact = {
    init: function () {
        contact.registerEvent();
        alert("Google map is ready to start")
    },
    registerEvent: function () {
        contact.initMap();
    },

    initMap: function () {
         // This example displays a marker at the center of Australia.
        // When the user clicks the marker, an info window opens.
        const uluru = { lat: -25.363, lng: 131.044 };
        const map = new google.maps.Map(document.getElementById("map_area"), {
            zoom: 4,
            center: uluru,
        });
        const contentString = $('#hidenContactDetail').val();
           
        const infowindow = new google.maps.InfoWindow({
            content: contentString,
        });
        const marker = new google.maps.Marker({
            position: uluru,
            map,
            title: "Uluru (Ayers Rock)",
        });

        marker.addListener("click", () => {
            infowindow.open({
                anchor: marker,
                map,
                shouldFocus: false,
            });
        });

        window.initMap = initMap;
    }
}

contact.init();
