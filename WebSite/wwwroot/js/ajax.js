$(document).ready(function () {
   $("#feedback-form").submit(function () {
       
       var formNm = $('#feedback-form');
        $.ajax({
            type: "POST",
            url: '/home/responser',
            data: formNm.serialize(),
            beforeSend: function () {
                // Вывод текста в процессе отправки
                $(formNm).html('<div style="text-align:center;"><img src="https://i.gifer.com/ZKZg.gif" width="50px"/></div>');
            },
            success: function (data) {
                if (data == 'true') 
                // Вывод текста результата отправки
                    $(formNm).html('<p style="color:green;">Ваша заявка успешно отправлена. Мы обязательно с Вами свяжемся!</p>');
                else
                    $(formNm).html('<p style="color:red;">Ошибка подключения к серверу. Попробуйте немного позже повторить запрос.</p>');
            },
            error: function (jqXHR, text, error) {
                // Вывод текста ошибки отправки
                $(formNm).html(error);
            }
        });
        return false;
    });
});