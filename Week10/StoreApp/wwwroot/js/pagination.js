/**
 * @param {Object} params
 * @param {Element} params.wrapperElement
 * @param {(page: number) => string} params.urlBuilder
 * @param {(item: unknown) => Element} params.childBuilder
*/
const paginate = ({ wrapperElement, urlBuilder, childBuilder }) => {
    let page = 1;

    const observer = new IntersectionObserver(entries => entries.forEach(entry => {
        if (entry.isIntersecting) {
            observer.disconnect();
            fetch(urlBuilder(page)).then(res => res.json()).then(data => {
                page++;
                if (data.length > 0) {
                    data.forEach(item =>
                        wrapperElement.appendChild(childBuilder(item))
                    );
                    observer.observe(wrapperElement.lastElementChild);
                }
            })
        }
    }), { threshold: 1.0 });

    observer.observe(wrapperElement.lastElementChild);
};
