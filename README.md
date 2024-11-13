# funcao-desafio

Projeto contruido para atender a um processo seletivo:

-> Implementação do CPF do cliente:
* Na tela de cadastramento/alteração de clientes, incluir um novo campo denominado CPF, que permitirá o cadastramento do CPF do cliente.
* Pontos relevantes:
. O novo campo deverá seguir o padrão visual dos demais campos da tela
. O cadastramento do CPF será obrigatório
. Deverá possuir a formatação padrão de CPF (999.999.999-99)
. Deverá consistir se o dado informado é um CPF válido (conforme o cálculo padrão de verificação do dígito verificador de CPF)
. Não permitir o cadastramento de um CPF já existente no banco de dados, ou seja, não é permitida a existência de um CPF duplicado

-> Implementação do botão Beneficiários:
* Na tela de cadastramento/alteração de clientes, incluir um novo botão denominado “Beneficiários”, que permitirá o cadastramento de beneficiários do cliente, o mesmo deverá abrir um pop-up para inclusão do “CPF” e “Nome do beneficiário”, além disso deverá existir um grid onde serão exibidos os beneficiários que já foram inclusos, no mesmo grid será possível realizar a manutenção dos beneficiários cadastrados, permitindo a alteração e exclusão dos mesmos.
* Pontos relevantes:
. O novo botão e novos campos deverão seguir o padrão visual dos demais botões e campos da tela
. O campo CPF deverá possuir a formatação padrão (999.999.999-99)
. Deverá consistir se o dado informado éum CPF válido (conforme o cálculo padrão de verificação do dígito verificador de CPF)
. Não permitir o cadastramento de mais de um beneficiário com o mesmo CPF para o mesmo cliente
. O beneficiário deverá ser gravado na base de dados quando for acionado o botão “Salvar” na tela de “Cadastrar Cliente”

-> Implementação do seguinte endpoint:
* Investmentos
* POST api/v1/investmentos/cdb: Este endpoint realiza o calculo do CDB baseado em seu Valor Inicial e o Prazo em Meses

-> Tecnologias , estilos e padrões de arquitetura utilizados
* MVC para Interface do usuário - UI 
* Módulo de negócios - BLL
* Módulo de acesso a dados - DAL
* Bootstrap 
* jTable 2.4.0
* inputmask 5.0.9
* LocalDB Sql Server Express

-> DevOps
* CI/CD com GitHub Actions
* O projeto MVC utiliza o .NET Framework 4.8
* Hospedagem da SITE: Azure Web App (S.O. Windows)

-> Links úteis
* SITE: https://funcao-desafio-site-epdsdxe9anbkdrhg.canadacentral-01.azurewebsites.net/Cliente
* Pipeline: https://github.com/samuelfsantos/funcao-desafio/actions