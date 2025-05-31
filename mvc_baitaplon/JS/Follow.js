//document.addEventListener('DOMContentLoaded', function () {
//    document.querySelectorAll('.follow-btn').forEach(button => {
//        button.addEventListener('click', function () {
//            const followedUserId = this.dataset.AccountID;
//            const btn = this;

//            fetch('/Follows/ToggleFollow', {
//                method: 'POST',
//                headers: {
//                    'Content-Type': 'application/x-www-form-urlencoded',
//                },
//                body: `followedUserId=${encodeURIComponent(followedUserId)}`
//            })
//                .then(response => {
//                    if (!response.ok) {
//                        throw new Error(`HTTP error! status: ${response.status}`);
//                    }
//                    const contentType = response.headers.get('content-type') || '';
//                    if (!contentType.includes('application/json')) {
//                        throw new Error('Response is not JSON');
//                    }
//                    return response.json();
//                })
//                .then(data => {
//                    if (data.success) {
//                        if (data.isFollowing) {
//                            btn.classList.remove('btn-outline-secondary');
//                            btn.classList.add('btn-success');
//                            btn.textContent = 'Đang theo dõi';
//                            btn.dataset.isFollowing = "true";
//                        } else {
//                            btn.classList.remove('btn-success');
//                            btn.classList.add('btn-outline-secondary');
//                            btn.textContent = 'Theo dõi';
//                            btn.dataset.isFollowing = "false";
//                        }
//                    } else {
//                        alert(data.message || 'Lỗi không xác định.');
//                    }
//                })
//                .catch(error => {
//                    console.error('Lỗi chi tiết:', error);
//                    alert('Có lỗi xảy ra khi gửi yêu cầu theo dõi hoặc nhận dữ liệu.');
//                });
//        });
//    });
//});
