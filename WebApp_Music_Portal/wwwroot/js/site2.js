$('.input-file input[type=file]').on('change', function () {
	let file = this.files[0];
	$(this).closest('.input-file').find('.input-file-text').html(file.name);
});
 
$(document).ready(function () {


    ////show Список користувачів
    $("#indexUser_").on("click", function () {
        ClosedViews();
        $("#indexUsers").css('display', "block");
        GetUsers();
    });
    let ListUsers = function (user) {
        return " <tr style='height: 50px;'  data-rowid='" + user.id + "'><td class='str2'>" + user.name + "</td>" +
            "<td class='str2'>" + user.email + "</td>" +
            "<td class='str2'>" + user.age + "</td>" +
            "<td><a href='javascript:void(0)' data-editid='" + user.id + "' class='buttonOK UserEdits'>редагування</a></td>" +
            "<td><a href='javascript:void(0)' data-delid='" + user.id + "' class='buttonDel UserDel'>видалення</a></td>/tr>";
    };
    function GetUsers() {
        $.ajax({
            url: 'https://localhost:7070/api/Users',
            method: 'GET',
            contentType: "application/json",
            success: function (UserModels) {
                let rowsUser = "";
                $.each(UserModels, function (index, UserModel) { rowsUser += ListUsers(UserModel); })
                $("#blockUsers").html(rowsUser);
            },
            error: function (x, y, z) {
                alert(x.responseJSON.detail + '\n' + y + '\n' + z);
            }
        });
    }

    ////show додавання користувача
    $("#newUser").on("click", function () {
        ClosedViews();
        $("#UserCreate").css('display', "block");
         NewUser(); 
    });
    function CheckFill() {
        let Name = $("#Name_Cr").val();
        let Password = $("#Password_Cr").val();
        let PasswordConfirm = $("#PasswordConfirm_Cr").val();
        let Email = $("#Email_Cr").val();
        let Age = $("#Age_Cr").val();
        if (Name == "") {
            $("#Name_CrError").text("Поле має бути встановлене!");
            return false;
        }
        else
            $("#Name_CrError").text("");
        if (Password == "") {
            $("#Password_CrError").text("Поле має бути встановлене!");
            return false;
        }
        else
            $("#Password_CrError").text("");
        if (PasswordConfirm == "") {
            $("#PasswordConfirm_CrError").text("Поле має бути встановлене!");
            return false;
        }
        else
            $("#PasswordConfirm_CrError").text("");
        if (Email == "") {
            $("#Email_CrError").text("Поле має бути встановлене!");
            return false;
        }
        else
            $("#Email_CrError").text("");
        if (Age == "") {
            $("#Age_CrError").text("Поле має бути встановлене!");
            return false;
        }
        else
            $("#Age_CrError").text("");
        if (Password != PasswordConfirm) {
            $("#PasswordConfirm_CrError").text("Паролі не рівні!");
            return false;
        }
        else
            $("#PasswordConfirm_CrError").text("");

        if (Password.length < 6)
        {
            $("#Password_CrError").text("Довжина пароля має бути від 6 символів!");
            return false;
        }

        let age_ = parseInt(Age)
        if (age_ > 60 || age_ < 5)
        {
            $("#Age_CrError").text("Неприпустимий вік!");
            return false;
        }

        return true;
    }
    function CheckFillEditUser() {
        let Name = $("#Name_r").val();
        let Password = $("#Password_r").val();
        let PasswordConfirm = $("#PasswordConfirm_r").val();
        let Email = $("#Email_r").val();
        let Age = $("#Age_r").val();
        if (Name == "") {
            $("#Name_rError").text("Поле має бути встановлене!");
            return false;
        }
        else
            $("#Name_rError").text("");
        if (Password == "") {
            $("#Password_rError").text("Поле має бути встановлене!");
            return false;
        }
        else
            $("#Password_rError").text("");
        if (PasswordConfirm == "") {
            $("#PasswordConfirm_rError").text("Поле має бути встановлене!");
            return false;
        }
        else
            $("#PasswordConfirm_rError").text("");
        if (Email == "") {
            $("#Email_CrError").text("Поле має бути встановлене!");
            return false;
        }
        else
            $("#Email_rError").text("");
        if (Age == "") {
            $("#Age_rError").text("Поле має бути встановлене!");
            return false;
        }
        else
            $("#Age_rError").text("");
        if (Password != PasswordConfirm) {
            $("#PasswordConfirm_CrError").text("Паролі не рівні!");
            return false;
        }
        else
            $("#PasswordConfirm_rError").text("");

        if (Password.length < 6) {
            $("#Password_rError").text("Довжина пароля має бути від 6 символів!");
            return false;
        }

        let age_ = parseInt(Age)
        if (age_ > 60 || age_ < 5) {
            $("#Age_rError").text("Неприпустимий вік!");
            return false;
        }

        return true;
    }
    function NewUser() {

        if (!CheckFill()) {
            return;
        }
        $.ajax({
            url: 'https://localhost:7070/api/Users',
            contentType: "application/json",
            method: "POST",
            data: JSON.stringify({
                Name: $("#Name_Cr").val(),
                Password: $("#Password_Cr").val(),
                Email: $("#Email_Cr").val(),
                Age: $("#Age_Cr").val()
            }),
            success: function (student) {
               
                $("#CreateError").text("Новий користувач доданий!");
                $("#Name_Cr").val("");
                $("#Password_Cr").val("");
                $("#PasswordConfirm_Cr").val("");
                $("#Email_Cr").val("");
                $("#Age_Cr").val("");
                form.elements["Id"].value = 0;
            },
            error: function (x, y, z) {
                if (x.responseText == "dd") {
                    $("#Email_CrError").text("Некорректный Email!");
                    return;
                }
                else if (x.responseText == "nn")
                {
                    $("#Name_CrError").text("Ім'я  вже використовується!");
                    return;
                }
                else if (x.responseText == "ee") {
                    $("#Email_CrError").text("Email вже використовується!");
                    return;
                }
                else
                alert(x.responseText);
            }
        })

    }
    $("#CreateUser").on("click", function () {
        NewUser();
    });

     ////show редагування користувача
    $("body").on("click", ".UserEdits", function () {
        let id = $(this).data("editid");
        $("#UserCreate").css('display', "none");
        $("#indexUsers").css('display', "none");
        $("#UserEdit").css('display', "block");

        getUser(id);
    });
    function getUser(id)
    {
        $.ajax({
            url: "https://localhost:7070/api/Users/" + id,
            contentType: "application/json",
            method: "GET",
            success: function (user) {
                $("#IdUsed").val(user.id);
                $("#Name_r").val(user.name);
                $("#Password_r").val(user.password);
                $("#PasswordConfirm_r").val("");
                $("#Email_r").val(user.email);
                $("#Age_r").val(user.age);
                $("#Activate_r").val(user.admin);
            },
            error: function (x, y, z) {

                alert(x.responseText);
            }
        });
    }
    function EditUser()
    {
        let request = JSON.stringify({
            Id: $("#IdUsed").val(),
            Name: $("#Name_r").val(),
            Password: $("#Password_r").val(),
            Email: $("#Email_r").val(),
            Age: $("#Age_r").val(),
            Admin: $("#Activate_r").val()
        });
        $.ajax({
            url: "https://localhost:7070/api/Users/",
            contentType: "application/json",
            method: "PUT",
            data: request,
            success: function (user) {
                ClosedViews();
                GetUsers();
                $("#IdUsed").val('0');
            },
            error: function (x, y, z) {
                if (x.responseText == "dd") {
                    $("#Email_rError").text("Некорректный Email!");
                    return;
                }
                else
                    alert(x.responseText);
            }
        })

    }
    $("#EditUser").on("click", function ()
    {
        if (!CheckFillEditUser()) 
            return;
        EditUser();
    });

    ////show видалення користувача
    $("body").on("click", ".UserDel", function () {
        let id = $(this).data("delid");
        DeleteUser(id);
    });
    function DeleteUser(id)
    {
        if (!confirm("Ви дійсно хочете видалити користувачa?"))
            return;
        $.ajax({
            url: "https://localhost:7070/api/Users/" + id,
            contentType: "application/json",
            method: "DELETE",
            success: function (student) {
                $("tr[data-rowid='" + student.id + "']").remove();
            },
            error: function (x, y, z) {
                alert(x + '\n' + y + '\n' + z);
            }
        })
    }


    ////show Список жанрів
    $("#indexGenres_").on("click", function () {
        ClosedViews();
        $("#indexGenres").css('display', "block");
        GetGenre();
    });
    let ListGenres = function (genre) {
        return " <tr style='height: 50px;'  data-rowid='" + genre.id + "'><td class='str2'>" + genre.genres + "</td>" +
            "<td><a href='javascript:void(0)' data-editgenreid='" + genre.id + "' class='buttonOK genreEdits'>редагування</a></td>" +
            "<td><a href='javascript:void(0)' data-delgenreid='" + genre.id + "' class='buttonDel genreDel'>видалення</a></td>/tr>";
    };
    function GetGenre() {
        $.ajax({
            url: 'https://localhost:7070/api/Genres',
            method: 'GET',
            contentType: "application/json",
            success: function (GenreModels) {
                let rowsGenre = "";
                $.each(GenreModels, function (index, GenreModel) { rowsGenre += ListGenres(GenreModel); })
                $("#blockGenres").html(rowsGenre);
            },
            error: function (x, y, z) {
                alert(x.responseJSON.detail + '\n' + y + '\n' + z);
            }
        });
    }

    ////show додавання жанру
    $("#newGenres").on("click", function () {
        ClosedViews();
        $("#GenreCreate").css('display', "block");
        NewGenre();
    });
    function NewGenre() {
        let Genr = $("#Genre_Cr").val();
        if (Genr == "") {
            $("#Genre_CrError").text("Поле має бути встановлене!");
            $("#CreateGenreError").text("");
            return ;
        }
        else
            $("#Genre_CrError").text("");
        $.ajax({
            url: 'https://localhost:7070/api/Genres',
            contentType: "application/json",
            method: "POST",
            data: JSON.stringify({
                Genres: $("#Genre_Cr").val(),
            }),
            success: function (student) {

                $("#CreateGenreError").text("Новий жанр доданий!");
                $("#Genre_Cr").val("");
            },
            error: function (x, y, z) {
                if (x.responseText == "gg") {
                    $("#Genre_CrError").text("Жанр існує!");
                    return;
                }
                else
                    alert(x.responseText);
            }
        })

    }
    $("#CreateGenre").on("click", function () {
        NewGenre();
    });

    ////show редагування жанру
    $("body").on("click", ".genreEdits", function () {
        let id = $(this).data("editgenreid");
        ClosedViews();
        $("#GenreEdit").css('display', "block");
        getGenre(id);
    });
    function getGenre(id) {
        $.ajax({
            url: "https://localhost:7070/api/Genres/" + id,
            contentType: "application/json",
            method: "GET",
            success: function (Genre) {
                $("#IdGenre").val(Genre.id);
                $("#Genre_r").val(Genre.genres);
            },
            error: function (x, y, z) {

                alert(x.responseText);
            }
        });
    }
    function EditGenre() {
        let request = JSON.stringify({
            Id: $("#IdGenre").val(),
            Genres: $("#Genre_r").val(),
        });
        $.ajax({
            url: "https://localhost:7070/api/Genres/",
            contentType: "application/json",
            method: "PUT",
            data: request,
            success: function (user) {
                ClosedViews();
                GetGenre();
                $("#indexGenres").css('display', "block");
                $("#IdGenre").val('0');
            },
            error: function (x, y, z) {
                if (x.responseText == "gg") {
                    $("#Genre_rError").text("Жанр існує!");
                    return;
                }
                else
                    alert(x.responseText);
            }
        })

    }
    $("#EditGenre").on("click", function () {
        let Genr = $("#Genre_r").val();
        if (Genr == "") {
            $("#Genre_rError").text("Поле має бути встановлене!");
            $("#CreateGenreError2").text("");
            return;
        }
        else
            $("#Genre_rError").text("");
        EditGenre();
    });


    ////show видалення жанру
    $("body").on("click", ".genreDel", function () {
        let id = $(this).data("delgenreid");
        DeleteGenre(id);
    });
    function DeleteGenre(id) {
        if (!confirm("Ви дійсно хочете видалити жанр?"))
            return;
        $.ajax({
            url: "https://localhost:7070/api/Genres/" + id,
            contentType: "application/json",
            method: "DELETE",
            success: function (Genres) {
                $("tr[data-rowid='" + Genres.id + "']").remove();
            },
            error: function (x, y, z) {
                alert(x + '\n' + y + '\n' + z);
            }
        })
    }

    
    ////show Список пісень
    $("#indexMusics_").on("click", function () {
        ClosedViews();
        $("#indexMusics").css('display', "block");
        GetMusics();
    });
    let ListMusics = function (Music) {
        return "<tr class='str1' data-rowid='" + Music.id + "'>" +
            "<td>" + Music.name + "</td>" +
            "<td>" + Music.executor + "</td>" +
            "<td>" + Music.size + "</td>" +
            "<td> " + Music.gpa + "</td>" +
            "<td class='td th'><audio controls class='mp3'>" +
            "<source src='" + "/mp3/" + Music.path + "' type='audio/ogg'>" +
            "<source src='" + "/mp3/" + Music.path + "' type = 'audio/mpeg'>" +
            "</audio></td>"+  
            "<td><a href='javascript:void(0)' data-editmusiceid='" + Music.id + "' class='buttonOK musicEdits'>редагування</a></td>" +
            "<td><a href='javascript:void(0)' data-delmusicid='" + Music.id + "' class='buttonDel musicDel'>видалення</a></td>/tr>";
    };
    function GetMusics() {
        $.ajax({
            url: 'https://localhost:7070/api/Musics',
            method: 'GET',
            contentType: "application/json",
            success: function (MusicsModels) {
                let rowsMusics = "";
                $.each(MusicsModels, function (index, MusicsModel) { rowsMusics += ListMusics(MusicsModel); })
                $("#listMusic").html(rowsMusics);
            },
            error: function (x, y, z) {
                alert(x.responseJSON.detail + '\n' + y + '\n' + z);
            }
        });
    }

     ////show редагування пісень
    $("body").on("click", ".musicEdits", function () {
        let id = $(this).data("editmusiceid");
        ClosedViews();
        GetGenre3();
        $("#MusicEdit").css('display', "block");
        getMusic(id);
    });
    function CheckMusic2() {
        let Name = $("#Music_NameE").val();
        let Executor = $("#Music_ExecutorE").val();
        let ComboBox = $("#MyComboBox_E").val();

        if (Name == "") {
            $("#Music_NameErrorE").text("Поле має бути встановлене!");
            return false;
        }
        else
            $("#Music_NameErrorE").text("");
        if (Executor == "") {
            $("#Music_ExecutorErrorE").text("Поле має бути встановлене!");
            return false;
        }
        else
            $("#Music_ExecutorErrorE").text("");
        if (ComboBox == "") {
            $("#MyComboBox_ErrorE").text("Поле має бути встановлене!");
            return false;
        }
        else
            $("#MyComboBox_ErrorE").text("");

        return true;
    }
    function GetGenre3() {
        $.ajax({
            url: 'https://localhost:7070/api/Genres',
            method: 'GET',
            contentType: "application/json",
            success: function (GenreModels) {
                let rowsGenre = "";
                $.each(GenreModels, function (index, GenreModel) {
                    rowsGenre += myComboBox(GenreModel);
                })
                $("#MyComboBox_E").html(rowsGenre);
            },
            error: function (x, y, z) {
                alert(x.responseJSON.detail + '\n' + y + '\n' + z);
            }
        });
    }
    function getMusic(id) {
        $.ajax({
            url: "https://localhost:7070/api/Musics/" + id,
            contentType: "application/json",
            method: "GET",
            success: function (Musics) {
                $("#Music_NameE").val(Musics.name);
                $("#Music_ExecutorE").val(Musics.executor);
                $("#IdMusic").val(Musics.id);
                $("#MyComboBox_E").val(Musics.genres);
            },
            error: function (x, y, z) {

                alert(x.responseText);
            }
        });
    }
    function EditMusic() {

        if (!CheckMusic2())
            return;
        let request = JSON.stringify({
            Id: $("#IdMusic").val(),
            Name: $("#Music_NameE").val(),
            Executor: $("#Music_ExecutorE").val(),
            Genres: $("#MyComboBox_E").val(),
        });
        $.ajax({
            url: "https://localhost:7070/api/Musics/",
            contentType: "application/json",
            method: "PUT",
            data: request,
            success: function (user) {
                ClosedViews();
                GetMusics();
                $("#indexMusics").css('display', "block");
                $("#IdMusic").val('0');
            },
            error: function (x, y, z) {

                    alert(x.responseText);
            }
        })

    }
    $("#EditMus").on("click", function () {
        EditMusic();
    });

    ////show додавання пісені
    $("#newMusic").on("click", function () {
        ClosedViews();
        GetGenre2();
        $("#MusicCreate").css('display', "block");
    });
    let myComboBox = function (Genre) {
        return " <option value='" + Genre.genres + "'>" + Genre.genres + "</option>";
    }; 
    function NewMusic() {
        if (!CheckMusic())
            return;
        let formData = new FormData();
        formData.append("uploadedFilej", $("#UploadedFile_").prop("files")[0]);
        formData.append("Music_Namej", $("#Music_Name").val());
        formData.append("Music_Executorj", $("#Music_Executor").val());
        formData.append("myComboBoxj", $("#MyComboBox_").val());

        $.ajax({
            url: 'https://localhost:7070/api/Musics',
            method: "POST",
            contentType: false,
            processData: false,
            data: formData,
            success: function (data) {
                $("#Music_Name").val("");
                $("#Music_Executor").val("");
                $("#MyComboBox_").val("");
                $("#UploadedFile_").val("");
                $("#CreateMusicError").text("Пісня додана!");
            },
            error: function (xhr, status, error) {
                alert(xhr.responseText);
            }
        });
    }
    function GetGenre2() {
        $.ajax({
            url: 'https://localhost:7070/api/Genres',
            method: 'GET',
            contentType: "application/json",
            success: function (GenreModels) {
                let rowsGenre = "";
                $.each(GenreModels, function (index, GenreModel)
                {
                    rowsGenre += myComboBox(GenreModel);
                })
                $("#MyComboBox_").html(rowsGenre);
            },
            error: function (x, y, z)
            {
                alert(x.responseJSON.detail + '\n' + y + '\n' + z);
            }
        });
    }
    function CheckMusic()
    {
        let Name = $("#Music_Name").val();
        let Executor = $("#Music_Executor").val();
        let ComboBox = $("#MyComboBox_").val();

        if (Name == "") {
            $("#Music_NameError").text("Поле має бути встановлене!");
            return false;
        }
        else
            $("#Music_NameError").text("");
        if (Executor == "")
        { 
            $("#Music_ExecutorError").text("Поле має бути встановлене!");
             return false;
        }
        else
            $("#Music_ExecutorError").text("");
        if (ComboBox == "")
        { 
            $("#MyComboBox_Error").text("Поле має бути встановлене!");
            return false;
        }
        else
            $("#MyComboBox_Error").text("");

        return true;
    }
    $("#CreateMus").on("click", function () {
        NewMusic();
    });

    ////show видалення жанру
    $("body").on("click", ".musicDel", function () {
        let id = $(this).data("delmusicid");
        DeleteMusic(id);
    });
    function DeleteMusic(id) {
        if (!confirm("Ви дійсно хочете видалити пісню?"))
            return;
        $.ajax({
            url: "https://localhost:7070/api/Musics/" + id,
            contentType: "application/json",
            method: "DELETE",
            success: function (Musics) {
                $("tr[data-rowid='" + Musics.id + "']").remove();
            },
            error: function (x, y, z) {
                alert(x + '\n' + y + '\n' + z);
            }
        })
    }

    function ClosedViews()
    {
        $("#MusicEdit").css('display', "none");
        $("#MusicCreate").css('display', "none");
        $("#indexMusics").css('display', "none");
        $("#GenreEdit").css('display', "none");
        $("#GenreCreate").css('display', "none");
        $("#indexGenres").css('display', "none");
        $("#UserEdit").css('display', "none");
        $("#indexUsers").css('display', "none");
        $("#UserCreate").css('display', "none"); 
    }
});