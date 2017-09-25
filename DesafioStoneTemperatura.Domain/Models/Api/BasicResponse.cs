using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioStoneTemperatura.Domain.Models.Api
{
    public class BasicResponse
    {

        public string Status { get; set; }
        public string Message { get; set; }

        public BasicResponse(Status status, string message)
        {
            this.Status = status.ToString();
            this.Message = message;
        }

    }

    public enum Status
    {
        Success,
        Error
    }
}
