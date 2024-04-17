function loadComments(hotelId) {
    $.ajax({
        url: '/HotelComment/GetComments?hotelId=' + hotelId,
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
    var hotelId = $('#hotelComments input[name="HotelId"]').val();
    loadComments(hotelId);

    $('#addCommentForm').submit(function (e) {
        e.preventDefault();
        var formData = {
            HotelId: hotelId,
            Content: $('#hotelComments textarea[name="content"]').val()
        };

        $.ajax({
            url: '/HotelComment/AddComment',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(formData),
            success: function (response) {
                if (response.success) {
                    $('#hotelComments textarea[name="content"]').val(''); // clear message area
                    loadComments(hotelId);
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