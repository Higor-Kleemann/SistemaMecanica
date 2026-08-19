document.addEventListener('DOMContentLoaded', function () {
    const selectServico = document.getElementById('selectServico');
    const precoServico = document.getElementById('precoServico');

    if (!selectServico || !precoServico) return;

    selectServico.addEventListener('change', function () {
        const opcaoSelecionada = selectServico.options[selectServico.selectedIndex];
        const preco = opcaoSelecionada.getAttribute('data-preco');

        if (preco) {
            // serviço com preço fixo: preenche e trava o campo
            precoServico.value = preco;
            precoServico.readOnly = true;
        } else {
            // serviço com valor variável (ou nenhum selecionado): libera para digitação
            precoServico.value = '';
            precoServico.readOnly = false;
        }
    });
});