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

// vo modal za slika na receptot 
function UploadRecipeFile(fileUpload) {
    console.log("UPLOAD RECIPE FILE *******************************");
    var img = document.getElementById("mainContent_recipePicShow");
    //var loader = document.getElementById("mainContent__loader");
    console.log(img.id);
    $(img).hide();
    //$(loader).show();

    if (fileUpload.value != '') {
        console.log('yeah done');
        img.src = "";
        img.src = window.URL.createObjectURL(fileUpload.files[0]);
        $(img).css({
            width: 'auto',
            height: 'auto'
        });

        // pushti modal
    }
}

// koga kje se uploadne fajlot za da se prikazhe slikata a da ne ja zachuvuvame za dzabe vo folder
function UploadFile(fileUpload) {


    var img = document.getElementById("mainContent_showPic");
    var loader = document.getElementById("mainContent__loader");

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

$(document).ready(function () {
    
   /* PROFILE PICTURE */
    var img = document.getElementById("mainContent_showPic");
    var loader = document.getElementById("mainContent_loader");

    // loader resizing - if bigger than 200, 200
    var res = scaleSize(100, 100, $(loader).width(), $(loader).height());
    $(loader).width(res[0]);
    $(loader).height(res[1]);
    $(loader).hide();
    // loader resizing - if bigger than 200, 200

    $(img).load(function () {
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


    /* RECIPE PICTURE */
    var img1 = document.getElementById("mainContent_recipePicShow");
    //var loader = document.getElementById("mainContent_loader");

    // loader resizing - if bigger than 200, 200
    var res1 = scaleSize(100, 100, $(loader).width(), $(loader).height());
    //$(loader).width(res[0]);
    //$(loader).height(res[1]);
    //$(loader).hide();
    // loader resizing - if bigger than 200, 200

    $(img1).load(function () {
        console.log("IMG LOAD");
        var maxW = 300;
        var maxH = 300;
        var currW = $(this).width();
        var currH = $(this).height();
        var res = scaleSize(maxW, maxH, currW, currH);
        $(this).width(res[0]);
        $(this).height(res[1]);
        setTimeout(function () {
            console.log("IMG TO SHOW NOW");
            // malce da se pokazhe loaderot
            $(img1).show();
            //$(loader).hide();
        }, 1000);

    });
});


var prm = Sys.WebForms.PageRequestManager.getInstance();

prm.add_endRequest(function () {
    // re-bind your jQuery events here
    /* PROFILE PICTURE */
    var img = document.getElementById("mainContent_showPic");
    var loader = document.getElementById("mainContent_loader");

    // loader resizing - if bigger than 200, 200
    var res = scaleSize(100, 100, $(loader).width(), $(loader).height());
    $(loader).width(res[0]);
    $(loader).height(res[1]);
    $(loader).hide();
    // loader resizing - if bigger than 200, 200

    $(img).load(function () {
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


    /* RECIPE PICTURE */
    var img1 = document.getElementById("mainContent_recipePicShow");
    //var loader = document.getElementById("mainContent_loader");

    // loader resizing - if bigger than 200, 200
    var res1 = scaleSize(100, 100, $(loader).width(), $(loader).height());
    //$(loader).width(res[0]);
    //$(loader).height(res[1]);
    //$(loader).hide();
    // loader resizing - if bigger than 200, 200

    $(img1).load(function () {
        console.log("IMG LOAD");
        var maxW = 300;
        var maxH = 300;
        var currW = $(this).width();
        var currH = $(this).height();
        var res = scaleSize(maxW, maxH, currW, currH);
        $(this).width(res[0]);
        $(this).height(res[1]);
        setTimeout(function () {
            console.log("IMG TO SHOW NOW");
            // malce da se pokazhe loaderot
            $(img1).show();
            //$(loader).hide();
        }, 1000);

    });
    // proveri dali e potrebno za profile picture
});


function hideAgain() {
    console.log("hide again");
    setTimeout(function () {
        document.getElementById("alertNoEnteredIngr").attributes("visible", "false");
    }, 3000);

}


