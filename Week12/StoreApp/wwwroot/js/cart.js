const __RequestVerificationToken = document.querySelector('input[name="__RequestVerificationToken"]')?.value;

const updateBadge = () => {
    const badgeElem = document.getElementById("dyn-cart-badge");
    const cartItemCount = document
        .cookie
        .split("; ")
        .filter(c => c.startsWith("article"))
        .map(c => parseInt(c.split("=")[1]))
        .reduce((s, a) => s + a, 0);

    badgeElem.innerText = cartItemCount;
    badgeElem.dataset.text = cartItemCount;
}

[...document.getElementsByClassName("act-add-to-cart")].forEach(form => {
    form.addEventListener("submit", event => {
        event.preventDefault();
        fetch(
            form.action,
            {
                method: form.method,
                body: new URLSearchParams({ __RequestVerificationToken })
            }
        ).then(() => updateBadge());
    });
});
