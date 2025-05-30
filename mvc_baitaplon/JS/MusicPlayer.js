document.addEventListener("DOMContentLoaded", function () {
    const musicPlayer = document.getElementById("music-player");
    const songTitle = document.getElementById("song-title");
    const playButton = document.getElementById("play-btn");
    const vol = document.getElementById("vol");
    const progressBar = document.getElementById("progress-bar");
    const currentTimeLabel = document.getElementById("current-time");
    const durationLabel = document.getElementById("duration");
    const loopButton = document.getElementById("loop-btn");
    const likeButton = document.getElementById("like-btn");

    const audio = new Audio();
    let isPlaying = false;
    let currentSongTitle = "";
    let currentFilePath = "";
    let isLooping = false;
    let isLiked = false;

    window.currentSongId = null;

    function formatTime(seconds) {
        const mins = Math.floor(seconds / 60);
        const secs = Math.floor(seconds % 60);
        return `${mins}:${secs.toString().padStart(2, '0')}`;
    }

    function updateLikeButtonUI() {
        if (isLiked) {
            likeButton.classList.remove("btn-outline-danger");
            likeButton.classList.add("btn-danger");
            likeButton.innerHTML = '<i class="fas fa-heart"></i>';
        } else {
            likeButton.classList.remove("btn-danger");
            likeButton.classList.add("btn-outline-danger");
            likeButton.innerHTML = '<i class="fas fa-heart"></i>';
        }
    }


    window.playSong = function (title, filePath, songId, coverImagePath, username,id) {
        songTitle.innerHTML = `
            <div>
                <a href="/UserProfile/Index/${id}" style="font-weight: bold; color: #ccc; text-decoration: none;">
                    ${username}
                </a>
            </div>
        <div style="font-size: 1.2em;">${title}</div>
            `;
        window.currentSongId = songId;
        currentFilePath = filePath;

        const coverImg = document.getElementById("cover-image");
        if (coverImagePath) {
            coverImg.src = coverImagePath;
            coverImg.style.display = "block";
        } else {
            coverImg.src = "";
            coverImg.style.display = "none";
        }

        fetch(`/Likes/IsLiked/${songId}`)
            .then(res => res.json())
            .then(data => {
                isLiked = data.liked;
                updateLikeButtonUI();
            })
            .catch(err => {
                console.error("Lỗi khi kiểm tra like:", err);
            });

        audio.src = filePath;
        audio.volume = vol.value / 100;

        audio.onerror = function () {
            alert("Không thể phát nhạc! Kiểm tra đường dẫn file.");
        };

        audio.play()
            .then(() => {
                isPlaying = true;
                playButton.innerHTML = '<i class="fas fa-pause"></i>';
            })
            .catch(error => {
                console.error("Lỗi khi phát nhạc:", error);
            });

        musicPlayer.classList.remove("d-none");
    };

    playButton.addEventListener("click", function () {
        if (audio.src === "") {
            alert("Chưa chọn bài nhạc!");
            return;
        }

        if (audio.paused) {
            audio.play();
            playButton.innerHTML = '<i class="fas fa-pause"></i>';
        } else {
            audio.pause();
            playButton.innerHTML = '<i class="fas fa-play"></i>';
        }
    });

    vol.addEventListener("input", function () {
        audio.volume = this.value / 100;
    });

    audio.addEventListener("timeupdate", () => {
        progressBar.value = Math.floor(audio.currentTime);
        currentTimeLabel.innerText = formatTime(audio.currentTime);
    });

    audio.addEventListener("loadedmetadata", () => {
        progressBar.max = Math.floor(audio.duration);
        durationLabel.innerText = formatTime(audio.duration);
    });

    progressBar.addEventListener("input", () => {
        audio.currentTime = progressBar.value;
    });

    loopButton.addEventListener("click", () => {
        isLooping = !isLooping;
        audio.loop = isLooping;

        if (isLooping) {
            loopButton.classList.remove("btn-outline-secondary");
            loopButton.classList.add("btn-success");
        } else {
            loopButton.classList.remove("btn-success");
            loopButton.classList.add("btn-outline-secondary");
        }
    });
    likeButton.addEventListener("click", () => {
        if (!window.currentSongId) {
            alert("Chưa chọn bài nhạc để thả tim.");
            return;
        }

        fetch("/Likes/Toggle", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ songId: window.currentSongId })
        })
            .then(res => {
                if (!res.ok) throw new Error("Lỗi khi gửi yêu cầu like/unlike");
                return res.json();
            })
            .then(data => {
                if (data.success) {
                    isLiked = data.liked;
                    updateLikeButtonUI();
                    alert(data.message);
                } else {
                    alert(data.message);
                }
            })
            .catch(err => {
                console.error(err);
            });
    });

});
