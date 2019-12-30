$(document).ready(function () {
    $('#btnsubmit').click(function () {
        var IsPostback = true;
        $('.contactno').each(function () {
            var yourInput = $(this).val();
            if (yourInput.length == 10 || yourInput == '') {
                IsPostback = true;
            }
            else {
                $(this).css("background-color", "#fdc181");
                IsPostback = false;
            }
        });
        if (!IsPostback) {
            alert("Phone number must be in 10 digits");
            return false;
        }

        $('.mail').each(function () {
            if (validateEmail(this.value) || yourInput == '') {
                IsPostback = true;
            }
            else {
                $(this).css("background-color", "#fdc181");
                IsPostback = false;
            }
        });
        if (!IsPostback) {
            alert("Invalid Mail address");
            return false;
        }

        $('.mandatory').each(function () {
            var yourInput = $(this).val();
            if (yourInput == '') {
                $(this).css("background-color", "#fdc181");
                IsPostback = false;
            }
            else {
                IsPostback = true;
            }
        });
        if (!IsPostback) {
            alert("Fill all mandatory fields");
            return false;
        }
    });
});


$(function () {
    $('.upper').keyup(function () {
        $(this).val($(this).val().toUpperCase());

    });

});

$(function () {
    $('.alphabet').keyup(function () {
        var yourInput = $(this).val();
        re = /[`~0-9!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi;
        var isSplChar = re.test(yourInput);
        if (isSplChar) {
            var no_spl_char = yourInput.replace(/[`~0-9!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi, '');
            $(this).val(no_spl_char);
        }
    });
});

$(function () {
    $('.numeric').keyup(function () {
        var yourInput = $(this).val();
        re = /[`~a-zA-Z!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi;
        var isSplChar = re.test(yourInput);
        if (isSplChar) {
            var no_spl_char = yourInput.replace(/[`~a-zA-Z!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi, '');
            $(this).val(no_spl_char);
        }

    });

});

function validateEmail(sEmail) {
    var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    if (filter.test(sEmail)) {
        return true;
    }
    else {
        return false;
    }
}

