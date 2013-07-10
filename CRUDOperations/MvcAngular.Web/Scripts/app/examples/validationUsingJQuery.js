
$(function () {

    var person,
        originalPerson = {
            firstName: 'Janet',
            lastName: 'Parker',
            emailAddress: 'janet.parker@somedomain.com'
        },
        formState = {
            isValid: function() {
                return this.isEmailAddressNameValid && this.isLastNameValid && this.isEmailAddressValid;
            },
            isDirty: false,
            isFirstNameValid: true,
            isLastNameValid: true,
            isEmailAddressValid: true
        };

    $('#firstNameGroup input')
        .bind('change keyup input', function (e) {
            updateFirstName($(this).val());
        });
    
    $('#lastNameGroup input')
        .bind('change keyup input', function (e) {
            updateLastName($(this).val());
        });

    $('#emailAddressGroup input')
        .bind('change keyup input', function (e) {
            updateEmailAddress($(this).val());
        });
    
    $('#cancelButton')
        .click(function (e) {
            e.preventDefault();
            copyOriginalPerson();
        });

    $('#okButton')
        .click(function (e) {
            e.preventDefault();
            alert(JSON.stringify(person, null, '  '));
        });

    copyOriginalPerson();

    function updateFirstName(value) {
        if (person.firstName !== value) {
            formState.isDirty = true;
        }
        person.firstName = value;
        if (value.match(/^\s*$/)) {
            $('#firstNameGroup .err-req').show();
            formState.isFirstNameValid = false;
        } else {
            $('#firstNameGroup .err-req').hide();
            formState.isFirstNameValid = true;
        }
        if (formState.isFirstNameValid) {
            $('#firstNameGroup').removeClass('error');
        } else {
            $('#firstNameGroup').addClass('error');
        }
        updateButtonState();
    }

    function updateLastName(value) {
        if (person.lastName !== value) {
            formState.isDirty = true;
        }
        person.lastName = value;
        if (value.match(/^\s*$/)) {
            $('#lastNameGroup .err-req').show();
            formState.isLastNameValid = false;
        } else {
            $('#lastNameGroup .err-req').hide();
            formState.isLastNameValid = true;
        }
        if (formState.isLastNameValid) {
            $('#lastNameGroup').removeClass('error');
        } else {
            $('#lastNameGroup').addClass('error');
        }
        updateButtonState();
    }

    function updateEmailAddress(value) {
        if (person.emailAddress !== value) {
            formState.isDirty = true;
        }
        person.emailAddress = value;
        formState.isEmailAddressValid = true;
        if (value.match(/^\s*$/)) {
            $('#emailAddressGroup .err-req').show();
            formState.isEmailAddressValid = false;
        } else {
            $('#emailAddressGroup .err-req').hide();
        }
        if (value.match(/^[^@]+\@[^@]+\.[a-z]{2,3}$/)) {
            $('#emailAddressGroup .err-valid').hide();
        } else {
            $('#emailAddressGroup .err-valid').show();
            formState.isEmailAddressValid = false;
        }
        if (formState.isEmailAddressValid) {
            $('#emailAddressGroup').removeClass('error');
        } else {
            $('#emailAddressGroup').addClass('error');
        }
        updateButtonState();
    }

    function updateButtonState() {
        if (formState.isDirty && formState.isValid()) {
            $('#okButton').removeAttr('disabled');
        } else {
            $('#okButton').attr('disabled', 'disabled');
        }
    }

    function copyOriginalPerson() {
        
        person = {};
        
        $('#firstNameGroup input').val(originalPerson.firstName).trigger('change');
        $('#lastNameGroup input').val(originalPerson.lastName).trigger('change');
        $('#emailAddressGroup input').val(originalPerson.emailAddress).trigger('change');
        
        formState.isDirty = false;
        updateButtonState();
    }

});
