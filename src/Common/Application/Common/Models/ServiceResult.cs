namespace Application.Common.Models
{
    /// <summary>
    /// A standard response for service calls.
    /// </summary>
    /// <typeparam name="T">Return data type</typeparam>
    public class ServiceResult<T>
    {
        public T Data { get; set; }

        public ServiceResult(T data)
        {
            Data = data;
        }
    }
}
