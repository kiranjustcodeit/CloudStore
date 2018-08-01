using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Benday.WebCalculator.Api;
using Benday.WebCalculator.WebUi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace Benday.WebCalculator.WebUi.Controllers
{
    public class CalculatorController : Controller
    {
        private ICalculatorService _CalculatorService;
        private IConfiguration _Configuration;

        public CalculatorController(ICalculatorService calculator, IConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config), $"{nameof(config)} is null.");
            }

            if (calculator == null)
            {
                throw new ArgumentNullException(nameof(calculator), $"{nameof(calculator)} is null.");
            }

            _CalculatorService = calculator;
            _Configuration = config;
        }

        public IActionResult Index()
        {
            var model = new CalculatorViewModel();

            model.Operator = CalculatorConstants.Message_ChooseAnOperator;

            model.Operators = GetOperators();

            model.Message = String.Empty;

            model.IsResultValid = false;

            model.BuildVersionMessage = GetBuildVersionMessage();

            return View(model);
        }

        private string GetBuildVersionMessage()
        {
            var value = _Configuration["CalculatorSettings:BuildVersionMessage"];

            if (value == null)
            {
                return "(not set)";
            }
            else
            {
                return value;
            }
        }

        private void PopulateOperators(CalculatorViewModel model, string operation)
        {
            model.Operator = operation;

            var operators = GetOperators();

            foreach (var item in operators)
            {
                item.Selected = false;
            }

            var selectThisOperator = (from temp in operators
                                      where temp.Text == operation
                                      select temp).FirstOrDefault();

            if (selectThisOperator != null)
            {
                selectThisOperator.Selected = true;
            }

            model.Operators = operators;
        }

        private List<SelectListItem> GetOperators()
        {
            var operators = new List<SelectListItem>();

            operators.Add(
                String.Empty,
                CalculatorConstants.Message_ChooseAnOperator, 
                true);

            operators.Add(CalculatorConstants.OperatorAdd);
            operators.Add(CalculatorConstants.OperatorSubtract);
            operators.Add(CalculatorConstants.OperatorMultiply);
            operators.Add(CalculatorConstants.OperatorDivide);
            
            return operators;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Calculate(CalculatorViewModel model)
        {
            var operation = model.Operator;

            if (operation == CalculatorConstants.OperatorAdd)
            {
                model.ResultValue = 
                    _CalculatorService.Add(
                        model.Value1, model.Value2);
                model.Message = CalculatorConstants.Message_Success;
                model.IsResultValid = true;
                PopulateOperators(model, operation);
            }
            else if (operation == CalculatorConstants.OperatorSubtract)
            {
                model.ResultValue =
                    _CalculatorService.Subtract(
                        model.Value1, model.Value2);
                model.Message = CalculatorConstants.Message_Success;
                model.IsResultValid = true;
                PopulateOperators(model, operation);
            }
            else if (operation == CalculatorConstants.OperatorMultiply)
            {
                model.ResultValue =
                    _CalculatorService.Multiply(
                        model.Value1, model.Value2);
                model.Message = CalculatorConstants.Message_Success;
                model.IsResultValid = true;
                PopulateOperators(model, operation);
            }
            else if (operation == CalculatorConstants.OperatorDivide)
            {
                if (model.Value2 == 0d)
                {
                    model.ResultValue = 0;
                    model.Message = CalculatorConstants.Message_CantDivideByZero;
                    model.IsResultValid = false;
                    PopulateOperators(model, operation);
                }
                else
                {
                    model.ResultValue =
                    _CalculatorService.Divide(
                        model.Value1, model.Value2);
                    model.Message = CalculatorConstants.Message_Success;
                    model.IsResultValid = true;
                    PopulateOperators(model, operation);
                }                
            }
            else
            {
                model.IsResultValid = false;
                model.ResultValue = 0;
                model.Message = CalculatorConstants.Message_UnknownOperatorMessage;
                PopulateOperators(model, operation);
            }

            model.BuildVersionMessage = GetBuildVersionMessage();

            return View("Index", model);
        }
    }
}