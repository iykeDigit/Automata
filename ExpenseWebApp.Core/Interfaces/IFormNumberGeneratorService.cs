using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebApp.Core.Interfaces
{
    public interface IFormNumberGeneratorService
    {
        string GenerateFormNumber(string formType, string cacNumber);
    }
}
