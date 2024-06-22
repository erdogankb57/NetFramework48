$(document).ready(function () {

    $("#saveForm").submit(function (e) {
        e.preventDefault();

        $("#saveForm .error").remove();

        var formData = new FormData($('#saveForm')[0]);
        $.ajax({
            url: "/Account/Save",
            type: "POST",
            data: formData,
            dataType: 'json',
            processData: false,
            contentType: false,
            success: function (data) {
                if (data.ResultType == 0) {
                    setTimeout(function () {
                        showAlert(".popupMessage", "Kayıt işlemi başarıyla tamamlandı.", "success");
                    }, 100);
                }
                else {
                    //Hata mesajı var ise hataları gösterir
                    if (data.Validation != null) {
                        if (data.Validation.length > 0) {
                            for (var i = 0; i < data.Validation.length; i++) {
                                var item = data.Validation[i];
                                var key = item.Key;
                                console.log(key);
                                for (var j = 0; j < item.Error.length; j++) {
                                    var ErrorMessage = item.Error[j].ErrorMessage;
                                    $("#saveForm #" + key).parent("div").append("<div class='error text-danger'>" + ErrorMessage + "</div>");
                                }
                            }
                        }
                    } else {
                        setTimeout(function () {
                            showAlert(".popupMessage", "Kayıt işlemi sırasında hata oluştu. Lütfen alanları kontrol ediniz.", "error");
                        }, 100);
                    }
                }
                scroolTop(0, 300);

            }, error: function (data) {
                setTimeout(function () {
                    showAlert(".popupMessage", "Kayıt işlemi sırasında hata oluştu. Lütfen alanları kontrol ediniz.", "error");
                }, 100);
                scroolTop(0, 300);
            }
        });
    });

});