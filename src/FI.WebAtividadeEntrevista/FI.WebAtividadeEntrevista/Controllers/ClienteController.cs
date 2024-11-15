﻿using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;
using FI.WebAtividadeEntrevista.Customs;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            BoCliente bo = new BoCliente();
            
            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                model.CPF = model.CPF.LimparFormatacaoCpf();

                if (bo.VerificarExistencia(model.CPF))
                {
                    Response.StatusCode = 400;
                    return Json("CPF informado para o cliente já existe");
                }

                model.Beneficiarios = model.Beneficiarios == null ? new List<BeneficiarioModel>() : model.Beneficiarios;
                if (model.Beneficiarios.GroupBy(b => b.CPF.LimparFormatacaoCpf()).Any(g => g.Count() > 1))
                {
                    Response.StatusCode = 400;
                    return Json("CPF do beneficiário está duplicado");
                }

                model.Id = bo.Incluir(new Cliente()
                {                    
                    CEP = model.CEP,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone,
                    CPF = model.CPF
                });

                model.Beneficiarios.ForEach(b =>
                {
                    Beneficiario beneficiario = new Beneficiario() { Nome = b.Nome, CPF = b.CPF.LimparFormatacaoCpf(), IdCliente = model.Id };
                    new BoBeneficiario().Incluir(beneficiario);
                });

                return Json("Cadastro efetuado com sucesso");
            }
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
            BoCliente bo = new BoCliente();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                model.CPF = model.CPF.LimparFormatacaoCpf();

                Cliente cliente = bo.Consultar(model.Id);
                if (bo.VerificarExistencia(model.CPF) && model.CPF != cliente.CPF)
                {
                    Response.StatusCode = 400;
                    return Json("CPF informado para o cliente já existe");
                }

                model.Beneficiarios = model.Beneficiarios == null ? new List<BeneficiarioModel>() : model.Beneficiarios;
                if (model.Beneficiarios.GroupBy(b => b.CPF.LimparFormatacaoCpf()).Any(g => g.Count() > 1))
                {
                    Response.StatusCode = 400;
                    return Json("CPF do beneficiário está duplicado");
                }

                bo.Alterar(new Cliente()
                {
                    Id = model.Id,
                    CEP = model.CEP,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone,
                    CPF = model.CPF
                });

                new BoBeneficiario().Listar(model.Id).ForEach(b =>
                {
                    new BoBeneficiario().Excluir(b.Id);
                });

                model.Beneficiarios.ForEach(b =>
                {
                    Beneficiario beneficiario = new Beneficiario() { Nome = b.Nome, CPF = b.CPF.LimparFormatacaoCpf(), IdCliente = model.Id };
                    new BoBeneficiario().Incluir(beneficiario);
                });

                return Json("Cadastro alterado com sucesso");
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoCliente bo = new BoCliente();
            Cliente cliente = bo.Consultar(id);
            Models.ClienteModel model = null;

            if (cliente != null)
            {
                model = new ClienteModel()
                {
                    Id = cliente.Id,
                    CEP = cliente.CEP,
                    Cidade = cliente.Cidade,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone,
                    CPF = cliente.CPF,
                    Beneficiarios = new BoBeneficiario().Listar(cliente.Id).Select(b => new BeneficiarioModel() 
                    { 
                        Id = b.Id, 
                        Nome = b.Nome, 
                        CPF = b.CPF, 
                        IdCliente = b.IdCliente 
                    }).ToList()
                };            
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }
}