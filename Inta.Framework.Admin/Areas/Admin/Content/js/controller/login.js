$(function () {
    $(function () {
        $("input,textarea,select").change(function () {
            $(this).parent("div").find(".error").remove();
        })
    });

    $("#loginForm").submit(function (e) {
        e.preventDefault();

        //if ($(this).FormValidate() == false)
        //    return;
        $("#loginForm .error").remove();

        var formData = new FormData($('#loginForm')[0]);
        $.ajax({
            url: "/Admin/Login/SignIn",
            type: "POST",
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (data) {

                if (data.Status == "OK") {
                    location.href = data.ReturnUrl;
                } else {
                    //showAlert(".loginMessage", data.Message, "error");

                    //Hata mesajı var ise hataları gösterir
                    if (data.Validation != null) {
                        if (data.Validation.length > 0) {
                            for (var i = 0; i < data.Validation.length; i++) {
                                var item = data.Validation[i];
                                var key = item.Key;
                                console.log(key);
                                for (var j = 0; j < item.Error.length; j++) {
                                    var ErrorMessage = item.Error[j].ErrorMessage;
                                    $("#loginForm #" + key).parent("div").append("<div class='error text-danger'>" + ErrorMessage + "</div>");
                                }
                            }
                        }
                    } else {
                        setTimeout(function () {
                            showAlert(".loginMessage", "Kullanıcı adı yada şifre yanlış.Lütfen bilgilerinizi kontrol ediniz.", "error");
                        }, 100);
                    }
                }
                $("#saveForm").find("button[type='submit']").prop('disabled', false);


            }, error: function (data) {
                setTimeout(function () {
                    showAlert(".popupMessage", "Login işlemi sırasında hata oluştu. Lütfen tekrar deneyiniz.", "error");
                }, 100);
            }
        });
    });

})