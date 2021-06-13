﻿using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Services
{
    public class AutenticacaoService :Service, IAutenticacaoService
    {
        private readonly HttpClient _httpClient;

        public AutenticacaoService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.AutenticacaoUrl);
        }
        public async Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin)
        {
            try
            {
                var loginContent = ObterConteudo(usuarioLogin);

                var response = await _httpClient.PostAsync("/api/identidade/autenticar", loginContent);

                if (!TratarErrosResponse(response))
                {
                    return new UsuarioRespostaLogin
                    {
                        ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                    };
                }

                return await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
            }catch(Exception e)
            {
                throw e;
            }
           
        }

        public async Task<UsuarioRespostaLogin> Registro(UsuarioRegistro usuarioRegistro)
        {
            var registroContent = ObterConteudo(usuarioRegistro);

            var response = await _httpClient.PostAsync("/api/identidade/nova-conta", registroContent);

            if (!TratarErrosResponse(response))
            {
                return new UsuarioRespostaLogin
                {
                    ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }

            return await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);
        }
    }
}