using Alura.ListaLeitura.App.HTML;
using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App.Logica
{
   public class LivrosController : Controller
    {
        public static string Detalhes(int id)
        {
            var _repo = new LivroRepositorioCSV();
            var livroEncontrado = _repo.Todos.First(livro => livro.Id == id);

            return livroEncontrado.Detalhes();
        }        

        private static string CarregaLista(IEnumerable<Livro> livros)
        {
            var conteudoArquivo = HtmlUtils.CarregaArquivoHTML("lista");           

            return conteudoArquivo;
        }

        public IActionResult ParaLer()
        {
            var _repo = new LivroRepositorioCSV();
            ViewBag.Livros = _repo.ParaLer.Livros;
            return View("lista");
        }

        public IActionResult Lendo()
        {
            var _repo = new LivroRepositorioCSV();
            ViewBag.Livros = _repo.Lendo.Livros;
            return View("lista");
        }

        public IActionResult Lidos()
        {
            var _repo = new LivroRepositorioCSV();
            ViewBag.Livros = _repo.Lidos.Livros;
            return View("lista");
        }
    }
}
