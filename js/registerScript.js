function scaleSize(maxW, maxH, currW, currH) {
    var ratio = currH / currW;
    if (currW >= maxW && ratio <= 1) {
        //console.log("currW >= maxW i shirinata e pogolema od visinata");
        currW = maxW;
        currH = currW * ratio;
    } else if (currH >= maxH) {
        //console.log("currH >= maxH");
        currH = maxH;
        currW = currH / ratio;
    }
    return [currW, currH];
}

$(document).ready(function () {
    var img = document.getElementById("mainContent_RegisterUser_CreateUserStepContainer_showProfile");
    var loader = document.getElementById("mainContent_RegisterUser_CreateUserStepContainer_loader");

    // loader resizing - if bigger than 200, 200
    var res = scaleSize(200, 200, $(loader).width(), $(loader).height());
    $(loader).width(res[0]);
    $(loader).height(res[1]);
    $(loader).hide();
    // loader resizing - if bigger than 200, 200

    $(img).load(function () {
        // 200 na 200 sakam da mi bide bounding box-ot (max)
        // naogjam aspect ratio i ja namaluvam proprcionalno slikata :)
        // maxW, maxH (these parameters will set the size of the bounding box)
        // currW, currH - current or original size of the image.
        var maxW = 300;
        var maxH = 300;
        var currW = $(this).width();
        var currH = $(this).height();
        var res = scaleSize(maxW, maxH, currW, currH);
        //console.log("prethodni dimenzii: " + currW + " " + currH);
        //console.log("segasni dimenzii: " + res[0] + " " + res[1]);
        $(this).width(res[0]);
        $(this).height(res[1]);
        setTimeout(function () {
            // malce da se pokazhe loaderot
            $(img).show();
            $(loader).hide();
        }, 1000);

    });


});

function UploadFile(fileUpload) {


    var img = document.getElementById("mainContent_RegisterUser_CreateUserStepContainer_showProfile");
    var loader = document.getElementById("mainContent_RegisterUser_CreateUserStepContainer_loader");

    $(img).hide();
    $(loader).show();

    if (fileUpload.value != '') {

        img.src = window.URL.createObjectURL(fileUpload.files[0]);
        $(img).css({
            width: 'auto',
            height: 'auto'
        }); // ova za da ne presmetuva spored prethodnata visina i shirina (pri vtor obid)
    }

}