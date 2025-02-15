using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Chatter.Api.Extensions;

public class ProblemDetailsResult([ActionResultObjectValue] object? objectResult) : ObjectResult(objectResult);
