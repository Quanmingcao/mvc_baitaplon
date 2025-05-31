document.addEventListener("DOMContentLoaded", function () {
    const musicPlayer = document.getElementById("music-player");
    const songTitle = document.getElementById("song-title");
    const playButton = document.getElementById("play-btn");
    const vol = document.getElementById("vol");
    const progressBar = document.getElementById("progress-bar");
    const currentTimeLabel = document.getElementById("current-time");
    const durationLabel = document.getElementById("duration");
    const loopButton = document.getElementById("loop-btn");

    const audio = new Audio();
    let isPlaying = false;
    let isLooping = false;

    window.currentSongId = null;

    function formatTime(seconds) {
        const mins = Math.floor(seconds / 60);
        const secs = Math.floor(seconds % 60);
        return `${mins}:${secs.toString().padStart(2, '0')}`;
    }

    window.playSong = function (title, filePath, songId, coverImagePath, username, userId) {
        if (window.currentSongId === songId && !audio.paused) {
            return;
        }

        songTitle.innerHTML = `
            <div>
                <a href="/UserProfile/Index/${userId}" style="font-weight: bold; color: #ccc; text-decoration: none;">
                    ${username}
                </a>
            </div>
            <div style="font-size: 1.2em;">${title}</div>
        `;

        window.currentSongId = songId;

        const coverImg = document.getElementById("cover-image");
        if (coverImagePath) {
            coverImg.src = coverImagePath;
            coverImg.style.display = "block";
        } else {
            coverImg.src = "";
            coverImg.style.display = "none";
        }

        audio.src = filePath;
        audio.volume = vol.value / 100;

        audio.onerror = function () {
            alert("Không thể phát nhạc! Kiểm tra đường dẫn file.");
        };

        audio.play()
            .then(() => {
                isPlaying = true;
                playButton.innerHTML = '<i class="fas fa-pause"></i>';

                fetch("/ListenHistorys/AddToHistory", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/x-www-form-urlencoded"
                    },
                    body: `songId=${songId}`
                })
                    .then(res => {
                        if (!res.ok) {
                            console.error("Lỗi ghi lịch sử:", res.status);
                        }
                    })
                    .catch(err => {
                        console.error("Lỗi fetch ghi lịch sử:", err);
                    });

                fetch("/Songs/AddToView", {
                    method: "POST",
                    headers: { "Content-Type": "application/x-www-form-urlencoded" },
                    body: `songId=${songId}`
                })
                    .then(res => {
                        if (!res.ok) {
                            console.error("Lỗi tăng views:", res.status);
                        }
                    })
                    .catch(err => console.error("Lỗi fetch tăng views:", err));
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
            isPlaying = true;
            playButton.innerHTML = '<i class="fas fa-pause"></i>';
        } else {
            audio.pause();
            isPlaying = false;
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
});
