document.addEventListener('DOMContentLoaded', function () {
    const selectCliente = document.getElementById('ClienteId');
    const selectVeiculo = document.getElementById('VeiculoId');

    if (!selectCliente || !selectVeiculo) return;

    async function carregarVeiculos(clienteId, veiculoSelecionadoId) {
        selectVeiculo.innerHTML = '<option value="">Selecione o veículo...</option>';

        if (!clienteId) return;

        try {
            const response = await fetch(`/Veiculos/GetPorCliente?clienteId=${clienteId}`);
            if (!response.ok) return;

            const veiculos = await response.json();

            veiculos.forEach(function (veiculo) {
                const option = document.createElement('option');
                option.value = veiculo.id;
                option.textContent = veiculo.placa;

                if (veiculoSelecionadoId && veiculo.id == veiculoSelecionadoId) {
                    option.selected = true;
                }

                selectVeiculo.appendChild(option);
            });
        } catch (erro) {
            console.error('Erro ao carregar veículos:', erro);
        }
    }

    selectCliente.addEventListener('change', function () {
        carregarVeiculos(this.value, null);
    });

    // Ao carregar a página (Edit, ou Create com ModelState inválido), filtra pelo cliente já selecionado
    if (selectCliente.value) {
        carregarVeiculos(selectCliente.value, selectVeiculo.value);
    }
});