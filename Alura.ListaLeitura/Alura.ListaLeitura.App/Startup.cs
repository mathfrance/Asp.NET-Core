using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            var builder = new RouteBuilder(app);
            builder.MapRoute("Livros/ParaLer", LivrosParaLer);
            builder.MapRoute("Livros/Lendo", LivrosLendos);
            builder.MapRoute("Livros/Lidos", LivrosLidos);
            builder.MapRoute("Cadastro/NovoLivro/{nome}/{autor}", NovoLivroParaLer);
            builder.MapRoute("Livros/Detalhes/{id:int}", ExibirLivroDetalhado);

            var rotas = builder.Build();

            app.UseRouter(rotas);

            //app.Run(Roteamento);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public Task Roteamento(HttpContext contexto)
        {
            var _repo = new LivroRepositorioCSV();

            var caminhosAtendidos = new Dictionary<string, RequestDelegate>
            {
                {"/Livros/ParaLer", LivrosParaLer},
                {"/Livros/Lendo", LivrosLendos},
                {"/Livros/Lidos", LivrosLidos }
            };


            if (caminhosAtendidos.ContainsKey(contexto.Request.Path))
            {
                var metodo = caminhosAtendidos[contexto.Request.Path];
                return metodo.Invoke(contexto);
            }

            contexto.Response.StatusCode = 404;
            return contexto.Response.WriteAsync("Caminho Inexistente");
        }


        public Task NovoLivroParaLer(HttpContext contexto)
        {
            var livro = new Livro()
            {
                Titulo = contexto.GetRouteValue("nome").ToString(),
                Autor = contexto.GetRouteValue("autor").ToString()
            };
            var _repo = new LivroRepositorioCSV();
            _repo.Incluir(livro);

            return contexto.Response.WriteAsync("Livro Adicionado com sucesso");
        }

        public Task ExibirLivroDetalhado(HttpContext contexto)
        {
            var id = Convert.ToInt32(contexto.GetRouteValue("id"));
            var _repo = new LivroRepositorioCSV();
            var livroEncontrado = _repo.Todos.First(livro => livro.Id == id);

            return contexto.Response.WriteAsync(livroEncontrado.Detalhes());
        }

        public Task LivrosParaLer(HttpContext contexto)
        {
            var _repo = new LivroRepositorioCSV();

            return contexto.Response.WriteAsync(_repo.ParaLer.ToString());
        }

        public Task LivrosLendos(HttpContext contexto)
        {
            var _repo = new LivroRepositorioCSV();

            return contexto.Response.WriteAsync(_repo.Lendo.ToString());
        }

        public Task LivrosLidos(HttpContext contexto)
        {
            var _repo = new LivroRepositorioCSV();

            return contexto.Response.WriteAsync(_repo.Lidos.ToString());
        }
    }
}