


function ShowImagePreview(imageUploader, previewImage) {
    if (imageUploader.files && imageUploader.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImage).attr('src', e.target.result);
        }
        reader.readAsDataURL(imageUploader.files[0]);
    }
}




function JQueryAjaxPost(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        var ajaxConfig = {
            type: "POST",
            url: form.action,
            data: new FormData(form),
            success: function (response) {
                $("#ViewAll").html(response);
                refreshAddNewTab($(form).attr('data_resetUrl'),true)
            }

        }
        if ($(form).attr('enctype') === "multipart/form-data") {
            ajaxConfig["contentType"] = false;
            ajaxConfig["processData"] = false;
        }
        $.ajax(ajaxConfig);
    }
    else return false;

}


//function refreshAddNewTab(resetUrl, showViewTab) {
//    $.ajax({

//        type: 'GET',
//        url: resetUrl,
//        success: function (response) {
//            $("#AddNew").html(response);
//            $("ul.nav.nav-tabs a:eq(1)").html('Add New');
//            if (showViewTab) {
//                $("ul.nav.nav-tabs a:eq(0)").tab('show');
//            }
//        }



//    });
//}


