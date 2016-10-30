angular.module('EuroJobsCrm.controllers').controller('EventController', function ($scope, $location, $http, $state) {
    $scope.registrationActive = false;
    $scope.personsCount = 1;
    $scope.accommodation = true;
    $scope.booked = false;
    $scope.totalPrice = 0;
    $scope.email = '';
    $scope.orderId = (Math.random() * 10000).toFixed(0);
    $scope.sessionId = (Math.random() * 1000000).toFixed(0);
    $scope.bookedHotel;
    $scope.phone = '';

    $scope.registerClick = function () {
        $scope.registrationActive = true;
        $scope.calcPrice();
    }

    $scope.personsCountChange = function () {
        $scope.calcPrice();
    }

    $scope.accommodationChanged = function () {
        $scope.calcPrice();
        $scope.booked = false;
        for (i in $scope.hotels) {
            $scope.hotels[i].show = true;
        }
    }

    $scope.calcPrice = function () {
        today = new Date();
        if (today.getFullYear() == 2016 &&
            today.getMonth() == 9 &&
            today.getDate() < 17) {
            $scope.totalPrice = 19.90 * $scope.personsCount;
        }
        else {
            $scope.totalPrice = 59.90 * $scope.personsCount;
        }
    }

    $scope.bookHotel = function (hotel) {
        booked = false;
        for (i in hotel.rooms) {
            if (hotel.rooms[i].two_nigths_count > 0 ||
                hotel.rooms[i].first_nigth_count > 0 ||
                hotel.rooms[i].second_nigth_count > 0) {
                booked = true;
            }
        }

        if (!booked) {
            alert('Wybież najpierw ilość osób.');
            return;
        }

        for (i in $scope.hotels) {
            if ($scope.hotels[i].id == hotel.id) {
                continue;
            }

            $scope.hotels[i].show = false;
        }

        $scope.calcPrice();
        for (i in hotel.rooms) {
            $scope.totalPrice += hotel.rooms[i].two_nigths_count * hotel.rooms[i].price_two_nights;
            $scope.totalPrice += hotel.rooms[i].first_nigth_count * hotel.rooms[i].price_per_night;
            $scope.totalPrice += hotel.rooms[i].second_nigth_count * hotel.rooms[i].price_per_night;
        }

        $scope.booked = true;
        $scope.bookedHotel = hotel;
    }

    $scope.orderAndPay = function () {
        if($scope.email.length === 0){
            alert('Wprowadź email');
            return;
        }

        var order = {
            email: $scope.email,
            totalPrice: $scope.totalPrice,
            personsCount : $scope.personsCount,
            persons : $scope.persons,
            needAccommodation : $scope.accommodation,
            hotel: $scope.bookedHotel,
            phone: $scope.phone
        }
        $http({
            url: '/event/OrderEvent',
            method: "POST",
            data: order
        }).success(function(){
            document.getElementById('payuForm').submit()
        }).error(function(error){
            $state.go('error');
        });
    }

    $scope.persons = [];
    for (var i = 0; i < 11; i++) {
        $scope.persons.push({
            name: '',
            surname: '',
            number: i
        });
    }

    $scope.hotels = [
        {
            id: 1,
            name: 'Willa u Jadzi - miejsce szkolenia',
            address: 'ul.Krzeptowki 99 B Zakopane',
            website: 'http://www.ujadzi.com/ ',
            logo: '/images/willa-u-jadzi.png',
            rooms: [
                {
                    name: 'Pokój 1 osobowy – nocleg ze śniadaniem. Cena od 87 zł/osoba/doba',
                    price_per_night: 120,
                    price_two_nights: 174,
                    two_nigths_count: 0,
                    first_nigth_count: 0,
                    second_nigth_count: 0,
                    id : 1
                },
                {
                    name: 'Pokój 2 osobowy – nocleg ze śniadaniem. Cena od 67 zł/osoba/doba',
                    price_per_night: 194,
                    price_two_nights: 268,
                    two_nigths_count: 0,
                    first_nigth_count: 0,
                    second_nigth_count: 0,
                    id : 2
                },
            ],
            show: false,
        },
        {
            id: 2,
            name: 'Willa Ryś',
            address: 'Droga na Antałówkę 15 Zakopane',
            website: 'http://www.willarys.pl/',
            logo: '/images/willa-rys.png',
            rooms: [
                {
                    name: 'Pokój 2 osobowy - nocleg ze śniadaniem. Cena od 95 zł/osoba/doba',
                    price_per_night: 240,
                    price_two_nights: 380,
                    two_nigths_count: 0,
                    first_nigth_count: 0,
                    second_nigth_count: 0,
                    id : 1
                },
                {
                    name: 'Pokój 3 osobowy - nocleg ze śniadaniem. Cena od 95 zł/osoba/doba',
                    price_per_night: 360,
                    price_two_nights: 570,
                    two_nigths_count: 0,
                    first_nigth_count: 0,
                    second_nigth_count: 0,
                    id : 2
                },
                {
                    name: 'Pokój 4 osobowy - nocleg ze śniadaniem. Cena od 95 zł/osoba/doba',
                    price_per_night: 480,
                    price_two_nights: 760,
                    two_nigths_count: 0,
                    first_nigth_count: 0,
                    second_nigth_count: 0,
                    id : 3
                },
            ],
            show: true,
        },
        {
            id: 3,
            name: 'Pokoje gościnne Światłomir',
            address: 'ul. Żeromskiego 35 Zakopane',
            website: 'http://swiatlomirzakopane.pl/ ',
            logo: '/images/swiatlomir.png',
            rooms: [
                {
                    name: 'Pokój 2 osobowy - nocleg bez śniadania. Cena od 53 zł/osoba/doba',
                    price_per_night: 154,
                    price_two_nights: 212,
                    two_nigths_count: 0,
                    first_nigth_count: 0,
                    second_nigth_count: 0,
                    id : 1
                },
                {
                    name: 'Pokój 3 osobowy - nocleg bez śniadania. Cena od 53 zł/osoba/doba',
                    price_per_night: 231,
                    price_two_nights: 318,
                    two_nigths_count: 0,
                    first_nigth_count: 0,
                    second_nigth_count: 0,
                    id : 2
                },
                {
                    name: 'Pokój 4 osobowy - nocleg bez śniadania. Cena od 53 zł/osoba/doba',
                    price_per_night: 308,
                    price_two_nights: 424,
                    two_nigths_count: 0,
                    first_nigth_count: 0,
                    second_nigth_count: 0,
                    id : 3
                },
            ],
            show: true,
        },
        {
            id: 4,
            name: 'Hotel Kasprowy Wierch ',
            address: 'ul. Krupówki 50 B Zakopane',
            website: 'http://www.hotelkasprowy.com/',
            logo: '/images/kasprowy-wierch.png',
            rooms: [
                {
                    name: 'Pokój 2-osobowy z łazienką na korytarzu do własnej dyspozycji. - nocleg ze śniadaniem. Cena od 73 zł/osoba/doba',
                    price_per_night: 0,
                    price_two_nights: 292,
                    two_nigths_count: 0,
                    first_nigth_count: 0,
                    second_nigth_count: 0,
                    id : 1
                },
                {
                    name: 'Pokój 2-osobowy Standard - nocleg ze śniadaniem. Cena od 73 zł/osoba/doba',
                    price_per_night: 0,
                    price_two_nights: 322,
                    two_nigths_count: 0,
                    first_nigth_count: 0,
                    second_nigth_count: 0,
                    id : 2
                },
                {
                    name: 'Pokój 2-osobowy Delux - nocleg ze śniadaniem. Cena od 73 zł/osoba/doba',
                    price_per_night: 0,
                    price_two_nights: 428,
                    two_nigths_count: 0,
                    first_nigth_count: 0,
                    second_nigth_count: 0,
                    id : 3
                },
                {
                    name: 'Pokój 3-osobowy z łazienką na korytarzu do własnej dyspozycji - nocleg ze śniadaniem. Cena od 73 zł/osoba/doba',
                    price_per_night: 0,
                    price_two_nights: 438,
                    two_nigths_count: 0,
                    first_nigth_count: 0,
                    second_nigth_count: 0,
                    id : 4
                },
                {
                    name: 'Pokój 3-osobowy Standard - nocleg ze śniadaniem. Cena od 83 zł/osoba/doba',
                    price_per_night: 0,
                    price_two_nights: 498,
                    two_nigths_count: 0,
                    first_nigth_count: 0,
                    second_nigth_count: 0,
                    id : 5
                },
                {
                    name: 'Pokój 3-osobowy Delux - nocleg ze śniadaniem. Cena od 107 zł/osoba/doba',
                    price_per_night: 0,
                    price_two_nights: 642,
                    two_nigths_count: 0,
                    first_nigth_count: 0,
                    second_nigth_count: 0,
                    id : 6
                },
                {
                    name: 'Pokój 4-osobowy z łazienką na korytarzu do własnej dyspozycji - nocleg ze śniadaniem. Cena od 73 zł/osoba/doba',
                    price_per_night: 0,
                    price_two_nights: 584,
                    two_nigths_count: 0,
                    first_nigth_count: 0,
                    second_nigth_count: 0,
                    id : 7
                },
                {
                    name: 'Pokój 4-osobowy Delux - nocleg ze śniadaniem. Cena od 107 zł/osoba/doba',
                    price_per_night: 0,
                    price_two_nights: 856,
                    two_nigths_count: 0,
                    first_nigth_count: 0,
                    second_nigth_count: 0,
                    id : 8
                },
            ],
            show: true,
        }

    ];

    var json = 'http://ipv4.myexternalip.com/json';
    $http.get(json).then(function (result) {
        $scope.ip = result.data.ip;
    }, function (e) {
        $scope.ip = '127.0.0.1';
    });
}); 