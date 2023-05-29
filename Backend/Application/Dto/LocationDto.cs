using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class LocationDto
    {
        public Guid Id { get; set; }
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}