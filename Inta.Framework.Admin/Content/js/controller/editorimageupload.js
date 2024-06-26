var CropCoordinat;

var ResizeImage = function () {
    var width = 700;
    var height = 700;
    var boxWidth = 700;
    var boxHeight = 700;


    const img = new Image();
    img.onload = function () {
        trueWidth = this.width;
        trueHeight = this.height;

        var imageWidth = trueWidth;
        var imageHeight = trueHeight;


        //Resim 800px genişliğe göre ayarlanır.
        if (trueWidth > 700) {
            var sizeRatio = 700 / trueWidth;

            boxWidth = parseInt(width * sizeRatio);
            boxHeight = parseInt(height * sizeRatio);
            imageWidth = parseInt(trueWidth * sizeRatio);
            imageHeight = parseInt(trueHeight * sizeRatio);
        }

        //if (imageWidth >= 800) {
        //    imageWidth = 800;
        //    imageHeight = imageWidth * this.height / this.width;
        //}


        $("#imagePreview img").attr("width", this.width);
        $("#imagePreview img").attr("height", this.height);



        var oranDurumu = false;
        if (boxWidth > imageWidth) {
            boxWidth = imageWidth;
        }

        if (boxHeight > imageHeight) {
            boxHeight = imageHeight;
        }

        var x = imageWidth / 2 - boxWidth / 2;
        var y = imageHeight / 2 - boxHeight / 2;
        var x1 = x + width;
        var y1 = y + height;


        $("#imagePreview").Jcrop({
            aspectRatio: oranDurumu ? boxWidth / boxHeight : false,
            setSelect: [x, y, x1, y1],
            allowSelect: false,
            allowMove: true,
            allowResize: true,
            fixedSupport: false,
            //boxWidth: boxWidth,
            //boxHeight: boxHeight,
            boxWidth: imageWidth,
            boxHeight: imageHeight,
            trueSize: [trueWidth, trueHeight],
            onSelect: function (c) {
                CropCoordinat = c;
            }
        });
    }
    img.src = $("#imagePreview").attr("src");

}

var CropImage = function (id) {
    var w = CropCoordinat.w;
    var h = CropCoordinat.h;
    var x = CropCoordinat.x;
    var y = CropCoordinat.y;

    var splitImageUrl = $("#" + id).attr("src").split("/");

    $.ajax({
        url: "/EditorImageUpload/CropImage",
        type: "POST",
        data: { "imageName": splitImageUrl[splitImageUrl.length - 1].split("?")[0], "width": parseInt(w), "height": parseInt(h), "x": parseInt(x), "y": parseInt(y) },
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success: function (data) {
            if (data.ResultMessage == "OK") {
                console.log(data.ImageUrl);
                $("#jcrop").html("<img id='imagePreview' src='" + data.ImageUrl +"'/>");
                ResizeImage();
                ListImageLoad();
                setTimeout(function () {
                    showAlert(".popupMessage", "Resim başarıyla croplandı.", "success");
                }, 100);
            }

        }, error: function (data) {
            setTimeout(function () {
                showAlert(".popupMessage", "Resim croplama sırasında hata oluştu.", "error");
            }, 100);
        }
    });

    console.log(CropCoordinat);
}

var ListImageLoad = function () {
    $('#fileList').find('option').remove();

    $.ajax({
        url: "/EditorImageUpload/GetImageList",
        type: "GET",
        dataType: 'json',
        contentType: false,
        processData: false,
        success: function (data) {
            debugger;
            var img = "";
            for (var i = 0; i < data.length; i++) {
                if (data[i].FullName == window.top.opener.CKEDITOR.dialog.getCurrent().preview.$.src.split(window.origin)[1]) {
                    img += "<a href='#'><img src='" + data[i].FullName + "' class='selected' /></a>";
                } else {
                    img += "<a href='#'><img src='" + data[i].FullName + "'  /></a>";
                }

            }
            $("#fileListImages").html(img);


            $("#fileListImages img").click(function () {
                $("#jcrop").html("<img src='' id='imagePreview' />");
                $("#imagePreview").css("display", "block");
                $("#imagePreview").attr("src", $(this).attr("src"));
                ResizeImage();

            });

        }, error: function (data) {

        }
    });

}


$(function () {
    setTimeout(function () { $("#fileListImages").perfectScrollbar(); }, 200);



    ListImageLoad();



    $("#saveForm").submit(function (e) {
        e.preventDefault();

        if ($(this).FormValidate() == false)
            return;

        $("#saveForm").find("button[type='submit']").prop('disabled', true);


        var form = new FormData($('form')[0]);


        $.ajax({
            url: "/EditorImageUpload/Save",
            type: "POST",
            data: form,
            dataType: 'json',
            processData: false,
            contentType: false,
            success: function (data) {
                if (data == "OK") {
                    ListImageLoad();
                    setTimeout(function () {
                        showAlert(".popupMessage", "Resim yükleme işlemi başarıyla tamamlandı.", "success");
                    }, 100);
                } else if (data == "EXISTS") {
                    showAlert(".popupMessage", "Resim yükleme işlemi sırasında hata oluştu. Lütfen dosya ismini kontrol ediniz.", "error");
                }

                $("#saveForm").find("button[type='submit']").prop('disabled', false);
                $('#saveForm').trigger("reset");


            }, error: function (data) {
                setTimeout(function () {
                    showAlert(".popupMessage", "Resim yükleme işlemi sırasında hata oluştu. Lütfen alanları kontrol ediniz.", "error");
                }, 100);
            }
        });
    });



})