using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LJJ_VITINHO.Data;
using LJJ_VITINHO.Models;

namespace LJJ_VITINHO.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrador")]
    public class UsuariosController : Controller
    {
        //variavel globais
        BancoDados bd;
        IWebHostEnvironment servidorweb;

        // método construtor
        public UsuariosController(IWebHostEnvironment webHostEnvironment)
        {
            servidorweb = webHostEnvironment;
        }

        public string SalvarArquivo(IFormFile arquivo)
        {
            if (arquivo == null)
            {
                return string.Empty;
            }
            string nomeArquivo = $"{Guid.NewGuid()}.{Path.GetExtension(arquivo.FileName)}";
            string pastaArquivo = Path.Combine(servidorweb.WebRootPath, "uploads");
            string caminhoArquivo = Path.Combine(pastaArquivo, nomeArquivo);

            var dadosArquivo = new FileStream(caminhoArquivo, FileMode.Create);
            arquivo.CopyTo(dadosArquivo);
            dadosArquivo.Close();

            return nomeArquivo;
        }

        private bool ExcluirArquivo(string nomeArquivo)
        {
            // verificar se existe um nome de arquivo
            if (string.IsNullOrEmpty(nomeArquivo))
            {
                return false;
            }
            // pasta do arquivo
            string pastaArquivo = Path.Combine(servidorweb.WebRootPath, "uploads");
            string caminhoArquivo = Path.Combine(pastaArquivo, nomeArquivo);

            System.IO.File.Delete(caminhoArquivo);
            return true;
        }



        public IActionResult Index()
        {
            //inicia o banco de dados
            bd = new BancoDados();
            //consulta para obter todos os usuarios
            var listaUsuarios = bd.Usuarios.ToList();
            return View(listaUsuarios);
        }

        [HttpPost]
        public IActionResult Index(string busca)
        {
            //inicia o banco de dados
            bd = new BancoDados();
            //consulta para obter todos os usuarios
            var listaUsuarios = bd.Usuarios.ToList();
            //verificacao da busca
            if (!string.IsNullOrWhiteSpace(busca))
            {
                listaUsuarios = listaUsuarios.Where(u =>
                u.Email.Contains(busca)).ToList();
            }
            return View(listaUsuarios);
        }

        [HttpGet]
        public IActionResult Incluir()
        {
            Usuarios usuarios = new Usuarios();
            return View(usuarios);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Incluir(Usuarios model, IFormFile? arquivo)
        {
            //verificar se as informacoes estão corretas
            if (ModelState.IsValid)
            {
                bd = new BancoDados();
                // Verifica se existe um cadastro da conta pelo email
                var usuario = bd.Usuarios.FirstOrDefault(u => u.Email == model.Email);
                if (usuario != null)
                {
                    ModelState.AddModelError("Email", "Conta de e-mail já cadastrado!");
                    return View(usuario);
                }
                //Salva o arquivo de foto do usuário
                if (arquivo != null)
                {
                    var nomeArquivo = SalvarArquivo(arquivo);
                    model.Arquivo = nomeArquivo;
                }


                // Caso não exista, realiza o novo cadastro
                bd.Usuarios.Add(model);
                bd.SaveChanges();
                if (model.Perfil == Perfil.Clientes)
                {
                    return RedirectToAction("Incluir", "Clientes", new { area = "Admin", idusuario = model.Id });
                }
                else
                {
                    return RedirectToAction("Incluir", "Funcionarios", new { area = "Admin", idusuario = model.Id });
                }

            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Alterar(int id)
        {
            bd = new BancoDados();
            var usuario = bd.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alterar(Usuarios model, IFormFile arquivo)
        {
            if (ModelState.IsValid)
            {
                // Verifica para fazer a troca do arquivo

                if (arquivo != null)
                {
                    // Excluir o arquivo antigo
                    ExcluirArquivo(model.Arquivo);
                    // Salva o arquivo novo
                    var nomeArquivo = SalvarArquivo(arquivo);
                    model.Arquivo = nomeArquivo;
                }

                bd = new BancoDados();
                bd.Usuarios.Update(model);
                bd.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Exibir(int id)
        {
            bd = new BancoDados();
            var usuario = bd.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }
        [HttpGet]
        public IActionResult Excluir(int id)
        {
            bd = new BancoDados();
            var usuario = bd.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Excluir(Usuarios model)
        {
            bd = new BancoDados();
            var usuario = bd.Usuarios.FirstOrDefault(u => u.Id == model.Id);
            if (model.Id > 0)
            {
                if (!string.IsNullOrWhiteSpace(model.Arquivo))
                {
                    //Exclui o arquivo pelo nome
                    ExcluirArquivo(model.Arquivo);
                }
                bd.Usuarios.Remove(usuario);
                bd.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }


    }
}
