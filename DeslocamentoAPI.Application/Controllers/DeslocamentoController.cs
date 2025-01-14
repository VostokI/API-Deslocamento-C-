﻿using DeslocamentoAPI.Application.Documentos.Commands.CriarDeslocamento;
using DeslocamentoAPI.Application.Documentos.Queries;
using DeslocamentoAPI.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DeslocamentoApp.WebAPI.Controllers
{
    public class DeslocamentoController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] GetDeslocamentosQuery query)
        {
            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("{deslocamentoId:long}")]
        public async Task<IActionResult> GetSync(long deslocamentoId)//[FromRoute] GetDocumentoQuery query)
        {
            var query = new GetDeslocamentoQuery() { DeslocamentoId = deslocamentoId };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CriarDeslocamentoCommand command)
        {
            var result = await Mediator.Send(command);

            return Created($"id={result.Id}", result);
        }
        [HttpPut("{deslocamentoId:long}/encerrar_deslocamento")]
        public async Task<IActionResult> PutAdicionarSignatarioAsync(
            [FromRoute] long deslocamentoId,
            [FromBody] EncerrarDeslocamentoCommand command)
        {
            if (deslocamentoId != command.DeslocamentoId) return BadRequest();

            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
