document.addEventListener('DOMContentLoaded', function () {
    const toggleButton = document.querySelector('.sidebar-toggle');
    const sidebar = document.querySelector('.sidebar');

    if (!toggleButton || !sidebar) return;

    toggleButton.addEventListener('click', function () {
        sidebar.classList.toggle('open');
    });

    // fecha o menu ao clicar em um link (útil no mobile)
    sidebar.querySelectorAll('.sidebar-link').forEach(function (link) {
        link.addEventListener('click', function () {
            sidebar.classList.remove('open');
        });
    });
});