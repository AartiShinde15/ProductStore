function changeImage(event) {
    var selectedFile = event.target.files[0];
    if (selectedFile) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#imgSrc').attr('src', e.target.result);
            $('#hdnImageUrl').value = e.target.result;
            //$('hdnImageUrl').attr('required', false);
            //$('div#imageDiv').find('span').html('');
        }
        reader.readAsDataURL(selectedFile);
    }
}

var currentValue = 0;
function handleClick(ref) {
    currentValue = ref.value;
    return currentValue;
}
$('#Rating').value = handleClick(ref);
