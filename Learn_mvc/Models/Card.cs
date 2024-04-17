using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Learn_mvc.Models
{
    public class Card
    {
        public string CardName { get; set; }

        public string CardNumber { get; set; }

        public string EncryptedCardNumber { get; set; }
    }
}