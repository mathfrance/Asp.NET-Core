using Alura.ListaLeitura.App.HTML;
using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace Alura.ListaLeitura.App.Logica
{
    public class CadastroController
    {
        public static string Incluir(Livro livro)
        {           
            var _repo = new LivroRepositorioCSV();
            _repo.Incluir(livro);
            return "Livro Adicionado com sucesso";
        }

        public IActionResult ExibirFormulario()
        {
            var html = new ViewResult { ViewName = "formulario" };
            return html;
        }
    }
}
