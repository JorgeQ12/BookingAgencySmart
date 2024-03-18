using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
namespace Application.Utilities
{
    public class ResultResponse<T>
    {
        public bool IsSucces { get; set; }
        public T Data { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Message {get; set;}

        public ResultResponse(bool isSucces, List<string> validationErrors)
        {
            IsSucces = isSucces;
            Message = validationErrors.ToString();
        }
        public ResultResponse(bool isSucces, string message)
        {
            IsSucces = isSucces;
            Message = message;
        }
        public ResultResponse(bool isSucces, T data)
        {
            IsSucces = isSucces;
            Data = data;
        }
    }
}
