$(function () {
    $('#btn-add-gist').on('click', function () {
        var getDescription = $('#txt-gist').val().trim();
        if (getDescription) {

            var gistData = {
                "description": getDescription,
                "public": true,
                "files": {
                    "file1.txt": {
                        "content": "Your file content goes here."
                    }
                }
            };

            $.ajax({
                url: 'https://api.github.com/gists',
                data: JSON.stringify(gistData),
                type:'POST',
                headers: {
                    'Authorization': authHeader
                },
                success: function (result) {
                    $('#txt-gist').val('');
                    alert('Gist added successfully.');
                    setTimeout(function () {
                        location.reload();
                    }, 1500);

                }, error: function (result) {
                    alert('Gist could not be added.');
                }
            })
        } else {
            alert('Please enter gist description.')
        }
    });

});