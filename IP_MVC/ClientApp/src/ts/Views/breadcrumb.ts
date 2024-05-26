document.addEventListener("DOMContentLoaded", () => {
    // Functie om de breadcrumb dynamisch bij te werken
    const updateBreadcrumb = () => {
        const breadcrumb = document.getElementById("breadcrumb");
        if (!breadcrumb) return; // Controleer of breadcrumb niet null is

        breadcrumb.innerHTML = ""; // Leeg de breadcrumb

        // Voeg het "Home" item toe
        const homeItem = document.createElement("li");
        homeItem.classList.add("breadcrumb-item");
        const homeLink = document.createElement("a");
        homeLink.href = "/";
        homeLink.textContent = "Home";
        homeItem.appendChild(homeLink);
        breadcrumb.appendChild(homeItem);

        // Voeg andere items toe op basis van de paginatitel
        const pageTitle = (document.title as HTMLTitleElement).textContent?.split(" - ")[0]; // Haal de paginatitel op zonder de site-titel
        if (!pageTitle) return; // Stop als de paginatitel niet gevonden kan worden

        const pageTitleArray = pageTitle.split(" "); // Split de titel op spaties
        const currentUrl = window.location.pathname; // Haal de huidige URL op

        // Loop door elk deel van de paginatitel en voeg een breadcrumb-item toe
        let cumulativeUrl = "/";
        pageTitleArray.forEach((part, index) => {
            cumulativeUrl += part.toLowerCase(); // Voeg het huidige deel van de titel toe aan de URL
            const item = document.createElement("li");
            item.classList.add("breadcrumb-item");
            if (index === pageTitleArray.length - 1) {
                // Laatste item in de breadcrumb actief maken
                item.classList.add("active");
                item.setAttribute("aria-current", "page");
                item.textContent = part;
            } else {
                const link = document.createElement("a");
                link.href = cumulativeUrl;
                link.textContent = part;
                item.appendChild(link);
            }
            breadcrumb.appendChild(item);
            cumulativeUrl += "/"; // Voeg een slash toe aan de URL
        });
    };

    // Breadcrumb bijwerken wanneer de DOM is geladen en wanneer de paginatitel verandert
    updateBreadcrumb();
    document.title.addEventListener("change", updateBreadcrumb as EventListener);
});