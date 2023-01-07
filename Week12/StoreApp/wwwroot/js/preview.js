document.getElementById("preview-input")?.addEventListener("change", event => {
    const reader = new FileReader();
    reader.onload = () => {
        const img = document.getElementById("preview-img")
        img.src = reader.result;
    };
    reader.readAsDataURL(event.target.files[0]);
});
