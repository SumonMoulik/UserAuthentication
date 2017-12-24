
function CheckAllPermission(cntrl) {
    cntrl = jQuery(cntrl);
    if (cntrl.is(":checked")) {
        $('#Menus').find('tr:not(:first)').each(function () {
            var row = $(this);
            row.find('input[type="checkbox"]').prop("checked", true);
        });
    }
    else {
        $('#Menus').find('tr:not(:first)').each(function () {
            var row = $(this);
            row.find('input[type="checkbox"]').prop("checked", false);
        });
    }
}

function MenuItem(userId, userMenuId, permission) {
    this.userId = userId;
    this.userMenuId = userMenuId;
    this.permission = permission;
}
(function ($) {
    $(document).ready(function () {
        var userId = sessionStorage.getItem("UserId");
        if (userId =="null")
            window.location = '/Home/Login'
        $('#btnSave').hide();
        if ($('#UserId').val() == '' || $('#UserId').val() == 0)
            $('#UserMenuId').prop('disabled', true);
        else
            $('#UserMenuId').prop('disabled', false);

        $('#UserId').change(function () {
            if ($('#UserId').val() == '' || $('#UserId').val() == 0)
                $('#UserMenuId').prop('disabled', true);
            else
                $('#UserMenuId').prop('disabled', false);
        });

        var userMenus = new Array();

        function LoadUserMenu() {
            if ($('#UserMenuId').val() > 0 || $('#UserMenuId').val() != '') {
                $('#btnSave').hide();
                $("#Menus").empty();
                $.ajax({
                    url: '/Menu/GetMenuByCode?menuId=' + $('#UserMenuId').val(),
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        $("#Menus").empty();
                        $('#btnSave').show();
                        $("#Menus").append('<tr><th hidden="true"></th><th>Menu</th><th>Permission<input type="checkbox" id="chkAllPermission" onclick="CheckAllPermission(this)"/>All</th></tr>');
                        $.each(data, function (key, value) {
                            $.ajax({
                                url: '/Menu/CheckUserMenu',//?menuId=' + $('#UserMenuId').val(),
                                type: "GET",
                                data: { userId: $('#UserId').val(), menuId: value.MenuId },
                                contentType: "application/json; charset=utf-8",
                                success: function (permission) {
                                    if (permission.Permission == true)
                                        $("#Menus").append('<tr><td hidden="true">' + value.MenuId + '</td><td>' + value.MenuText + '</td><td><input type="checkbox" id="chkPermission" style="margin-left:40%;" checked/></td></tr>');
                                    else
                                        $("#Menus").append('<tr><td hidden="true">' + value.MenuId + '</td><td>' + value.MenuText + '</td><td><input type="checkbox" id="chkPermission" style="margin-left:40%;"/></td></tr>');
                                },
                                error: function () {
                                    $("#Menus").append('<tr><td hidden="true">' + value.MenuId + '</td><td>' + value.MenuText + '</td><td><input type="checkbox" id="chkPermission" style="margin-left:40%;"/></td></tr>');
                                }
                            });
                        });
                    }
                });
            }
            else {
                $('#btnSave').hide();
                $("#Menus").empty();
            }
        }

        $('#UserMenuId').change(function () {
            LoadUserMenu();
        });

        $('#btnSave').click(function (e) {
            userMenus.length = 0;
            if ($('#UserMenuId').val() == '' || $('#UserMenuId').val() == 0) {
                alert('Select main menu.');
                $('#UserMenuId').focus();
                return;
            }
            userMenus.push(new MenuItem(parseInt($('#UserId').val()), parseInt($('#UserMenuId').val()), true));
            $('#Menus').find('tr:not(:first)').each(function () {
                var row = $(this);
                var menuPermission = false;
                if (row.find('input[type="checkbox"]').is(':checked')) {
                    menuPermission = true;
                }
                var menuId = parseInt(row.find('td:eq(0)').text());
                userMenus.push(new MenuItem($('#UserId').val(), menuId, menuPermission));
            });
            $.ajax({
                url: '/Menu/SaveUserMenu',
                type: 'POST',
                data: JSON.stringify({
                    menu: {
                        UserMenuList: userMenus
                    }
                }),
                contentType: 'application/json;charset=utf-8',
                success: function (data) {
                    alert('Save Success.');
                    LoadUserMenu();
                }
            });
            e.preventDefault();
        });
    });
})(jQuery);