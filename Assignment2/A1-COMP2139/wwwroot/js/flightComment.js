function loadComments(flightId) {
    $.ajax({
        url: '/FlightComment/GetComments?flightId=' + flightId,
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
    var flightId = $('#flightComments input[name="FlightId"]').val();
    loadComments(flightId);

    $('#addCommentForm').submit(function (e) {
        e.preventDefault();
        var formData = {
            FlightId: flightId,
            Content: $('#flightComments textarea[name="content"]').val()
        };

        $.ajax({
            url: '/FlightComment/AddComment',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(formData),
            success: function (response) {
                if (response.success) {
                    $('#flightComments textarea[name="content"]').val(''); // clear message area
                    loadComments(flightId);
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