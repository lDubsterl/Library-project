using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Common.Exceptions
{
	public class ValidationErrorResponse
	{
		public string FieldName { get; set; }
		public string Message { get; set; }
		public object AttemptedValue { get; set; }

		public ValidationErrorResponse(ValidationFailure error)
		{
			FieldName = error.PropertyName;
			Message = error.ErrorMessage;
			AttemptedValue = error.AttemptedValue;
		}
	}
}
