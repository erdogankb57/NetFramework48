
var selectId = [];


var $PagingDataList = {
    Init: function () {
        $PagingDataList.OrderBy();
    },
    ReloadData: function (ObjectId, Query) {
        var Url = $("#" + ObjectId).attr("Url");
        var PagingRowCount = $("#" + ObjectId).attr("PagingRowCount");
        var OrderColumnn = $("#" + ObjectId).attr("OrderColumnn");
        var OrderType = $("#" + ObjectId).attr("OrderType");
        var ActivePageNumber = $("#" + ObjectId).attr("ActivePageNumber");


        $("#" + ObjectId).attr("Query", JSON.stringify(Query));

        var Data = { Search: Query, PageRowCount: PagingRowCount, OrderColumn: OrderColumnn, OrderType: OrderType, ActivePageNumber: ActivePageNumber };

        //Seçili olan checkboxlar alınır.
        $PagingDataList.SelectedRecord(ObjectId);

        $.ajax({
            url: Url,
            type: "POST",
            dataType: 'json',
            /*         contentType: 'application/x-www-form-urlencoded; charset=UTF-8',*/
            data: Data,
            success: function (response) {
                var Count = 0;
                var htmlValue = '';
                $("#" + ObjectId).find("input[type='checkbox']").is(":checked");

                $("#" + ObjectId).attr("PageCount", response.PageCount);
                for (var i = 0; i < response.Data.length; i++) {
                    //var keys = Object.keys(response.Data[0]);
                    var keys = $("#" + ObjectId + " thead tr th");
                    htmlValue += "<tr>";
                    for (var j = 0; j < keys.length; j++) {
                        //var column = $("#" + ObjectId + " thead tr th[name='" + keys[j] + "']")
                        var column = keys[j];
                        if (column != undefined) {
                            var ColumnFormat = $(column).attr("format");
                            var width = $(column).attr("width");
                            var TextAllign = $(column).attr("TextAllign");


                            var row = response.Data[i];
                            var value = row[$(column).attr("name")];
                            if (ColumnFormat != "") {
                                //value = eval(ColumnFormat);
                                value = ColumnFormat;
                            }
                            htmlValue += "<td width='" + width + "px'>" + value + "</td>";
                        }
                    }
                    htmlValue += "</tr>";

                    Count++;
                }
                $("#" + ObjectId + " tbody").html(htmlValue);

                //if (parseInt(response.PageCount) > 1)
                {

                    var pageHtml = "<ul>";
                    pageHtml += "<li><a href='javascript:void(0)' class='prev'>Geri</a></li>";

                    if (ActivePageNumber == null) {
                        ActivePageNumber = 1
                    }
                    var PageNumberStart = 0;
                    var SkipNumber = 3;
                    var PageNumberEnd = 0;

                    if ((parseInt(SkipNumber) * 2) + 1 >= response.PageCount) {
                        PageNumberEnd = response.PageCount;
                        PageNumberStart = 1;

                        for (var i = PageNumberStart; i <= PageNumberEnd; i++) {
                            pageHtml += "<li><a href='javascript:void(0)' class='number' PageNumber='" + i + "'>" + i + "</a></li>";
                        }
                    } else {

                        if (ActivePageNumber <= SkipNumber) {
                            PageNumberEnd = parseInt(PageNumberStart) + parseInt(SkipNumber) * 2 + 1 > response.PageCount ? response.PageCount : parseInt(PageNumberStart) + parseInt(SkipNumber) * 2 + 1;
                        } else {
                            PageNumberEnd = parseInt(ActivePageNumber) + parseInt(SkipNumber) > response.PageCount ? response.PageCount : parseInt(ActivePageNumber) + parseInt(SkipNumber);
                        }
                        PageNumberStart = PageNumberEnd - (SkipNumber * 2);

                        for (var i = PageNumberStart; i <= PageNumberEnd; i++) {
                            if (PageNumberEnd - (SkipNumber * 2) == i) {
                                pageHtml += "<li><a href='javascript:void(0)' class='number' PageNumber='1'>1</a></li>";
                            } else if (PageNumberEnd - (SkipNumber * 2 - 1) == i && ActivePageNumber > SkipNumber + 1) {
                                pageHtml += "<li><span>...</span></li>";
                            } else if (parseInt(PageNumberStart) + parseInt(SkipNumber) * 2 == i &&
                                parseInt(ActivePageNumber) + SkipNumber < response.PageCount) {
                                pageHtml += "<li><a href='javascript:void(0)' class='number' PageNumber='" + response.PageCount + "'>" + response.PageCount + "</a></li>";
                            } else if (parseInt(PageNumberStart) + parseInt(SkipNumber) * 2 - 1 == i &&
                                parseInt(ActivePageNumber) + SkipNumber < response.PageCount) {
                                pageHtml += "<li><span>...</span></li>";
                            }
                            else
                                pageHtml += "<li><a href='javascript:void(0)' class='number' PageNumber='" + i + "'>" + i + "</a></li>";

                        }
                    }


                    pageHtml += "<li><a href='javascript:void(0)' class='next'>İleri</a></li>";
                    pageHtml += "</ul>";

                    $("#" + ObjectId).parent(".PagingDataList").find(".PagingRow").html(pageHtml);

                    for (var i = 0; i < selectId.length; i++) {
                        debugger;
                        $("#" + ObjectId + " input[type='checkbox'][name='" + selectId[i] + "']").prop("checked", true);
                    }
                }
                $PagingDataList.PageNumberEvent();
                $PagingDataList.PrevEvent();
                $PagingDataList.NextEvent();
                $PagingDataList.Active(ObjectId);
                $PagingDataList.SelectedRecord(ObjectId);
            },
            error: function (response) {

            }
        });
    },
    SelectedRecord: function (ObjectId) {
        $("#" + ObjectId).find("tr td input[type='checkbox']").each(function () {
            var index = -1;
            for (var i = 0; i < selectId.length; i++) {
                if (selectId[i] == $(this).attr("name")) {
                    index = i;
                    break;
                }
            }

            if ($(this).is(":checked") && index == -1) {
                selectId.push($(this).attr("name"));
            } else if ($(this).is(":checked") == false && index > -1) {
                delete selectId[index];
            }

            console.log(selectId);
        });
    },
    SelectedPassiveRecord: function (ObjectId) {
        selectId = [];
        $("#" + ObjectId).find("tr td input[type='checkbox']").prop('checked', false);
    },
    Active: function (ObjectId) {
        var table = $("#" + ObjectId).parent(".PagingDataList").find("table");
        var pagingRow = $("#" + ObjectId).parent(".PagingDataList").find(".PagingRow");
        var activePageNumber = $(table).attr("activepagenumber") != undefined ? $(table).attr("activepagenumber") : 1;
        $(pagingRow).find("li a").removeClass("active");
        $(pagingRow).find("li a[pagenumber='" + activePageNumber + "']").addClass("active");
    },
    PrevEvent: function () {
        $(".PagingDataList .PagingRow ul li .prev").click(function () {
            var table = $(this).parents(".PagingDataList").find("table");
            var activePageNumber = parseInt($(table).attr("activepagenumber") != undefined ? $(table).attr("activepagenumber") : 1);
            if (activePageNumber > 1) {
                activePageNumber--;
                $(table).attr("ActivePageNumber", activePageNumber);
                var jsonData = JSON.parse($(table).attr("Query"));
                $PagingDataList.ReloadData($(table).attr("id"), jsonData);
            }
        });
    },
    NextEvent: function () {
        $(".PagingDataList .PagingRow ul li .next").click(function () {
            var table = $(this).parents(".PagingDataList").find("table");
            var activePageNumber = parseInt($(table).attr("activepagenumber") != undefined ? $(table).attr("activepagenumber") : 1);
            if (activePageNumber < parseInt($(table).attr("PageCount"))) {
                activePageNumber++;
                $(table).attr("ActivePageNumber", activePageNumber);
                var jsonData = JSON.parse($(table).attr("Query"));
                $PagingDataList.ReloadData($(table).attr("id"), jsonData);
            }

        });
    },
    PageNumberEvent: function () {
        $(".PagingDataList .PagingRow ul li .number").click(function () {
            var table = $(this).parents(".PagingDataList").find("table");
            $(table).attr("ActivePageNumber", $(this).attr("pagenumber"));
            var jsonData = JSON.parse($(table).attr("Query"));
            $PagingDataList.ReloadData($(table).attr("id"), jsonData);
        });
    },
    OrderBy: function () {
        debugger;
        $(".PagingDataList table thead tr th[Short='True']").click(function () {
            debugger;
            var table = $(this).parents("table");
            $(table).attr("ordercolumnn", $(this).attr("name"));

            if ($(table).attr("ordertype") == "1") {
                $(table).attr("ordertype", "2");
            } else {
                $(table).attr("ordertype", "1");
            }
            var jsonData = JSON.parse($(table).attr("Query"));
            jsonData["OrderType"] = $(table).attr("ordertype");
            $PagingDataList.ReloadData($(table).attr("id"), jsonData);
        })
    },
    AddRecordModal: function (AddUrl, isAddPopup, id, AddCallBackFunction = null) {
        debugger;
        if (isAddPopup == 'False') {
            location.href = AddUrl + "/" + id;
            return;
        }
        $.ajax({
            url: AddUrl,
            type: "POST",
            dataType: 'html',
            data: { "id": id },
            success: function (response) {
                debugger;
                $("#addRecordModal").html($.parseHTML(response));
                $("#addRecordModal").modal('show');

                $("textarea.ckeditor").each(function () {
                    var shortContentEditor = CKEDITOR.instances[$(this).attr("id")];
                    if (shortContentEditor) { shortContentEditor.destroy(true); }
                    CKEDITOR.replace($(this).attr("id"), {
                        enterMode: CKEDITOR.ENTER_BR,
                        htmlEncodeOutput: false,
                        height: 250,
                        filebrowserImageUploadUrl: '/Admin/Contents',//for uploading image
                        filebrowserImageBrowseUrl: '/Admin/EditorImageUpload'
                    });
                    CKEDITOR.instances[$(this).attr("id")].setData($(this).val());

                });

            },
            complete: function () {
                if (AddCallBackFunction != null) {
                    AddCallBackFunction.call();
                }
            },
            error: function (response) {

            }
        });
    },
    DeleteRecordModal: function (ObjectId, DeleteUrl, CallBackFunction, id) {
        debugger;

        $PagingDataList.SelectedRecord(ObjectId);

        var text = "Bu kaydı silmek istediğinizden emin misiniz?";
        if (id == 0) {
            var text = "Seçilen kayıtları silmek istediğinizden emin misiniz?";
        }
        var onay = confirm(text);
        var ids = "";
        if (onay) {
            if (id == 0) {
                for (var i = 0; i < selectId.length; i++) {
                    ids += selectId[i] + ",";
                }

                ids = ids.substring(0, ids.length - 1);
                if (ids.length == 0) {
                    showAlert(".listMessage", "Lütfen silmek istediğiniz kayıtları seçiniz.", "error");
                    return;
                }
            } else
                ids = id;

            $.ajax({
                url: DeleteUrl,
                type: "POST",
                dataType: 'json',
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                data: { "ids": ids },
                success: function (response) {
                    debugger;
                    CallBackFunction.call();
                    showAlert(".listMessage", "Kayıt silme işlemi başarıyla tamamlandı.", "success");
                    $PagingDataList.SelectedPassiveRecord(ObjectId);

                },
                error: function (response) {
                    showAlert(".listMessage", "Kayıt silme işlemi sırasında hata oluştu.", "error");

                }
            });
        }
    }, ActiveRecordModal: function (ObjectId, ActiveUrl, CallBackFunction, id) {
        debugger;

        $PagingDataList.SelectedRecord(ObjectId);

        var onay = confirm("Seçilen kayıtları aktif yapmak istediğinizden emin misiniz?");
        var ids = "";
        if (onay) {

            for (var i = 0; i < selectId.length; i++) {
                ids += selectId[i] + ",";
            }

            ids = ids.substring(0, ids.length - 1);
            if (ids.length == 0) {
                showAlert(".listMessage", "Lütfen aktif yapmak istediğiniz kayıtları seçiniz.", "error");
                return;
            }

            $.ajax({
                url: ActiveUrl,
                type: "POST",
                dataType: 'json',
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                data: { "ids": ids },
                success: function (response) {
                    debugger;
                    CallBackFunction.call();
                    showAlert(".listMessage", "Kayıt aktif etme işlemi başarıyla tamamlandı.", "success");
                    $PagingDataList.SelectedPassiveRecord(ObjectId);

                },
                error: function (response) {
                    showAlert(".listMessage", "Kayıt aktif etme işlemi sırasında hata oluştu.", "error");

                }
            });
        }
    }, PassiveRecordModal: function (ObjectId, PassiveUrl, CallBackFunction, id) {
        debugger;

        $PagingDataList.SelectedRecord(ObjectId);

        var onay = confirm("Seçilen kayıtları pasif yapmak istediğinizden emin misiniz?");
        var ids = "";
        if (onay) {

            for (var i = 0; i < selectId.length; i++) {
                ids += selectId[i] + ",";
            }

            ids = ids.substring(0, ids.length - 1);
            if (ids.length == 0) {
                showAlert(".listMessage", "Lütfen pasif yapmak istediğiniz kayıtları seçiniz.", "error");
                return;
            }

            $.ajax({
                url: PassiveUrl,
                type: "POST",
                dataType: 'json',
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                data: { "ids": ids },
                success: function (response) {
                    debugger;
                    CallBackFunction.call();
                    showAlert(".listMessage", "Kayıt pasif etme işlemi başarıyla tamamlandı.", "success");
                    $PagingDataList.SelectedPassiveRecord(ObjectId);
                },
                error: function (response) {
                    showAlert(".listMessage", "Kayıt pasif etme işlemi sırasında hata oluştu.", "error");

                }
            });
        }
    },
    Save: function (formId, ObjectId, CallBack, CallBack2 = null) {
        debugger;
        if ($("#" + formId).FormValidate() == false)
            return;

        $("#" + formId).find("button[type='submit']").prop('disabled', true);

        $(function () {
            $("input,textarea,select").change(function () {
                $(this).parent("div").find(".error").remove();
            })
        });

        //Hata mesajları post edilmeden önce temizlenir.
        $("#saveForm").find(".error").remove();

        var saveUrl = $("#" + ObjectId).attr("SaveUrl");

        $("textarea.ckeditor").each(function () {
            CKEDITOR.instances[$(this).attr("id")].updateElement();
        });

        var formData = new FormData($('#saveForm')[0]);
        $.ajax({
            url: saveUrl,
            type: "POST",
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (data) {

                $("#" + formId).find("button[type='submit']").prop('disabled', false);

                if (data.ResultType == 0) {
                    $PagingDataList.AddRecordModal($("#" + ObjectId).attr("AddUrl"), $("#" + ObjectId).attr("AddUrl"), $("#Id").val());

                    $("#" + formId).find("button[type='submit']").prop('disabled', false);

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
                CallBack.call();
                if (CallBack2 != null) {
                    CallBack2.call();
                }

            }, error: function (data) {
                setTimeout(function () {
                    showAlert(".popupMessage", "Kayıt işlemi sırasında hata oluştu. Lütfen alanları kontrol ediniz.", "error");
                }, 100);
            }
        });

    }


}

$(function () {
    $PagingDataList.Init();
});





/*CheckBoxListFilter*/
console.log("embed js");

var $CheckBoxListFilter = {
    Init: function () {
        $('.CheckBoxListFilterOpen ul').perfectScrollbar();

        $("input[name='CheckBoxListFilterSearch']").keyup(function () {
            debugger;
            var item = $(this);
            $(item).parents(".CheckBoxListFilter").find(".CheckBoxListFilterOpen").find("ul li").css("display", "none");
            var objList = $(item).parents(".CheckBoxListFilter").find(".CheckBoxListFilterOpen").find("ul li label");
            var filter = $(item).val().toLocaleLowerCase("tr-Tr");
            for (i = 0; i < objList.length; i++) {
                txtValue = $(objList[i]).html();

                if (txtValue.toLocaleLowerCase("tr-Tr").indexOf(filter) > -1) {
                    $(objList[i]).parents("li").css("display", "block");
                } else {
                    $(objList[i]).parents("li").css("display", "none");
                }
            }
            $('.CheckBoxListFilterOpen ul').perfectScrollbar("update");
        });

        $(".checkboxListFilter input[name='CheckBoxListFilterSearch']").click(function (e) {
            var item = $(this);
            $(item).parent(".CheckBoxListFilter").find(".CheckBoxListFilterOpen").fadeIn(200);
        });

        $("body,html").click(function (event) {
            var item = $("input[name='CheckBoxListFilterSearch']");
            $(item).parents(".CheckBoxListFilter").find(".CheckBoxListFilterOpen").fadeOut(200);
        });

        $(".CheckBoxListFilter .CheckBoxListFilterOpen").each(function (i, obj) {
            var count = $(".CheckBoxListFilter .CheckBoxListFilterOpen").length;
            $(this).css("z-index", count);
            count--;
        });

        $(".CheckBoxListFilter").on('click', function (e) {
            e.stopPropagation();
            $(".CheckBoxListFilter").find(".CheckBoxListFilterOpen").css("display", "none");
            $(this).find(".CheckBoxListFilterOpen").css("display", "block");

        });

        $(".CheckBoxListFilter .CheckBoxListFilterInput").click(function () {
            $(this).find("input[type='text']").focus();
        })

        $('.CheckBoxListFilterOpen').show();
        $('.CheckBoxListFilterOpen ul').perfectScrollbar();
        $('.CheckBoxListFilterOpen').hide();
        $('.SelectedValue').perfectScrollbar();

        const element = document.querySelector(".SelectedValue");

        //element.addEventListener('wheel', (event) => {
        //    event.preventDefault();

        //    element.scrollBy({
        //        left: event.deltaY < 0 ? -30 : 30,

        //    });
        //});

        $CheckBoxListFilter.Select();
    },
    Remove: function () {
        $(".CheckBoxListFilter .SelectedValue li").click(function () {

            $(this).parents(".CheckBoxListFilter").find(".CheckBoxListFilterOpen ul li input[type='checkbox'][value='" + $(this).attr("value") + "']").prop('checked', false);
            $(this).remove();

        });
    },
    Select: function () {
        $('.CheckBoxListFilterOpen ul li input[type="checkbox"]').click(function () {
            debugger;
            val = $(this).parents(".CheckBoxListFilter").find(".CheckBoxListFilterInput .SelectedValue");
            if ($(this).is(":checked")) {
                $(val).append("<li value='" + $(this).attr("value") + "'>" + $(this).parents("li").find("label").html() + "</li>");
            } else {
                $(val).find("li[value='" + $(this).attr("value") + "']").remove();
            }

            $CheckBoxListFilter.Remove();
            $(this).parents(".CheckBoxListFilter").find('.SelectedValue').perfectScrollbar("update");
            console.log($(this).parents(".CheckBoxListFilter").find('.SelectedValue').html());


        });


    },
    ReloadData: function (ObjectId, Url, Data) {
        if ($("#" + ObjectId).hasClass("CheckBoxListFilter") == false) {
            console.log(ObjectId + " değerine ait bir CheckBoxListFilter kontrolü bulunamadı")
            return;
        }
        $("#" + ObjectId).find("ul li").remove();
        var DisplayName = $("#" + ObjectId).attr("DisplayName");
        var ValueName = $("#" + ObjectId).attr("ValueName");
        var ObjectName = $("#" + ObjectId).attr("ObjectName");

        $("#" + ObjectId).find(".CheckBoxListFilterOpen input[name='CheckBoxListFilterSearch']").val("");
        $("#" + ObjectId).find(".CheckBoxListFilterOpen input[name='CheckBoxListFilterSearch']").val("");


        $.ajax({
            url: Url,
            type: "POST",
            dataType: 'json',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            data: Data,
            success: function (response) {
                var htmlValue = "";
                var Count = 0;
                for (var i = 0; i < response.Data.length; i++) {
                    htmlValue += "<li><input class='form-check-input shadow-none' type='checkbox' id='CheckBoxList_" + ObjectId + Count + "'  name='" + ObjectName + "' value='" + response.Data[i][ValueName] + "' /><label class='form-check-label' for='CheckBoxList_" + ObjectId + Count + "'>" + response.Data[i][DisplayName] + "</label></li>";
                    Count++;
                }
                $("#" + ObjectId).find(".CheckBoxListFilterOpen ul").html(htmlValue);
                $("#" + ObjectId).find('.CheckBoxListFilterOpen ul').perfectScrollbar("update");
                $CheckBoxListFilter.Select();

            },
            error: function (response) {

            }
        });
    }


}

$(function () {
    $CheckBoxListFilter.Init();
})

/*CheckBoxListFilter*/


/*RadioButtonList*/
console.log("embed js");

var $RadioButtonList = {
    Init: function () {
        $("input[name='RadioButtonListSearch']").keyup(function () {
            var item = $(this);
            $(item).parent("div").find("ul li").css("display", "none");
            var objList = $(item).parent("div").find("ul li label");
            var filter = $(item).val().toLocaleLowerCase("tr-Tr");
            for (i = 0; i < objList.length; i++) {
                txtValue = $(objList[i]).html();

                if (txtValue.toLocaleLowerCase("tr-Tr").indexOf(filter) > -1) {
                    $(objList[i]).parents("li").css("display", "block");
                }
            }
            $(this).parents(".RadioButtonList").find('.RadioButtonListOpen ul').perfectScrollbar("update");

        });

        $(".RadioButtonList .RadioButtonListOpen").each(function (i, obj) {
            var count = $(".RadioButtonList .RadioButtonListOpen").length;
            $(this).css("z-index", count);
            count--;
        });

    },
    ReloadData: function (ObjectId, Url, Data) {
        if ($("#" + ObjectId).hasClass("RadioButtonList") == false) {
            console.log(ObjectId + " değerine ait bir select RadioButtonList bulunamadı")
            return;
        }
        $("#" + ObjectId).find("ul").html("Yükleniyor");
        $("#" + ObjectId).find("ul li").remove();
        var DisplayName = $("#" + ObjectId).attr("DisplayName");
        var ValueName = $("#" + ObjectId).attr("ValueName");
        var ObjectName = $("#" + ObjectId).attr("ObjectName");


        $.ajax({
            url: Url,
            type: "POST",
            dataType: 'json',
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            data: Data,
            success: function (response) {
                $("#" + ObjectId).find("ul").html("");
                var htmlValue = "";
                var Count = 0;
                for (var i = 0; i < response.Data.length; i++) {
                    htmlValue += "<li><input class='form-check-input shadow-none' type='radio' id='CheckBoxList_" + ObjectId + Count + "'  name='" + ObjectName + "' value='" + response.Data[i][ValueName] + "' /><label class='form-check-label' for='CheckBoxList_" + ObjectId + Count + "'>" + response.Data[i][DisplayName] + "</label></li>";
                    Count++;
                }
                $("#" + ObjectId).find(".RadioButtonListOpen ul").html(htmlValue);
                $("#" + ObjectId).find('.RadioButtonListOpen ul').perfectScrollbar("update");
            },
            error: function (response) {

            }
        });
    }

}

$(function () {
    $RadioButtonList.Init();
    $('.RadioButtonListOpen ul').perfectScrollbar();


})

/*RadioButtonList*/


/*CategorySelectBox*/
$TreeSelectBox = {
    Init: function () {
        $(".TreeSelectBox select").on("change", function (e) {
            debugger;
            e.stopPropagation();
            var obj = $(this);
            var parent = $(obj).parent("div");
            if ($(obj).val() == "") {
                return;
            }
            console.log($(obj));
            var Data = {
                Id: $(obj).val(),
                ObjectId: $(obj).attr("ObjectId"),
                ObjectName: $(obj).attr("ObjectName"),
                DisplayName: $(obj).attr("DisplayName"),
                ValueName: $(obj).attr("ValueName"),
                DefaultValue: $(obj).attr("DefaultValue"),
                DefaultText: $(obj).attr("DefaultText")
            };

            $(obj).parent("div").find("input[type='hidden']").val($(obj).val());

            $(obj).parent("div").find("select").remove();
            $.ajax({
                url: "/CategoryBase/GetCategory",
                type: "POST",
                dataType: 'json',
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                data: Data,
                success: function (response) {
                    $(parent).html(response.Data);
                }, complete: function () {
                    debugger;
                    $TreeSelectBox.Init();
                    $TreeSelectBox.Select();
                },
                error: function (response) {

                }
            });
        });
    }, Select: function () {

        $(".TreeSelectBox ul li").click(function (e) {
            debugger;
            e.stopPropagation();
            var TreeSelectBox = $(this).parents(".TreeSelectBox");
            var obj = $(TreeSelectBox).find("select");
            var Data = {
                //Id: $(obj).val(),
                Id: $(this).attr("id"),
                ObjectId: $(obj).attr("ObjectId"),
                ObjectName: $(obj).attr("ObjectName"),
                DisplayName: $(obj).attr("DisplayName"),
                ValueName: $(obj).attr("ValueName"),
                DefaultValue: $(obj).attr("DefaultValue"),
                DefaultText: $(obj).attr("DefaultText")
            };

            $(TreeSelectBox).html("Yükleniyor");
            $.ajax({
                url: "/CategoryBase/GetCategory",
                type: "POST",
                dataType: 'json',
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                data: Data,
                success: function (response) {
                    console.log(response.Data);
                    $(TreeSelectBox).html(response.Data);
                }, complete: function () {
                    debugger;
                    $TreeSelectBox.Init();
                    $TreeSelectBox.Select();
                },
                error: function (response) {

                }
            });
        });
    }
}

$(function () {

})
/*CategorySelectBox*/

$ImageFileUpload = {
    Delete: function (id, ObjectId, ListObjectId) {
        var onay = confirm("Resmi silmek istediğinizden emin misiniz?");
        if (onay) {
            var deleteUrl = $("input[type='file'][id='" + ObjectId + "']").attr("DeleteUrl");
            $.ajax({
                url: deleteUrl,
                type: "POST",
                dataType: 'json',
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                data: { "id": id },
                success: function (response) {
                    debugger;
                    if ($("#" + ListObjectId).attr("AddUrl") != undefined) {
                        $PagingDataList.AddRecordModal($("#" + ListObjectId).attr("AddUrl"), true, id);
                    } else {
                        location.reload();
                    }

                    setTimeout(function () {
                        showAlert(".popupMessage", "Resim silme işlemi başarıyla tamamlandı.", "success");
                    }, 100);


                    scroolTop(0, 300);
                },
                error: function (response) {
                    setTimeout(function () {
                        showAlert(".popupMessage", "Resim silme işlemi sırasında hata oluştu. Lütfen alanları kontrol ediniz.", "error");
                    }, 100);
                }
            });
        }
    }
}

$Form = {
    //Validation: function (formId) {
    //    $("#" + formId + " input").each(function () {
    //        debugger;
    //        if ($(this).attr("data-val-required") != null && $(this).val() == "") {
    //            $(this).parent("div").append("<div class='error text-danger'>" + $(this).attr("data-val-required") + "</div>");
    //            return;
    //        } else {
    //            $(this).parent("div").find(".error").remove();
    //        }
    //    });
    //},
    Init: function (formId) {
        $("input,textarea,select").change(function () {
            $(this).parent("div").find(".error").remove();
        });

        $("textarea.ckeditor").each(function () {
            var shortContentEditor = CKEDITOR.instances[$(this).attr("id")];
            if (shortContentEditor) { shortContentEditor.destroy(true); }
            CKEDITOR.replace($(this).attr("id"), {
                enterMode: CKEDITOR.ENTER_BR,
                htmlEncodeOutput: false,
                height: 250,
                filebrowserImageUploadUrl: '/Admin/Contents',//for uploading image
                filebrowserImageBrowseUrl: '/Admin/EditorImageUpload'
            });
            CKEDITOR.instances[$(this).attr("id")].setData($(this).val());

        });

    },
    Save: function (formId, formPostUrl) {
        //$("#" + formId + " input, #" + formId + " select").change(function () {
        //    $Form.Validation(formId);
        //});

        $("#" + formId).submit(function (e) {
            debugger;
            e.preventDefault();

            $("#" + formId + " .error").remove();



            //$Form.Validation(formId);

            var formData = new FormData($("#" + formId)[0]);
            


            $("textarea.ckeditor").each(function () {
                var shortContentEditor = CKEDITOR.instances[$(this).attr("id")];
                formData.set([$(this).attr("name")], shortContentEditor.getData());


            });


            $.ajax({
                url: formPostUrl,
                type: "POST",
                data: formData,
                dataType: 'json',
                contentType: false,
                processData: false,
                success: function (data) {
                    debugger;
                    if (data.ResultType == 0) {
                        $("#" + formId).find("button[type='submit']").prop('disabled', false);
                        if (data.RedirectUrl != null) {
                            $('#' + formId)[0].reset();
                            location.href = data.RedirectUrl;
                        }

                        $Form.Init(formId);

                        //var keys = Object.keys(data.Data);
                        //for (var i = 0; i < keys.length; i++) {
                        //    var key = keys[i];
                        //    var val = data.Data[keys[i]];

                        //    if ($("#" + key).attr("type") == "file") {
                        //        $("#" + key).val("");
                        //        $("#" + key).parents(".imagePanel").find(".ImagePreview").attr("src", val);
                        //        $("#" + key).parents(".imagePanel").find(".image").show();
                        //    } else {
                        //        $("#" + key).val(val);
                        //    }
                        //}

                        //setTimeout(function () {
                        //    showAlert(".popupMessage", "Kayıt işlemi başarıyla tamamlandı.", "success");
                        //}, 100);
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


                            scroolTop(0, 300);
                        } else {
                            setTimeout(function () {
                                showAlert(".popupMessage", "Kayıt işlemi sırasında hata oluştu. Lütfen alanları kontrol ediniz.", "error");
                            }, 100);
                        }
                    }


                    $("#" + formId).find("button[type='submit']").prop('disabled', false);


                }, error: function (data) {
                    setTimeout(function () {
                        showAlert(".popupMessage", "Kayıt işlemi sırasında hata oluştu. Lütfen alanları kontrol ediniz.", "error");
                    }, 100);
                }
            });
        });

    }
}

$CategoryTree = {
    Init: function () {
        $(".CategoryTreeBox ul li a").click(function () {
            debugger;
            if ($(this).hasClass("active")) {
                $(".CategoryTreeBox ul li a").removeClass("active");
            } else {
                $(".CategoryTreeBox ul li a").removeClass("active");
                $(this).addClass("active");
            }
        });
    },
    Edit: function () {
        var id = $(".CategoryTreeBox ul li a.active").attr("id")
        if (id != undefined) {
            location.href = "/Admin/Category/Add/" + id;
        } else {
            showAlert(".listMessage", "Lütfen düzenlemek istediğiniz kaydı seçiniz", "error");
        }
    },
    AddCategory: function () {
        var id = $(".CategoryTreeBox ul li a.active").attr("id")
        if (id != undefined) {
            location.href = "/Admin/Category/Add/0?MainCategoryId=" + id;
        } else {
            showAlert(".listMessage", "Lütfen düzenlemek istediğiniz kaydı seçiniz", "error");
        }
    },
    Delete: function (DeleteCallBack) {
        var id = $(".CategoryTreeBox ul li a.active").attr("id")
        var text = "Bu kaydı silmek istediğinizden emin misiniz?";

        var onay = confirm(text);

        if (onay) {
            $.ajax({
                url: "/Admin/Category/Delete",
                type: "POST",
                dataType: 'json',
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                data: { "ids": id },
                success: function (response) {
                    debugger;
                    if (response.ResultType == 0) {
                        showAlert(".listMessage", response.ErrorMessage, "success");
                    } else {
                        showAlert(".listMessage", response.ErrorMessage, "error");
                    }
                    

                },
                complete: function () {
                    if (DeleteCallBack != null) {
                        DeleteCallBack.call();
                    }
                },
                error: function (response) {
                    showAlert(".listMessage", "Kayıt silme işlemi sırasında hata oluştu.", "error");

                }
            });
        }
    },
    ReloadData: function (ObjectId, Data, CallBackFunction) {
        debugger;
        $("#" + ObjectId).find(".CategoryTreeMain").html("Yükleniyor");
        $.ajax({
            url: "/Admin/Category/GetTreeList",
            type: "POST",
            dataType: 'json',
            /*         contentType: 'application/x-www-form-urlencoded; charset=UTF-8',*/
            data: Data,
            success: function (response) {
                $("#" + ObjectId).find(".CategoryTreeMain").html(response.Data);
            },
            error: function (response) {

            },
            complete: function () {
                if (CallBackFunction != null) {
                    CallBackFunction.call();
                }
            }
        });
    }
}