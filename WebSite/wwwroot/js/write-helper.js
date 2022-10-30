function insertText(id, text) {
	//ищем элемент по id
	var txtarea = document.getElementById(id);
	//ищем первое положение выделенного символа
	var start = txtarea.selectionStart;
	//ищем последнее положение выделенного символа
	var end = txtarea.selectionEnd;
	// текст до + вставка + текст после (если этот код не работает, значит у вас несколько id)
	var finText = txtarea.value.substring(0, start) + text + txtarea.value.substring(end);
	// подмена значения
	txtarea.value = finText;
	// возвращаем фокус на элемент
	txtarea.focus();
	// возвращаем курсор на место - учитываем выделили ли текст или просто курсор поставили
	txtarea.selectionEnd = (start == end) ? (end + text.length) : end;
}
$('.add_text').click(function () {
	if ($(this).val() == "<br/>") {
		insertText('info_sms_id', "<br/>");
	}

	if ($(this).val() == "<p>") {
		insertText('info_sms_id', "<p>");
	}

	if ($(this).val() == "</p>") {
		insertText('info_sms_id', "</p>");
	}

	if ($(this).val() == "<b>") {
		insertText('info_sms_id', "<b>");
	}

	if ($(this).val() == "</b>") {
		insertText('info_sms_id', "</b>");
	}
});