var Bifrost = Bifrost || {};
Bifrost.commands = Bifrost.commands || {};

Bifrost.commands.commandFormEvents = {
    onSuccess: function (query, data) {
        var form = $(query);
        form.validate().errors().html("");
        $(".field-validation-error", form)
                .removeClass("field-validation-error")
                .addClass("field-validation-valid");
        $(".input-validation-error", form)
                .removeClass("input-validation-error")
                .addClass("valid");

        if (data.Invalid === true) {
            var errors = {};
            
            data.ValidationResults.forEach(function (validationResult, validationResultIndex) {
                validationResult.MemberNames.forEach(function (member, memberIndex) {
                    errors[member] = validationResult.ErrorMessage;
                });
            });
            form.validate().showErrors(errors);
        }

        if (data.Success === true) {
            form.trigger("commandSuccess", data);
        }
    }
}
