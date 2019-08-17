function submitForm(form, productId) {
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
        var newImageId = $("#product-gallery").children().length + 1;

        // do something with the result now
        var newImage = $(`
                <div class="product-thumbnail-container" data-imageId="${newImageId}">
                    <img src=${result} alt="ASP.NET" class="img-responsive"/>
                    <button class="btn-danger" onclick="deletePhoto(${productId}, '${newImageId}', this)">
                        <i class="fa fa-window-close-o" aria-hidden="true"></i>
                    </button>
                </div>`);

        $("#product-gallery").append(newImage);
        console.log(result);
    });
}

function deletePhoto(productId, imageId, caller) {
    var data = {
        productId: productId,
        imageId: imageId
    };

    $.ajax({
        url: '/api/products/deleteproductphoto',
        type: 'DELETE',
        data: data,
        success: function (result) {
            $(caller).parent().remove();
        }
    });
}