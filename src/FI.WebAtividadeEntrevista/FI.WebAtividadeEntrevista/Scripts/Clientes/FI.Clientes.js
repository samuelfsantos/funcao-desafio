
var beneficiarios = [];

$(document).ready(function () {
    $('#formCadastro').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: urlPost,
            method: "POST",
            data: {
                "NOME": $(this).find("#Nome").val(),
                "CEP": $(this).find("#CEP").val(),
                "Email": $(this).find("#Email").val(),
                "Sobrenome": $(this).find("#Sobrenome").val(),
                "CPF": $(this).find("#CPF").val(),
                "Nacionalidade": $(this).find("#Nacionalidade").val(),
                "Estado": $(this).find("#Estado").val(),
                "Cidade": $(this).find("#Cidade").val(),
                "Logradouro": $(this).find("#Logradouro").val(),
                "Telefone": $(this).find("#Telefone").val(),
                "Beneficiarios": beneficiarios
            },
            error:
            function (r) {
                if (r.status == 400)
                    ModalDialog("Ocorreu um erro", r.responseJSON);
                else if (r.status == 500)
                    ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
            },
            success:
            function (r) {
                ModalDialog("Sucesso!", r)
                $("#formCadastro")[0].reset();
            }
        });
    })

    CarregarMascaras();
    CarregarPopupBeneficiarios();
})

function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}

function CarregarMascaras() {
    $("#CPF").inputmask("mask", { "mask": "999.999.999-99" });
    $("#CEP").inputmask("mask", { "mask": "99999-999" });
    $("#Telefone").inputmask("mask", { "mask": "(99) 9999-99999" });
}

function CarregarPopupBeneficiarios() {
    $("#Beneficiarios").click(function () {
        $("#modalBeneficiarios").modal("show");
        $("#BeneficiarioCPF").inputmask("mask", { "mask": "999.999.999-99" });
    });

    $("#IncluirBeneficiario").click(function () {
        var cpf = $("#BeneficiarioCPF").val();
        var nome = $("#BeneficiarioNome").val();

        if (cpf && nome) {
            AdicionarBeneficiario(cpf, nome);
            $("#BeneficiarioCPF, #BeneficiarioNome").val('');
        } else {
            alert("Por favor, preencha CPF e Nome.");
        }
    });
}

function AdicionarBeneficiario(cpf, nome) {
    var cpfExistente = beneficiarios.some(function (beneficiario) {
        return beneficiario.CPF === cpf;
    });

    if (cpfExistente) {
        alert("Este CPF já foi adicionado.");
        return;
    }

    beneficiarios.push({ CPF: cpf, Nome: nome });
    AtualizarGridBeneficiarios();
}

function AtualizarGridBeneficiarios() {
    var tbody = $("#gridBeneficiarios tbody");
    tbody.empty();

    beneficiarios.forEach(function (beneficiario, index) {
        var row = `<tr>
            <td>${beneficiario.CPF}</td>
            <td>${beneficiario.Nome}</td>
            <td>
                <button type="button" class="btn btn-xs btn-primary" onclick="AlterarBeneficiario(${index})">Alterar</button>
                <button type="button" class="btn btn-xs btn-primary" onclick="RemoverBeneficiario(${index})">Excluir</button>
            </td>
        </tr>`;
        tbody.append(row);
    });
}

function RemoverBeneficiario(index) {
    beneficiarios.splice(index, 1);
    AtualizarGridBeneficiarios();
}

function AlterarBeneficiario(index) {
    var beneficiario = beneficiarios[index];
    $("#BeneficiarioCPF").val(beneficiario.CPF);
    $("#BeneficiarioNome").val(beneficiario.Nome);

    beneficiarios.splice(index, 1);
    AtualizarGridBeneficiarios();
}