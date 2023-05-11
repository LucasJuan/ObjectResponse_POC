using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ObjectResponse_POC.Model;

namespace ObjectResponse_POC.Filter;

public class ValueObjectTFilter: ResultFilterAttribute
{
    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;

        if (result != null)
        {
            
            if (result.StatusCode == 200 || result.StatusCode == 201)
            {
                result.Value = VO(result.Value);
            }
            else if (result.StatusCode == 400)
            {
                if (result.Value.GetType() == typeof(ValidationProblemDetails))
                {
                    var obj = result.Value as ValidationProblemDetails;
                    var res = string.Join("; ", obj.Errors.SelectMany(el => el.Value.Select(el => el)));
                    result.Value = VOError(res);
                }
                else

                {
                    result.Value = VOError(result.Value?.ToString() ?? "Value is null");
                }
            }
            else if (result.StatusCode == 404)
            {
                if (result.Value.GetType() == typeof(ValidationProblemDetails))
                {
                    var obj = result.Value as ValidationProblemDetails;
                    var res = string.Join("; ", obj.Errors.SelectMany(el => el.Value.Select(el => el)));
                    result.Value = VOError(res);
                }
                else

                {
                    result.Value = VOError("Value not found");
                }
            }
        }

        await next();
    }
    private ObjectResponse<object> VO(object data)
    {
        if (data.GetType() != typeof(string))
        {
            return new ObjectResponse<object>
            {
                Data = data,
                Success = true,
                Message = ""
            };
        }
        else
        {
            return new ObjectResponse<object>
            {
                Data = null,
                Success = false,
                Message = data.ToString()
            };
        }
    }

    private ObjectResponse<object> VOError(object data)
    {
        if (data.GetType() != typeof(string))
        {
            return new ObjectResponse<object>
            {
                Data = data,
                Success = false,
                Message = ""
            };
        }
        else
        {
            return new ObjectResponse<object>
            {
                Data = null,
                Success = false,
                Message = data.ToString()
            };
        }
    }
    private ObjectResponse<object> VONotFound(object data)
    {
        string errorMessage;

        if (data == null)
        {
            errorMessage = "Resource not found";
        }
        else if (data is string)
        {
            errorMessage = data.ToString();
        }
        else if (data is IEnumerable<string> errors)
        {
            errorMessage = string.Join("; ", errors);
        }
        else
        {
            errorMessage = "Resource not found";
        }

        return new ObjectResponse<object>
        {
            Data = null,
            Success = false,
            Message = errorMessage
        };
    }

}
