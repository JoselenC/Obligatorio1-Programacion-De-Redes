namespace WebAPI.Dto
{
    public class ErrorDto
    {
        public object Content { get; set; }
        public bool IsSuccess { get; set; }
        public int Code { get; set; }
        public string ErrorMessage { get; set; }
        
        public override bool Equals(object obj)
        {
            if (obj==null) return false;
            if (obj.GetType() != GetType()) return false;
            return Content == ((ErrorDto)obj).Content && IsSuccess == ((ErrorDto)obj).IsSuccess
                && Code==((ErrorDto)obj).Code && ErrorMessage == ((ErrorDto)obj).ErrorMessage;
        }

    }
}