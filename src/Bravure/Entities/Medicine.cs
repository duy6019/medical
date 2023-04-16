using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Bravure.Entities.Abstractions;
using Bravure.Entities.Enums;

namespace Bravure.Entities
{
    public class Medicine : IdentifiableEntity<Guid>
    {
        public string Receiver { get; set; }
        public string MadeFrom { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string Cost { get; set; }
        public string Usage { get; set; }
        public DateTime ReceiveDate { get; set; }
    }
}
