const __RequestVerificationToken = document.querySelector('input[name="__RequestVerificationToken"]')?.value;
const cartBadge = document.getElementById("dyn-cart-badge");
[...document.getElementsByClassName("act-add-to-cart")].forEach(form => {
    form.addEventListener("submit", event => {
        event.preventDefault();
        fetch(
            form.action,
            {
                method: form.method,
                body: new URLSearchParams({ __RequestVerificationToken })
            }
        ).then(() => {
            cartBadge.innerText = parseInt(cartBadge.innerText) + 1;
        });
    });
});
