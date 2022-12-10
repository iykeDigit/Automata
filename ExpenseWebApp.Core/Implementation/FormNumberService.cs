using ExpenseWebApp.Core.Interfaces;
using ExpenseWebApp.Data.ContextClass;
using ExpenseWebApp.Data.UnitOfWork.Abstractions;
using ExpenseWebApp.Models;
using ExpenseWebApp.Utilities;
using ExpenseWebApp.Utilities.ResourceFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseWebApp.Core.Implementation
{
    public class FormNumberService : IFormNumberService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public FormNumberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string GetFormCount(string formType, string cacNumber)
        {
            var form = _unitOfWork.CompanyFormDataRepository.GetFormData(cacNumber);

            int formCount;

            if (form != null) 
            {
                formCount = UpdateFormCount(form, formType);
                return $"{formCount}".PadLeft(4, '0');
            }

            
            var newData = new CompanyFormData();
            newData.CACNumber = cacNumber;

            var newForm = _unitOfWork.CompanyFormDataRepository.AddNewCompanyData(newData);

            formCount = UpdateFormCount(newForm, formType);
            return $"{formCount}".PadLeft(4, '0');
        }

        private int UpdateFormCount(CompanyFormData form, string formType) 
        {
            int formCount = 0;
            var isSameDay = form.TransactionDate == DateTime.Now.ToString("yyyy-MM-dd");

            form.TransactionDate = DateTime.Now.ToString("yyyy-MM-dd");


            if (formType == ResourceFile.CashAdvanceForm) 
            {
                formCount = (isSameDay) ? form.CashAdvanceFormCount + 1 : 1;
                form.CashAdvanceFormCount = formCount;
                form.CashAdvanceRetirementFormCount = (isSameDay) ? form.CashAdvanceRetirementFormCount : 0;
                form.ExpenseFormCount = (isSameDay) ? form.ExpenseFormCount : 0;
                _unitOfWork.CompanyFormDataRepository.UpdateCompanyFormData(form);
            }

            if(formType == ResourceFile.CashAdvanceRetirementForm) 
            {
                formCount = (isSameDay) ? form.CashAdvanceRetirementFormCount + 1 : 1;
                form.CashAdvanceRetirementFormCount = formCount;
                form.ExpenseFormCount = (isSameDay) ? form.ExpenseFormCount : 0;
                form.CashAdvanceFormCount = (isSameDay) ? form.CashAdvanceFormCount : 0;
                _unitOfWork.CompanyFormDataRepository.UpdateCompanyFormData(form);
            }

            if(formType == ResourceFile.ExpenseForm)
            {
                formCount = (isSameDay) ? form.ExpenseFormCount + 1 : 1;
                form.ExpenseFormCount = formCount;
                form.CashAdvanceFormCount = (isSameDay) ? form.CashAdvanceFormCount : 0;
                form.CashAdvanceRetirementFormCount = (isSameDay) ? form.CashAdvanceRetirementFormCount : 0;
                _unitOfWork.CompanyFormDataRepository.UpdateCompanyFormData(form);
            }

            return formCount;
            
        } 

      
    }
}

