//Verdiğim id ye sahip modal gösterildiği anda içinden modalCommentBodyID id sine sahip etiketin içini gidip partialı çağırıp doldurcam.
//Jquery on() metodu, içerisine bir çok farklı event yani olay girebildiğimiz çok kullanışlı bir fonksiyondur.
//İçerisine girebildiğimiz olaylardan kastım, click, doubleclick, mousedown, mouseenter ve mouseleave gibi olaylardır.

var noteid = -1;
var modalCommentBodyID = "#modal_comment_body"

$('#modal_comment').on('show.bs.modal', function (e) {
    //Modal show olduğunda çalışacak fonksiyon paremetresine bir js nesnesi yollanıyor. Orada olayı gerçekleştiren elementin tüm kimlik bilgileri tutuluyor.
    //Mesela burada bir button elementi ile olay gerçekleşiyor.Buton elmentinin data attribute ile tutulan id değerini gidip alacağım.
    //relatedTarget clikclenen olayı gerçekleştiren elementi nesneyi bize verecek.

    //Jquery elementi haline  getireceğim.
    var btn = $(e.relatedTarget);
    noteid = btn.data('note-id');

    $(modalCommentBodyID).load("/Comment/ShowNoteComments/" + noteid);
});

function doComment(btn, click, commentID, spanID) {

    //Jquery ile ilgili metodları kullanmak için o elementi ilk önce jquery nesnesine
    //çevirmek gerekiyor.Eğer burada çevirmez isek aşağıdaki Jquery metotolarını parametreden
    //gelen btn nesnesi bulamaz.

    var button = $(btn);

    var mode = button.data("edit-mode");

    if (click == "edit_clicked") {
        if (!mode) {

            //Eğer bir attributua değer atamak istersek şöyle yaparız.
            button.data("edit-mode", true);

            button.removeClass("btn-warning");
            button.addClass("btn-success")

            //find() yöntemi, seçilen öğenin alt öğelerini döndürür.
            var btnSpan = button.find("span")

            btnSpan.removeClass("glyphicon-edit");
            btnSpan.addClass("glyphicon-ok");

            $("#" + spanID).addClass("editable");
            $("#" + spanID).attr("contenteditable", true);
            $("#" + spanID).focus();

        }
        else {
            button.data("edit-mode", false);
            button.addClass("btn-warning");
            button.removeClass("btn-success")

            var btnSpan = button.find("span")

            btnSpan.addClass("glyphicon-edit");
            btnSpan.removeClass("glyphicon-ok");

            $("#" + spanID).removeClass("editable");
            $("#" + spanID).attr("contenteditable", false);

            var txt = $("#" + spanID).text();

            $.ajax({
                method: "POST",
                url: "/Comment/Edit/" + commentID,
                data: { text: txt }
            }).
                done(function (e) {
                    //Oradan buraya bir değer döndürcem.
                    if (e.result) {
                        //yorumlar partial tekrar yüklenir
                        $(modalCommentBodyID).load("/Comment/ShowNoteComments/" + noteid);
                    }
                    else {
                        alert("Yorum Güncellenemedi.");
                    }
                }).fail(function () {
                    //URL i gönderdik ama hata aldık.
                    alert("Sunucu ile bağlantı kurulamadı.");
                });
        }
    }
    else if (click == "delete_clicked") {
        var dialog_res = confirm("Yorum Silinsin Mi?");
        if (!dialog_res) {
            return false;
        }
        else {
            $.ajax({
                method: "GET",
                url: "/Comment/Delete/" + commentID
            }).
                done(function (e) {
                    if (e.result) {
                        //yorumlar partial tekrar yüklenir
                        $(modalCommentBodyID).load("/Comment/ShowNoteComments/" + noteid);
                    }
                    else {
                        alert("Yorum Silinemedi.");
                    }
                })
                .fail(function () {
                    alert("Sunucu İle Bağlantı Kurulmadı.");
                });
        }
    }
    else if (click == "new_clicked") {

        var txt = $("#new_comment_text").val();

        $.ajax(
            {
                method: "POST",
                url: "/Comment/Create",
                data: { text: txt, "noteID": noteid }
            }
        ).done(function (e) {
            if (e.result) {
                //yorumlar partial tekrar yüklenir
                $(modalCommentBodyID).load("/Comment/ShowNoteComments/" + noteid);
            }
            else {
                alert("Yorum eklenemedi.");
            }
        }).fail(function () {
            alert("Sunucu ile bağlantı kurulamadı.");
        });
    }
}