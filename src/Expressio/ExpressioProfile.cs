using AutoMapper;
using Expressio.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

public class ExpressioProfile : Profile
{
	public ExpressioProfile()
	{
		CreateMap<Expression, ExpressionDTO>();
		CreateMap<MixedExpression, MixedExpressionDTO>();
	}
}
