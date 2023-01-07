document.getElementById("export")?.addEventListener("click", () => {
    const data = JSON.stringify(Array.from(document.querySelectorAll("tbody tr")).map(tr => {
        const [, Name, priceStr, dateStr, Category] = Array.from(tr.children).map(td => td.textContent.trim());
        const Price = parseFloat(priceStr.replace(/[,$]/g, ""));
        const ExpirationDate = dateStr === "N/A" ? undefined : new Date(dateStr);
        return { Name, Price, ExpirationDate, Category };
    }));

    const file = new Blob([data], { type: "application/json" });

    const a = document.createElement("a");
    a.href = URL.createObjectURL(file);
    a.download = "data.json";
    a.click();
    URL.revokeObjectURL(a.href);
});

document.getElementById("import")?.addEventListener("click", () => {
    const __RequestVerificationToken = document.querySelector('input[name="__RequestVerificationToken"]').value;
    const input = document.createElement("input");
    input.type = "file";
    input.accept = ".json";
    input.onchange = e => {
        const reader = new FileReader();
        reader.onload = e => {
            const items = JSON.parse(e.target.result);
            console.log(items);
            Promise.all(items.map(item => fetch("/Article/Create", {
                method: "POST",
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded"
                },
                body: new URLSearchParams({ ...item, __RequestVerificationToken })
            }))).then(() => location.reload());
        };
        reader.readAsText(e.target.files[0]);
    }
    input.click();
});
