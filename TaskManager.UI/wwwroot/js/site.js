$(".sidebar-toggle").click(function (e) {
    e.preventDefault();
    $("#wrapper").toggleClass("toggled");
});

$("[data-role=dropdown]").dropdown();