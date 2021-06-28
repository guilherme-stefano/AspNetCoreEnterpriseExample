﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSE.Clientes.API.Application.Commands;
using NSE.Core.Mediator;
using NSE.WebAPI.Core.Controllers;

namespace NSE.Clientes.API.Controllers
{
    public class ClientesController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public ClientesController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet("clientes")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var resultado = await _mediatorHandler.EnviarComando(
                    new RegistrarClienteCommand(Guid.NewGuid(), "Eduardo", "edu@edu.com", "30314299076"));
                return CustomResponse(resultado);
            }
            catch(Exception e)
            {
                throw e;
            }

            
        }
    }
}