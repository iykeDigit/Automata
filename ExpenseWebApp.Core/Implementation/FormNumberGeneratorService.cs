using ExpenseWebApp.Core.Interfaces;
using ExpenseWebApp.Data.UnitOfWork.Abstractions;
using ExpenseWebApp.Utilities.Helpers;
using ExpenseWebApp.Utilities.ResourceFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebApp.Core.Implementation
{
    public class FormNumberGeneratorService: IFormNumberGeneratorService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IFormNumberService _updateFormCount;

        public FormNumberGeneratorService(IUnitOfWork unitOfWork, IFormNumberService updateFormCount)
        {
            _unitOfWork = unitOfWork;
            _updateFormCount = updateFormCount;
        }

        /// <summary>
        /// This method generates the form number
        /// </summary>
        /// <param name="formType"></param>
        /// <param name="cacNumber"></param>
        /// <returns></returns>
        public string GenerateFormNumber(string formType, string cacNumber)
        {
            var formCount = _updateFormCount.GetFormCount(formType, cacNumber);
            
            if(formType == ResourceFile.CashAdvanceForm) 
            {
                return $"{FormNumberGenerator.GenerateFormNumber(formType)}-{formCount}";
            }

            if(formType == ResourceFile.CashAdvanceRetirementForm) 
            {
                return $"{FormNumberGenerator.GenerateFormNumber(formType)}-{formCount}";
            }

           if(formType == ResourceFile.ExpenseForm) 
            {
                return $"{FormNumberGenerator.GenerateFormNumber(formType)}-{formCount}";
            }

            return default;

        }
     }
}
