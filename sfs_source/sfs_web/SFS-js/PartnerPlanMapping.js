$(document).ready(function () {
    $('#txtPlanCode').change(function (event) {
        var _PartnerData = new Object();
        _PartnerData.PlanCode = $('#txtPlanCode').val();
        _PartnerData.PartnerCode = $('#txtPartnerCode').val();
        $.ajax({
            type: "POST",
            url: "/Partner/AjaxPartnerPlanDetails", // the URL of the controller action method
            data: JSON.stringify(_PartnerData),
            //data: { Name: 'Malli' },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.Result == 1) {
                    alert("Plan already added to this partner ");
                    return false;
                }
                else {
                    alert("Plan not added to this partner");
                    return false;
                }
            },
            failure: function (response) {
                alert("Failure!");
            },
        });
    });
});