$(document).ready(function () {

    $(".textbox1digit").prop("maxlength", "1");

    $(".textbox2digit").prop("maxlength", "2");

    $(".textbox3digit").prop("maxlength", "3");

    $('.textbox1digit, .textbox2digit, .textbox3digit').keypress(function (event) {
        return isNumber(event);
    });

    
    $('.primary').keyup(function (event) {
        
        var keyvlaue = '';
        $('.primary').each(function () {
            
            if ($(this).val() == '')
            {
                $(this).val(0);
            }
            if (keyvlaue == '')
            {
                keyvlaue += $(this).val();
            }
            else {
                if ($(this).val() == '')
                {
                    keyvlaue += ('-' + 0);
                }
                else {
                    keyvlaue += ('-' + $(this).val());
                }
                
            }
        });
        $('#primarykeyvalue').val(keyvlaue);

    });

    $('.secondary').keyup(function (event) {

        var keyvlaue = '';
        $('.secondary').each(function () {

            if ($(this).val() == '') {
                $(this).val(0);
            }
            if (keyvlaue == '') {
                keyvlaue += $(this).val();
            }
            else {
                if ($(this).val() == '') {
                    keyvlaue += ('-' + 0);
                }
                else {
                    keyvlaue += ('-' + $(this).val());
                }

            }
        });
        $('#secondarykeyvalue').val(keyvlaue);

    });



    function isNumber(evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }




});