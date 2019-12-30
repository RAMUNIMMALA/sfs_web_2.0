$(document).ready(function () {
    var title, details, startdate, enddate, noofmonths, noofpartners, plantype, commission, planvalue, monthlyamount, baseamount;
    $("#planvalidation").click(function () {
        $("#title").each(function () {
            title = this.value;
        });
        $("#details").each(function () {
            details = this.value;
        });
        $("#startdate").each(function () {
            startdate = this.value;
        });
        $("#enddate").each(function () {
            enddate = this.value;
        });
        $("#noofmonths").each(function () {
            noofmonths = this.value;
        });
        $("#noofpartners").each(function () {
            noofpartners = this.value;
        });
        $("#plantype").each(function () {
            plantype = this.value;
        });
        $("#commission").each(function () {
            commission = this.value;
        });
        $("#planvalue").each(function () {
            planvalue = this.value;
        });
        $("#monthlyamount").each(function () {
            monthlyamount = this.value;
        });
        $("#baseamount").each(function () {
            baseamount = this.value;
        });

        if (title == '' || details == '' || startdate == '' || enddate == '' || noofmonths == '' || noofpartners == '' || plantype == '' || commission == '' || planvalue == '' || monthlyamount == '' || baseamount == '' ) {
            alert("Fill All Mandatory Fields");
            //$("#errmsg").html("Digits Only").show().fadeOut("slow");
            return false;
            
        }
        else{
            alert("Plan added successfully ");
        }
    });
});

$(function () {
$('#title').keyup(function () {
    $(this).val($(this).val().toUpperCase());

    });

});

$(function () {
    $('#planvalue').keyup(function () {
        var yourInput = $(this).val();
        re = /[`~a-zA-Z!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi;
        var isSplChar = re.test(yourInput);
        if (isSplChar) {
            var no_spl_char = yourInput.replace(/[`~a-zA-Z!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi, '');
            $(this).val(no_spl_char);
        }

    });

});
