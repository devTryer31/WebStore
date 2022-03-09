document.addEventListener('DOMContentLoaded', function () {

    var login_inputs = Array.from(document.querySelectorAll('._l_inp'));
    var register_inputs = Array.from(document.querySelectorAll('._r_inp'));

    for (var i = 0; i < login_inputs.length; i++) {
        login_inputs[i].addEventListener('mouseenter', disableRegisterInputs);
        login_inputs[i].addEventListener('mouseleave', enableRegisterInputs);
        login_inputs[i].addEventListener('focusin', disableRegisterInputs);
        login_inputs[i].addEventListener('focusout', enableRegisterInputs);
    }

    for (var i = 0; i < register_inputs.length; i++) {
        register_inputs[i].addEventListener('mouseenter', disableLoginInputs);
        register_inputs[i].addEventListener('mouseleave', enableLoginInputs);
        register_inputs[i].addEventListener('focusin', disableLoginInputs);
        register_inputs[i].addEventListener('focusout', enableLoginInputs);
    }

    function disableRegisterInputs() {
        for (var j = 0; j < register_inputs.length; j++)
            register_inputs[j].disabled = true;
    }

    function enableRegisterInputs() {
        if (!login_inputs.some(c => c === document.activeElement))
            for (var j = 0; j < register_inputs.length; j++)
                register_inputs[j].disabled = false;
    }

    function disableLoginInputs() {
        for (var j = 0; j < login_inputs.length; j++)
            login_inputs[j].disabled = true;
    }

    function enableLoginInputs() {
        if (!register_inputs.some(c => c === document.activeElement))
            for (var j = 0; j < login_inputs.length; j++)
                login_inputs[j].disabled = false;
    }

    var login_errs = 0;
    var register_errs = 0;
    var login_name_err_txt = document.getElementById('login_name_err');
    var register_name_err = document.getElementById('register_name_err');

    var name_inputs = document.querySelectorAll('._name_inp');
    name_inputs.forEach(ni => {
        ni.addEventListener('focusout', (e) => {
            is_register = e.target.classList.contains('_r_inp');
            var err = is_register ? register_name_err : login_name_err_txt;

            if (checkNameInput(e.target.value)) {
                err.innerHTML = '';
                e.target.classList.remove('_err');
                if (is_register)
                    --register_errs;
                else
                    --login_errs;
                return;
            }
            e.target.classList.add('_err');
            err.innerHTML = '<p>Name should be e-mail or \'Name Surname\' like.</p>';
            if (is_register)
                ++register_errs;
            else
                ++login_errs;
            //e.target.setCustomValidity('Name should be e-mail or \'Name Surname\' type.');
        });
    });

    function checkNameInput(name) {
        return name && (
            /^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$/.test(name)
            ||
            /^[A-Z][a-z]+\ [A-Z][a-z]+$/.test(name)
        );
    }

    var login_form = document.getElementById('login_form');
    var form_valid_summary = document.getElementById('valid_summary');
    login_form.addEventListener('submit', loginFormSent);


    function loginFormSent(e) {
        e.preventDefault();//TODO: enable login form!
        login_inputs.forEach(el => el.focus());
        form_valid_summary.innerHTML = '';

        if (login_errs > 0) {
            form_valid_summary.innerHTML = 'Some errors got in login form!';
            return;
        }

        console.log('sent_form');
    }

    var register_form = document.getElementById('register_form');
    register_form.addEventListener('submit', registerFormSent);
    var pass_confirmed = false;

    function registerFormSent(e) {
        e.preventDefault();//TODO: enable register form!
        register_inputs.forEach(el => el.focus());
        form_valid_summary.innerHTML = '';

        if (register_errs > 0 || !pass_confirmed) {
            form_valid_summary.innerHTML = 'Some errors got in register form!';
            return;
        }

        console.log('sent_form');
    }

    var pass_box = document.getElementById('pass_box');
    var conf_pass_box = document.getElementById('conf_pass_box');
    var spes_symbols = ['@', '$', '%', '^'];

    var pass_err_box = document.getElementById('pass_err');

    function validatePass(e) {
        var text = e.target.value;
        var is_good = text.length > 6 && text.split('').some(c => c === c.toLowerCase() || c === c.toUpperCase() || spes_symbols.some(s => s === c));
        if (!is_good) {
            conf_pass_box.disabled = true;
            e.target.classList.add('_err');
            pass_err_box.innerHTML = 'Password must contain one ore more of [@, $, %, ^] symbols, lower and upper case char';
        }
        else {
            e.target.classList.remove('_err');
            conf_pass_box.disabled = false;
            pass_err_box.innerHTML = '';
        }
    }

    var conf_pass_err_box = document.getElementById('conf_pass_err');

    function validatePassConf(e) {
        var text = e.target.value;
        if (pass_box.value !== text) {
            e.target.classList.add('_err');
            pass_confirmed = false;
            conf_pass_err_box.innerHTML = 'Not same password.';
        } 
        else {
            e.target.classList.remove('_err');
            pass_confirmed = true;
            conf_pass_err_box.innerHTML = '';
        }
    }
    pass_box.addEventListener('focusout', validatePass);
    pass_box.oninput = validatePass;
    conf_pass_box.addEventListener('focusout', validatePassConf);
    conf_pass_box.oninput = validatePassConf;
});