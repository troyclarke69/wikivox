
// Autocomplete: ARTIST (Name)
$(document).ready(function () {
    $('#artistName').autocomplete({
        source: '/api/SearchRest/searchArtist'
    });
});

// Autocomplete: GENRE (Name)
$(document).ready(function () {
    $('#genreName').autocomplete({
        source: '/api/SearchRest/searchGenre'
    });
});

// Autocomplete: ALBUM (Name)
$(document).ready(function () {
    $('#albumName').autocomplete({
        source: '/api/SearchRest/searchAlbum'
    });
});

// Autocomplete: Musician (MusicianName)
$(document).ready(function () {
    $('#musicianName').autocomplete({
        source: '/api/SearchRest/searchMusician'
    });
});

// OnClick: Main/Index > Artist Search/AutoComplete > Go button
$(document).ready(function () {
    $("#loadArtist").click(function () {
        alert($('#artistName').val()); // this works!! but need the ID
        //source: '/Main/Index?artist=1'
    });
});




// SetGenre functionality not working ...
// attempting to complete code for each click/update event on a genre selection
// thru ajax to api/SearchRest

//$(document).ready(function () {
//    $('#setGenre').change({
//        source: '/api/SearchRest/SetGenre'
//    });
//});

//function SetGenre(genre, artist) {
//    alert("SetGenre");
//};
   