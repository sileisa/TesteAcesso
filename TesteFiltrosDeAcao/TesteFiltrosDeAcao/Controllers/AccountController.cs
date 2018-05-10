using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TesteFiltrosDeAcao.Models;

namespace TesteFiltrosDeAcao.Controllers
{
    public class AccountController : Controller
    {
        private TesteFiltrosDeAcaoContext db = new TesteFiltrosDeAcaoContext();

        // GET: Account
        public ActionResult Login(string returnURL)
        {
            ViewBag.ReturnUrl = returnURL;
            return View(new ACESSO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(ACESSO login, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (TesteFiltrosDeAcaoContext db = new TesteFiltrosDeAcaoContext())
                {
                    var vLogin = db.ACESSOes.Where(p => p.EMAIL.Equals(login.EMAIL)).FirstOrDefault();
                    /*Verificar se a variavel vLogin está vazia. Isso pode ocorrer caso o usuário não existe. 
              Caso não exista ele vai cair na condição else.*/
                    if (vLogin != null)
                    {
                        /*Código abaixo verifica se o usuário que retornou na variavel tem está 
                         ativo. Caso não esteja cai direto no else*/
                        if (Equals(vLogin.ATIVO, "S"))
                        {
                            /*Código abaixo verifica se a senha digitada no site é igual a senha que está sendo retornada 
                             do banco. Caso não cai direto no else*/
                            if (Equals(vLogin.SENHA, login.SENHA))
                            {
                                FormsAuthentication.SetAuthCookie(vLogin.EMAIL, false);
                                if (Url.IsLocalUrl(returnUrl)
                                  && returnUrl.Length > 1
                                 && returnUrl.StartsWith("/")
                                 && !returnUrl.StartsWith("//")
                                  && returnUrl.StartsWith("/\\"))
                                {
                                    return Redirect(returnUrl);
                                }
                                /*código abaixo cria uma session para armazenar o nome do usuário*/
                                Session["Nome"] = vLogin.NOME;
                                /*código abaixo cria uma session para armazenar o sobrenome do usuário*/
                                Session["Sobrenome"] = vLogin.SOBRENOME;
                                /*retorna para a tela inicial do Home*/
                                return RedirectToAction("Index", "Home");
                            }
                            /*Else responsável da validação da senha*/
                            else
                            {
                                /*Escreve na tela a mensagem de erro informada*/
                                ModelState.AddModelError("", "Senha informada Inválida!!!");
                                /*Retorna a tela de login*/
                                return View(new ACESSO());
                            }
                        }
                        /*Else responsável por verificar se o usuário está ativo*/
                        else
                        {
                            /*Escreve na tela a mensagem de erro informada*/
                            ModelState.AddModelError("", "Usuário sem acesso para usar o sistema!!!");
                            return View(new ACESSO());
                        }
                    }
                    /*Else responsável por verificar se o usuário existe*/
                    else
                    {
                        /*Escreve na tela a mensagem de erro informada*/
                        ModelState.AddModelError("", "E-mail informado inválido!!!");
                        /*Retorna a tela de login*/
                        return View(new ACESSO());
                    }
                }
                
                
            }
            return View(login);
        }



        // GET: Account/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACESSO aCESSO = db.ACESSOes.Find(id);
            if (aCESSO == null)
            {
                return HttpNotFound();
            }
            return View(aCESSO);
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoginId,EMAIL,SENHA,PERFIL,NOME,SOBRENOME")] ACESSO aCESSO)
        {
            if (ModelState.IsValid)
            {
                db.ACESSOes.Add(aCESSO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aCESSO);
        }

        // GET: Account/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACESSO aCESSO = db.ACESSOes.Find(id);
            if (aCESSO == null)
            {
                return HttpNotFound();
            }
            return View(aCESSO);
        }

        // POST: Account/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoginId,EMAIL,SENHA,PERFIL,NOME,SOBRENOME")] ACESSO aCESSO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aCESSO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aCESSO);
        }

        // GET: Account/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACESSO aCESSO = db.ACESSOes.Find(id);
            if (aCESSO == null)
            {
                return HttpNotFound();
            }
            return View(aCESSO);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ACESSO aCESSO = db.ACESSOes.Find(id);
            db.ACESSOes.Remove(aCESSO);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
