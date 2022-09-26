using AvaliacaoLp3.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AvaliacaoLp3.Controllers;

public class EstabelecimentosController : Controller
{
    public static List<EstabelecimentoViewModel> estabelecimentos = new List<EstabelecimentoViewModel>();

    public IActionResult Index()
    {
        return View(estabelecimentos);
    }

    public IActionResult Gerenciamento()
    {
        return View(estabelecimentos);
    }

    public IActionResult Detalhes(int id)
    {
        return View(estabelecimentos[id - 1]);
    }

    public IActionResult Excluir(int id)
    {
        estabelecimentos.RemoveAt(id - 1);

        for(var i = id - 1; i < estabelecimentos.Count; i++)
        {
            estabelecimentos[i].Id -= 1;
        }

        return View();
    }

    public IActionResult Cadastro()
    {
        return View();
    }

    public IActionResult ConfirmacaoCadastro([FromForm] string piso, [FromForm] string nome, [FromForm] string descricao, [FromForm] string tipo, [FromForm] string email)
    {
        foreach (var estabelecimentos in estabelecimentos)
        {
            if(nome == estabelecimentos.Nome)
            {
                ViewData["Titulo"] = "Cadastro negado...";
                ViewData["Mensagem"] = "Estabelecimento já é cadastrado...";
                return View();
            }
        }

        int id = 1;
        if(estabelecimentos.Count != 0) id = estabelecimentos[estabelecimentos.Count - 1].Id + 1;

        estabelecimentos.Add(new EstabelecimentoViewModel(id, piso, nome, descricao, tipo, email));

        ViewData["Titulo"] = "Cadastro aprovado!";
        ViewData["Mensagem"] = "Estabelecimento cadastrado com sucesso!";

        return View();
    }
}