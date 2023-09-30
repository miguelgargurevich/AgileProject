


$(window).ready(function () {
    iframeHome();
   
    //Mas funciones/codigo que necesites se ejecute en el onload
});

function iframeHome() {
    //var link = "https://app.mural.co/t/ayllu6528/m/ayllu6528/1689787122035/ce7b2f691678e53e1c3433b8a37242a381615582?sender=a1fa2006-3c16-4bc4-b8f7-18573327f065"
    //var link = "https://www.share-kudos.com/card-creator";
    var link = "https://www.w3schools.com";
    var iframe = document.createElement('iframe');
    iframe.frameBorder = 0;
    iframe.width = "75%";
    iframe.height = "450px";
    iframe.style = "position: absolute; left: 13%; margin-top: 33px;";
    iframe.id = "randomid";
    iframe.setAttribute("src", link);
    document.getElementById("divContainer").appendChild(iframe);
}


function onChangeMyTeamList() {
    var e = document.getElementById("myteamsList");
    var value = e.value;
    var text = e.options[e.selectedIndex].text;

    //console.log(text);
}



function isNullOrEmpty(str) {
    var returnValue = false;
    if (!str
        || str == null
        || str === 'null'
        || str === ''
        || str === '{}'
        || str === 'undefined'
        || str === '-Select-'
        || str.length === 0) {
        returnValue = true;
    }
    return returnValue;
}
