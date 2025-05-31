$(document).ready(function () {
    const $searchBox = $('#searchBox');
    const $suggestBox = $('#suggestBox');
    let timeout;

    $searchBox.on('keyup', function () {
        const keyword = $(this).val().trim();

        clearTimeout(timeout);
        if (keyword.length === 0) {
            $suggestBox.hide();
            return;
        }

        timeout = setTimeout(() => {
            $.ajax({
                url: '/Search/Suggest',
                type: 'GET',
                data: { term: keyword },
                success: function (data) {
                    let html = '';

                    if (data.songs && data.songs.length > 0) {
                        html += '<h6 class="dropdown-header">Bài hát</h6>';
                        data.songs.forEach(item => {
                            html += `<a href="/Search?search=${encodeURIComponent(item.title)}" class="dropdown-item">${item.title}</a>`;
                        });
                    }

                    if (data.collections && data.collections.length > 0) {
                        html += '<h6 class="dropdown-header">Bộ sưu tập</h6>';
                        data.collections.forEach(item => {
                            html += `<a href="/Songs/Index/${item.id}" class="dropdown-item">${item.name}</a>`;
                        });
                    }

                    if (data.users && data.users.length > 0) {
                        html += '<h6 class="dropdown-header">Người dùng</h6>';
                        data.users.forEach(item => {
                            html += `<a href="/UserProfile/Index/${item.id}" class="dropdown-item">${item.username}</a>`;
                        });
                    }

                    if (html === '') {
                        html = '<span class="dropdown-item text-muted">Không tìm thấy kết quả</span>';
                    }

                    $suggestBox.html(html).show();
                },
                error: function () {
                    $suggestBox.hide();
                }
            });
        }, 300);
    });

    $(document).on('click', function (e) {
        if (!$(e.target).closest('#searchBox, #suggestBox').length) {
            $suggestBox.hide();
        }
    });
});
