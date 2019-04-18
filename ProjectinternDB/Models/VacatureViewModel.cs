using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Internal;
using Model;
using Model.Onderwijsdelen;
using Newtonsoft.Json;

namespace ProjectinternDB.Models
{
    public class VacatureViewModel
    {
        public List<Vacature> Vacatures { get; set; }
        [Required]
        public List<Taak> Onderwijstaken { get; set; }
    }
}
