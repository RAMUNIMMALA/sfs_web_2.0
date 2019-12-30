$(document).ready(function () {
    var userid, password;
    $("#loginvalidation").click(function () {
        $("#userid").each(function () {
            userid = this.value;
        });
        $("#password").each(function () {
            password = this.value;
        });
        
        if (userid == '' || password == '' ) {
            alert("Fill All Mandatory Fields");
            return false;
        }
        else {
                alert("Success");
        }
    });  
}); 
