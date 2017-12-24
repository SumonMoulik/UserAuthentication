function EditUser(userId) {
    $('#UserEmail').prop('disabled', true);
}
(function ($) {
    $(document).ready(function () {
        $('#UserEmail').prop('disabled', false);
        $("#Users tr:not(:first)").remove();
        GetUsersList();
        function GetUsersList() {
            $.ajax({
                url: '/User/GetUsersList',
                type: 'GET',
                contentType: 'application/json; charset=utf-8',
                success: function (usersList) {
                    $.each(usersList, function (key, value) {
                        $("#Users").append('<tr><td hidden>' + value.UserId + '</td><td>' + value.UserName + '</td><td>' + value.UserEmail + '</td><td hidden>' + value.Password + '</td><td><a href="#" onclick="EditUser(' + value.UserId + ');">Edit</a></td></tr>');
                    });
                }
            });
        }

        function InputValidation() {
            if ($("#UserName").val() == '')
                return false;
            else if ($("#UserEmail").val() == '')
                return false;
            else if ($("#Password").val() == '')
                return false;
            else if ($("#ConfirmPassword").val() == '')
                return false;
            else if ($("#Password").val() !==$("#ConfirmPassword").val())
                return false;
            else
                return true;
        }
        $('.glyphicon').click(function () {
            if ($('#Password').val() !== '') {
                if ($('#Password').attr('type') == 'password') {
                    $('#Password').attr('type', 'text');
                }
                else if ($('#Password').attr('type') == 'text') {
                    $('#Password').attr('type', 'password');
                }
                $('.glyphicon').toggleClass('glyphicon-eye-close').toggleClass('glyphicon-eye-open');
            }
        });

        $('#btnSave').click(function () {
            var retValue = InputValidation();
            if (retValue == true) {
                $.ajax({
                    url: '/User/SaveUser',
                    type: 'POST',
                    data: JSON.stringify({
                        users: {
                            UserId: $("#UserId").val(),
                            UserName: $("#UserName").val(),
                            UserEmail: $("#UserEmail").val(),
                            Password: $("#Password").val()
                        }
                    }),
                    contentType: 'application/json;charset=utf-8',
                    success: function (data) {
                        if (data > 0) {
                            alert('Save Success.');
                            GetUsersList();
                        }
                        else {
                            alert('Save Failed.');
                            window.stop();
                        }
                    }
                });
            }


        });

    });
})(jQuery);