function openAccordion(orderId) {
    var button = document.getElementById("b" + orderId);
    var div = document.getElementById("d" + orderId);

    button.classList.toggle("active");

    var panel = div;
    if (panel.style.display === "block") {
        panel.style.display = "none";
    } else {
        panel.style.display = "block";
    }
}