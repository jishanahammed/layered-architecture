function LoadPagination(url, page) {
  
    var sizeperpage = $("#sizeperpageselect").val();
    var stringsearch = $("#Searchvalue").val();
    console.log(sizeperpage, stringsearch);

    $('#paginatedsection').empty();
    $.ajax({
        url: "/" + url + "?page=" + page + "&&pagesize=" + sizeperpage + "&&stringsearch=" + stringsearch + "",
        type: 'GET',
        success: function (result) {
            $('#paginatedsection').html(result);
        }
    });
}


