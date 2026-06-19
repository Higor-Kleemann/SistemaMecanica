const select = document.getElementById('TipoPessoa');

function atualizarCampos() {
    const tipo = select.value;

    document.getElementById('campo-cpf').style.display = tipo === 'PF' ? 'block' : 'none';
    document.getElementById('campo-cnpj').style.display = tipo === 'PJ' ? 'block' : 'none';
    document.getElementById('campo-nomefantasia').style.display = tipo === 'PJ' ? 'block' : 'none';

    const campoNome = document.getElementById('campo-nome');
    if (campoNome) {
        campoNome.querySelector('label').textContent = tipo === 'PF' ? 'Nome Completo' : 'Razão Social';
    }
}

select.addEventListener('change', atualizarCampos);
atualizarCampos();