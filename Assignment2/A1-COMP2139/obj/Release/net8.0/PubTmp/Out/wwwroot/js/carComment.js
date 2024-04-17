function loadComments(carId) {
    $.ajax({
        url: '/CarComment/GetComments?carId=' + carId,
        method: 'GET',
        success: function (data) {
            var commentHtml = '';
            for (var i = 0; i < data.length; i++) {
                commentHtml += '<div class="comment">';
                commentHtml += '<p>' + data[i].content + '</p>';
                commentHtml += '<span>Posted on ' + new Date(data[i].datePosted).toLocaleString() + '</span>';
                commentHtml += '</div>';
            }
            $('#commentsList').html(commentHtml);
        }
    });
}

$(document).ready(function () {
    var carId = $('#carComments input[name="CarId"]').val();
    loadComments(carId);

    $('#addCommentForm').submit(function (e) {
        e.preventDefault();
        var formData = {
            CarId: carId,
            Content: $('#carComments textarea[name="content"]').val()
        };

        $.ajax({
            url: '/CarComment/AddComment',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(formData),
            success: function (response) {
                if (response.success) {
                    $('#carComments textarea[name="content"]').val(''); // clear message area
                    loadComments(carId);
                } else {
                    alert(response.message);
                }
            },
            error: function (xhr, status, error) {
                alert("Error " + error);
            }
        });
    });
});