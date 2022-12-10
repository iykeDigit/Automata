using ExpenseWebApp.Data.ContextClass;
using System;

namespace ExpenseWebApp.Utilities.Helpers
{
    public static class FormNumberGenerator
    {
        public static string GenerateFormNumber(string formType)
        {
            return $"{formType}-{DateTime.Now:yyyyMMdd}";
        }

        

    }
}
