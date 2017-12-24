(function ($) {
    $(document).ready(function () {
        $('#btnLogin').click(function () {
            var retValue = InputValidation();
            if (retValue == true) {
                $.ajax({
                    url: '/Home/LoginInfo',
                    type: 'GET',
                    data: { email: $('#Email').val(), password: $('#Password').val() },
                    success: function (data) {
                        if (data > 0) {
                            sessionStorage.setItem("UserId", data);
                            window.location = '../Home/Index/';
                            $('#loginError').hide();
                        }
                        else {
                            $('#loginError').show();
                            window.stop();
                            return false;
                        }
                    }
                });
            }
            else
                window.stop();
        });

        function InputValidation() {
            if ($('#Email').val() == "")
                return false;
            else if ($('#Password').val() == "")
                return false;
            else
                return true;
        }
    });
})(jQuery);