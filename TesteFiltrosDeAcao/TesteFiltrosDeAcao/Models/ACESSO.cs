using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TesteFiltrosDeAcao.Models
{
    public class ACESSO
    {
        [Key]
        public int LoginId { get; set; }
        public String EMAIL { get; set; }
        public String SENHA { get; set; }
        public char ATIVO { get; set; }
        public String PERFIL { get; set; }
        public String NOME { get; set; }
        public String SOBRENOME { get; set; }
    }
}