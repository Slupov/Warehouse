function submitForm(form) {
    var formAction = $(form).attr("action");
    var fdata = new FormData();

    var fileInput = $('#UploadPhoto')[0];
    var file = fileInput.files[0];
    fdata.append("UploadPhoto", file);

    // You can update the jquery selector to use a css class if you want
    $("input[type='hidden'").each(function (x, y) {
        fdata.append($(y).attr("name"), $(y).val());
    });

    $.ajax({
        type: 'post',
        url: formAction,
        data: fdata,
        processData: false,
        contentType: false
    }).done(function (result) {
        // do something with the result now
        var newImage = $(`<img src=${result} alt="ASP.NET" class="img-responsive"/>`);

        $("#product-gallery").append(newImage);
        console.log(result);
    });
}