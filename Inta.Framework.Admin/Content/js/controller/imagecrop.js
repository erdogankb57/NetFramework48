
var CropCoordinat;

var ResizeImage = function (width, height) {
    var boxWidth = width * 0.5;
    var boxHeight = height * 0.5;


    const img = new Image();
    img.onload = function () {
        trueWidth = this.width;
        trueHeight = this.height;


        var imageWidth = trueWidth*0.5;
        var imageHeight = trueHeight*0.5;

        //if (imageWidth >= 800) {
        //    imageWidth = 800;
        //    imageHeight = imageWidth * this.height / this.width;
        //}


        $("#imagePreview img").attr("width", this.width);
        $("#imagePreview img").attr("height", this.height);



        var oranDurumu = true;
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

var CropImage = function (id, saveUrl) {
    var w = CropCoordinat.w;
    var h = CropCoordinat.h;
    var x = CropCoordinat.x;
    var y = CropCoordinat.y;

    var splitImageUrl = $("#" + id).attr("src").split("/");

    $.ajax({
        url: "/ImageCrop/CropImage",
        type: "POST",
        data: { "imageName": splitImageUrl[splitImageUrl.length - 1].split("?")[0], "width": parseInt(w), "height": parseInt(h), "x": parseInt(x), "y": parseInt(y) },
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success: function (data) {
            debugger;
            if (data.ResultMessage == "OK") {
                console.log(data.ImageUrl);
                $("#jcrop").html("<img id='imagePreview' src='" + data.ImageUrl + "'/>");
                ResizeImage($("#Width").val(), $("#Height").val());
                setTimeout(function () {
                    showAlert(".popupMessage", "Resim başarıyla croplandı.", "success");
                }, 100);

                if (saveUrl != null) {
                    location.href = saveUrl;
                }
            }

        }, error: function (data) {
            setTimeout(function () {
                showAlert(".popupMessage", "Resim croplama sırasında hata oluştu.", "error");
            }, 100);
        }
    });

    console.log(CropCoordinat);
}
