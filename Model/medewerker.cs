using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class medewerker
    {
        public medewerker()
        {

        }
        public medewerker(string id, string email, string role_id)
        {
            this.id = id;
            this.role_id = role_id;
            this.email = email;
        }

        public string id { get; set; }

  public string role_id { get; set; }

        public string email { get; set; }

    }
}
