//on company-id input field focus lost -> addCompanyName

$("input#Company_IdentificationCode.form-control").focusout(function () {
    addCompanyNameFieldText($("input#Company_IdentificationCode.form-control").val());
});


function addCompanyNameFieldText(companyId) {
    var request = $.ajax({
        type: "POST",
        url: "/api/companies/getname",
        data: "" + companyId, // Send data
        contentType: 'application/json' //Send data type
    });

    request.done(function(data) {
        $("input#Company_Name.form-control").val(data);
    });

    request.fail(function(data) {
    });
}