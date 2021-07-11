using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NSE.Core.Communication;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services
{
    public class ComparasBffService : Service, IComparasBffService
    {
        private readonly HttpClient _httpClient;

        public ComparasBffService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.ComprasBffUrl);
        }

        public async Task<CarrinhoViewModel> ObterCarrinho()
        {
            var response = await _httpClient.GetAsync("/compras/carrinho/");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<CarrinhoViewModel>(response);
        }
        public async Task<int> ObterQuantidadeCarrinho()
        {
            try
            {
                var response = await _httpClient.GetAsync("/compras/carrinho-quantidade/");

                TratarErrosResponse(response);

                return await DeserializarObjetoResponse<int>(response);
            }
            catch(Exception e)
            {
                throw e;
            }
 

        }
        public async Task<ResponseResult> AdicionarItemCarrinho(ItemCarrinhoViewModel carrinho)
        {
            var itemContent = ObterConteudo(carrinho);

            var response = await _httpClient.PostAsync("/compras/carrinho/items/", itemContent);

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }
        public async Task<ResponseResult> AtualizarItemCarrinho(Guid produtoId, ItemCarrinhoViewModel item)
        {
            var itemContent = ObterConteudo(item);

            var response = await _httpClient.PutAsync($"/compras/carrinho/items/{produtoId}", itemContent);

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }
        public async Task<ResponseResult> RemoverItemCarrinho(Guid produtoId)
        {
            var response = await _httpClient.DeleteAsync($"/compras/carrinho/items/{produtoId}");

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }
        public async Task<ResponseResult> AplicarVoucherCarrinho(string voucher)
        {
            var itemContent = ObterConteudo(voucher);

            var response = await _httpClient.PostAsync("/compras/carrinho/aplicar-voucher/", itemContent);

            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

            return RetornoOk();
        }




        //public async Task<ResponseResult> FinalizarPedido(PedidoTransacaoViewModel pedidoTransacao)
        //{
        //    var pedidoContent = ObterConteudo(pedidoTransacao);

        //    var response = await _httpClient.PostAsync("/compras/pedido/", pedidoContent);

        //    if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

        //    return RetornoOk();
        //}

        //public async Task<PedidoViewModel> ObterUltimoPedido()
        //{
        //    var response = await _httpClient.GetAsync("/compras/pedido/ultimo/");

        //    TratarErrosResponse(response);

        //    return await DeserializarObjetoResponse<PedidoViewModel>(response);
        //}

        //public async Task<IEnumerable<PedidoViewModel>> ObterListaPorClienteId()
        //{
        //    var response = await _httpClient.GetAsync("/compras/pedido/lista-cliente/");

        //    TratarErrosResponse(response);

        //    return await DeserializarObjetoResponse<IEnumerable<PedidoViewModel>>(response);
        //}
    }
}