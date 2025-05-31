document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.like-btn').forEach(button => {
        button.addEventListener('click', function () {
            const songId = this.dataset.songId;
            const isLiked = this.dataset.isLiked === 'true';
            const btn = this;

            fetch('/Likes/ToggleLike', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded',
                },
                body: `songId=${encodeURIComponent(songId)}`
            })
                .then(response => {
                    return response.json();
                })
                .then(data => {
                    if (data.isLiked) {
                        btn.classList.remove('btn-outline-danger');
                        btn.classList.add('btn-danger');
                        btn.innerHTML = `<i class="fas fa-heart"></i>`;
                    //    btn.dataset.isLiked = "true";
                    } else {
                        btn.classList.remove('btn-danger');
                        btn.classList.add('btn-outline-danger');
                        btn.innerHTML = `<i class="fas fa-heart"></i>`;
                    //    btn.dataset.isLiked = "false";
                    }
                })
                .catch(error => {
                    console.error(error);
                    alert('Có lỗi xảy ra khi gửi yêu cầu Like.');
                });
        });
    });
});
